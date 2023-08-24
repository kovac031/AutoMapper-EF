using Service.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Model;
using System.Net.Mail;
using Project.Models;

namespace Project.Controllers
{
    public class DefaultController : Controller
    {
        public IService Service { get; set; }
        public DefaultController(IService service)
        {
            Service = service;
        }
        // ---------------- GET ALL ----------------        
        public async Task<ActionResult> GetAllAsync()
        {
            List<StudentDTO> listDTO = await Service.GetAllAsync();
            List<StudentView> listView = new List<StudentView>();
            foreach (StudentDTO studentDTO in listDTO)
            {
                StudentView studentView = new StudentView();

                studentView.Id = studentDTO.Id;
                studentView.FirstName = studentDTO.FirstName;
                studentView.LastName = studentDTO.LastName;
                studentView.DateOfBirth = studentDTO.DateOfBirth;
                studentView.EmailAddress = studentDTO.EmailAddress;
                studentView.RegisteredOn = studentDTO.RegisteredOn;

                listView.Add(studentView);
            }
            return View(listView);
        }
    }
}