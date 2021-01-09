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
    [ApiController]
    [Route("AforoMex/Usuarios")]
    public class UsuariosController : ControllerBase
    {
        private readonly AforoMexContext _context;

        public UsuariosController(AforoMexContext context)
        {
            _context = context;
        }

        // GET: api/Usuarios
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Usuario>>> GetUsuarios()
        {
            return await _context.Usuarios.ToListAsync();
        }

        // GET: api/Usuarios/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Usuario>> GetUsuario(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);

            if (usuario == null)
            {
                return NotFound();
            }

            return usuario;
        }

        // PUT: api/Usuarios/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUsuario(int id, Usuario usuario)
        {
            if (id != usuario.IdUsuario)
            {
                return BadRequest();
            }

            _context.Entry(usuario).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsuarioExists(id))
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

        // POST: api/Usuarios
        [HttpPost]
        public async Task<ActionResult<Usuario>> PostUsuario([Bind("Nombre,Apellidos,Telefono,Correo,FechaNacimiento,Contrasena,Rol")] Usuario usuario)
        {
            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetUsuario), new { id = usuario.IdUsuario }, usuario);
        }

        // DELETE: api/Usuarios/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Usuario>> DeleteUsuario(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }

            _context.Usuarios.Remove(usuario);
            await _context.SaveChangesAsync();

            return usuario;
        }

        private bool UsuarioExists(int id)
        {
            return _context.Usuarios.Any(e => e.IdUsuario == id);
        }

        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<Usuario>> IniciarSesion([Bind("Correo,Contrasena")] Usuario usuario)
        {
            var usuarioEncontrado = await _context.Usuarios.FirstOrDefaultAsync(x => x.Correo.Equals(usuario.Correo.ToLower()) && x.Contrasena.Equals(usuario.Contrasena));
            if (usuarioEncontrado != null)
            {
                var usuarioRegreso = new Usuario();
                usuarioRegreso.IdUsuario = usuarioEncontrado.IdUsuario;
                usuarioRegreso.Nombre = usuarioEncontrado.Nombre;
                usuarioRegreso.Correo = usuarioEncontrado.Correo;
                usuarioRegreso.Rol = usuarioEncontrado.Rol;
                return usuarioRegreso;
            }
            else
            {
                return null;
            }
        }

        [HttpGet]
        [Route("reservacionesSemana/{idUsuario}")]
        public async Task<ActionResult<IEnumerable<Reservacione>>> GetReservacionesSemanaUsuario(int idUsuario)
        {
            DateTime hoy = DateTime.Now;
            var reservaciones = await _context.Reservaciones.Where(x => x.IdUsuario == idUsuario && x.FechaReserva.Date.CompareTo(hoy.Date) >= 0).ToListAsync();
            foreach (var reservacion in reservaciones)
            {
                var negocio = await _context.Negocios.FirstOrDefaultAsync(x => x.IdNegocio == reservacion.IdNegocio);
                reservacion.IdNegocioNavigation = new Negocio() { Nombre =  negocio.Nombre };
            }
            return reservaciones;
        }
    }
}
