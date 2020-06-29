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
    public class ContactController : ControllerBase
    {
        private ApplicationDbContext _db;

        public ContactController(ApplicationDbContext db)
        {
            _db = db;
        }


        /// <summary>
        /// Adds a new Contact Role 
        /// </summary>
        /// <param name="contact"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Post(Contact contact)
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

            var contactObj = new Contact()
            {
                CompanyId = contact.CompanyId,
                DepartmentId = contact.DepartmentId,
                ContactRoleId = contact.ContactRoleId,
                FirstName = contact.FirstName,
                LastName = contact.LastName,
                Phome = contact.Phome
            };
            _db.Contacts.Add(contactObj);
            _db.SaveChanges();

            return Ok(new { vehicleId = contactObj.Id, message = "Company Contact Added Successfully" });
        }


        /// <summary>
        /// Search contacts by last name
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        [HttpGet("[action]")]
        [ProducesResponseType(200, Type = typeof(List<ContactDTO>))]
        public IActionResult Search(string search)
        {
            var contactRoles = from c in _db.Contacts
                               where c.LastName.StartsWith(search)
                               select new ContactDTO
                               {
                                   CompanyId = c.CompanyId,
                                   DepartmentId = c.DepartmentId,
                                   ContactRoleId = c.ContactRoleId,
                                   FirstName = c.FirstName,
                                   LastName = c.LastName,
                                   Phome = c.Phome
                               };

            return Ok(contactRoles);
        }





        /// <summary>
        /// Get company contact details
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(ContactDTO))]
        public IActionResult Details(int id)
        {
            var foundContact = _db.Contacts.Find(id);
            if (foundContact == null)
            {
                return NoContent();
            }

            //map and return DTO
            ContactDTO contactDTO = new ContactDTO();

            contactDTO.CompanyId = foundContact.CompanyId;
            contactDTO.DepartmentId = foundContact.DepartmentId;
            contactDTO.ContactRoleId = foundContact.ContactRoleId;
            contactDTO.FirstName = foundContact.FirstName;
            contactDTO.LastName = foundContact.LastName;
            contactDTO.Phome = foundContact.Phome;

            return Ok(contactDTO);
        }

    }
}
