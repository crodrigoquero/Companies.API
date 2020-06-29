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
    public class RelationshipController : Controller
    {
        private ApplicationDbContext _db;

        public RelationshipController(ApplicationDbContext db)
        {
            _db = db;
        }


        /// <summary>
        /// Adds a new Contact Role 
        /// </summary>
        /// <param name="relationshipType"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Post(RelationshipType relationshipType)
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

            var relationshipTypetRoleObj = new ContactRole()
            {
                Description = relationshipType.Description
                //UserId = user.Id,
            };
            _db.ContactRoles.Add(relationshipTypetRoleObj);
            _db.SaveChanges();

            return Ok(new { vehicleId = relationshipTypetRoleObj.Id, message = "Company Relationship Added Successfully" });
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        [HttpGet("[action]")]
        [ProducesResponseType(200, Type = typeof(List<RelationshipDTO>))]
        public IActionResult Search(string search)
        {
            var relationships = from r in _db.RelationshipTypes
                               where r.Description.StartsWith(search)
                               select new ContactRoleDTO
                               {
                                   Id = r.Id,
                                   Description = r.Description,
                               };

            return Ok(relationships);
        }


        /// <summary>
        /// Get All the Company Relationships
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        [ProducesResponseType(200, Type = typeof(List<RelationshipDTO>))]
        public IActionResult List()
        {

            var relationships = from c in _db.RelationshipTypes
                               select new ContactRoleDTO
                               {
                                   Id = c.Id,
                                   Description = c.Description,
                               };

            return Ok(relationships);
        }


        /// <summary>
        /// Get details for a given Company Relationship
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(RelationshipDTO))]
        public IActionResult Details(int id)
        {
            var foundRelationship = _db.RelationshipTypes.Find(id);
            if (foundRelationship == null)
            {
                return NoContent();
            }

            //map and return DTO
            RelationshipDTO relationshipDTO = new RelationshipDTO();
            relationshipDTO.Description = foundRelationship.Description;

            return Ok(relationshipDTO);
        }


    }
}
