using System.Net.Http;
using System.Net.Http.Headers;

namespace MyBills.Application.IntegrationTests.Common
{
    public static class HttpRequestMessageUtils
    {
        public static HttpRequestMessage GetHttpRequestMessage()
        {
            return new HttpRequestMessage
            {
                Headers =
                {
                    Authorization = GetAuthorization()
                }
            };
        }

        public static AuthenticationHeaderValue GetAuthorization()
        {
            return new AuthenticationHeaderValue("breeer",
                "eyJ0eXAiOiJKV1QiLCJhbGciOiJSUzI1NiIsImtpZCI6Ilg1ZVhrNHh5b2pORnVtMWtsMll0djhkbE5QNC1jNTdkTzZRR1RWQndhTmsifQ.eyJpc3MiOiJodHRwczovL215YmlsbHMuYjJjbG9naW4uY29tLzIzMTI2ZDcyLTM0YmYtNDRjOS1iYTI2LTczN2U2ZmI0YmY5Ny92Mi4wLyIsImV4cCI6MTYxMzYxNDAzOSwibmJmIjoxNjEzNjEwNDM5LCJhdWQiOiI0NmVjNmE4Mi1mMGUzLTQzZDAtOGQ3OC0xZWRmYzYwNDczOTIiLCJvaWQiOiIyNTRlMTZiNi1iMjU0LTQwZjMtYTRlMS1jZDBmNzZjZTRjYjciLCJzdWIiOiIyNTRlMTZiNi1iMjU0LTQwZjMtYTRlMS1jZDBmNzZjZTRjYjciLCJjaXR5IjoiQW1zdGVyZGFtIiwiZ2l2ZW5fbmFtZSI6Ik51bm8iLCJmYW1pbHlfbmFtZSI6IkNvcnJlaWEiLCJwb3N0YWxDb2RlIjoiMjE1MktIIiwiY291bnRyeSI6IkhvbGxhbmQiLCJuYW1lIjoiTnVubyIsImVtYWlscyI6WyJudW5vY29ycmVpYTg1QGdtYWlsLmNvbSJdLCJ0ZnAiOiJCMkNfMV9TaWduSW5VcCIsIm5vbmNlIjoiOWI2NzgwNTEtOTFiNy00M2IzLTk2ZWEtMmE4N2MzOWViZWRhIiwic2NwIjoiZGVmYXVsdCIsImF6cCI6ImQ1YWZjNDgzLWZkMjctNGE2MC1hMzY4LTM2YWIwMGVmNzAwMyIsInZlciI6IjEuMCIsImlhdCI6MTYxMzYxMDQzOX0.TApZQgtTeFb3uX70zVmBurFxY3oogVm7XhjRL3WdRqDdcr1PQxZpe5uYz82ZXSi_S6P7Gdevd9a2YsGyndmd_wzCXWAKktRs7oIS0OFbImqxBJy-c6w0rpWzE9lS_kehcQ-1rVS6a9FBeSk7HMzWQk9lIswGQqoIemsmLSsXbvK1VZ4qDCCiN5mHGvBTnhE9mGYNBsI6UqouCBsjyNWH8qmGz9ISX9P2_G8NXCVRkIhGobcIv_BfEocl9KNLf65vIDr7Bhg_z03y8UFQRDJcd1iBFHYC_M2Byon17FLClhp1BT2eo2Ye5jcccJFtoIPBpkWOF4qdtedQuXZ8UsDAmg");
        }
    }
}