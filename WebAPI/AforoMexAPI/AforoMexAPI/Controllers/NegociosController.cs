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
            var nuevoUsuario = negocio.IdUsuarioNavigation;
            nuevoUsuario.Rol = "negocio";
            nuevoUsuario.Correo = nuevoUsuario.Correo.ToLower();
            negocio.IdUsuarioNavigation = null;
            nuevoUsuario.Negocios.Add(negocio);
            _context.Usuarios.Add(nuevoUsuario);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetNegocio), new { id = negocio.IdNegocio }, negocio);
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

        [HttpGet]
        [Route("reservaciones")]
        public async Task<ActionResult<IEnumerable<Reservacione>>> GetReservacionesNegocio(int idNegocio, string fechaConsulta, string periodo, string fechaFin)
        {
            DateTime fecha = Convert.ToDateTime(fechaConsulta);
            IntervaloTiempo intervaloFechas = new IntervaloTiempo() { FechaInicio = fecha, FechaFin = fecha };
            if (fechaFin != null && periodo.Equals("fechaFin"))
            {
                intervaloFechas.FechaFin = Convert.ToDateTime(fechaFin);
            }
            switch (periodo)
            {
                case "semana":
                    DayOfWeek diaDeInicioSemanal = DayOfWeek.Monday;
                    while (intervaloFechas.FechaInicio.DayOfWeek != diaDeInicioSemanal)
                        intervaloFechas.FechaInicio = intervaloFechas.FechaInicio.AddDays(-1);
                    intervaloFechas.FechaFin = intervaloFechas.FechaInicio.AddDays(6);
                    break;
                case "mes":
                    intervaloFechas.FechaInicio = new DateTime(fecha.Year, fecha.Month, 1);
                    intervaloFechas.FechaFin = new DateTime(fecha.Year, fecha.Month, 1).AddMonths(1).AddDays(-1);
                    break;
                case "bimestre":
                    intervaloFechas = this.CalcularIntervaloFechas(fecha, 1, 2);
                    break;
                case "trimestre":
                    intervaloFechas = this.CalcularIntervaloFechas(fecha, 2, 3);
                    break;
                case "cuatrimestre":
                    intervaloFechas = this.CalcularIntervaloFechas(fecha, 3, 4);
                    break;
                case "semestre":
                    intervaloFechas = this.CalcularIntervaloFechas(fecha, 5, 6);
                    break;
                case "ano":
                    intervaloFechas.FechaInicio = new DateTime(fecha.Year, 1, 1);
                    intervaloFechas.FechaFin = new DateTime(fecha.Year, fecha.Month, 1).AddMonths(1).AddDays(-1);
                    break;
            }
            var reservaciones = await _context.Reservaciones.Where(x => x.IdNegocio == idNegocio && x.FechaReserva.Date.CompareTo(intervaloFechas.FechaInicio.Date) >= 0
            && x.FechaReserva.Date.CompareTo(intervaloFechas.FechaFin.Date) <= 0).ToListAsync();
            foreach (var reservacion in reservaciones)
            {
                var usuario = await _context.Usuarios.FirstOrDefaultAsync(x => x.IdUsuario == reservacion.IdUsuario);
                reservacion.IdUsuarioNavigation = new Usuario() { Nombre = usuario.Nombre, Apellidos = usuario.Apellidos, Telefono = usuario.Telefono };
            }
            return reservaciones;
        }

        private IntervaloTiempo CalcularIntervaloFechas(DateTime fecha, int offset, int mesesPeriodo)
        {
            IntervaloTiempo intervalo = new IntervaloTiempo();
            var periodoCalculado = (fecha.Month + offset) / mesesPeriodo;
            var totalMeses = periodoCalculado * mesesPeriodo;
            intervalo.FechaInicio = new DateTime(fecha.Year, totalMeses - offset, 1);
            intervalo.FechaFin = new DateTime(fecha.Year, totalMeses, DateTime.DaysInMonth(fecha.Year, totalMeses));
            return intervalo;
        }
    }
}
