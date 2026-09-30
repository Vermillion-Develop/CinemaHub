using CinemaHubBackground.Data;
using CinemaHubShared.Models;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;

namespace CinemaHubBackground.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class FilmsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public FilmsController(AppDbContext context)
        {
            _context = context;
        }


        [HttpGet]

        public async Task<ActionResult<IEnumerable<SeansDTO>>> GetSeansByDate([FromQuery] DateOnly date)
        {
            var filmList = await (from s in _context.Seanses join f in _context.Films on s.FK_Film equals f.Id 
                                  join c in _context.Countries on f.FK_Country_Version equals c.Id
                                  join l in _context.AdultRatings on f.FK_Adult_Rating equals l.Id
                                  where s.Day == date select new SeansDTO()
                                  {
                                      Id = s.Id,
                                      FK_Film = s.FK_Film,
                                      FK_Zal = s.FK_Zal,
                                      Date_start = s.Date_start,
                                      Date_end = s.Date_end,
                                      Day = s.Day,

                                      FilmName = f.Name,
                                      FilmDescription = f.Description,
                                      FilmAdultRate = l.Name,
                                      FilmVersion = c.Name

                                  }).ToListAsync();
            return Ok(filmList);
        }
        
    }
}
