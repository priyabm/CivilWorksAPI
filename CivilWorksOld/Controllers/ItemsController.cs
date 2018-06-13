using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CivilWorks.Models;
using Bo=CivilWorks.BOModel;


namespace CivilWorks.Controllers
{

    [RoutePrefix("item")]
    public class ItemsController : ApiController
    {
       
            CivilWorksEntities2 _context = null;

        [HttpGet]
        [Route("GetItem")]
        public HttpResponseMessage GetItem(int id)
        {

            try
            {
                _context = new CivilWorksEntities2();
                var user = _context.Items.Where(u => u.ID== id).FirstOrDefault();
                return Request.CreateResponse(HttpStatusCode.OK, user);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }

        }

        [HttpGet]
        [Route("GetAllItems")]
        public HttpResponseMessage GetAllItems()
        {
            try
            {

                _context = new CivilWorksEntities2();

               // _context.Configuration.ProxyCreationEnabled = false;
                var users = _context.Items.ToList();
                //List<models> lst = new List<models>();
                //users.ForEach(A =>
                //    {
                //     lst.Add(new models { })
                //    }

                //    );
                //List<Bo.Item> lstItem = new List<Bo.Item>();
              
                //foreach (var item in users)
                //{
                //    Bo.Item Item = new Bo.Item();
                //    Item.ID = item.ID;
                //    Item.ItemName = item.ItemName;
                //    lstItem.Add(Item);
                //}
                return Request.CreateResponse(HttpStatusCode.OK,users);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }

        }
    }
}
