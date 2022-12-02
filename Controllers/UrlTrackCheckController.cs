using HealthCheckApp.Data.Context;
using HealthCheckApp.Data.Entities;
using HealthCheckApp.Services.UrlTrack;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HealthCheckApp.Controllers
{
    [Authorize]
    public class UrlTrackCheckController : Controller
    {
        private readonly IUrlTrackService _urlTrackService;
        private readonly UserManager<User> _userManager;
        private readonly HealthCheckContext _dbContext;


        public UrlTrackCheckController(IUrlTrackService urlTrackService, UserManager<User> userManager , HealthCheckContext dbContext)
        {
            _urlTrackService = urlTrackService;  
            _userManager= userManager;
            _dbContext= dbContext;
        }

        // GET: Track
        public async Task<IActionResult> Track()
        {
            return View(await _urlTrackService.GetUrlTracksWithHealthCheckAsync());
        }
    }
}
