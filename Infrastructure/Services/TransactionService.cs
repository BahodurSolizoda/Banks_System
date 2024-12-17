using System.Net;
using Dapper;
using Domain.Entities;
using Infrastructure.DataContext;
using Infrastructure.Interfaces;
using Infrastructure.Responses;

namespace Infrastructure.Services;

public class TransactionService(DapperContext context) : IGenericService<Transaction>
{
    public async Task<ApiResponse<List<Transaction>>> GetAll()
    {
        using var connection = context.Connection;
        string sql = "select * from Transactions";
        var res = (await connection.QueryAsync<Transaction>(sql)).ToList();
        return new ApiResponse<List<Transaction>>(res);
    }

    public async Task<ApiResponse<Transaction>> GetById(int id)
    {
        using var connection = context.Connection;
        string sql = "select * from Transactions where transactionId = @Id";
        var res = await connection.QuerySingleOrDefaultAsync<Transaction>(sql, new { Id = id });
        if (res == null) return new ApiResponse<Transaction>(HttpStatusCode.NotFound, "Transaction not found");
        return new ApiResponse<Transaction>(res);
    }

    public async Task<ApiResponse<bool>> Add(Transaction data)
    {
        using var connection = context.Connection;
        string sql = @"""
                     insert into Transactions(transactionstatus, dateissued, amount, createdat, deletedat, fromaccountid, toaccountid)
                                    values(@transactionstatus, @dateissued, @amount, @createdat, @deletedat, @fromaccountid, @toaccountid);
                     """;
        var res = await connection.ExecuteAsync(sql, data);
        if (res == 0) return new ApiResponse<bool>(HttpStatusCode.InternalServerError, "Internal server error");
        return new ApiResponse<bool>(res > 0);
    }

    public async Task<ApiResponse<bool>> Update(Transaction data)
    {
        using var connection = context.Connection;
        string sql = @"""
                     update Transactions set transactionstatus = @transactionstatus, dateissued = @dateissued, amount = @amount, 
                                            createdat = @createdat, deletedat = @deletedat, fromaccountid = @fromaccountid, 
                                            toaccountid = @toaccountid 
                     where transactionId = @transactionId;
                     """;
        var res = await connection.ExecuteAsync(sql, data);
        if (res == 0) return new ApiResponse<bool>(HttpStatusCode.InternalServerError, "Internal server error");
        return new ApiResponse<bool>(res > 0);
    }

    public async Task<ApiResponse<bool>> Delete(int id)
    {
        using var connection = context.Connection;
        string sql = "delete from Transactions where transactionId = @Id";
        var res = await connection.ExecuteAsync(sql, new { Id = id });
        if (res == 0) return new ApiResponse<bool>(HttpStatusCode.NotFound, "Transaction not found");
        return new ApiResponse<bool>(res != 0);
    }

    //Condition
    public async Task<ApiResponse<List<Transaction>>> GetByCondition(string condition)
    {
        using var connection = context.Connection;
        string sql = $"select * from Transactions where {condition}";
        var result = (await connection.QueryAsync<Transaction>(sql)).ToList();
        return new ApiResponse<List<Transaction>>(result);
    }

    //Existance
    public async Task<ApiResponse<bool>> Exists(int id)
    {
        using var connection = context.Connection;
        string sql = "select count(1) from Transactions where TransactionId = @Id";
        var result = await connection.ExecuteScalarAsync<int>(sql, new { Id = id });
        return new ApiResponse<bool>(result > 0);
    }

    //Counting
    public async Task<ApiResponse<int>> Count()
    {
        using var connection = context.Connection;
        string sql = "select count(*) from Transactions";
        var result = await connection.ExecuteScalarAsync<int>(sql);
        return new ApiResponse<int>(result);
    }

    //Partial Updating
    public async Task<ApiResponse<bool>> UpdatePartial(int id, string propertyName, object newValue)
    {
        using var connection = context.Connection;
        string sql = $"update Transactions set {propertyName} = @NewValue where TransactionId = @Id";
        var result = await connection.ExecuteAsync(sql, new { NewValue = newValue, Id = id });
        return new ApiResponse<bool>(result > 0);
    }
}
