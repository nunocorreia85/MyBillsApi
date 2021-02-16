using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyBills.Api.Common
{
    public static class HttpRequestUtils
    {
        public static List<T> GetQueryKeyValues<T>(HttpRequest req, string queryKeyName)
            where T : IConvertible
        {
            var values = req.Query[queryKeyName];
            return values.Select(value => (T)Convert.ChangeType(value, typeof(T))).ToList();
        }
    }
}