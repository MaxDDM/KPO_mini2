using Domain;
using Infrastructure.RepositoryInterfaces;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.Serialization.Formatters.Binary;

namespace WebApp.Controllers
{
    [Route("/api/[controller]")]
    public class FeedingController : Controller
    {
        public static FeedingRepository repo;

        [HttpGet]
        public IActionResult Get()
        {
            List<Feeding> feedings = repo.GetAllFeedings();

            if (feedings == null)
            {
                return NotFound();
            }

            return Ok(feedings);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Feeding feeding = repo.GetFeeding(id);
            if (feeding == null)
            {
                return NotFound("Нет кормления с таким Id");
            }
            repo.DeleteFeeding(id);

            return Ok();
        }

        [HttpPost]
        public IActionResult Post(Feeding feeding)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Animal animal = AnimalController.repo.GetAnimal(feeding.AnimalId); ;
            if (animal == null)
            {
                return Ok("Нет животного с таким Id");
            }
            if (!TimeOnly.TryParse(feeding.Time, out var time))
            {
                return Ok("Введите время в формате hh:mm");
            }
            if (feeding.TypeOfFood.Contains('-') || feeding.TypeOfFood.Contains('_') || feeding.TypeOfFood.Contains(';') || feeding.TypeOfFood.Contains(' '))
            {
                return Ok("Данные содержат запрещённые символы");
            }
            ObjectsCounter.Upgrade();

            feeding.Id = ObjectsCounter.Count;
            repo.AddFeeding(feeding);
            return CreatedAtAction(nameof(Get), new { id = feeding.Id }, feeding);
        }

        [HttpPost("AddFeeding")]
        public IActionResult PostBody([FromBody] Feeding schedule)
        {
            return Post(schedule);
        }
    }
}
