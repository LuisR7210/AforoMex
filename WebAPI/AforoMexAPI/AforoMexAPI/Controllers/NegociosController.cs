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
        [HttpPut("{id}")]
        public async Task<IActionResult> PutNegocio(int id, Negocio negocio)
        {
            if (id != negocio.IdNegocio)
            {
                return BadRequest();
            }
            var horariosNegocio = await _context.Horarios.Where(h => h.IdNegocio == negocio.IdNegocio).ToListAsync();
            foreach (var horario in horariosNegocio)
            {
                _context.Horarios.Remove(horario);
            }
            await _context.SaveChangesAsync();
            _context.Entry(negocio).State = EntityState.Modified;
            var direccion = negocio.Direcciones.First();
            _context.Entry(direccion).State = EntityState.Modified;
            foreach (var horario in negocio.Horarios)
            {
                _context.Horarios.Add(horario);
            }
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

        // PUT: api/Negocios/5
        [HttpPut("actualizarCupo/{id}")]
        public async Task<IActionResult> ActualizarCupo(int id, [Bind("IdUsuario, IdNegocio, CupoOcupado")]Negocio negocio)
        {
            if (id != negocio.IdNegocio)
            {
                return BadRequest();
            }
            var negocioEncontrado = await _context.Negocios.FirstOrDefaultAsync(x => x.IdNegocio == id && x.IdUsuario == negocio.IdUsuario);
            negocioEncontrado.CupoOcupado = negocio.CupoOcupado;
            _context.Entry(negocioEncontrado).State = EntityState.Modified;

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

        [HttpGet]
        [Route("categorias")]
        public async Task<ActionResult<IEnumerable<string>>> GetCategorias()
        {
            var categorias = new List<string>();
            foreach (var negocio in await _context.Negocios.ToListAsync())
            {
                if (!categorias.Contains(negocio.Categoria))
                {
                    categorias.Add(negocio.Categoria);
                }
            }
            return categorias;
        }

        [HttpGet]
        [Route("buscarNegocios")]
        public async Task<ActionResult<IEnumerable<Negocio>>> BuscarNegocios(string nombre, string ciudad, string colonia, string categoria)
        {
            if (nombre == null)
                nombre = "";
            if (ciudad == null)
                ciudad = "";
            if (colonia == null)
                colonia = "";
            if (categoria == null)
                categoria = "";
            var negociosEncontrados = await _context.Negocios.Where(x => x.Nombre.ToLower().Contains(nombre.ToLower()) && x.Direcciones.First().Ciudad.Contains(ciudad) 
            && x.Direcciones.First().Colonia.Contains(colonia) && x.Categoria.ToLower().Contains(categoria.ToLower())).ToListAsync();
            return negociosEncontrados;
        }

        // POST: api/Reservaciones
        [HttpPost]
        [Route("reservaciones/post")]
        public async Task<ActionResult<Mensaje>> PostReservacion([Bind("FechaReserv,NumLugares,Estado,IdUsuario,IdNegocio")] Reservacione reservacion)
        {
            //Console.WriteLine("--------------------------------------------------");
            //Console.WriteLine("datos enviados: "+reservacion.Estado+", "+reservacion.FechaReserva+", "+reservacion.IdNegocio+", "+reservacion.IdUsuario+", "+reservacion.NumLugares);
            //Console.WriteLine("--------------------------------------------------");
            var mensaje = new Mensaje();
            /////////////////////////validacion de lugares////////////////////////////
            var reservaciones = await _context.Reservaciones.Where(x => x.IdNegocio == reservacion.IdNegocio).ToListAsync();
            var negocio = await _context.Negocios.FindAsync(reservacion.IdNegocio);
            var listaReservacioes = negocio.Reservaciones.ToList();
            var reservacionesDia = new List<Reservacione>();
            DateTime fechaRes = DateTime.Parse(reservacion.Estado);
            DateTime limI = fechaRes.AddHours(-1);
            DateTime limS = fechaRes.AddHours(2);
            //Console.WriteLine("" +
            //"dias: "+fechaRes.Day+" mes "+fechaRes.Month+" año: "+fechaRes.Year+" tamaño:"+listaReservacioes.Count+" otra: "+reservaciones.Count);
            foreach (Reservacione reser in reservaciones)
            {
                if (fechaRes.Day == reser.FechaReserva.Day && fechaRes.Month == reser.FechaReserva.Month && fechaRes.Year == reser.FechaReserva.Year)
                {
                    reservacionesDia.Add(reser);
                }
            }
            //Console.WriteLine("res dia: "+reservacionesDia.Count);
            var limiteARes = negocio.Aforo * negocio.AforoReservaciones;
            int intentoARes = 0;
            foreach (Reservacione res in reservacionesDia)
            {
                if (res.FechaReserva > limI && res.FechaReserva < limS)
                {
                    Console.WriteLine("1: " + res.FechaReserva + " > " + limI + " && " + res.FechaReserva + " < " + limS);
                    intentoARes += res.NumLugares;
                }
            }
            Console.WriteLine("los datos a comparar son: " + intentoARes + reservacion.NumLugares + " 11 " + limiteARes);
            if (intentoARes + reservacion.NumLugares > limiteARes)
            {
                mensaje.error = true;
                mensaje.mensaje = "No es posible crear una reservacion" +
                    "en esta fecha y hora, por que ya hay muchas reservaciones para ese momento. " +
                    "Prueba con otra fecha o con menos lugares";
                return StatusCode(StatusCodes.Status409Conflict, mensaje);
            }
            ///////////////////////////////////////////////////////////////
            var nuevaReservacion = new Reservacione();
            nuevaReservacion.Estado = "Vigente";
            nuevaReservacion.FechaReserva = DateTime.Parse(reservacion.Estado);
            nuevaReservacion.IdNegocio = reservacion.IdNegocio;
            nuevaReservacion.IdUsuario = reservacion.IdUsuario;
            nuevaReservacion.NumLugares = reservacion.NumLugares;
            Console.WriteLine("--------------------------------------------------");
            Console.WriteLine("DATOS GUARDADOS: " + nuevaReservacion.Estado + ", " + nuevaReservacion.FechaReserva + ", " + nuevaReservacion.IdNegocio + ", " + nuevaReservacion.IdUsuario + ", " + nuevaReservacion.NumLugares);
            Console.WriteLine("--------------------------------------------------");
            _context.Reservaciones.Add(nuevaReservacion);
            await _context.SaveChangesAsync();
            mensaje.objeto = nuevaReservacion;
            return CreatedAtAction(nameof(GetNegocio), new { id = nuevaReservacion.IdReservacion }, mensaje);
        }
    }
}
