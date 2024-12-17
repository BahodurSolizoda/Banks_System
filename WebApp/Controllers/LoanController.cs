using Domain.Entities;
using Infrastructure.Interfaces;
using Infrastructure.Responses;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers;

[ApiController]
[Route("[controller]")]

public class LoanController(IGenericService<Loan> loanService):ControllerBase
{
    [HttpGet]
    public async Task<ApiResponse<List<Loan>>> GetAll()
    {
        return await loanService.GetAll();
    }

    [HttpGet("{id:int}")]
    public async Task<ApiResponse<Loan>> GetById(int id)
    {
        return await loanService.GetById(id);
    }

    [HttpPost]
    public async Task<ApiResponse<bool>> Add([FromBody] Loan loan)
    {
        return await loanService.Add(loan);
    }

    [HttpPut]
    public async Task<ApiResponse<bool>> Update([FromBody] Loan loan)
    {
        return await loanService.Update(loan);
    }

    [HttpDelete("{id:int}")]
    public async Task<ApiResponse<bool>> Delete(int id)
    {
        return await loanService.Delete(id);
    }

    [HttpGet("GetByCondition")]
    public async Task<ApiResponse<List<Loan>>> GetByCondition([FromQuery] string condition)
    {
        return await loanService.GetByCondition(condition);
    }

    [HttpGet("Exists/{id:int}")]
    public async Task<ApiResponse<bool>> Exists(int id)
    {
        return await loanService.Exists(id);
    }

    [HttpGet("Count")]
    public async Task<ApiResponse<int>> Count()
    {
        return await loanService.Count();
    }

    [HttpPatch("{id:int}")]
    public async Task<ApiResponse<bool>> UpdatePartial(int id, [FromQuery] string propertyName, [FromQuery] object newValue)
    {
        return await loanService.UpdatePartial(id, propertyName, newValue);
    }
}