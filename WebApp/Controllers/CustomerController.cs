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
    public ApiResponse<List<Customer>> GetAll()
    {
        return customerService.GetAll();
    }
    [HttpGet("{id:int}")]
    public ApiResponse<Customer> GetById(int id)
    {
        return customerService.GetById(id);
    }
    [HttpPost]
    public ApiResponse<bool> Add(Customer customer)
    {
        return customerService.Add(customer);
    }
    [HttpPut]
    public ApiResponse<bool> Update(Customer customer)
    {
        return customerService.Update(customer);
    }
    [HttpDelete]
    public ApiResponse<bool> Delete(int id)
    {
        return customerService.Delete(id);
    }
    
    //New
    
    [HttpGet("GetByCondition")]
    public ApiResponse<List<Customer>> GetByCondition([FromQuery] string condition)
    {
        return customerService.GetByCondition(condition);
    }
    
    [HttpGet("Exists/{id:int}")]
    public ApiResponse<bool> Exists(int id)
    {
        return customerService.Exists(id);
    }
    
    [HttpGet("Count")]
    public ApiResponse<int> Count()
    {
        return customerService.Count();
    }
    
    [HttpPatch("{id:int}")]
    public ApiResponse<bool> UpdatePartial(int id, [FromQuery] string propertyName, [FromQuery] object newValue)
    {
        return customerService.UpdatePartial(id, propertyName, newValue);
    }
}