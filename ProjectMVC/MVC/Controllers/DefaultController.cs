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
using AutoMapper;
using Service;

namespace Project.Controllers
{
    public class DefaultController : Controller
    {
        public readonly IMapper _mapper; // AutoMapper
        public IService Service { get; set; }
        public DefaultController(IService service, IMapper mapper)
        {
            Service = service;
            _mapper = mapper;
        }
        // ---------------- GET ALL ----------------        
        public async Task<ActionResult> GetAllAsync(string sortBy)
        {
            List<StudentDTO> listDTO = await Service.GetAllAsync(sortBy);

            //List<StudentView> listView = new List<StudentView>();
            //foreach (StudentDTO studentDTO in listDTO)
            //{
            //    StudentView studentView = new StudentView();

            //    studentView.Id = studentDTO.Id;
            //    studentView.FirstName = studentDTO.FirstName;
            //    studentView.LastName = studentDTO.LastName;
            //    studentView.DateOfBirth = studentDTO.DateOfBirth;
            //    studentView.EmailAddress = studentDTO.EmailAddress;
            //    studentView.RegisteredOn = studentDTO.RegisteredOn;

            //    listView.Add(studentView);
            //}

            // SORTING
            ViewBag.SortBySignUp = string.IsNullOrEmpty(sortBy) ? "signup_asc" : "";
            ViewBag.SortByDob = sortBy == "dob_asc" ? "dob_desc" : "dob_asc"; 
            ViewBag.SortByFirstName = sortBy == "name_asc" ? "name_desc" : "name_asc"; 
            ViewBag.SortByLastName = sortBy == "surname_asc" ? "surname_desc" : "surname_asc"; 
            //ViewBag.CurrentSort = sortBy;

            List<StudentView> listView = _mapper.Map<List<StudentView>>(listDTO);

            return View(listView);
        }
        // ---------------- GET ONE BY ID ----------------
        public async Task<ActionResult> GetOneByIdAsync(Guid id)
        {
            StudentDTO studentDTO = await Service.GetOneByIdAsync(id);
            StudentView studentView = _mapper.Map<StudentView>(studentDTO);
            return View(studentView);
        }
        // ---------------- CREATE NEW ----------------
        [HttpGet]
        public async Task<ActionResult> CreateAsync()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> CreateAsync(StudentView studentView)
        {
            try
            {
                StudentDTO studentDTO = _mapper.Map<StudentDTO>(studentView);

                bool created = await Service.CreateAsync(studentDTO);
                if (!created)
                {
                    return View("Failed to create");
                }
                return RedirectToAction("GetAllAsync");
            }
            catch (Exception)
            {
                return View("Exception");
            }
        }
        // ---------------- EDIT ----------------
        [HttpGet]
        public async Task<ActionResult> EditAsync(Guid id)
        {
            StudentDTO studentDTO = await Service.GetOneByIdAsync(id);
            StudentView studentView = _mapper.Map<StudentView>(studentDTO);
            return View(studentView);
        }
        [HttpPost]
        public async Task<ActionResult> EditAsync(StudentView studentView)
        {
            try
            {
                StudentDTO studentDTO = _mapper.Map<StudentDTO>(studentView);

                bool created = await Service.EditAsync(studentDTO, studentDTO.Id);
                if (!created)
                {
                    return View("Failed to edit");
                }
                return RedirectToAction("GetAllAsync");
            }
            catch (Exception)
            {
                return View("Exception");
            }
        }
        // ---------------- DELETE ----------------
        public async Task<ActionResult> DeleteAsync(Guid id)
        {
            await Service.DeleteAsync(id);
            return RedirectToAction("GetAllAsync");
        }
    }
}