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
    public async Task<ApiResponse<List<Branch>>> GetAll()
    {
        return await branchService.GetAll();
    }

    [HttpGet("{id:int}")]
    public async Task<ApiResponse<Branch>> GetById(int id)
    {
        return await branchService.GetById(id);
    }

    [HttpPost]
    public async Task<ApiResponse<bool>> Add([FromBody] Branch branch)
    {
        return await branchService.Add(branch);
    }

    [HttpPut]
    public async Task<ApiResponse<bool>> Update([FromBody] Branch branch)
    {
        return await branchService.Update(branch);
    }

    [HttpDelete("{id:int}")]
    public async Task<ApiResponse<bool>> Delete(int id)
    {
        return await branchService.Delete(id);
    }

    [HttpGet("GetByCondition")]
    public async Task<ApiResponse<List<Branch>>> GetByCondition([FromQuery] string condition)
    {
        return await branchService.GetByCondition(condition);
    }

    [HttpGet("Exists/{id:int}")]
    public async Task<ApiResponse<bool>> Exists(int id)
    {
        return await branchService.Exists(id);
    }

    [HttpGet("Count")]
    public async Task<ApiResponse<int>> Count()
    {
        return await branchService.Count();
    }

    [HttpPatch("{id:int}")]
    public async Task<ApiResponse<bool>> UpdatePartial(int id, [FromQuery] string propertyName, [FromQuery] object newValue)
    {
        return await branchService.UpdatePartial(id, propertyName, newValue);
    }
}