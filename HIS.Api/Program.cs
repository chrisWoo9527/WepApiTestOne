using Autofac;
using Autofac.Extensions.DependencyInjection;
using AutoMapper;
using HIS.Common;
using HIS.Common.AutoFacManager;
using HIS.Common.FileManager;
using HIS.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Serilog;
using System.Reflection;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// 启用日志
Log.Logger = new LoggerConfiguration()
             .MinimumLevel.Information()
             .WriteTo.Console()
             .WriteTo.File($"{AppContext.BaseDirectory}00_Logs\\log.log", rollingInterval: RollingInterval.Day)
             .CreateLogger();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddLogging(loggingBuilder =>
          loggingBuilder.AddSerilog(dispose: true));

// 扫描继承自Profile的类
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// 指定前段地址访问 
/*
builder.Services.AddCors(options =>
  options.AddDefaultPolicy(builder => builder.WithOrigins(
      new string[] { "http://localhost:3000" }).
      AllowAnyMethod().AllowAnyHeader().AllowCredentials())
);
       */

// 替换内置的ServiceProviderFactory
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());


// 自动注入服务
builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
{
    AutofacExtend.UseCustomConfigureContainer(containerBuilder);
});

// 注入Sqlserver 服务器
builder.Services.AddDbContext<MirDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetSection("conn").Value);
    options.LogTo(Console.WriteLine);
});

// 使用redis作为分布式缓存服务器
builder.Services.AddDistributedRedisCache(options =>
{
    options.InstanceName = "Cache_";
    options.Configuration = builder.Configuration.GetSection("Cacheredis").Value;
});

//builder.Configuration.AddToMasuitTools();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

// 启动这东西   
/*
app.UseCors();
*/

app.Run();
