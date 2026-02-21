using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NewsletterAppCore.Data;
using NewsletterAppCore.Models;
using NewsletterAppCore.Models.ViewModels;

namespace NewsletterAppCore.Controllers.Api
{
    [ApiController]
    [Route("api/[controller]")]   
    public class SignUpsController : ControllerBase
    {
        private readonly NewsletterDbContext _db;

        public SignUpsController(NewsletterDbContext db)
        {
            _db = db;
        }

        // GET: /api/signups
        [HttpGet]
        [Authorize(Policy = "AdminOnly")]
        public async Task<ActionResult<List<SignUpVm>>> GetAll()
        {
            var items = await _db.SignUps
                .AsNoTracking()
                .OrderByDescending(s => s.Id)
                .Select(s => new SignUpVm
                {
                    Id = s.Id,
                    FirstName = s.FirstName,
                    LastName = s.LastName,
                    EmailAddress = s.EmailAddress,
                    SocialSecurityNumber = s.SocialSecurityNumber // include only for Admin
                })
                .ToListAsync();

            return Ok(items);
        }

        // GET: /api/signups/5
        [HttpGet("{id:int}")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<ActionResult<SignUpVm>> GetById(int id)
        {
            var s = await _db.SignUps.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            if (s == null) return NotFound();

            return Ok(new SignUpVm
            {
                Id = s.Id,
                FirstName = s.FirstName,
                LastName = s.LastName,
                EmailAddress = s.EmailAddress,
                SocialSecurityNumber = s.SocialSecurityNumber
            });
        }

        // POST: /api/signups
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> Create([FromBody] SignUpVm model)
        {
            if (!ModelState.IsValid) return ValidationProblem(ModelState);

            var entity = new NewsletterSignUp
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                EmailAddress = model.EmailAddress,
                SocialSecurityNumber = model.SocialSecurityNumber
            };

            _db.SignUps.Add(entity);
            await _db.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = entity.Id }, new { entity.Id });
        }

        // PUT: /api/signups/5
        [HttpPut("{id:int}")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<ActionResult> Update(int id, [FromBody] SignUpVm model)
        {
            if (!ModelState.IsValid) return ValidationProblem(ModelState);

            var entity = await _db.SignUps.FirstOrDefaultAsync(x => x.Id == id);
            if (entity == null) return NotFound();

            entity.FirstName = model.FirstName;
            entity.LastName = model.LastName;
            entity.EmailAddress = model.EmailAddress;
            entity.SocialSecurityNumber = model.SocialSecurityNumber;

            await _db.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: /api/signups/5
        [HttpDelete("{id:int}")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<ActionResult> Delete(int id)
        {
            var entity = await _db.SignUps.FirstOrDefaultAsync(x => x.Id == id);
            if (entity == null) return NotFound();

            _db.SignUps.Remove(entity);
            await _db.SaveChangesAsync();
            return NoContent();
        }
    }
}