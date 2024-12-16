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
    public ApiResponse<List<Loan>> GetAll()
    {
        return loanService.GetAll();
    }
    [HttpGet("{id:int}")]
    public ApiResponse<Loan> GetById(int id)
    {
        return loanService.GetById(id);
    }
    [HttpPost]
    public ApiResponse<bool> Add(Loan loan)
    {
        return loanService.Add(loan);
    }
    [HttpPut]
    public ApiResponse<bool> Update(Loan loan)
    {
        return loanService.Update(loan);
    }
    [HttpDelete]
    public ApiResponse<bool> Delete(int id)
    {
        return loanService.Delete(id);
    }
    
    //New [HttpGet("GetByCondition")]
    public ApiResponse<List<Loan>> GetByCondition([FromQuery] string condition)
    {
        return loanService.GetByCondition(condition);
    }
    
    [HttpGet("Exists/{id:int}")]
    public ApiResponse<bool> Exists(int id)
    {
        return loanService.Exists(id);
    }
    
    [HttpGet("Count")]
    public ApiResponse<int> Count()
    {
        return loanService.Count();
    }
    
    [HttpPatch("{id:int}")]
    public ApiResponse<bool> UpdatePartial(int id, [FromQuery] string propertyName, [FromQuery] object newValue)
    {
        return loanService.UpdatePartial(id, propertyName, newValue);
    }
}