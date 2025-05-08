using System;
using System.Collections.Generic;
using GitWrap.Infrastructure.Extensions;

namespace GitWrap.Infrastructure.Utils;

public class QueryStringBuilder(Dictionary<string, string>? query = null)
{
    private readonly Dictionary<string, string> _query = query ?? new Dictionary<string, string>();

    public QueryStringBuilder AddParam(string key, string value)
    {
        _query.Add(key, Uri.EscapeDataString(value));
        return this;
    }

    public QueryStringBuilder AddParamIfNotNull(string key, string? value)
        => value is null
            ? this
            : AddParam(key, value);

    public string Build() => _query.ToQueryString();
}