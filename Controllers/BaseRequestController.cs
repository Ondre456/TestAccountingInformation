using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TestAccountingInformation.DataBase;
using TestAccountingInformation.DataBase.Entities;
using TestAccountingInformation.DataBase.Entityes;

public abstract class BaseRequestController : Controller
{
    protected readonly ApplicationDataBase _context;
    protected readonly UserManager<UserEntity> _userManager;

    protected BaseRequestController(ApplicationDataBase context, UserManager<UserEntity> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    protected async Task<List<RequestEntity>> LoadRequestsWithBasicIncludesAsync(
            Expression<Func<RequestEntity, bool>> filter)
    {
        return await _context.Requests
            .Include(r => r.Author)
            .Include(r => r.Executor)
            .Include(r => r.Status)
            .Where(filter)
            .ToListAsync();
    }

    protected async Task<RequestEntity?> LoadRequestWithDetailsAsync(int id)
    {
        return await _context.Requests
            .Include(r => r.Author)
            .Include(r => r.Executor)
            .Include(r => r.Status)
            .Include(r => r.RequestInformations)
                .ThenInclude(ri => ri.Information)
            .FirstOrDefaultAsync(r => r.Id == id);
    }
}
