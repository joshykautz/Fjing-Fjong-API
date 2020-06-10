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
    public class PlayerController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public PlayerController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Player
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<IEnumerable<PlayerDto>>> GetPlayers()
        {
            var playerList = await _context.Players.ToListAsync();

            var playerDtoList = new List<PlayerDto>();

            foreach (var player in playerList)
            {
                playerDtoList.Add(_mapper.Map<PlayerDto>(player));
            }

            return Ok(playerDtoList);
        }

        // GET: api/Player/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PlayerDto>> GetPlayer(Guid id)
        {
            var player = await _context.Players.FindAsync(id);

            if (player == null)
            {
                return NotFound();
            }

            var playerDto = _mapper.Map<PlayerDto>(player);

            return Ok(playerDto);
        }

        // PUT: api/Player/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> PutPlayer(Guid id, PlayerUpdateDto playerUpdateDto)
        {
            if (id != playerUpdateDto.Id)
                return BadRequest();

            if (playerUpdateDto.Name == null)
                ModelState.AddModelError("Name", "Player name is required.");

            if (playerUpdateDto.Name == String.Empty)
                ModelState.AddModelError("Name", "Player name cannot be empty.");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            Player player = await _context.Players.FindAsync(id);

            if (player == null)
            {
                return NotFound();
            }

            player.Name = playerUpdateDto.Name;

            _context.Players.Update(player);

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/Player
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<PlayerDto>> PostPlayer(PlayerCreateDto playerCreateDto)
        {
            if (playerCreateDto.Name == null)
                ModelState.AddModelError("Name", "Player name is required.");

            if (playerCreateDto.Name == String.Empty)
                ModelState.AddModelError("Name", "Player name cannot be empty.");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            Player player = _mapper.Map<Player>(playerCreateDto);

            _context.Players.Add(player);

            await _context.SaveChangesAsync();

            PlayerDto playerDto = _mapper.Map<PlayerDto>(player);

            return CreatedAtAction(nameof(GetPlayer), new { id = player.Id }, playerDto);
        }

        // DELETE: api/Player/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Player>> DeletePlayer(Guid id)
        {
            Player player = await _context.Players.FindAsync(id);

            if (player == null)
            {
                return NotFound();
            }

            _context.Players.Remove(player);

            await _context.SaveChangesAsync();

            PlayerDto playerDto = _mapper.Map<PlayerDto>(player);

            return Ok(playerDto);
        }

        private bool PlayerExists(Guid id)
        {
            return _context.Players.Any(e => e.Id == id);
        }
    }
}
