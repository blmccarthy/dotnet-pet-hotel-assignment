using System.Net.NetworkInformation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using pet_hotel.Models;
using Microsoft.EntityFrameworkCore;

namespace pet_hotel.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PetsController : ControllerBase
    {
        private readonly ApplicationContext _context;
        public PetsController(ApplicationContext context) {
            _context = context;
        }

        // --- Get All Pets ----------------------------------------------------
        [HttpGet] 
        public IEnumerable<Pet> GetPets() 
        {
            Console.WriteLine("Get All Pets!");
            return _context.Pets;
        }

        // --- Get Pet by ID ---------------------------------------------------
        [HttpGet("{id}")]
        public ActionResult<PetOwner> GetById(int id) 
        {
            Console.WriteLine("get one pet");
            PetOwner pet = _context.Pets
                .SingleOrDefault(pet => pet.id == id); 

            if(pet is null) {
                return NotFound(); // res.sendStatus(404)
            }

            return pet;
        }

        // --- Post New Pet ----------------------------------------------------
        [HttpPost]
        public IActionResult Post(Pet pet)
        {
            _context.Add(pet); // This posts the pet to the database
            _context.SaveChanges(); // This commits the change
            
            return CreatedAtAction(nameof(Post), new { id = pet.id }, pet); // This returns the added pet's information
        }

        // --- Delete Pet ------------------------------------------------------
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Pet pet = _context.Pets.SingleOrDefault(p => p.id == id); // I believe this checks if the pet exists or not

            if(pet is null){
                return NotFound(); // If the pet doesn't exist, it returns 404 Not Found error
            }
            
            _context.Pets.Remove(pet); // This deletes the pet
            _context.SaveChanges(); // This commits the change

            // 204
            return NoContent(); // This returns a confirmation of deletion
        }
        
        // --- Update Pet ------------------------------------------------------
        [HttpPut("{id}")]
        public IActionResult Put(int id, Pet pet)
        {
            Console.WriteLine("in PUT pet");
            if (id != pet.id)
            {
                return BadRequest();
            }
            // update in DB
            _context.Update(pet);
            _context.SaveChanges();
            return NoContent();
        }







        // This is just a stub for GET / to prevent any weird frontend errors that 
        // occur when the route is missing in this controller
        
        // [HttpGet]
        // public IEnumerable<Pet> GetPets() {
        //     return new List<Pet>();
        // }

        // [HttpGet]
        // [Route("test")]
        // public IEnumerable<Pet> GetPets() {
        //     PetOwner blaine = new PetOwner{
        //         name = "Blaine"
        //     };

        //     Pet newPet1 = new Pet {
        //         name = "Big Dog",
        //         petOwner = blaine,
        //         color = PetColorType.Black,
        //         breed = PetBreedType.Poodle,
        //     };

        //     Pet newPet2 = new Pet {
        //         name = "Little Dog",
        //         petOwner = blaine,
        //         color = PetColorType.Golden,
        //         breed = PetBreedType.Labrador,
        //     };

        //     return new List<Pet>{ newPet1, newPet2};
        // }
    }
}
