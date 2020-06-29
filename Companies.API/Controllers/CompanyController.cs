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
    public class CompanyController : ControllerBase
    {

        private ApplicationDbContext _db;

        public CompanyController(ApplicationDbContext db)
        {
            _db = db;
        }


        /// <summary>
        /// Adds a new Vehicle
        /// </summary>
        /// <param name="company"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Post(Company company)
        {
            var userEmail = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email).Value;
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value; //USER ID RETRIEVAL (FROM TOKEN) !!!!!

            //HOUSTON, TNENEMOS UN PROBLEMA: la tbala de usuarios esta vacia en esta API,
            //porque nos hemos autentificado en otra API.
            //var user = _db.Users.FirstOrDefault(u => u.Email == userEmail);
            //if (user == null)
            //{
            //    return NotFound();
            //}

            var companyObj = new Company()
            {
                Name = company.Name,
                Street = company.Street,
                Street2 = company.Street2,
                HouseNumber = company.HouseNumber,
                City = company.City,
                County = company.County,
                State = company.State,
                Province = company.Province,
                Region = company.Region,
                Country = company.Country,
                Postcode = company.Postcode,
                CompanySiteUrl1 = company.CompanySiteUrl1,
                CompanySiteUrl2 = company.CompanySiteUrl2,
                CompanySiteUrl3 = company.CompanySiteUrl3,
                LinkedIn = company.LinkedIn,
                Twitter = company.Twitter,
                Facebook = company.Facebook,
                CompanyRelationshipTypeId = company.CompanyRelationshipTypeId,
                BusinessTypeId = company.BusinessTypeId
                //UserId = user.Id,
            };
            _db.Companies.Add(companyObj);
            _db.SaveChanges();

            return Ok(new { vehicleId = companyObj.Id, message = "Company Added Successfully" });
        }





        [HttpGet("[action]")]
        public IActionResult Search(string search)
        {
            var vehicles = from c in _db.Companies
                           where c.Name.StartsWith(search)
                           select new
                           {
                               Id = c.Id,
                               Title = c.Name,
                           };

            return Ok(vehicles);
        }

        /// <summary>
        /// Get all the Companies from a given relationship type (providers, clients, etc)
        /// </summary>
        /// <param name="companyRelationshipTypeId"></param>
        /// <returns></returns>
        [HttpGet("[action]")]
        [ProducesResponseType(200, Type = typeof(List<Company>))]
        [ProducesResponseType(404)]
        public IActionResult SearchByRelationship(int companyRelationshipTypeId)
        {
            var companies = from c in _db.Companies
                           where c.CompanyRelationshipTypeId == companyRelationshipTypeId
                            select new
                           {
                               Id = c.Id,
                               Name = c.Name
                           };

            if (companies == null) return NotFound();
            return Ok(companies);
        }


        /// <summary>
        /// Get All Company Contacts
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        [ProducesResponseType(200, Type = typeof(List<ContactDTO>))]
        public IActionResult Contacts(int companyId)
        {

            var companyContacts = from c in _db.Contacts
                                  where c.CompanyId == companyId
                                  select new ContactDTO
                                  {
                                      Id = c.Id,
                                      LastName = c.LastName,
                                  };

            return Ok(companyContacts);
        }


        /// <summary>
        /// Get details for a given company
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(Company))]
        public IActionResult Details(int id)
        {
            var foundCompany = _db.Companies.Find(id);
            if (foundCompany == null)
            {
                return NoContent();
            }

            var company = from c in _db.Companies
                          //join u in _db.Users on v.UserId equals u.Id
                          where c.Id == id
                          select new
                          {
                              Id = c.Id,
                              Name = c.Name,
                              BusinessTypeId = c.BusinessTypeId,
                              CompanyRelationshipTypeId = c.CompanyRelationshipTypeId
                          };

            return Ok(company);
        }

    }
}
