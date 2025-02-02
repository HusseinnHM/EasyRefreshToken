﻿using EasyRefreshToken.Models;
using EasyRefreshToken.TokenService;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace EasyRefreshToken.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Add refresh token service to your project
        /// </summary>
        /// <typeparam name="TDbContext"></typeparam>
        /// <typeparam name="TRefreshToken"></typeparam>
        /// <typeparam name="TUser"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="services"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public static IServiceCollection AddRefreshToken<TDbContext, TRefreshToken, TUser, TKey>(
            this IServiceCollection services, Action<RefreshTokenOptions> options = default)
            where TDbContext : DbContext
            where TRefreshToken : RefreshToken<TUser, TKey>, new()
            where TKey : IEquatable<TKey>
            => services.Configure(options)
                .AddScoped<ITokenService, TokenService<TDbContext, TRefreshToken, TUser, TKey>>();

        /// <summary>
        /// Add refresh token service to your project
        /// </summary>
        /// <param name="services"></param>
        /// <param name="options"></param>
        /// <typeparam name="TDbContext"></typeparam>
        /// <typeparam name="TUser"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <returns></returns>
        public static IServiceCollection AddRefreshToken<TDbContext, TUser, TKey>(this IServiceCollection services,
            Action<RefreshTokenOptions> options = default)
            where TDbContext : DbContext where TKey : IEquatable<TKey>
            => services.AddRefreshToken<TDbContext, RefreshToken<TUser, TKey>, TUser, TKey>(options);

        /// <summary>
        /// Add refresh token service to your project
        /// </summary>
        /// <typeparam name="TDbContext"></typeparam>
        /// <typeparam name="TRefreshToken"></typeparam>
        /// <param name="services"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public static IServiceCollection AddRefreshToken<TDbContext, TRefreshToken>(this IServiceCollection services,
            Action<RefreshTokenOptions> options = default)
            where TDbContext : DbContext
            where TRefreshToken : RefreshToken<IdentityUser<string>, string>, new()
            => services.AddRefreshToken<TDbContext, TRefreshToken, IdentityUser<string>, string>(options);

        /// <summary>
        /// Add refresh token service to your project
        /// </summary>
        /// <typeparam name="TDbContext"></typeparam>
        /// <param name="services"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public static IServiceCollection AddRefreshToken<TDbContext>(this IServiceCollection services,
            Action<RefreshTokenOptions> options = default)
            where TDbContext : DbContext
            => services
                .AddRefreshToken<TDbContext, RefreshToken<IdentityUser<string>, string>, IdentityUser<string>, string>(
                    options);
    }
}