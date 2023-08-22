using Model;
using Service.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace Project.WebAPI.Controllers
{
    public class DefaultController : ApiController
    {
        public IService Service { get; set; }
        public DefaultController(IService service)
        {
            Service = service;
        }
        [HttpGet]
        [Route("getall/")]
        public async Task<HttpResponseMessage> GetAllAsync()
        {
            try
            {
                List<StudentDTO> list = await Service.GetAllAsync();
                return Request.CreateResponse(HttpStatusCode.OK, list);
            }
            catch (Exception x)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, $"Error for GetAllAsync: {x.Message}");
            }
        }
    }
}
