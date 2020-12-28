using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AforoMexAPI.Models;

namespace AforoMexAPI.Controllers
{
    [Route("AforoMex/Negocios")]
    [ApiController]
    public class NegociosController : ControllerBase
    {
        private readonly AforoMexContext _context;

        public NegociosController(AforoMexContext context)
        {
            _context = context;
        }

        // GET: api/Negocios
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Negocio>>> GetNegocios()
        {
            return await _context.Negocios.ToListAsync();
        }

        // GET: api/Negocios/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Negocio>> GetNegocio(int id)
        {
            var negocio = await _context.Negocios.FindAsync(id);

            if (negocio == null)
            {
                return NotFound();
            }
            negocio.Direcciones.Add(await _context.Direcciones.FirstOrDefaultAsync(x => x.IdNegocio == negocio.IdNegocio));
            foreach (var horario in _context.Horarios.Where(x => x.IdNegocio == negocio.IdNegocio))
            {
                negocio.Horarios.Add(horario);
            }
            return negocio;
        }

        // PUT: api/Negocios/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutNegocio(int id, Negocio negocio)
        {
            if (id != negocio.IdNegocio)
            {
                return BadRequest();
            }

            _context.Entry(negocio).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NegocioExists(id))
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

        // POST: api/Negocios
        [HttpPost]
        public async Task<ActionResult<Negocio>> PostNegocio(Negocio negocio)
        {
            _context.Negocios.Add(negocio);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetNegocio", new { id = negocio.IdNegocio }, negocio);
        }

        // DELETE: api/Negocios/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Negocio>> DeleteNegocio(int id)
        {
            var negocio = await _context.Negocios.FindAsync(id);
            if (negocio == null)
            {
                return NotFound();
            }

            _context.Negocios.Remove(negocio);
            await _context.SaveChangesAsync();

            return negocio;
        }

        private bool NegocioExists(int id)
        {
            return _context.Negocios.Any(e => e.IdNegocio == id);
        }
    }
}
