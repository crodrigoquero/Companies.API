using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Companies.API.Data;
using Companies.API.DTOs;
using Companies.API.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Companies.API.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    [Authorize(Roles = "Admin")] //it works (roles are working)
    public class DepartmentController : ControllerBase
    {

        private ApplicationDbContext _db;

        public DepartmentController(ApplicationDbContext db)
        {
            _db = db;
        }


        /// <summary>
        /// Adds a new Contact Role 
        /// </summary>
        /// <param name="department"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Post(Department department)
        {
            var userEmail = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email).Value;
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value; //USER ID RETRIEVAL (FROM TOKEN) !!!!!

            //HOUSTON, WE GOT A PROBLEM: la tbala de usuarios esta vacia en esta API,
            //porque nos hemos autentificado en otra API.
            //var user = _db.Users.FirstOrDefault(u => u.Email == userEmail);
            //if (user == null)
            //{
            //    return NotFound();
            //}

            var departmentObj = new Department()
            {
                Description = department.Description
                //UserId = user.Id,
            };



            _db.Departments.Add(departmentObj);
            _db.SaveChanges();

            return Ok(new { Id = departmentObj.Id, message = "Department Added Successfully" });
        }


        /// <summary>
        /// Search department by name
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        [HttpGet("[action]")]
        [ProducesResponseType(200, Type = typeof(List<DepartmentDTO>))]
        public IActionResult Search(string search)
        {
            var departments = from r in _db.RelationshipTypes
                               where r.Description.StartsWith(search)
                               select new DepartmentDTO
                               {
                                   Id = r.Id,
                                   Description = r.Description,
                               };

            return Ok(departments);
        }


        /// <summary>
        /// Get complete list of departments
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        [ProducesResponseType(200, Type = typeof(List<DepartmentDTO>))]
        public IActionResult List()
        {

            List<Department> departments = _db.Departments.ToList();

            if (departments.Count == 0) return NotFound();

            return Ok(departments);
        }


        /// <summary>
        /// Get a single department
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(DepartmentDTO))]
        [ProducesResponseType(404)]
        public IActionResult Details(int id)
        {
            var foundDepartments = _db.RelationshipTypes.Find(id);
            if (foundDepartments == null)
            {
                return NotFound();
            }

            //map and return DTO
            DepartmentDTO departmentDTO = new DepartmentDTO();
            departmentDTO.Description = foundDepartments.Description;

            return Ok(departmentDTO);
        }


        // I added this action for testing purpouses
        [HttpPost("[action]")]
        public IActionResult Redirection(int id)
        {
            if (id == 0)
            {
                return RedirectToAction("List", "Department");
            }

            else
            {
                return null;
            }


            //return Response.Redirect("List");
            //return RedirectToAction("List");
        }


    }

}
