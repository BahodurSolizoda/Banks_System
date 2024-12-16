using Domain.Entities;
using Infrastructure.DataContext;
using Infrastructure.Interfaces;
using Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddScoped<DapperContext>();
builder.Services.AddScoped<IGenericService<Accounts>, AccountService>();
builder.Services.AddScoped<IGenericService<Branch>, BranchService>();
builder.Services.AddScoped<IGenericService<Customer>, CustomerService>();
builder.Services.AddScoped<IGenericService<Loan>, LoanService>();
builder.Services.AddScoped<IGenericService<Transaction>, TransactionService>();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    //app.MapScalarApiReference();
    app.UseSwaggerUI(options => options.SwaggerEndpoint("/openapi/v1.json", "WebApp v1"));
}

app.MapControllers();
app.Run();