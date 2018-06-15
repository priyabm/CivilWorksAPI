using BO = CivilWorks.BusinessObjects;
using CivilWorks.WebAPI.RequestHandler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CivilWorks.DataRepository.Model;

namespace CivilWorks.WebAPI.Controllers
{
    [RoutePrefix ("api/item")]
    public class ItemController: ApiController
    {
        private IRequestHandler<BO.Item> RequestHandler;
        public ItemController()
        {
            RequestHandler = new APIRequestHandler<BO.Item>();
        }

        public HttpResponseMessage Get()
        {
            return RequestHandler.Get(Request);

        }


    }
}