using Microsoft.Extensions.Options;
using System.Net;

namespace WhiteBlackList.Web.MiddleWares
{
    public class IPSafeMiddleWare
    {

        private readonly RequestDelegate _next;

        private readonly IPList _iPList;

        public IPSafeMiddleWare(RequestDelegate next, IOptions<IPList> ipList)
        {
            _next = next;
            _iPList = ipList.Value;

        }

        public async Task Invoke(HttpContext context) 
        {
            var requestIpAddress = context.Connection.RemoteIpAddress;

            var isWhiteList = _iPList.WhiteList.Where(x => IPAddress.Parse(x).Equals(requestIpAddress)).Any();

            if (!isWhiteList) 
            {
                context.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                return;
            }

            await _next(context);

        }

    }
}
