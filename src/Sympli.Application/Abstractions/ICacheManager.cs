﻿namespace Sympli.Application.Abstractions;

public interface ICacheManager
{
    T? Get<T>(string key);

    void Set<T>(string key, T value, TimeSpan expiration);
}

