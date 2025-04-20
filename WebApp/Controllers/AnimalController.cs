using Domain;
using Infrastructure;
using Infrastructure.RepositoryImplementations;
using Infrastructure.RepositoryInterfaces;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.Serialization.Formatters.Binary;

namespace WebApp.Controllers
{
    [Route("/api/[controller]")]
    public class AnimalController : Controller
    {
        public static AnimalRepository repo;

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            Animal animal = new Animal();
            animal = repo.GetAnimal(id);
            if (animal == null) 
            { 
                return NotFound("Нет животного с таким Id");
            }
            animal.Id = id;

            return Ok(animal);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Animal animal = repo.GetAnimal(id);
            if (animal == null)
            {
                return Ok("Нет животного с таким Id");
            }
            repo.DeleteAnimal(id);
            EnclosureController.repo.DeleteFromAllEnclosures(id);

            return Ok();
        }

        [HttpPost]
        public IActionResult Post(Animal animal)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            ObjectsCounter.Upgrade();

            animal.Id = ObjectsCounter.Count;
            string s = animal.Kind + animal.Name + animal.FavouriteFood;
            if (s.Contains('-') || s.Contains(';') || s.Contains('_') || s.Contains(' '))
            {
                return Ok("Данные содержат запрещённые символы");
            }
            if (animal.Sex != "Male" && animal.Sex != "Female")
            {
                return Ok("В качестве пола нужно указать Male или Female");
            }
            if (animal.HealthStatus != "Healthy" && animal.HealthStatus != "Ill")
            {
                return Ok("В качестве состояния здоровья нужно указать Healthy или Ill");
            }
            repo.AddAnimal(animal);
            return CreatedAtAction(nameof(Get), new { id = animal.Id }, animal);
        }

        [HttpPost("AddAnimal")]
        public IActionResult PostBody([FromBody] Animal animal)
        {
            return Post(animal);
        }
    }
}
