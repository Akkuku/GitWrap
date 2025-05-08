using System.Collections.Generic;
using System.Linq;

namespace GitWrap.Infrastructure.Extensions;

internal static class DictionaryExtensions
{
    public static string ToQueryString(this Dictionary<string, string> query)
    {
        if (query.Count == 0)
            return "";

        var queryString = string.Join('&', query.Select(param => $"{param.Key}={param.Value}"));

        return $"?{queryString}";
    }
}