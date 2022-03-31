using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using pet_hotel.Models;
using Microsoft.EntityFrameworkCore;

namespace pet_hotel.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PetOwnersController : ControllerBase
    {
        private readonly ApplicationContext _context;
        public PetOwnersController(ApplicationContext context)
        {
            _context = context;
        }

        // This is just a stub for GET / to prevent any weird frontend errors that 
        // occur when the route is missing in this controller
        [HttpGet]
        public IEnumerable<PetOwner> GetPetOwners()
        { //GetPetOwners is taco
            Console.WriteLine("get all bakers");
            // no SQL
            return _context.PetOwners;
        }

        [HttpPost]
        public IActionResult Post(PetOwner petOwner)
        {
            _context.Add(petOwner);
            _context.SaveChanges();

            return CreatedAtAction(nameof(Post), new { id = petOwner.id }, petOwner);
        }


        [HttpGet("{id}")]
        public ActionResult<PetOwner> GetById(int id) 
        {
            Console.WriteLine("get one pet owner");
            PetOwner petOwner = _context.PetOwners
                .SingleOrDefault(petOwner => petOwner.id == id); 

            if(petOwner is null) {
                return NotFound(); // res.sendStatus(404)
            }

            return petOwner;
        }


           [HttpDelete("{id}")]
            public IActionResult Delete(int id)
        {
            PetOwner petOwner = _context.PetOwners.SingleOrDefault(p => p.id == id); // I believe this checks if the pet exists or not

            if(petOwner is null){
                return NotFound();      // If the pet doesn't exist, it returns 404 Not Found error
            }
            
            _context.PetOwners.Remove(petOwner);  // This deletes the pet
            _context.SaveChanges();     // This commits the change

            // 204
            return NoContent();         // This returns a confirmation of deletion
        }

            [HttpPut("{id}")]
            public IActionResult Put(int id, PetOwner petOwner)
        {
            Console.WriteLine("in PUT petOwner");
            if (id != petOwner.id)
            {
                return BadRequest();
            }
            // update in DB
            _context.Update(petOwner);       // Updates pet
            _context.SaveChanges();     // Saves pet
            return NoContent();
        }


    }
}
