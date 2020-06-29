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
    public class ContactRoleController : ControllerBase
    {

        private ApplicationDbContext _db;

        public ContactRoleController(ApplicationDbContext db)
        {
            _db = db;
        }


        /// <summary>
        /// Adds a new Contact Role 
        /// </summary>
        /// <param name="contactRole"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Post(ContactRole contactRole)
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

            var contactRoleObj = new ContactRole()
            {
                Description = contactRole.Description
                //UserId = user.Id,
            };
            _db.ContactRoles.Add(contactRoleObj);
            _db.SaveChanges();

            return Ok(new { vehicleId = contactRoleObj.Id, message = "Contact Role Added Successfully" });
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        [HttpGet("[action]")]
        [ProducesResponseType(200, Type = typeof(List<ContactRoleDTO>))]
        public IActionResult Search(string search)
        {
            var contactRoles = from c in _db.ContactRoles
                                where c.Description.StartsWith(search)
                                select new ContactRoleDTO
                                {
                                    Id = c.Id,
                                    Description = c.Description,
                                };

            return Ok(contactRoles);
        }


        /// <summary>
        /// Get contact roles full list
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        [ProducesResponseType(200, Type = typeof(List<ContactRoleDTO>))]
        public IActionResult List()
        {

            var contactRoles = from c in _db.ContactRoles
                                select new ContactRoleDTO
                                {
                                    Id = c.Id,
                                    Description = c.Description,
                                };

            return Ok(contactRoles);
        }


        /// <summary>
        /// Get details for a given business type
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(ContactRoleDTO))]
        public IActionResult Details(int id)
        {
            var foundContactRole = _db.BusinessTypes.Find(id);
            if (foundContactRole == null)
            {
                return NoContent();
            }

            //map and return DTO
            ContactRoleDTO contactRoleDTO = new ContactRoleDTO();
            contactRoleDTO.Description = foundContactRole.Description;

            return Ok(contactRoleDTO);
        }

    }
}
