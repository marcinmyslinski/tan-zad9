using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using WebApplication4.Models;
using WebApplication4.Models.DTOs.Requests;
using WebApplication4.Models.DTOs.Responses;
using WebApplication4.Services;

namespace WebApplication4.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientsController : ControllerBase
    {
        private pd3809Context _context;

        public ClientsController(pd3809Context context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetClients()
        {
            var context = new pd3809Context();
            var client = context.Clients
                         .Select(c => new
                         {
                             cos = c.LastName
                         });
            return Ok(client);
           
        }
        /// <summary>
        /// Tutaj pisać
        /// </summary>
        /// <returns></returns>
        ///   Name = t.Name,
        //Description = t.Description,
        //                                  DateFrom = t.DateFrom,
        //                                  DoteTo = t.DateTo,
        //                                  MaxPeople = t.MaxPeople,
        //                                  Country = t.

        [HttpGet("trips")]
        public async Task<IActionResult> GetData()
        {
            var context = new pd3809Context();         


            var result = context.Trips
                            .Select(t => new
                            {
                                Name = t.Name,
                                Description = t.Description,
                                DateFrom = t.DateFrom,
                                DoteTo = t.DateTo,
                                MaxPeople = t.MaxPeople,
                                Destination = t.CountryTrips.ToList(),
                                Clients = t.ClientTrips.ToList()
                            }).ToList()
                            .OrderByDescending(t =>t.DateFrom);


            return Ok(result);
        }

        [HttpDelete("clients/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var context = new pd3809Context();
            var check = context.ClientTrips
                           
                            .Where(ct => ct.IdClient.Equals(id));

            if (!check.Any())
            {
                var client = context.Clients.First(c => c.IdClient == id);

                context.Clients.Remove(client);
                context.SaveChanges();
            }
            else
            {
                return BadRequest();
                
            }
                
            


            return Ok();
        }

        [HttpPost]
        public IActionResult CreateClientAndTrip(CreateClientAndTripRequestDto request)
        {

            var client = new Client
            {
                LastName = request.LastName
            };
            var trip = new Trip
            {
                Name = request.TripName
            };

            return Ok();
        }


    }
}
