﻿namespace WiseReminder.Infrastructure.Caching;

public interface ICacheService
{
    Task CreateAsync<T>(string key, T entity, TimeSpan? time = null) where T : notnull;
    Task<T?> GetAsync<T>(string key) where T : class;
}