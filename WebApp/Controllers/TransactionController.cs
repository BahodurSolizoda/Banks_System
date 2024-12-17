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
    public async Task<ApiResponse<List<Transaction>>> GetAll()
    {
        return await transactionService.GetAll();
    }

    [HttpGet("{id:int}")]
    public async Task<ApiResponse<Transaction>> GetById(int id)
    {
        return await transactionService.GetById(id);
    }

    [HttpPost]
    public async Task<ApiResponse<bool>> Add([FromBody] Transaction transaction)
    {
        return await transactionService.Add(transaction);
    }

    [HttpPut]
    public async Task<ApiResponse<bool>> Update([FromBody] Transaction transaction)
    {
        return await transactionService.Update(transaction);
    }

    [HttpDelete("{id:int}")]
    public async Task<ApiResponse<bool>> Delete(int id)
    {
        return await transactionService.Delete(id);
    }

    [HttpGet("GetByCondition")]
    public async Task<ApiResponse<List<Transaction>>> GetByCondition([FromQuery] string condition)
    {
        return await transactionService.GetByCondition(condition);
    }

    [HttpGet("Exists/{id:int}")]
    public async Task<ApiResponse<bool>> Exists(int id)
    {
        return await transactionService.Exists(id);
    }

    [HttpGet("Count")]
    public async Task<ApiResponse<int>> Count()
    {
        return await transactionService.Count();
    }

    [HttpPatch("{id:int}")]
    public async Task<ApiResponse<bool>> UpdatePartial(int id, [FromQuery] string propertyName, [FromQuery] object newValue)
    {
        return await transactionService.UpdatePartial(id, propertyName, newValue);
    }
}