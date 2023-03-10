using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TodoListWebApi.Application.Handlers;
using TodoListWebApi.Application.Mapping;
using TodoListWebApi.Domain.Interfaces;
using TodoListWebApi.Repository.Data;
using TodoListWebApi.Repository.Repositories;

namespace TodoListWebApi.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection ConfigureServices()
        {
            IServiceCollection services = new ServiceCollection();

            const string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=todolist;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";
            
            services.AddDbContext<DataContext>(optionsAction => optionsAction.UseSqlServer(connectionString));
            services.AddTransient<DbContext, DataContext>();
            services.AddScoped<ITodoRepository, TodoRepository>();
            
            services.AddMediatR(typeof(GetAllTodosHandler).Assembly);

            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            var mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);
            
            return services;
        }
    }
}