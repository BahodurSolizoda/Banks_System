using Domain.Entities;
using Infrastructure.Interfaces;
using Infrastructure.Responses;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers;

[ApiController]
[Route("[controller]")]

public class AccountController(IGenericService<Accounts> accountService):ControllerBase
{
    [HttpGet]
    public async Task<ApiResponse<List<Accounts>>> GetAll()
    {
        return await accountService.GetAll();
    }

    [HttpGet("{id:int}")]
    public async Task<ApiResponse<Accounts>> GetById(int id)
    {
        return await accountService.GetById(id);
    }

    [HttpPost]
    public async Task<ApiResponse<bool>> Add([FromBody] Accounts accounts)
    {
        return await accountService.Add(accounts);
    }

    [HttpPut]
    public async Task<ApiResponse<bool>> Update([FromBody] Accounts accounts)
    {
        return await accountService.Update(accounts);
    }

    [HttpDelete("{id:int}")]
    public async Task<ApiResponse<bool>> Delete(int id)
    {
        return await accountService.Delete(id);
    }

    [HttpGet("GetByCondition")]
    public async Task<ApiResponse<List<Accounts>>> GetByCondition([FromQuery] string condition)
    {
        return await accountService.GetByCondition(condition);
    }

    [HttpGet("Exists/{id:int}")]
    public async Task<ApiResponse<bool>> Exists(int id)
    {
        return await accountService.Exists(id);
    }

    [HttpGet("Count")]
    public async Task<ApiResponse<int>> Count()
    {
        return await accountService.Count();
    }

    [HttpPatch("{id:int}")]
    public async Task<ApiResponse<bool>> UpdatePartial(int id, [FromQuery] string propertyName, [FromQuery] object newValue)
    {
        return await accountService.UpdatePartial(id, propertyName, newValue);
    }
}