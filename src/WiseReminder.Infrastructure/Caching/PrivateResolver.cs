﻿namespace WiseReminder.Infrastructure.Caching;

public sealed class PrivateResolver : DefaultContractResolver
{
    protected override JsonProperty CreateProperty(MemberInfo member,
        MemberSerialization memberSerialization)
    {
        var prop = base.CreateProperty(member, memberSerialization);

        if (!prop.Writable)
        {
            var property = member as PropertyInfo;

            var hasPrivateSetter = property?.GetSetMethod(true) != null;

            prop.Writable = hasPrivateSetter;
        }

        return prop;
    }
}