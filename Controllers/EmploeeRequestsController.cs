using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using TestAccountingInformation.Constants;
using TestAccountingInformation.DataBase;
using TestAccountingInformation.DataBase.Entities;
using TestAccountingInformation.DataBase.Entityes;
using TestAccountingInformation.Models;

namespace TestAccountingInformation.Controllers
{
    [Authorize(Roles = "Сотрудник")]
    public class EmploeeRequestsController : Controller
    {
        private readonly ApplicationDataBase _context;
        private readonly UserManager<UserEntity> _userManager;

        public EmploeeRequestsController(ApplicationDataBase context, UserManager<UserEntity> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> MyRequests()
        {
            var currentUser = await _userManager.GetUserAsync(User);
            var requests = await _context.Requests
                .Include(r => r.Status)
                .Include(r => r.Executor)
                .Where(r => r.AuthorId == currentUser.Id)
                .OrderByDescending(r => r.Id)
                .ToListAsync();

            return View(requests);
        }

        public IActionResult Create()
        {
            var infoTypes = _context.Informations.ToList();
            
            var model = new RequestViewModel
            {
                Items = infoTypes.Select(i => new RequestItemViewModel
                {
                    InformationId = i.Id,
                    InformationType = i.Type,
                    Quantity = 1,
                    IsSelected = false
                }).ToList()
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RequestViewModel model)
        {
            if (ModelState.IsValid)
            {
                var currentUser = await _userManager.GetUserAsync(User);
                var request = new RequestEntity
                {
                    AuthorId = currentUser.Id,
                    Author = currentUser,
                    StatusId = (int)RequestStatus.Sent
                };

                _context.Requests.Add(request);
                await _context.SaveChangesAsync();

                var informationIds = model.Items
                    .Where(i => i.IsSelected && i.Quantity > 0)
                    .Select(i => i.InformationId)
                    .Distinct()
                    .ToList();

                var informations = await _context.Informations
                    .Where(info => informationIds.Contains(info.Id))
                    .ToDictionaryAsync(info => info.Id);

                foreach (var item in model.Items.Where(i => i.IsSelected && i.Quantity > 0))
                {
                    _context.RequestInformations.Add(new RequestInformation
                    {
                        RequestId = request.Id,
                        Request = request,
                        InformationId = item.InformationId,
                        Quantity = item.Quantity,
                        Information = informations[item.InformationId]
                    });
                }

                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(MyRequests));
            }

            ViewBag.InformationTypes = _context.Informations.ToList();
            
            return View(model);
        }

        public async Task<IActionResult> Details(int id)
        {
            var currentUser = await _userManager.GetUserAsync(User);
            var request = await _context.Requests
                .Include(r => r.Author)
                .Include(r => r.Executor)
                .Include(r => r.Status)
                .Include(r => r.RequestInformations)
                    .ThenInclude(ri => ri.Information)
                .FirstOrDefaultAsync(r => r.Id == id && r.AuthorId == currentUser.Id);

            if (request == null)
                return NotFound();

            return View(request);
        }
    }
}
