using Domain;
using Infrastructure;
using Infrastructure.RepositoryInterfaces;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.Serialization.Formatters.Binary;

namespace WebApp.Controllers
{
    [Route("/api/[controller]")]
    public class EnclosureController : Controller
    {
        public static EnclosureRepository repo;

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            Enclosure enclosure = repo.GetEnclosure(id);

            if (enclosure == null)
            {
                return NotFound("Нет вольера с таким Id");
            }
            enclosure.Id = id;

            return Ok(enclosure);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Enclosure enclosure = repo.GetEnclosure(id);
            if (enclosure == null)
            {
                return Ok("Нет вольера с таким Id");
            }
            repo.DeleteEnclosure(id);

            return Ok();
        }

        [HttpPost]
        public IActionResult Post(Enclosure enclosure)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            ObjectsCounter.Upgrade();

            enclosure.Id = ObjectsCounter.Count;
            enclosure.AnimalsCount = 0;
            enclosure.Animals = new List<Animal>();
            if (enclosure.Type.Contains('-') || enclosure.Type.Contains('_') || enclosure.Type.Contains(';') || enclosure.Type.Contains(' '))
            {
                return Ok("Данные содержат запрещённые символы");
            }
            if (enclosure.Size < 1 || enclosure.Capacity < 1)
            {
                return Ok("Size и Capacity должны быть не меньше 1");
            }
            repo.AddEnclosure(enclosure);
            return CreatedAtAction(nameof(Get), new { id = enclosure.Id }, enclosure);
        }

        [HttpPost("AddEnclosure")]
        public IActionResult PostBody([FromBody] Enclosure enclosure)
        {
            return Post(enclosure);
        }

        [HttpPut]
        public IActionResult Put(int enclosureId, int animalId)
        {
            if (enclosureId < 1 || animalId < 1)
            {
                return Ok("Значения Id должны быть больше 1");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var storedEnclosure = repo.GetEnclosure(enclosureId);
            if (storedEnclosure == null)
            {
                return NotFound("Нет вольера с таким Id");
            }
            storedEnclosure.Id = enclosureId;
            Animal animal = AnimalController.repo.GetAnimal(animalId);
            if (animal == null)
            {
                return NotFound("Нет животного с таким Id");
            }
            animal.Id = animalId;
            if (storedEnclosure.Animals.Count(anim => anim.Id == animalId) == 1)
            {
                storedEnclosure.Animals.RemoveAll(anim => anim.Id == animalId);
                --storedEnclosure.AnimalsCount;
            } else
            {
                storedEnclosure.Animals.Add(animal);
                ++storedEnclosure.AnimalsCount;
            }
            repo.ChangeEnclosure(enclosureId, storedEnclosure);
            return Ok(storedEnclosure);
        }
    }
}
