using Application.Interfaces;
using Application.Services;
using Domain.Services.Interfaces;
using Domain.Services.Services;
using EntityFrameworkCore.UnitOfWork.Extensions;
using FluentValidation.AspNetCore;
using Infrastructure.Data.Context;
using Infrastructure.Data.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var assembly = Assembly.Load("Application");
builder.Services.AddControllers()
    .AddFluentValidation(options =>
    {
        options.RegisterValidatorsFromAssembly(assembly);
    });

builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseMySql(builder.Configuration.GetConnectionString("Default"),
                    ServerVersion.Parse("8.0.29-mysql"),
                    config => config.MigrationsAssembly("Infrastructure.Data"));
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<DbContext, DataContext>();

builder.Services.AddTransient<ICustomerAppService, CustomerAppService>();
builder.Services.AddTransient<ICustomerService, CustomerService>();

builder.Services.AddTransient<ICustomerBankInfoAppService, CustomerBankInfoAppService>();
builder.Services.AddTransient<ICustomerBankInfoService, CustomerBankInfoService>();

builder.Services.AddTransient<IPortfolioAppService, PortfolioAppService>();
builder.Services.AddTransient<IPortfolioService, PortfolioService>();

builder.Services.AddTransient<IProductAppService, ProductAppService>();
builder.Services.AddTransient<IProductService, ProductService>();

builder.Services.AddTransient<IOrderAppService, OrderAppService>();
builder.Services.AddTransient<IOrderService, OrderService>();

builder.Services.AddTransient<IPortfolioProductService, PortfolioProductService>();

builder.Services.AddTransient<IInvestmentService, InvestmentService>();

builder.Services.AddAutoMapper(mapperConfiguration => mapperConfiguration.AddMaps(assembly), assembly);
builder.Services.AddUnitOfWork(ServiceLifetime.Transient);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

var supportedCultures = "en-US";
var localizationOptions = new RequestLocalizationOptions().SetDefaultCulture(supportedCultures)
    .AddSupportedCultures(supportedCultures)
    .AddSupportedUICultures(supportedCultures);

app.UseRequestLocalization(localizationOptions);

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

builder.Services.ApplyMigrations();

app.Run();