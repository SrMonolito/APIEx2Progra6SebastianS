using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using APIExamen2Progra6SebastianSancho.Models;
using APIExamen2Progra6SebastianSancho.ModelsDTOs;

namespace APIExamen2Progra6SebastianSancho.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly AnswersDbContext _context;

        public UsersController(AnswersDbContext context)
        {
            _context = context;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }

        //Get de la informacion del usuario segun su correo
        [HttpGet("GetUserInfoByEmail")]
        public ActionResult<IEnumerable<UserDTO>> GetUserInfoByEmail(string pEmail) 
        {

            var query = (from u in _context.Users
                         where u.UserName == pEmail
                         select new 
                         {
                            id = u.UserId,
                            correo = u.UserName,
                            nombre = u.FirstName,
                            apellido = u.LastName,
                            telefono = u.PhoneNumber,
                            contrasennia = u.UserPassword,
                            contadorStrikes = u.StrikeCount,
                            correoRespaldo = u.BackUpEmail,
                            descTrabajo = u.JobDescription,
                            usuarioEstadoId = u.UserStatusId,
                            idPais = u.CountryId,
                            idRolUsuario = u.UserRoleId
                         }
                         ).ToList();

            List<UserDTO> list = new List<UserDTO>();

            foreach (var item in query) 
            {
                UserDTO nuevoUsuario = new UserDTO() 
                {
                    UsuarioID = item.id,
                    UsuarioEmail = item.correo,
                    UsuarioName = item.nombre,
                    UsuarioLastName = item.apellido,
                    UsuarioPhone = item.telefono,
                    UsuarioPassword = item.contrasennia,
                    UsuarioStrikeCount = item.contadorStrikes,
                    UsuarioBackUpEmail = item.correoRespaldo,
                    UsuarioJobDescription = item.descTrabajo,
                    UsuarioEstadoID = item.usuarioEstadoId,
                    UsuarioPaisID = item.idPais,
                    UsuarioRolID = item.idRolUsuario
                };

                list.Add(nuevoUsuario);
            }
            if(list == null) {return NotFound();}
            return list;
            
        }


        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, User user)
        {
            if (id != user.UserId)
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
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

        // POST: api/Users
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUser", new { id = user.UserId }, user);
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.UserId == id);
        }
    }
}
