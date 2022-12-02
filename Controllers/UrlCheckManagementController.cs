using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HealthCheckApp.Data.Entities;
using HealthCheckApp.Data.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace HealthCheckApp.Controllers
{
    [Authorize]
    public class UrlCheckManagementController : Controller
    {
        private readonly IRepository<UrlHealthCheck> _repository;
        private readonly UserManager<User> _userManager;

        public UrlCheckManagementController(IRepository<UrlHealthCheck> repository, UserManager<User> userManager)
        {
            _repository = repository;
            _userManager = userManager;
        }

        // GET: HealthChecks
        public async Task<IActionResult> Index() => View(await _repository.GetAllAsync(x => x.User));

        // GET: HealthChecks/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: HealthChecks/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(UrlHealthCheck healthCheck)
        {
            if (ModelState.IsValid)
            {
                healthCheck.UserId = _userManager.GetUserId(User);
                await _repository.AddAsync(healthCheck);
                return RedirectToAction(nameof(Index));
            }
            return View(healthCheck);
        }

        // GET: HealthChecks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            var healthCheck = await _repository.GetByIdAsync(id.Value);
            if (healthCheck == null) return NotFound();
        
            return View(healthCheck);
        }

        // POST: HealthChecks/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, UrlHealthCheck healthCheck)
        {
            if (id != healthCheck.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    await _repository.Update(healthCheck);
                }
                catch (DbUpdateConcurrencyException)
                {
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(healthCheck);
        }

        // GET: HealthChecks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var healthCheck = await _repository.GetByIdAsync(id.Value);

            if (healthCheck == null)  return NotFound();

            return View(healthCheck);
        }

        // POST: HealthChecks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _repository.Delete(id);

            return RedirectToAction(nameof(Index));
        }   
    }
}
