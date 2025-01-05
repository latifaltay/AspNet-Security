using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Options;
using System.Net;
using WhiteBlackList.Web.MiddleWares;

namespace WhiteBlackList.Web.Filters
{
    public class CheckWhiteList : ActionFilterAttribute
    {
        private readonly IPList _iPList;

        public CheckWhiteList(IOptions<IPList> ipList)
        {
            _iPList = ipList.Value;
        }


        public override void OnActionExecuting(ActionExecutingContext context)
        {

            var reqIpAdress = context.HttpContext.Connection.RemoteIpAddress;

            var isWhiteList = _iPList.WhiteList.Where(x => IPAddress.Parse(x).Equals(reqIpAdress)).Any();

            if (!isWhiteList) 
            {
                context.Result = new StatusCodeResult((int)HttpStatusCode.Forbidden);
                return;
            }

            base.OnActionExecuting(context);
        }
    }
}
