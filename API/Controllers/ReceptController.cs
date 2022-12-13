using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using API.Models;

namespace API.Controllers
{
    //[Route("api/[controller]")]
    [ApiController]
    public class ReceptController : ControllerBase
    {
        ReceptContext context = new();

        [HttpGet]
        [Route("fogasok/all")]
        public IActionResult MindenFogas()
        {
            var fogasok = from x in context.Fogasok
                          select x;

            return Ok(fogasok.ToList());
        }

        [HttpGet]
        [Route("fogasok/{i}")]
        public IActionResult KonkretFogas(int i)
        {
            var fogas = from x in context.Fogasok
                          where x.FogasId == i
                          select x;

            return Ok(fogas.ToList());
        }

        [HttpGet]
        [Route("nyersanyagok/{i}")]
        public IActionResult KonkretNyersanyag(int i)
        {
            var nyersanyag = from x in context.Nyersanyagok
                        where x.NyersanyagId == i
                        select x;

            return Ok(nyersanyag.ToList());
        }
    }
}
