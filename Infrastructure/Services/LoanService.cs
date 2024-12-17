using System.Net;
using Dapper;
using Domain.Entities;
using Infrastructure.DataContext;
using Infrastructure.Interfaces;
using Infrastructure.Responses;

namespace Infrastructure.Services;

public class LoanService(DapperContext context) : IGenericService<Loan>
{
    public async Task<ApiResponse<List<Loan>>> GetAll()
    {
        using var connection = context.Connection;
        string sql = "select * from loans";
        var res = (await connection.QueryAsync<Loan>(sql)).ToList();
        return new ApiResponse<List<Loan>>(res);
    }

    public async Task<ApiResponse<Loan>> GetById(int id)
    {
        using var connection = context.Connection;
        string sql = "select * from loans where loanid = @Id";
        var res = await connection.QuerySingleOrDefaultAsync<Loan>(sql, new { Id = id });
        if (res == null) return new ApiResponse<Loan>(HttpStatusCode.NotFound, "Loan not found");
        return new ApiResponse<Loan>(res);
    }

    public async Task<ApiResponse<bool>> Add(Loan data)
    {
        using var connection = context.Connection;
        string sql = @"""
                     insert into loans(loanamount, dateissued, createdat, deletedat, customerid, branchid)
                                    values(@loanamount, @dateissued, @createdat, @deletedat, @customerid, @branchid);
                     """;
        var res = await connection.ExecuteAsync(sql, data);
        if (res == 0) return new ApiResponse<bool>(HttpStatusCode.InternalServerError, "Internal server error");
        return new ApiResponse<bool>(res > 0);
    }

    public async Task<ApiResponse<bool>> Update(Loan data)
    {
        using var connection = context.Connection;
        string sql = @"""
                     update loans set loanamount = @loanamount, dateissued = @dateissued, 
                                     createdat = @createdat, deletedat = @deletedat, 
                                     customerid = @customerid, branchid = @branchid 
                     where loanid = @loanid;
                     """;
        var res = await connection.ExecuteAsync(sql, data);
        if (res == 0) return new ApiResponse<bool>(HttpStatusCode.InternalServerError, "Internal server error");
        return new ApiResponse<bool>(res > 0);
    }

    public async Task<ApiResponse<bool>> Delete(int id)
    {
        using var connection = context.Connection;
        string sql = "delete from loans where loanid = @Id";
        var res = await connection.ExecuteAsync(sql, new { Id = id });
        if (res == 0) return new ApiResponse<bool>(HttpStatusCode.NotFound, "Loan not found");
        return new ApiResponse<bool>(res != 0);
    }

    //Condition
    public async Task<ApiResponse<List<Loan>>> GetByCondition(string condition)
    {
        using var connection = context.Connection;
        string sql = $"select * from loans where {condition}";
        var result = (await connection.QueryAsync<Loan>(sql)).ToList();
        return new ApiResponse<List<Loan>>(result);
    }

    //Existance
    public async Task<ApiResponse<bool>> Exists(int id)
    {
        using var connection = context.Connection;
        string sql = "select count(1) from loans where loanid = @Id";
        var result = await connection.ExecuteScalarAsync<int>(sql, new { Id = id });
        return new ApiResponse<bool>(result > 0);
    }

    //Counting
    public async Task<ApiResponse<int>> Count()
    {
        using var connection = context.Connection;
        string sql = "select count(*) from loans";
        var result = await connection.ExecuteScalarAsync<int>(sql);
        return new ApiResponse<int>(result);
    }

    //Partial Updating
    public async Task<ApiResponse<bool>> UpdatePartial(int id, string propertyName, object newValue)
    {
        using var connection = context.Connection;
        string sql = $"update loans set {propertyName} = @NewValue where loanid = @Id";
        var result = await connection.ExecuteAsync(sql, new { NewValue = newValue, Id = id });
        return new ApiResponse<bool>(result > 0);
    }
}
