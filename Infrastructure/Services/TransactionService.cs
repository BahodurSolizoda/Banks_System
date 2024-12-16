using System.Net;
using Dapper;
using Domain.Entities;
using Infrastructure.DataContext;
using Infrastructure.Interfaces;
using Infrastructure.Responses;

namespace Infrastructure.Services;

public class TransactionService(DapperContext context):IGenericService<Transaction>
{
    public ApiResponse<List<Transaction>> GetAll()
    {
        using var connection = context.Connection;
        string sql = "select * from Transactions";
        var res = connection.Query<Transaction>(sql).AsQueryable().ToList();
        return new ApiResponse<List<Transaction>>(res);
    }

    public ApiResponse<Transaction> GetById(int id)
    {
        using var connection = context.Connection;
        string sql = "select * from Transactions where transactionId = @Id";
        var res = connection.QuerySingleOrDefault<Transaction>(sql, new { Id = id });
        if (res == null) return new ApiResponse<Transaction>(HttpStatusCode.NotFound, "Transaction not found");
        return new ApiResponse<Transaction>(res);
    }

    public ApiResponse<bool> Add(Transaction data)
    {
        using var connection = context.Connection;
        string sql = """
                     insert into Transactions(transactionstatus, dateissued, amount, createdat, deletedat, fromaccountid, toaccountid)
                                    values(@transactionstatus, @dateissued, @amount, @createdat, @deletedat, @fromaccountid, @toaccountid)"";
                     """;
        var res = connection.Execute(sql, data);
        if (res == 0) return new ApiResponse<bool>(HttpStatusCode.InternalServerError, "Internal server error");
        return new ApiResponse<bool>(res > 0);
    }

    public ApiResponse<bool> Update(Transaction data)
    {
        using var connection = context.Connection;
        string sql = """
                     update Transactions set transactionstatus = @transactionstatus, dateissued = @dateissued, amount=@amount, 
                     createdat = @createdat, deletedat = @deletedat, fromaccountid = @fromaccountid, toaccountid = @toaccountid where transactionId = @transactionId;
                     """;
        var res = connection.Execute(sql, data);
        if (res == 0) return new ApiResponse<bool>(HttpStatusCode.InternalServerError, "Internal server error");
        return new ApiResponse<bool>(res > 0);
    }

    public ApiResponse<bool> Delete(int id)
    {
        using var connection = context.Connection;
        string sql = "delete from Transactions where transactionId = @Id";
        var res = connection.Execute(sql, new { Id = id });
        if (res == 0) return new ApiResponse<bool>(HttpStatusCode.NotFound, "Transaction not found");
        return new ApiResponse<bool>(res != 0);
    }
    
    
    //Condition
    public ApiResponse<List<Transaction>> GetByCondition(string condition)
    {
        using var connection = context.Connection;
        string sql = $"select * from Transactions where {condition}";
        var result = connection.Query<Transaction>(sql).ToList();
        return new ApiResponse<List<Transaction>>(result);
    }
    
    
    //Existance
    public ApiResponse<bool> Exists(int id)
    {
        using var connection = context.Connection;
        string sql = "select count(1) from Transactions where TransactionId = @Id";
        var result = connection.ExecuteScalar<int>(sql, new { Id = id });
        return new ApiResponse<bool>(result > 0);
    }
    
    
    //Counting
    public ApiResponse<int> Count()
    {
        using var connection = context.Connection;
        string sql = "select count(*) from Transactions";
        var result = connection.ExecuteScalar<int>(sql);
        return new ApiResponse<int>(result);
    }
    
    
    //Partial Updating
    public ApiResponse<bool> UpdatePartial(int id, string propertyName, object newValue)
    {
        using var connection = context.Connection;
        string sql = $"update Transactions set {propertyName} = @NewValue where TransactionId = @Id";
        var result = connection.Execute(sql, new { NewValue = newValue, Id = id });
        return new ApiResponse<bool>(result > 0);
    }
}