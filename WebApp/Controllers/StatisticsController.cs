using Domain;
using Infrastructure.RepositoryInterfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
    [Route("/api/[controller]")]
    public class StatisticsController : Controller
    {
        public static StatisticsRepository repo;

        [HttpGet("info")]
        public IActionResult Get(string info)
        {
            if (info == "count")
            {
                int count = repo.GetAnimalCount();

                return Ok(count);
            } else if (info == "free")
            {
                List<int> freeEnclosures = repo.GetFreeEnclosures();

                return Ok(freeEnclosures);
            }
            return NotFound("Доступны запросы count и free");
        }
    }
}
