using System.Net;
using Dapper;
using Domain.Entities;
using Infrastructure.DataContext;
using Infrastructure.Interfaces;
using Infrastructure.Responses;

namespace Infrastructure.Services;

public class CustomerService(DapperContext context) : IGenericService<Customer>
{
    public async Task<ApiResponse<List<Customer>>> GetAll()
    {
        using var connection = context.Connection;
        string sql = "select * from customers";
        var res = (await connection.QueryAsync<Customer>(sql)).ToList();
        return new ApiResponse<List<Customer>>(res);
    }

    public async Task<ApiResponse<Customer>> GetById(int id)
    {
        using var connection = context.Connection;
        string sql = "select * from customers where customerid = @Id";
        var res = await connection.QuerySingleOrDefaultAsync<Customer>(sql, new { Id = id });
        if (res == null) return new ApiResponse<Customer>(HttpStatusCode.NotFound, "Customer not found");
        return new ApiResponse<Customer>(res);
    }

    public async Task<ApiResponse<bool>> Add(Customer data)
    {
        using var connection = context.Connection;
        string sql = @"""
                     insert into customers(firstname, lastname, city, phonenumber, pancardno, dob, createdat)
                                    values(@firstname, @lastname, @city, @phonenumber, @pancardno, @dob, current_date);
                     """;
        var res = await connection.ExecuteAsync(sql, data);
        if (res == 0) return new ApiResponse<bool>(HttpStatusCode.InternalServerError, "Internal server error");
        return new ApiResponse<bool>(res > 0);
    }

    public async Task<ApiResponse<bool>> Update(Customer data)
    {
        using var connection = context.Connection;
        string sql = @"""
                     update customers set firstname = @firstname, lastname = @lastname, city = @city, 
                                         phonenumber = @phonenumber, pancardno = @pancardno, dob = @dob 
                     where customerid = @customerid;
                     """;
        var res = await connection.ExecuteAsync(sql, data);
        if (res == 0) return new ApiResponse<bool>(HttpStatusCode.InternalServerError, "Internal server error");
        return new ApiResponse<bool>(res > 0);
    }

    public async Task<ApiResponse<bool>> Delete(int id)
    {
        using var connection = context.Connection;
        string sql = "delete from customers where customerid = @Id";
        var res = await connection.ExecuteAsync(sql, new { Id = id });
        if (res == 0) return new ApiResponse<bool>(HttpStatusCode.NotFound, "Customer not found");
        return new ApiResponse<bool>(res != 0);
    }

    //Condition
    public async Task<ApiResponse<List<Customer>>> GetByCondition(string condition)
    {
        using var connection = context.Connection;
        string sql = $"select * from Customers where {condition}";
        var result = (await connection.QueryAsync<Customer>(sql)).ToList();
        return new ApiResponse<List<Customer>>(result);
    }

    //Existance
    public async Task<ApiResponse<bool>> Exists(int id)
    {
        using var connection = context.Connection;
        string sql = "select count(1) from Customers where CustomerId = @Id";
        var result = await connection.ExecuteScalarAsync<int>(sql, new { Id = id });
        return new ApiResponse<bool>(result > 0);
    }

    //Counting
    public async Task<ApiResponse<int>> Count()
    {
        using var connection = context.Connection;
        string sql = "select count(*) from Customers";
        var result = await connection.ExecuteScalarAsync<int>(sql);
        return new ApiResponse<int>(result);
    }

    //Partial Updating
    public async Task<ApiResponse<bool>> UpdatePartial(int id, string propertyName, object newValue)
    {
        using var connection = context.Connection;
        string sql = $"update Customers set {propertyName} = @NewValue where CustomerId = @Id";
        var result = await connection.ExecuteAsync(sql, new { NewValue = newValue, Id = id });
        return new ApiResponse<bool>(result > 0);
    }
}
