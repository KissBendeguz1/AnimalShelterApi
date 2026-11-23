using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShelterApi.Models;

namespace ShelterApi.Controllers
{
    [Route("/queries")]
    [ApiController]
    public class QueriesController : ControllerBase
    {
        private readonly AnimalshelterContext _context;

        public QueriesController(AnimalshelterContext context)
        {
            _context = context;
        }
        [HttpGet("allSheltersWithAnimals")]
        public async Task<IActionResult> AllSheltersWithAnimals()
        {
            var shelters = await _context.Shelters.Include(s => s.Animals).ToListAsync();

            var sheltersAnimals = shelters.Select(s => new
            {
                s.Id,
                s.Name,
                s.City,
                s.Capacity,
                s.RegTime,
                Animals = s.Animals.Select(a => new
                {
                    a.Id,
                    a.Name,
                    a.Species,
                    a.Age,
                    a.Weight,
                    a.RegTime
                })
            });

            return StatusCode(200, sheltersAnimals);
        }


        [HttpGet("allAnimalsWithShelter")]
        public async Task<IActionResult> AllAnimalsWithShelter()
        {
            var animals = await _context.Animals
                .Join(
                    _context.Shelters,
                    animal => animal.ShelterId,
                    shelter => shelter.Id,
                    (animal, shelter) => new
                    {
                        animal.Id,
                        animal.Name,
                        animal.Species,
                        animal.Age,
                        animal.Weight,
                        animal.RegTime,
                        ShelterName = shelter.Name
                    })
                .ToListAsync();

            return StatusCode(200, animals);
        }

        [HttpGet("animalsOfShelter")]
        public async Task<IActionResult> AnimalsOfShelter([FromQuery] Guid shelterId)
        {
            var shelter = await _context.Shelters
                .Include(s => s.Animals)
                .Where(s => s.Id == shelterId)
                .FirstOrDefaultAsync();

            if (shelter == null)
            {
                return StatusCode(404, "Nincs ilyen menhely" );
            }

            var result = new
            {
                shelter.Id,
                shelter.Name,
                shelter.City,
                shelter.Capacity,
                shelter.RegTime,
                Animals = shelter.Animals.Select(a => new
                {
                    a.Id,
                    a.Name,
                    a.Species,
                    a.Age,
                    a.Weight,
                    a.RegTime
                })
            };

            return StatusCode(200, result);
        }

        [HttpGet("animalCountByShelter")]
        public async Task<IActionResult> AnimalCountByShelter()
        {
            var counts = await _context.Shelters
                .Select(s => new
                {
                    ShelterName = s.Name,
                    AnimalCount = s.Animals.Count()
                })
                .OrderByDescending(x => x.AnimalCount)
                .ToListAsync();

            return StatusCode(200, counts);
        }
    }
}


