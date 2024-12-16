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
    public ApiResponse<List<Accounts>> GetAll()
    {
        return accountService.GetAll();
    }
    [HttpGet("{id:int}")]
    public ApiResponse<Accounts> GetById(int id)
    {
        return accountService.GetById(id);
    }
    [HttpPost]
    public ApiResponse<bool> Add(Accounts accounts)
    {
        return accountService.Add(accounts);
    }
    [HttpPut]
    public ApiResponse<bool> Update(Accounts accounts)
    {
        return accountService.Update(accounts);
    }
    [HttpDelete]
    public ApiResponse<bool> Delete(int id)
    {
        return accountService.Delete(id);
    }
    
    [HttpGet("GetByCondition")]
    public ApiResponse<List<Accounts>> GetByCondition([FromQuery] string condition)
    {
        return accountService.GetByCondition(condition);
    }
    
    [HttpGet("Exists/{id:int}")]
    public ApiResponse<bool> Exists(int id)
    {
        return accountService.Exists(id);
    }
    
    [HttpGet("Count")]
    public ApiResponse<int> Count()
    {
        return accountService.Count();
    }
    
    [HttpPatch("{id:int}")]
    public ApiResponse<bool> UpdatePartial(int id, [FromQuery] string propertyName, [FromQuery] object newValue)
    {
        return accountService.UpdatePartial(id, propertyName, newValue);
    }
}