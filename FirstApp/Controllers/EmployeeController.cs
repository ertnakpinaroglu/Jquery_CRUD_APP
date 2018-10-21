using FirstApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace FirstApp.Controllers
{
    public class EmployeeController : ApiController
    {
        DataAccess access = new DataAccess();
        [HttpGet]
        public HttpResponseMessage Get()
        {
            List<Employee> list = access.Employees.ToList();
            if (list == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Veritabanından veri çekilemedir");
            }
            return Request.CreateResponse(HttpStatusCode.OK, list);
        }
        [HttpGet]
        [Route(Name = "GetById")]
        public HttpResponseMessage Get(int id)
        {
            Employee findedEmployee = access.Employees.FirstOrDefault(m => m.Id == id);
            if (findedEmployee == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Boyle bir emp yok");
            }
            return Request.CreateResponse(HttpStatusCode.OK, findedEmployee);
        }
        [HttpPost]
        public HttpResponseMessage Post([FromBody] Employee employee)
        {
            access.Employees.Add(employee);
            if (access.SaveChanges() > 0)
            {
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, "Kişi başarıyla eklendi");
                response.Headers.Location = new Uri(Url.Link("GetById", new { id = employee.Id }));

                return response;
            }
            return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Kişi eklenemedi");
        }
    }
}
