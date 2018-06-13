using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CivilWorks.WebAPI.RequestHandler
{
    interface IRequestHandler<T>
    {
        HttpResponseMessage Get(HttpRequestMessage request);
    }
}
