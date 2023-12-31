﻿using LojaApi.Filters;

namespace LojaApi.DependencyInjections;

public static class LojaAutenticacaoDependencyInjection
{
    public static IServiceCollection AddLojaAuthentication(this IServiceCollection services)
    {
        services.AddDistributedMemoryCache();

        services.AddSession(options =>
        {
            options.IdleTimeout = TimeSpan.FromMinutes(20);
            options.Cookie.HttpOnly = true;
            options.Cookie.IsEssential = true;
        });
        
        services.AddScoped<UsuarioAutenticadoFilter>();
        
        return services;
    }
}