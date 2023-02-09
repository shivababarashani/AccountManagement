using AccountManagement.Contract.Dto;
using AccountManagement.Contract.Dto.Setting;
using AccountManagement.Contract.Interfaces.Proxy;
using AccountManagement.Contract.Interfaces.Repositories;
using AccountManagement.Contract.Interfaces.Services;
using AccountManagement.Framework.QueueProducer;
using Microsoft.Extensions.Options;
using AccountManagement.Service.Services;
using AccountManagement.ServicesProxy.Implementation.XProxy.Implementation;
using AccountManagement.Persistence.Implimentation;
using AccountManagement.Persistence.Implimentation.Repositories;
using AccountManagementApi.FluentValidators;
using FluentValidation;
using AccountManagement.Framework.Logger;
using Microsoft.Extensions.Configuration;
using AccountManagement.Framework.Common;
using QActionLogConsumer.LogManagement;
using System.Security.Authentication;
namespace AccountManagementApi.Helper
{
    public static class IocExtension
    {
        public static void AddCustomServices(this IServiceCollection services)
        {
            services.AddScoped<ILetterService, LetterService>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IDemandPacketService, DemandPacketService>();
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<IAccountManagementService, AccountManagementService>();
            services.AddScoped<IBlockDepositTransactionService, BlockDepositTransactionService>();
            services.AddScoped<IBlockWithdrawTransactionService, BlockWithdrawTransactionService>();

        }
        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<ILetterRepository, LetterRepository>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddScoped<IDemandPacketRepository, DemandPacketRepository>();
            services.AddScoped<IBlockUnblockReasonRepository, BlockUnblockReasonRepository>();
            services.AddScoped<IBlockDepositTransactionRepository, BlockDepositTransactionRepository>();
            services.AddScoped<IBlockWithdrawTransactionRepository, BlockWithdrawTransactionRepository>();
        }

        public static void AddUnitOfWork(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }

        public static void AddInfra(this IServiceCollection services)
        {
            services.AddScoped<IXProxy, XProxy>();
            services.AddScoped<ISignature, Signature>();

        }
        public static void AddValidators(this IServiceCollection services)
        {
            services.AddScoped<IValidator<GenerateTrackingCodeRequest>, GenerateTrackingCodeRequestValidator>();
        }
        public static void AddFreamwork(this IServiceCollection services)
        {
            services.AddHttpContextAccessor();
            services.AddTransient<IQueueProducer, RabitMQProducer>();
           
            services.AddSingleton<IRequestResponseLogger, RequestResponseLogger>();
            services.AddScoped<IRequestResponseLogModelCreator, RequestResponseLogModelCreator>();

            services.AddScoped<ILogCoreFactory,LogCoreFactory>();
            services.AddSingleton<ILogManager, LogManager>();
        }
        public static void AddHttpClientFactory(this IServiceCollection services)
        {
            var sp = services.BuildServiceProvider();
            var siteSetting = sp.GetService<IOptions<SiteSetting>>().Value;
         
            services.AddHttpClient("ebank", c =>
            {
                c.BaseAddress = new Uri(siteSetting.DotinConfig.ServiceUrl);
                c.Timeout = TimeSpan.FromSeconds(100);
                
            });
        }
    }
}
