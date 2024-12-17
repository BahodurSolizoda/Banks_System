using System.Net;
using Dapper;
using Domain.Entities;
using Infrastructure.DataContext;
using Infrastructure.Interfaces;
using Infrastructure.Responses;

namespace Infrastructure.Services;

public class BranchService(DapperContext context) : IGenericService<Branch>
{
    public async Task<ApiResponse<List<Branch>>> GetAll()
    {
        using var connection = context.Connection;
        string sql = "select * from branches";
        var res = await connection.QueryAsync<Branch>(sql);
        return new ApiResponse<List<Branch>>(res.ToList());
    }

    public async Task<ApiResponse<Branch>> GetById(int id)
    {
        using var connection = context.Connection;
        string sql = "select * from Branches where branchId = @Id";
        var res = await connection.QuerySingleOrDefaultAsync<Branch>(sql, new { Id = id });
        if (res == null) return new ApiResponse<Branch>(HttpStatusCode.NotFound, "Branch not found");
        return new ApiResponse<Branch>(res);
    }

    public async Task<ApiResponse<bool>> Add(Branch data)
    {
        using var connection = context.Connection;
        string sql = @"insert into Branches(branchname, branchlocation, createdat, deletedat)
                      values(@branchname, @branchlocation, @createdat, @deletedat)";
        var res = await connection.ExecuteAsync(sql, data);
        if (res == 0) return new ApiResponse<bool>(HttpStatusCode.InternalServerError, "Internal server error");
        return new ApiResponse<bool>(true);
    }

    public async Task<ApiResponse<bool>> Update(Branch data)
    {
        using var connection = context.Connection;
        string sql = @"update Branches set branchname = @branchname, branchlocation = @branchlocation, 
                      createdat = @createdat, deletedat = @deletedat 
                      where branchId = @branchId";
        var res = await connection.ExecuteAsync(sql, data);
        if (res == 0) return new ApiResponse<bool>(HttpStatusCode.InternalServerError, "Internal server error");
        return new ApiResponse<bool>(true);
    }

    public async Task<ApiResponse<bool>> Delete(int id)
    {
        using var connection = context.Connection;
        string sql = "delete from Branches where branchId = @Id";
        var res = await connection.ExecuteAsync(sql, new { Id = id });
        if (res == 0) return new ApiResponse<bool>(HttpStatusCode.NotFound, "Branch not found");
        return new ApiResponse<bool>(true);
    }

    // Condition
    public async Task<ApiResponse<List<Branch>>> GetByCondition(string condition)
    {
        using var connection = context.Connection;
        string sql = $"select * from Branches where {condition}";
        var result = await connection.QueryAsync<Branch>(sql);
        return new ApiResponse<List<Branch>>(result.ToList());
    }

    // Existence
    public async Task<ApiResponse<bool>> Exists(int id)
    {
        using var connection = context.Connection;
        string sql = "select count(1) from Branches WHERE BranchId = @Id";
        var result = await connection.ExecuteScalarAsync<int>(sql, new { Id = id });
        return new ApiResponse<bool>(result > 0);
    }

    // Counting
    public async Task<ApiResponse<int>> Count()
    {
        using var connection = context.Connection;
        string sql = "select count(*) from Branches";
        var result = await connection.ExecuteScalarAsync<int>(sql);
        return new ApiResponse<int>(result);
    }

    // Partial Update
    public async Task<ApiResponse<bool>> UpdatePartial(int id, string propertyName, object newValue)
    {
        using var connection = context.Connection;
        string sql = $"update Branches set {propertyName} = @NewValue where BranchId = @Id";
        var result = await connection.ExecuteAsync(sql, new { NewValue = newValue, Id = id });
        return new ApiResponse<bool>(result > 0);
    }
}