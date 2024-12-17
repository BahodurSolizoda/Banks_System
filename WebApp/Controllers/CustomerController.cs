using Domain.Entities;
using Infrastructure.Interfaces;
using Infrastructure.Responses;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers;

[ApiController]
[Route("[controller]")]

public class CustomerController(IGenericService<Customer> customerService) : ControllerBase
{
    [HttpGet]
    public async Task<ApiResponse<List<Customer>>> GetAll()
    {
        return await customerService.GetAll();
    }

    [HttpGet("{id:int}")]
    public async Task<ApiResponse<Customer>> GetById(int id)
    {
        return await customerService.GetById(id);
    }

    [HttpPost]
    public async Task<ApiResponse<bool>> Add([FromBody] Customer customer)
    {
        return await customerService.Add(customer);
    }

    [HttpPut]
    public async Task<ApiResponse<bool>> Update([FromBody] Customer customer)
    {
        return await customerService.Update(customer);
    }

    [HttpDelete("{id:int}")]
    public async Task<ApiResponse<bool>> Delete(int id)
    {
        return await customerService.Delete(id);
    }

    [HttpGet("GetByCondition")]
    public async Task<ApiResponse<List<Customer>>> GetByCondition([FromQuery] string condition)
    {
        return await customerService.GetByCondition(condition);
    }

    [HttpGet("Exists/{id:int}")]
    public async Task<ApiResponse<bool>> Exists(int id)
    {
        return await customerService.Exists(id);
    }

    [HttpGet("Count")]
    public async Task<ApiResponse<int>> Count()
    {
        return await customerService.Count();
    }

    [HttpPatch("{id:int}")]
    public async Task<ApiResponse<bool>> UpdatePartial(int id, [FromQuery] string propertyName, [FromQuery] object newValue)
    {
        return await customerService.UpdatePartial(id, propertyName, newValue);
    }
}