using System.Net;
using Dapper;
using Domain.Entities;
using Infrastructure.DataContext;
using Infrastructure.Interfaces;
using Infrastructure.Responses;

namespace Infrastructure.Services;

public class LoanService(DapperContext context):IGenericService<Loan>
{
    public ApiResponse<List<Loan>> GetAll()
    {
        using var connection = context.Connection;
        string sql = "select * from Loans";
        var res = connection.Query<Loan>(sql).AsQueryable().ToList();
        return new ApiResponse<List<Loan>>(res);
    }

    public ApiResponse<Loan> GetById(int id)
    {
        using var connection = context.Connection;
        string sql = "select * from Loans where loanId = @Id";
        var res = connection.QuerySingleOrDefault<Loan>(sql, new { Id = id });
        if (res == null) return new ApiResponse<Loan>(HttpStatusCode.NotFound, "Loan not found");
        return new ApiResponse<Loan>(res);
    }

    public ApiResponse<bool> Add(Loan data)
    {
        using var connection = context.Connection;
        string sql = """
                     insert into loans(loanamount, dateissued, createdat, deletedat, customerid, branchid)
                                    values(@loanamount, @dateissued, @createdat, @deletedat, @customerid, @branchid);
                     """;
        var res = connection.Execute(sql, data);
        if (res == 0) return new ApiResponse<bool>(HttpStatusCode.InternalServerError, "Internal server error");
        return new ApiResponse<bool>(res > 0);
    }

    public ApiResponse<bool> Update(Loan data)
    {
        using var connection = context.Connection;
        string sql = """
                     update loans set loanamount = @loanamount, dateissued = @dateissued, createdat = @createdat, deletedat = @deletedat, customerid = @customerid, branchid = @branchid where loanid = @loanid;
                     """;
        var res = connection.Execute(sql, data);
        if (res == 0) return new ApiResponse<bool>(HttpStatusCode.InternalServerError, "Internal server error");
        return new ApiResponse<bool>(res > 0);
    }

    public ApiResponse<bool> Delete(int id)
    {
        using var connection = context.Connection;
        string sql = "delete from loans where loanId = @Id";
        var res = connection.Execute(sql, new { Id = id });
        if (res == 0) return new ApiResponse<bool>(HttpStatusCode.NotFound, "loan not found");
        return new ApiResponse<bool>(res != 0);
    }
    
    
    //Condition
    public ApiResponse<List<Loan>> GetByCondition(string condition)
    {
        using var connection = context.Connection;
        string sql = $"select * from loans where {condition}";
        var result = connection.Query<Loan>(sql).ToList();
        return new ApiResponse<List<Loan>>(result);
    }
    
    
    //Existance
    public ApiResponse<bool> Exists(int id)
    {
        using var connection = context.Connection;
        string sql = "select count(1) from loans where LoanId = @Id";
        var result = connection.ExecuteScalar<int>(sql, new { Id = id });
        return new ApiResponse<bool>(result > 0);
    }
    
    
    //Counting
    public ApiResponse<int> Count()
    {
        using var connection = context.Connection;
        string sql = "select count(*) from loans";
        var result = connection.ExecuteScalar<int>(sql);
        return new ApiResponse<int>(result);
    }
    
    
    //Partial Updating
    public ApiResponse<bool> UpdatePartial(int id, string propertyName, object newValue)
    {
        using var connection = context.Connection;
        string sql = $"update loans set {propertyName} = @NewValue where LoanId = @Id";
        var result = connection.Execute(sql, new { NewValue = newValue, Id = id });
        return new ApiResponse<bool>(result > 0);
    }
}