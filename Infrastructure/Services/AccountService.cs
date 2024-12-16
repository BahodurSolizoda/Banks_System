using System.Net;
using Dapper;
using Domain.Entities;
using Infrastructure.DataContext;
using Infrastructure.Interfaces;
using Infrastructure.Responses;

namespace Infrastructure.Services;

public class AccountService(DapperContext context):IGenericService<Accounts>
{
    public ApiResponse<List<Accounts>> GetAll()
    {
        using var connection = context.Connection;
        string sql = "select * from accounts";
        var res = connection.Query<Accounts>(sql).AsQueryable().ToList();
        return new ApiResponse<List<Accounts>>(res);
    }

    public ApiResponse<Accounts> GetById(int id)
    {
        using var connection = context.Connection;
        string sql = "select * from Accounts where accountId = @Id";
        var res = connection.QuerySingleOrDefault<Accounts>(sql, new { Id = id });
        if (res == null) return new ApiResponse<Accounts>(HttpStatusCode.NotFound, "Account not found");
        return new ApiResponse<Accounts>(res);
    }

    public ApiResponse<bool> Add(Accounts data)
    {
        using var connection = context.Connection;
        string sql = """
                     insert into customers(balance, accountstatus, accounttype, currency, createdat, deletedat)
                                    values(@balance, @accountstatus, @accounttype, @currency, @createdat, @deletedat
                     """;
        var res = connection.Execute(sql, data);
        if (res == 0) return new ApiResponse<bool>(HttpStatusCode.InternalServerError, "Internal server error");
        return new ApiResponse<bool>(res > 0);
    }

    public ApiResponse<bool> Update(Accounts data)
    {
        using var connection = context.Connection;
        string sql = """
                     update accounts set balance = @balance, accountstatus = @accounttype, currency = @currency, createdat = @createdat, deletedat = @deletedat where accountId = @accountId;
                     """;
        var res = connection.Execute(sql, data);
        if (res == 0) return new ApiResponse<bool>(HttpStatusCode.InternalServerError, "Internal server error");
        return new ApiResponse<bool>(res > 0);
    }

    public ApiResponse<bool> Delete(int id)
    {
        using var connection = context.Connection;
        string sql = "delete from acoounts where accountId = @Id";
        var res = connection.Execute(sql, new { Id = id });
        if (res == 0) return new ApiResponse<bool>(HttpStatusCode.NotFound, "Account not found");
        return new ApiResponse<bool>(res != 0);
    }
    
    //Condition
    public ApiResponse<List<Accounts>> GetByCondition(string condition)
    {
        using var connection = context.Connection;
        string sql = $"select * from Accounts where {condition}";
        var result = connection.Query<Accounts>(sql).ToList();
        return new ApiResponse<List<Accounts>>(result);
    }
    
    //Existance
    public ApiResponse<bool> Exists(int id)
    {
        using var connection = context.Connection;
        string sql = "select count(1) from Accounts WHERE AccountId = @Id";
        var result = connection.ExecuteScalar<int>(sql, new { Id = id });
        return new ApiResponse<bool>(result > 0);
    }
    
    
    //Counting
    public ApiResponse<int> Count()
    {
        using var connection = context.Connection;
        string sql = "select count(*) from Accounts";
        var result = connection.ExecuteScalar<int>(sql);
        return new ApiResponse<int>(result);
    }
    
    //Update partial
    public ApiResponse<bool> UpdatePartial(int id, string propertyName, object newValue)
    {
        using var connection = context.Connection;
        string sql = $"update Accounts set {propertyName} = @NewValue WHERE AccountId = @Id";
        var result = connection.Execute(sql, new { NewValue = newValue, Id = id });
        return new ApiResponse<bool>(result > 0);
    }
    
    
}