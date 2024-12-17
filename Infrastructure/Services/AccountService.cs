using System.Net;
using Dapper;
using Domain.Entities;
using Infrastructure.DataContext;
using Infrastructure.Interfaces;
using Infrastructure.Responses;

namespace Infrastructure.Services;

public class AccountService(DapperContext context) : IGenericService<Accounts>
{
    public async Task<ApiResponse<List<Accounts>>> GetAll()
    {
        using var connection = context.Connection;
        string sql = "select * from accounts";
        var res = await connection.QueryAsync<Accounts>(sql);
        return new ApiResponse<List<Accounts>>(res.ToList());
    }

    public async Task<ApiResponse<Accounts>> GetById(int id)
    {
        using var connection = context.Connection;
        string sql = "select * from Accounts where accountId = @Id";
        var res = await connection.QuerySingleOrDefaultAsync<Accounts>(sql, new { Id = id });
        if (res == null) return new ApiResponse<Accounts>(HttpStatusCode.NotFound, "Account not found");
        return new ApiResponse<Accounts>(res);
    }

    public async Task<ApiResponse<bool>> Add(Accounts data)
    {
        using var connection = context.Connection;
        string sql = @"insert into accounts(balance, accountstatus, accounttype, currency, createdat, deletedat)
                      values(@balance, @accountstatus, @accounttype, @currency, @createdat, @deletedat)";
        var res = await connection.ExecuteAsync(sql, data);
        if (res == 0) return new ApiResponse<bool>(HttpStatusCode.InternalServerError, "Internal server error");
        return new ApiResponse<bool>(true);
    }

    public async Task<ApiResponse<bool>> Update(Accounts data)
    {
        using var connection = context.Connection;
        string sql = @"update accounts set balance = @balance, accountstatus = @accountstatus, accounttype = @accounttype, 
                      currency = @currency, createdat = @createdat, deletedat = @deletedat 
                      where accountId = @accountId";
        var res = await connection.ExecuteAsync(sql, data);
        if (res == 0) return new ApiResponse<bool>(HttpStatusCode.InternalServerError, "Internal server error");
        return new ApiResponse<bool>(true);
    }

    public async Task<ApiResponse<bool>> Delete(int id)
    {
        using var connection = context.Connection;
        string sql = "delete from accounts where accountId = @Id";
        var res = await connection.ExecuteAsync(sql, new { Id = id });
        if (res == 0) return new ApiResponse<bool>(HttpStatusCode.NotFound, "Account not found");
        return new ApiResponse<bool>(true);
    }

    // Condition
    public async Task<ApiResponse<List<Accounts>>> GetByCondition(string condition)
    {
        using var connection = context.Connection;
        string sql = $"select * from Accounts where {condition}";
        var result = await connection.QueryAsync<Accounts>(sql);
        return new ApiResponse<List<Accounts>>(result.ToList());
    }

    // Existence
    public async Task<ApiResponse<bool>> Exists(int id)
    {
        using var connection = context.Connection;
        string sql = "select count(1) from Accounts WHERE AccountId = @Id";
        var result = await connection.ExecuteScalarAsync<int>(sql, new { Id = id });
        return new ApiResponse<bool>(result > 0);
    }

    // Counting
    public async Task<ApiResponse<int>> Count()
    {
        using var connection = context.Connection;
        string sql = "select count(*) from Accounts";
        var result = await connection.ExecuteScalarAsync<int>(sql);
        return new ApiResponse<int>(result);
    }

    // Partial Update
    public async Task<ApiResponse<bool>> UpdatePartial(int id, string propertyName, object newValue)
    {
        using var connection = context.Connection;
        string sql = $"update Accounts set {propertyName} = @NewValue WHERE AccountId = @Id";
        var result = await connection.ExecuteAsync(sql, new { NewValue = newValue, Id = id });
        return new ApiResponse<bool>(result > 0);
    }
}
