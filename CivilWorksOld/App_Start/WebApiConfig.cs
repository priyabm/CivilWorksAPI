using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json.Serialization;
using System.Web.Http.Cors;
using System.Threading.Tasks;
using System.Threading;
using System.Net;

namespace CivilWorks
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            // Configure Web API to use only bearer token authentication.
            //config.SuppressDefaultHostAuthentication();
            //config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));

            // Web API routes
            config.MapHttpAttributeRoutes();
            var cors = new EnableCorsAttribute("*", "*", "*");
            config.EnableCors(cors);
          //config.MessageHandlers.Add(new PreflightRequestsHandler());


            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
            config.Formatters.Remove(config.Formatters.XmlFormatter);
        }
        //public class PreflightRequestsHandler : DelegatingHandler
        //{
        //    protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        //    {
        //        if (request.Headers.Contains("Origin") && request.Method.Method == "OPTIONS")
        //        {
        //            var response = new HttpResponseMessage { StatusCode = HttpStatusCode.OK };
        //            response.Headers.Add("Access-Control-Allow-Origin", "*");
        //            response.Headers.Add("Access-Control-Allow-Headers", "Origin, Content-Type, Accept, Authorization, x-requested-with, dwt-md5, inputjson ");
        //            response.Headers.Add("Access-Control-Allow-Methods", "*");
        //            var tsc = new TaskCompletionSource<HttpResponseMessage>();
        //            tsc.SetResult(response);
        //            return tsc.Task;
        //        }
        //        return base.SendAsync(request, cancellationToken);
        //    }
        //    //protected abstract Task IncommingMessageAsync(string correlationId, string requestInfo, byte[] message);
        //    //protected abstract Task OutgoingMessageAsync(string correlationId, string requestInfo, byte[] message);
        //}
    }
}
