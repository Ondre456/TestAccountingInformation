using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using TestAccountingInformation.Constants;
using TestAccountingInformation.DataBase;
using TestAccountingInformation.DataBase.Entities;
using TestAccountingInformation.DataBase.Entityes;

namespace TestAccountingInformation.Controllers
{
    [Authorize(Roles = "Бухгалтер")]
    public class AccountantController : Controller
    {
        private readonly ApplicationDataBase _context;
        private readonly UserManager<UserEntity> _userManager;

        public AccountantController(ApplicationDataBase context, UserManager<UserEntity> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            var requests = _context.Requests
                .Include(r => r.Author)
                .Include(r => r.Executor)
                .Include(r => r.Status)
                .ToList();

            return View(requests);
        }

        public async Task<IActionResult> Details(int id)
        {
            var request = await _context.Requests
                .Include(r => r.Author)
                .Include(r => r.Executor)
                .Include(r => r.Status)
                .Include(r => r.RequestInformations)
                    .ThenInclude(ri => ri.Information)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (request == null)
            {
                return NotFound();
            }

            ViewBag.CanTake = request.StatusId == (int)RequestStatus.Sent;
            ViewBag.CanComplete = request.StatusId == (int)RequestStatus.InProgress &&
                           request.ExecutorId == User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            ViewBag.CanReject = (request.StatusId == (int)RequestStatus.Sent ||
                               request.StatusId == (int)RequestStatus.InProgress) &&
                               (request.ExecutorId == null ||
                                request.ExecutorId == User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

            return View(request);
        }

        [HttpPost]
        public async Task<IActionResult> TakeInWork(int id)
        {
            var request = await _context.Requests.FindAsync(id);
            
            if (request == null || request.StatusId != (int)RequestStatus.Sent)
            {
                return BadRequest();
            }

            var currentUser = await _userManager.GetUserAsync(User);
            request.Executor = currentUser;
            request.ExecutorId = currentUser.Id;
            request.StatusId = (int)RequestStatus.InProgress;

            await _context.SaveChangesAsync();
            
            return RedirectToAction(nameof(Details), new { id });
        }

        [HttpPost]
        public async Task<IActionResult> MarkAsCompleted(int id)
        {
            var request = await _context.Requests.FindAsync(id);
            
            if (request == null || request.StatusId != (int)RequestStatus.InProgress || request.ExecutorId != User.FindFirst(ClaimTypes.NameIdentifier)?.Value)
            {
                return BadRequest();
            }

            request.StatusId = (int)RequestStatus.Completed;
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Details), new { id });
        }

        [HttpPost]
        public async Task<IActionResult> Reject(int id)
        {
            var request = await _context.Requests.FindAsync(id);
            if (request == null ||
                !(request.StatusId == (int)RequestStatus.Sent ||
                  request.StatusId == (int)RequestStatus.InProgress))
            {
                return BadRequest();
            }

            var currentUser = await _userManager.GetUserAsync(User);
            request.Executor = currentUser;
            request.ExecutorId = currentUser.Id;
            request.StatusId = (int)RequestStatus.Rejected;

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Details), new { id });
        }
    }
}
