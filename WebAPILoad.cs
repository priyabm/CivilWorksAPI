using System.Collections.Generic;
using System.Reflection;
using System.Web.Http.Dispatcher;


namespace APISelfHosting
{
    public class WebAPILoad : IAssembliesResolver
    {
        public string Path { get; set; }
        public WebAPILoad(string path)
        {
            Path = path;
        }

        public virtual ICollection<Assembly> GetAssemblies()
        {
            List<Assembly> assemblies = new List<Assembly>();
            assemblies.Add(Assembly.LoadFrom(Path));
            return assemblies;

        }
    }
}
