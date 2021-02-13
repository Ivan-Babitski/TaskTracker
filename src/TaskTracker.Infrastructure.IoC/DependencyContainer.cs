using Microsoft.Extensions.DependencyInjection;
using System;
using TaskTracker.Application.Interfaces;
using TaskTracker.Application.Services;
using TaskTracker.Core.Interfaces;
using TaskTracker.Infrastructure.Data.Repositories;

namespace TaskTracker.Infrastructure.IoC
{
    public class DependencyContainer
    {
        public static void RegisterServices(IServiceCollection services)
        {
            services.AddTransient<IFileDtoService, FileDtoService>();
            services.AddTransient<ITaskDtoService, TaskDtoService>();
            services.AddTransient<ITagDtoService, TagDtoService>();
            services.AddTransient<IAccountDtoService, AccountDtoService>();
            services.AddTransient<IAccessService, AccessService>();
            
        }

        public static void RegisterProvider(IServiceCollection services, string mode)
        {
            if (mode == "EF.NET")
            {
                services.AddScoped<IFilesRepository, FilesRepository>();
                services.AddScoped<IFriendshipsRepository, FriendshipsRepository>();
                services.AddScoped<INotificationsRepository, NotificationsRepository>();
                services.AddScoped<IPrioritiesRepository, PrioritiesRepository>();
                services.AddScoped<IProcessesRepository, ProcessesRepository>();
                services.AddScoped<ISpecialtiesRepository, SpecialtiesRepository>();
                services.AddScoped<ITagsRepository, TagsRepository>();
                services.AddScoped<ITasksRepository, TasksRepository>();
                services.AddScoped<ITasksTagsRepository, TasksTagsRepository>();
                services.AddScoped<IUsersRepository, UsersRepository>();
                services.AddScoped<IUsersSpecialtiesRepository, UsersSpecialtiesRepository>();
            }
        }
    }
}