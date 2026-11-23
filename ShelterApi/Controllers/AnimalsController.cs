using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using ShelterApi.Controllers.DTOs;
using ShelterApi.Models;
using System.Data;

namespace ShelterApi.Controllers
{
    [Route("/animals")]
    [ApiController]
    public class AnimalsController : ControllerBase
    {

        private readonly AnimalshelterContext _context;

        public AnimalsController(AnimalshelterContext context)
        {
            _context = context;
        }

        [HttpPost("newAnimal")]
        public async Task<IActionResult> CreateAnimal(AddAnimalDto addAnimalDto) {
            try
            {
                var Animal = new Animal
                {
                    Id = Guid.NewGuid(),
                    Name = addAnimalDto.Name,
                    Species = addAnimalDto.Species,
                    Age = addAnimalDto.Age,
                    Weight = addAnimalDto.Weight,
                    ShelterId = addAnimalDto.ShelterId,
                    RegTime = DateTime.Now
                };
                if (Animal.ShelterId != null)
                {
                    var mhely = await _context.Shelters.FindAsync(Animal.ShelterId);
                    if (mhely == null)
                    {
                        return StatusCode(404, "Nics ilyen menhely");
                    }
                    else
                    {
                        await _context.Animals.AddAsync(Animal);
                        await _context.SaveChangesAsync();
                        return StatusCode(201, Animal);
                    }
                }
                return StatusCode(404, Animal);
            }
            catch (Exception e)
            {

                return StatusCode(400, "Valami itt nem megfelelő" );
            }
        }

        [HttpGet("AllAnimal")]
            
        public async Task<IActionResult> GetAllAnimals() {
            var animals = await _context.Animals.ToListAsync();
            if (animals != null || animals.Count >0)
            {

                return StatusCode(200, animals);
            }
            else
            {
                return StatusCode(400, animals);
            }
        }

        [HttpGet("byId")]
        public async Task<IActionResult> GetAnimalById([FromQuery] Guid id)
        {
            var animal = await _context.Animals.FindAsync(id);
            if (animal == null)
            {
                return StatusCode(404, "Nincs ilyen állat");
            }
            return StatusCode(200, animal);
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateAnimal([FromQuery] Guid id, [FromBody] UpdateAnimalDto updateAnimalDto)
        {
            try
            {
                var animal = await _context.Animals.FindAsync(id);
                if (animal == null)
                {
                    return StatusCode(404, "Nincs ilyen állat");
                }

                var shelter = await _context.Shelters.FindAsync(updateAnimalDto.ShelterId);
                if (shelter == null)
                {
                    return StatusCode(404, "Nincs ilyen menhely" );
                }

                animal.Name = updateAnimalDto.Name;
                animal.Species = updateAnimalDto.Species;
                animal.Age = updateAnimalDto.Age;
                animal.Weight = updateAnimalDto.Weight;
                animal.ShelterId = updateAnimalDto.ShelterId;

                await _context.SaveChangesAsync();
                return StatusCode(200, animal);
            }
            catch (Exception e)
            {
                return StatusCode(400, "Valami itt nem megfelelő hiba: " + e.Message );
            }
        }

        [HttpDelete("deleteAnimal")]
        public async Task<IActionResult> DeleteAnimal([FromQuery] Guid id)
        {
            var animal = await _context.Animals.FindAsync(id);
            if (animal == null)
            {
                return StatusCode(404,"Nincs ilyen állat");
            }

            _context.Animals.Remove(animal);
            await _context.SaveChangesAsync();
            return StatusCode(200,"Törölve");
        }
    }
}
