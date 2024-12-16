using Domain.Entities;
using Infrastructure.Interfaces;
using Infrastructure.Responses;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers;

[ApiController]
[Route("[controller]")]

public class TransactionController(IGenericService<Transaction> transactionService):ControllerBase
{
    [HttpGet]
    public ApiResponse<List<Transaction>> GetAll()
    {
        return transactionService.GetAll();
    }
    [HttpGet("{id:int}")]
    public ApiResponse<Transaction> GetById(int id)
    {
        return transactionService.GetById(id);
    }
    [HttpPost]
    public ApiResponse<bool> Add(Transaction loan)
    {
        return transactionService.Add(loan);
    }
    [HttpPut]
    public ApiResponse<bool> Update(Transaction loan)
    {
        return transactionService.Update(loan);
    }
    [HttpDelete]
    public ApiResponse<bool> Delete(int id)
    {
        return transactionService.Delete(id);
    }
    
    
    //New
    
    //New [HttpGet("GetByCondition")]
    public ApiResponse<List<Transaction>> GetByCondition([FromQuery] string condition)
    {
        return transactionService.GetByCondition(condition);
    }
    
    [HttpGet("Exists/{id:int}")]
    public ApiResponse<bool> Exists(int id)
    {
        return transactionService.Exists(id);
    }
    
    [HttpGet("Count")]
    public ApiResponse<int> Count()
    {
        return transactionService.Count();
    }
    
    [HttpPatch("{id:int}")]
    public ApiResponse<bool> UpdatePartial(int id, [FromQuery] string propertyName, [FromQuery] object newValue)
    {
        return transactionService.UpdatePartial(id, propertyName, newValue);
    }
}