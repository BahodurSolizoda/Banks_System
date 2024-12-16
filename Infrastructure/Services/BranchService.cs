using System.Net;
using Dapper;
using Domain.Entities;
using Infrastructure.DataContext;
using Infrastructure.Interfaces;
using Infrastructure.Responses;

namespace Infrastructure.Services;

public class BranchService(DapperContext context):IGenericService<Branch>
{
    public ApiResponse<List<Branch>> GetAll()
    {
        using var connection = context.Connection;
        string sql = "select * from branches";
        var res = connection.Query<Branch>(sql).AsQueryable().ToList();
        return new ApiResponse<List<Branch>>(res);
    }

    public ApiResponse<Branch> GetById(int id)
    {
        using var connection = context.Connection;
        string sql = "select * from Branches where accountId = @Id";
        var res = connection.QuerySingleOrDefault<Branch>(sql, new { Id = id });
        if (res == null) return new ApiResponse<Branch>(HttpStatusCode.NotFound, "Branch not found");
        return new ApiResponse<Branch>(res);
    }

    public ApiResponse<bool> Add(Branch data)
    {
        using var connection = context.Connection;
        string sql = """
                     insert into Branches(branchname, branchlocation, createedat, deletedat)
                                    values(@branchname, @branchlocation, @createedat, @deletedat
                     """;
        var res = connection.Execute(sql, data);
        if (res == 0) return new ApiResponse<bool>(HttpStatusCode.InternalServerError, "Internal server error");
        return new ApiResponse<bool>(res > 0);
    }

    public ApiResponse<bool> Update(Branch data)
    {
        using var connection = context.Connection;
        string sql = """
                     update Branches set branchname = @branchname, branchlocation = @branchlocation,createdat = @createdat, deletedat = @deletedat where branchId = @accountId;
                     """;
        var res = connection.Execute(sql, data);
        if (res == 0) return new ApiResponse<bool>(HttpStatusCode.InternalServerError, "Internal server error");
        return new ApiResponse<bool>(res > 0);
    }

    public ApiResponse<bool> Delete(int id)
    {
        using var connection = context.Connection;
        string sql = "delete from Branches where branchId = @Id";
        var res = connection.Execute(sql, new { Id = id });
        if (res == 0) return new ApiResponse<bool>(HttpStatusCode.NotFound, "Branch not found");
        return new ApiResponse<bool>(res != 0);
    }
    
    
    //Condition
    public ApiResponse<List<Branch>> GetByCondition(string condition)
    {
        using var connection = context.Connection;
        string sql = $"select * from Branchs where {condition}";
        var result = connection.Query<Branch>(sql).ToList();
        return new ApiResponse<List<Branch>>(result);
    }
    
    
    //Existance
    public ApiResponse<bool> Exists(int id)
    {
        using var connection = context.Connection;
        string sql = "select count(1) from Branches WHERE BranchId = @Id";
        var result = connection.ExecuteScalar<int>(sql, new { Id = id });
        return new ApiResponse<bool>(result > 0);
    }
    
    
    //Counting
    public ApiResponse<int> Count()
    {
        using var connection = context.Connection;
        string sql = "select count(*) from Branches";
        var result = connection.ExecuteScalar<int>(sql);
        return new ApiResponse<int>(result);
    }
    
    
    //Partial Updating
    public ApiResponse<bool> UpdatePartial(int id, string propertyName, object newValue)
    {
        using var connection = context.Connection;
        string sql = $"update Branches set {propertyName} = @NewValue where BranchId = @Id";
        var result = connection.Execute(sql, new { NewValue = newValue, Id = id });
        return new ApiResponse<bool>(result > 0);
    }
}