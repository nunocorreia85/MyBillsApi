using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace MyBills.Api.Common
{
    public static class HttpRequestUtils
    {
        public static List<long> GetQueryIds(HttpRequest req)
        {
            var values = req.Query["id"];
            var ids = new List<long>();
            foreach (var value in values)
                if (long.TryParse(value, out var id))
                    ids.Add(id);

            return ids;
        }
    }
}