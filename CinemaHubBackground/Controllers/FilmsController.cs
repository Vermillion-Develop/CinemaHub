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
                                      FilmVersion = c.Name,
                                      FilmHashedImg = f.Image,
                                      FilmRate = f.Rating

                                  }).ToListAsync();
            return Ok(filmList);
        }
        [HttpPost("actors")]
        public async Task<ActionResult<IEnumerable<Actor_in_filmDTO>>> GetActorsInSelectedFilm([FromBody] SeansDTO? selectedFilm)
        {
            if(selectedFilm == null)
            {
                return BadRequest();
            }

            var actorsFilms = await (from actLst in _context.ActorsInFilm
                                      join act in _context.Actors on actLst.FK_Actor equals act.Id
                                     where actLst.FK_Film == selectedFilm.FK_Film
                                      select new Actor_in_filmDTO()
                                      {
                                          Id = actLst.Id,
                                          FK_Film = actLst.FK_Film,
                                          FK_Actor = actLst.FK_Actor,

                                          Actor_image = act.Actor_image,
                                          ActorFamily = act.Family,
                                          ActorName = act.Name,
                                          ActorFather = act.Father

                                      }).ToListAsync();
            return Ok(actorsFilms);
        }

        [HttpPost("rewards")]
        public async Task<ActionResult<IEnumerable<Rewards_in_filmDTO>>> GetRewardsInSelectedFilm([FromBody] SeansDTO? selectedFilm)
        {
            if(selectedFilm == null)
            {
                return BadRequest();
            }

            var rewardList = await (from awardList in _context.RewardsInFilm
                                    join rew in _context.Rewards on awardList.FK_Reward equals rew.Id
                                    join mov in _context.Films on awardList.FK_Film equals mov.Id
                                    where
                                    awardList.FK_Film == selectedFilm.FK_Film
                                    select new Rewards_in_filmDTO()
                                    {
                                        Id = awardList.Id,
                                        FK_Film = awardList.FK_Film,
                                        FK_Reward = awardList.FK_Reward,
                                        Year = awardList.Year,

                                        NameReward = rew.Name,
                                        RewardImage = rew.Reward_image

                                    }).ToListAsync();
            return Ok(rewardList);
        }

        [HttpPost("comments")]
        public async Task<ActionResult<IEnumerable<Comments_in_filmDTO>>> GetCommentsInSelectedFilm([FromBody] SeansDTO? selectedFilm)
        {

            if (selectedFilm == null)
            {
                return BadRequest();
            }

            var commentList = await (from comlist in _context.CommentsInFilm
                                     join z in _context.Comments on comlist.FK_Comment equals z.Id
                                     join fil in _context.Films on comlist.FK_Film equals fil.Id
                                     join usr in _context.Users on comlist.FK_User equals usr.Id
                                     join rl in _context.Roles on usr.FK_Role equals rl.Id
                                     where comlist.FK_Film == selectedFilm.FK_Film
                                     select new Comments_in_filmDTO()
                                     {
                                         Id = comlist.Id,
                                         FK_Film = comlist.FK_Film,
                                         FK_Comment = comlist.FK_Comment,
                                         FK_User = comlist.FK_User,

                                         UserFamily = usr.Family,
                                         UserName = usr.Name,
                                         UserRole = rl.Name,
                                         CommentDescription = z.Description,
                                         CommentDate = z.CommentDate

                                     }).ToListAsync();
            return Ok(commentList);
        }

        [HttpPost("rates")]
        public async Task<IActionResult> AddNewRate([FromBody] RateUserRequest request)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var newRate = new Rates_Film
                {
                    FK_Film = request.FilmId,
                    FK_User = request.UserId,
                    Rates = request.UserRate
                };
                _context.RatesFilm.Add(newRate);
                await _context.SaveChangesAsync();

                await transaction.CommitAsync();

                return Ok(new { message = "Оценка принята!" });
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return StatusCode(500, new { message = $"Ошибка на сервере: {ex.Message}" });
            }
        }
    }
}
