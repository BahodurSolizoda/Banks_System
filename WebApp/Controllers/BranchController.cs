using Domain.Entities;
using Infrastructure.Interfaces;
using Infrastructure.Responses;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers;

[ApiController]
[Route("[controller]")]

public class BranchController(IGenericService<Branch> branchService):ControllerBase
{
    [HttpGet]
    public ApiResponse<List<Branch>> GetAll()
    {
        return branchService.GetAll();
    }
    [HttpGet("{id:int}")]
    public ApiResponse<Branch> GetById(int id)
    {
        return branchService.GetById(id);
    }
    [HttpPost]
    public ApiResponse<bool> Add(Branch branch)
    {
        return branchService.Add(branch);
    }
    [HttpPut]
    public ApiResponse<bool> Update(Branch branch)
    {
        return branchService.Update(branch);
    }
    [HttpDelete]
    public ApiResponse<bool> Delete(int id)
    {
        return branchService.Delete(id);
    }
    
    
    //New
    [HttpGet("GetByCondition")]
    public ApiResponse<List<Branch>> GetByCondition([FromQuery] string condition)
    {
        return branchService.GetByCondition(condition);
    }
    
    [HttpGet("Exists/{id:int}")]
    public ApiResponse<bool> Exists(int id)
    {
        return branchService.Exists(id);
    }
    
    [HttpGet("Count")]
    public ApiResponse<int> Count()
    {
        return branchService.Count();
    }
    
    [HttpPatch("{id:int}")]
    public ApiResponse<bool> UpdatePartial(int id, [FromQuery] string propertyName, [FromQuery] object newValue)
    {
        return branchService.UpdatePartial(id, propertyName, newValue);
    }
}