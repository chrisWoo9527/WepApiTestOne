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

// ������־
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

// ɨ��̳���Profile����
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// ָ��ǰ�ε�ַ���� 
/*
builder.Services.AddCors(options =>
  options.AddDefaultPolicy(builder => builder.WithOrigins(
      new string[] { "http://localhost:3000" }).
      AllowAnyMethod().AllowAnyHeader().AllowCredentials())
);
       */

// �滻���õ�ServiceProviderFactory
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());


// �Զ�ע�����
builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
{
    AutofacExtend.UseCustomConfigureContainer(containerBuilder);
});

// ע��Sqlserver ������
builder.Services.AddDbContext<MirDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetSection("conn").Value);
    options.LogTo(Console.WriteLine);
});

// ʹ��redis��Ϊ�ֲ�ʽ���������
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

// �����ⶫ��   
/*
app.UseCors();
*/

app.Run();
