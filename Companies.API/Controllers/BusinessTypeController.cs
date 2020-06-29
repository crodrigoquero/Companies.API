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
    public class BusinessTypeController : ControllerBase
    {

        private ApplicationDbContext _db;

        public BusinessTypeController(ApplicationDbContext db)
        {
            _db = db;
        }


        /// <summary>
        /// Adds a new Company Business Type
        /// </summary>
        /// <param name="businessType"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Post(BusinessType businessType)
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

            var businessTypeObj = new BusinessType()
            {
                Description = businessType.Description
                //UserId = user.Id,
            };
            _db.BusinessTypes.Add(businessTypeObj);
            _db.SaveChanges();

            return Ok(new { vehicleId = businessTypeObj.Id, message = "Company Added Successfully" });
        }




        /// <summary>
        /// 
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        [HttpGet("[action]")]
        [ProducesResponseType(200, Type = typeof(List<BusinessTypeDTO>))]
        public IActionResult Search(string search)
        {
            var BusinessTypes = from b in _db.BusinessTypes
                           where b.Description.StartsWith(search)
                           select new BusinessTypeDTO
                           {
                               Id = b.Id,
                               Description = b.Description,
                           };

            return Ok(BusinessTypes);
        }


        /// <summary>
        /// Get All the possible business types
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        [ProducesResponseType(200, Type = typeof(List<BusinessTypeDTO>))]
        public IActionResult List()
        {

            var BusinessTypes = from b in _db.BusinessTypes
                                select new BusinessTypeDTO
                                {
                                    Id = b.Id,
                                    Description = b.Description,
                                };

            return Ok(BusinessTypes);
        }




        /// <summary>
        /// Get details for a given business type
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(BusinessTypeDTO))]
        public IActionResult Details(int id)
        {
            var foundBusinessType = _db.BusinessTypes.Find(id);
            if (foundBusinessType == null)
            {
                return NoContent();
            }

            //map and return DTO
            BusinessTypeDTO businessTypeDTO = new BusinessTypeDTO();
            businessTypeDTO.Description = foundBusinessType.Description;

            return Ok(businessTypeDTO);
        }



    }
}
