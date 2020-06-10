using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FjingFjongApi.Models;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;

namespace FjingFjongApi.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class MatchController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public MatchController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Match
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<IEnumerable<MatchDto>>> GetMatches()
        {
            var matchList = await _context.Matches
                .Include(blog => blog.PlayerOne)
                .Include(blog => blog.PlayerTwo)
                .Include(blog => blog.PlayerThree)
                .Include(blog => blog.PlayerFour)
                .ToListAsync();

            var matchDtoList = new List<MatchDto>();

            foreach (var match in matchList)
            {
                matchDtoList.Add(_mapper.Map<MatchDto>(match));
            }

            return Ok(matchDtoList);
        }

        // GET: api/Match/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<MatchDto>> GetMatch(Guid id)
        {
            var match = await _context.Matches
                .Include(blog => blog.PlayerOne)
                .Include(blog => blog.PlayerTwo)
                .Include(blog => blog.PlayerThree)
                .Include(blog => blog.PlayerFour)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (match == null)
            {
                return NotFound();
            }

            var matchDto = _mapper.Map<MatchDto>(match);

            return Ok(matchDto);
        }

        // PUT: api/Match/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> PutMatch(Guid id, MatchUpdateDto matchUpdateDto)
        {
            if (id != matchUpdateDto.Id)
                return BadRequest();

            if (matchUpdateDto.TeamOneScore == null)
                ModelState.AddModelError("TeamOneScore", "Score is required.");

            if (matchUpdateDto.TeamOneScore < 0)
                ModelState.AddModelError("TeamOneScore", "Score cannot be negative.");

            if (matchUpdateDto.TeamTwoScore == null)
                ModelState.AddModelError("TeamTwoScore", "Score is required.");

            if (matchUpdateDto.TeamTwoScore < 0)
                ModelState.AddModelError("TeamTwoScore", "Score cannot be negative.");

            if (matchUpdateDto.PlayerOneId == Guid.Empty)
                ModelState.AddModelError("PlayerOneId", "Id cannot be empty");

            if (matchUpdateDto.PlayerTwoId == Guid.Empty)
                ModelState.AddModelError("PlayerTwoId", "Id cannot be empty");

            if (matchUpdateDto.PlayerThreeId == Guid.Empty)
                ModelState.AddModelError("PlayerThreeId", "Id cannot be empty");

            if (matchUpdateDto.PlayerFourId == Guid.Empty)
                ModelState.AddModelError("PlayerFourId", "Id cannot be empty");

            var list = new List<Guid>();
            list.Add(matchUpdateDto.PlayerOneId);
            list.Add(matchUpdateDto.PlayerTwoId);
            list.Add(matchUpdateDto.PlayerThreeId);
            list.Add(matchUpdateDto.PlayerFourId);

            if (list.Count != list.Distinct().Count())
                ModelState.AddModelError("", "Duplicate players are not allowed.");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            Match match = await _context.Matches.FindAsync(id);

            if (match == null)
            {
                return NotFound();
            }

            match.TeamOneScore = (int)matchUpdateDto.TeamOneScore;
            match.TeamTwoScore = (int)matchUpdateDto.TeamTwoScore;
            match.PlayerOneId = matchUpdateDto.PlayerOneId;
            match.PlayerTwoId = matchUpdateDto.PlayerTwoId;
            match.PlayerThreeId = matchUpdateDto.PlayerThreeId;
            match.PlayerFourId = matchUpdateDto.PlayerFourId;

            _context.Matches.Update(match);

            await _context.SaveChangesAsync();

            await UpdateAllRatings();

            return NoContent();
        }

        // POST: api/Match
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<MatchDto>> PostMatch(MatchCreateDto matchCreateDto)
        {
            if (matchCreateDto.TeamOneScore == null)
                ModelState.AddModelError("TeamOneScore", "Score is required.");

            if (matchCreateDto.TeamOneScore < 0)
                ModelState.AddModelError("TeamOneScore", "Score cannot be negative.");

            if (matchCreateDto.TeamTwoScore == null)
                ModelState.AddModelError("TeamTwoScore", "Score is required.");

            if (matchCreateDto.TeamTwoScore < 0)
                ModelState.AddModelError("TeamTwoScore", "Score cannot be negative.");

            if (matchCreateDto.PlayerOneId == Guid.Empty)
                ModelState.AddModelError("PlayerOneId", "Id cannot be empty");

            if (matchCreateDto.PlayerTwoId == Guid.Empty)
                ModelState.AddModelError("PlayerTwoId", "Id cannot be empty");

            if (matchCreateDto.PlayerThreeId == Guid.Empty)
                ModelState.AddModelError("PlayerThreeId", "Id cannot be empty");

            if (matchCreateDto.PlayerFourId == Guid.Empty)
                ModelState.AddModelError("PlayerFourId", "Id cannot be empty");

            var list = new List<Guid>();
            list.Add(matchCreateDto.PlayerOneId);
            list.Add(matchCreateDto.PlayerTwoId);
            list.Add(matchCreateDto.PlayerThreeId);
            list.Add(matchCreateDto.PlayerFourId);

            if (list.Count != list.Distinct().Count())
                ModelState.AddModelError("", "Duplicate players are not allowed.");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            Match match = _mapper.Map<Match>(matchCreateDto);

            _context.Matches.Add(match);

            await _context.SaveChangesAsync();

            await UpdateRating(match);

            match = await _context.Matches
                .Include(m => m.PlayerOne)
                .Include(m => m.PlayerTwo)
                .Include(m => m.PlayerThree)
                .Include(m => m.PlayerFour)
                .FirstOrDefaultAsync(m => m.Id == match.Id);

            MatchDto matchDto = _mapper.Map<MatchDto>(match);

            return CreatedAtAction(nameof(GetMatch), new { id = match.Id }, matchDto);
        }

        // DELETE: api/Match/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Match>> DeleteMatch(Guid id)
        {
            Match match = await _context.Matches
                .Include(m => m.PlayerOne)
                .Include(m => m.PlayerTwo)
                .Include(m => m.PlayerThree)
                .Include(m => m.PlayerFour)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (match == null)
            {
                return NotFound();
            }

            _context.Matches.Remove(match);

            await _context.SaveChangesAsync();

            await UpdateAllRatings();

            MatchDto matchDto = _mapper.Map<MatchDto>(match);

            return Ok(matchDto);
        }

        private bool MatchExists(Guid id)
        {
            return _context.Matches.Any(e => e.Id == id);
        }

        private async Task UpdateRating(Match match)
        {
            Player playerOne = await _context.Players.FindAsync(match.PlayerOneId);
            Player playerTwo = await _context.Players.FindAsync(match.PlayerTwoId);
            Player playerThree = await _context.Players.FindAsync(match.PlayerThreeId);
            Player playerFour = await _context.Players.FindAsync(match.PlayerFourId);

            /*
             *  Ra - Player one's Rating points
             *  Rb - Player two's Rating points
             *  Rc - Player three's Rating points
             *  Rd - Player four's Rating points
             *  Rab - Team one's average Rating points
             *  Rcd - Team two's average Rating points
             *  Pab - Team one's match score
             *  Pcd - Team two's match score
             *  Pmax - Maximum points scored by a team
             *  Pmin - Minumum points scored by a team
             *  Sab - Team one's actual score
             *  Eab - Team one's expected score
             */

            Double Ra = playerOne.Rating;
            Double Rb = playerTwo.Rating;
            Double Rc = playerThree.Rating;
            Double Rd = playerFour.Rating;
            Double Rab = (Ra + Rb) / 2;
            Double Rcd = (Rc + Rd) / 2;
            Double Pab = match.TeamOneScore;
            Double Pcd = match.TeamTwoScore;
            Double Pmax = Pab > Pcd ? Pab : Pcd;
            Double Pmin = Pab > Pcd ? Pcd : Pab;
            Double Eab = 1 / (1 + Math.Pow(Pmax, (Rcd - Rab) / 500));
            Double Sab = Pab > Pcd ? Pmax / (Pmax + Pmin) : 1 - (Pmax / (Pmax + Pmin));

            playerOne.Rating = Ra + (100 * (Sab - Eab));
            playerTwo.Rating = Rb + (100 * (Sab - Eab));
            playerThree.Rating = Rc - (100 * (Sab - Eab));
            playerFour.Rating = Rd - (100 * (Sab - Eab));

            _context.Players.Update(playerOne);
            _context.Players.Update(playerTwo);
            _context.Players.Update(playerThree);
            _context.Players.Update(playerFour);

            await _context.SaveChangesAsync();
        }

        private async Task UpdateAllRatings()
        {
            List<Player> players = await _context.Players.ToListAsync();

            foreach (Player player in players)
            {
                player.Rating = 1000;
                _context.Players.Update(player);
            }

            await _context.SaveChangesAsync();

            List<Match> matches = await _context.Matches.OrderBy(m => m.Created).ToListAsync();

            foreach (Match item in matches)
            {
                await UpdateRating(item);
            }
        }
    }
}
