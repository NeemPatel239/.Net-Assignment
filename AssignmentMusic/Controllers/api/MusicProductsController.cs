using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AssignmentMusic.Data;
using AssignmentMusic.Models;

namespace AssignmentMusic.Controllers.api
{
    [Route("api/[controller]")]
    [ApiController]
    public class MusicProductsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public MusicProductsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/MusicProducts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MusicProducts>>> GetMusicProducts()
        {
            return await _context.MusicProducts.ToListAsync();
        }

        // GET: api/MusicProducts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MusicProducts>> GetMusicProducts(int id)
        {
            var musicProducts = await _context.MusicProducts.FindAsync(id);

            if (musicProducts == null)
            {
                return NotFound();
            }

            return musicProducts;
        }

        // PUT: api/MusicProducts/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMusicProducts(int id, MusicProducts musicProducts)
        {
            if (id != musicProducts.ProductId)
            {
                return BadRequest();
            }

            _context.Entry(musicProducts).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MusicProductsExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/MusicProducts
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<MusicProducts>> PostMusicProducts(MusicProducts musicProducts)
        {
            _context.MusicProducts.Add(musicProducts);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMusicProducts", new { id = musicProducts.ProductId }, musicProducts);
        }

        // DELETE: api/MusicProducts/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<MusicProducts>> DeleteMusicProducts(int id)
        {
            var musicProducts = await _context.MusicProducts.FindAsync(id);
            if (musicProducts == null)
            {
                return NotFound();
            }

            _context.MusicProducts.Remove(musicProducts);
            await _context.SaveChangesAsync();

            return musicProducts;
        }

        private bool MusicProductsExists(int id)
        {
            return _context.MusicProducts.Any(e => e.ProductId == id);
        }
    }
}
