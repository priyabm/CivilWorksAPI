using System;
using System.Web.Http.SelfHost;
using System.Web.Http;
using System.Web.Http.Dispatcher;
using System.Diagnostics;
using System.Web.Http.Cors;
using System.Net.Http;
using System.Threading.Tasks;
using System.Threading;
using System.Configuration;
using System.Net;

namespace APISelfHosting
{
    class Program
    {

        static void   Main(string[] args)
        {
            APIGet();          
        }
        public static void APIGet()
        {
            try
            {
                var config = new HttpSelfHostConfiguration("http://localhost:62447/");
                var cors = new EnableCorsAttribute("*", "*", "*");
                config.EnableCors(cors);

                config.MapHttpAttributeRoutes();

                config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }

            );
                //var cors = new EnableCorsAttribute("*", "*", "*");
                //config.EnableCors(cors);
                //config.MessageHandlers.Add(new PreflightRequestsHandler());
                //config.Formatters.Remove(config.Formatters.XmlFormatter);

                var assembly = System.Reflection.Assembly.GetExecutingAssembly().Location;
                string path = assembly.Substring(0, assembly.LastIndexOf("\\")) + "\\CivilWorks.dll";

               config.Services.Replace(typeof(IAssembliesResolver), new WebAPILoad(path));             
                HttpSelfHostServer server = new HttpSelfHostServer(config);
                var task = server.OpenAsync();
                task.Wait();
                StartProcess(assembly);
            }
            catch (Exception ex)
            {
                string name = ex.Message;
            }
        }

        public static void StartProcess(string assembly)
        {
            string path = assembly.Substring(0, assembly.LastIndexOf("\\")) + "\\dist";

            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = "cmd.exe";
            Process proStart = new Process();
            proStart.StartInfo = startInfo;
            proStart.StartInfo.RedirectStandardInput = true;
            proStart.StartInfo.UseShellExecute = false;
            startInfo.RedirectStandardOutput = true;
            startInfo.RedirectStandardError = true;
            proStart.StartInfo.WorkingDirectory = path;
            proStart.Start();
            proStart.StandardInput.WriteLine("http-server -p 3005 -c-1 -o");
            string output = proStart.StandardOutput.ReadToEnd();
        }
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
    //}
}

