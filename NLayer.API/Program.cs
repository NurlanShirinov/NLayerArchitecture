using Autofac;
using Autofac.Extensions.DependencyInjection;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NLayer.API.Filters;
using NLayer.API.Middlewares;
using NLayer.API.Modules;
using NLayer.Core.Repositories;
using NLayer.Core.Services;
using NLayer.Core.UnitOfWorks;
using NLayer.Repository;
using NLayer.Repository.Repositories;
using NLayer.Repository.UnitOfWorks;
using NLayer.Service.Mapping;
using NLayer.Service.Services;
using NLayer.Service.Validations;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
//Configuration 
// Add services to the container.

builder.Services.AddControllers(options => options.Filters.Add(new ValidateFilterAttribute())).AddFluentValidation(x=>x.RegisterValidatorsFromAssemblyContaining<ProductDtoValidator>());

builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;       //default olaraq geri donulen error modeli qapadiriq oz ValidateAttribudte clasimizin ishlemesi ucun.
});



builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMemoryCache();

builder.Services.AddScoped(typeof(NotFoundFilter<>));
builder.Services.AddAutoMapper(typeof(MapProfile));

builder.Services.AddDbContext<AppDbContext>(x =>
{
    x.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnection"), option =>
    {
        //option.MigrationsAssembly("NLayer.Repository");
        //bizim migrationlar Repository class library - de olacagi ucun biz ona tanidiriq burda.
        //Amma gelecekde birden classs libry yada proyektin adi deyishe biler deye ashagidaki dinamik
        //koddnan daha best practice yaziriq

        option.MigrationsAssembly(Assembly.GetAssembly(typeof(AppDbContext)).GetName().Name);
    });
});


builder.Host.UseServiceProviderFactory
    (new AutofacServiceProviderFactory());

builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder => containerBuilder.RegisterModule(new RepoServiceModule()));



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCustomException();

app.UseAuthorization();

app.MapControllers();

app.Run();




//builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
//builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));    // Generic oldugu ucun bu formada yazdig
//builder.Services.AddScoped(typeof(IService<>), typeof(Service<>));
