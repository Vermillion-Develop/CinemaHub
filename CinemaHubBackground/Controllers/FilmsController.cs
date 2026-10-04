using CinemaHubBackground.Data;
using CinemaHubShared.Models;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;

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


        [HttpPost("getFilms")]

        public async Task<ActionResult<IEnumerable<SeansDTO>>> GetSeansByDate([FromBody] FilmRequest request)
        {
            if (request == null) 
            {
                Debug.WriteLine("Хуйня реквест");
                return BadRequest();
            }
            Debug.WriteLine("Реквест не хуйня");
            var filmList = await (from s in _context.Seanses join f in _context.Films on s.FK_Film equals f.Id
                                  join c in _context.Countries on f.FK_Country_Version equals c.Id
                                  join l in _context.AdultRatings on f.FK_Adult_Rating equals l.Id
                                  where s.Day == request.DateFilm && s.FK_Zal == request.ZalId select new SeansDTO()
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
            if (selectedFilm == null)
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
            if (selectedFilm == null)
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

        [HttpGet("ticketType")]

        public async Task<ActionResult<IEnumerable<Ticket_type>>> GetTicketsTypes()
        {
           var ticketTypeList = await (from s in _context.Ticket_types select s).ToListAsync();
           return Ok(ticketTypeList);
        }

        [HttpPost("commentaries")]
        public async Task<IActionResult> AddNewComment([FromBody] CommentUserRequest? commentUserRequest)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var comment = new Comment
                {
                    Description = commentUserRequest?.CommentDescription,
                    CommentDate = commentUserRequest?.CommentDate
                };
                _context.Comments.Add(comment);
                await _context.SaveChangesAsync();

                var commentsInFilm = new Comments_in_film
                {
                    FK_Film = commentUserRequest?.FilmId,
                    FK_Comment = comment.Id,
                    FK_User = commentUserRequest?.UserId,
                };
                _context.CommentsInFilm.Add(commentsInFilm);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return Ok(new { message = "Комментарий оставлен!" });

            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return StatusCode(500, new { message = $"Ошибка на сервере: {ex.Message}" });
            }
        }

        [HttpGet("zals")]

        public async Task<ActionResult<IEnumerable<Zal>>> GetZals()
        {
            var zalsList = await (from z in _context.Zals select z).ToListAsync();
            return Ok(zalsList);
        }

        [HttpPost("mestos")]
        public async Task<ActionResult<IEnumerable<Mesta_in_zalDTO>>> GetMesta([FromBody] Zal? selectedZal) 
        {
            if(selectedZal == null)
            {
                return BadRequest();
            }

            var mestList = await (from z in _context.MestosInZals
                                  join
                                  m in _context.Mestos on z.FK_Mesto equals m.Id
                                  join
                                  x in _context.Zals on z.FK_Zal equals x.Id
                                  join
                                  s in _context.MestoStatuses on z.FK_Status_mesto equals s.Id
                                  join
                                  r in _context.MestoRowes on m.FK_Row equals r.Id
                                  where z.FK_Zal == selectedZal.Id
                                  select new Mesta_in_zalDTO()
                                  {
                                    Id = z.Id,
                                    FK_Mesto = z.FK_Mesto,
                                    FK_Zal = z.FK_Zal,

                                    MestoName = r.Name,
                                    MestoStatus = s.Name

                                  }).ToListAsync();
            return Ok(mestList);
        }

        [HttpPost("buyTicket")]
        public async Task<IActionResult> AddNewTicketUser([FromBody] BuyTicketRequest? ticketRequest)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var ticket = new Ticket
                {
                    Name = ticketRequest?.NameTicket,
                    Cost = ticketRequest?.CostTicket,
                    FK_Type_ticket = ticketRequest?.FK_Type_ticket,
                    FK_Zal = ticketRequest?.FK_Zal,
                    FK_Ticket_status = ticketRequest?.FK_Ticket_status,
                    Date_sold = ticketRequest?.DateSold,
                    FK_Seans = ticketRequest?.FK_Seans,
                };
                _context.Tickets.Add(ticket);
                await _context.SaveChangesAsync();
                Debug.WriteLine("Билет сохранен");
                var user_ticket = new User_tickets
                {
                    FK_User = ticketRequest?.UserId,
                    FK_Ticket = ticket.Id
                };                
                _context.UserTickets.Add(user_ticket);
                await _context.SaveChangesAsync();
                Debug.WriteLine("Юзер сохранен");
                var mestoInZal = await _context.MestosInZals.FirstOrDefaultAsync(x => x.FK_Mesto == ticketRequest.Mesto);
                if(mestoInZal == null)
                {
                    await transaction.RollbackAsync();
                    return BadRequest(new { message = "Указанное место не найдено в зале" });
                }

                mestoInZal.FK_Status_mesto = 1;
                await _context.SaveChangesAsync();
                Debug.WriteLine("Статус сохранен");
                await transaction.CommitAsync();

                return Ok(new { message = "Билет куплен>" });

            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return StatusCode(500, new { message = $"Ошибка на сервере: {ex.Message}" });
            }
        }

        [HttpGet("userBilets")]
        public async Task<ActionResult<IEnumerable<User_ticketsDTO>>> GetUserTickets([FromQuery]int? user)
        {
            var zalsList = await (from usrTic in _context.UserTickets
                                  join
                                  t in _context.Tickets on usrTic.FK_Ticket equals t.Id
                                  join
                                  tb in _context.Ticket_types on t.FK_Type_ticket equals tb.Id
                                  join 
                                  st in _context.StatusTickets on t.FK_Ticket_status equals st.Id
                                  join
                                  usr in _context.Users on usrTic.FK_User equals usr.Id
                                  join
                                  z in _context.Zals on t.FK_Zal equals z.Id
                                  where usrTic.FK_User == user
                                  select new User_ticketsDTO()
                                  {
                                      Id = usrTic.Id,
                                      FK_User = usrTic.FK_User,
                                      FK_Ticket = usrTic.FK_Ticket,

                                      Family = usr.Family,
                                      Name = usr.Name,
                                      Father = usr.Father,

                                      TypeBilet = tb.Name,
                                      CostBilet = t.Cost,
                                      DateBiletBuyed = t.Date_sold,
                                      NameBilet = t.Name,
                                      TicketZal = z.Name,
                                      BiletStatus = st.Name
                                  }).ToListAsync();
            return Ok(zalsList);
        }

    }  
}
