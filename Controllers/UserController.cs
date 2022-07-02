using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PisoAppBackend.Models;
using System;
using System.Linq;

namespace PisoAppBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IJWTManagerRepository _JWTRepository;
        private readonly PisoAppContext _DB;

        public UserController(IJWTManagerRepository JWTRepository)
        {
            _JWTRepository = JWTRepository;
            _DB = new PisoAppContext();
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("Login")]
        public IActionResult Login([FromBody] Usuario usuario)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(usuario.Username) && string.IsNullOrWhiteSpace(usuario.HashedPassword))
                    return Ok(new { success = false, error = "Las credenciales introducidas no son correctas" });

                string token = "";
                Usuario generatedUser = _JWTRepository.Authenticate(usuario.Username, usuario.HashedPassword, out token);

                if (generatedUser != null)
                    return Ok(new { success = true, usuario = generatedUser, token = token });
                else
                    return Ok(new { success = false, error = "Las credenciales introducidas no son correctas" });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " | " + ex.StackTrace);
                return Ok(new { success = false, error = "Ha ocurrido un error en el servidor" });
            }
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("Register")]
        public IActionResult Register([FromBody] Usuario usuario)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(usuario.Username) || string.IsNullOrWhiteSpace(usuario.HashedPassword) || string.IsNullOrWhiteSpace(usuario.Name))
                    return Ok(new { success = false, error = "Debes rellenar los campos obligatorios" });

                if (_DB.Usuarios.Any(u => u.Username.ToLower() == usuario.Username.ToLower()))
                    return Ok(new { success = false, error = "Ese usuario ya está registrado" });

                usuario.HashedPassword = HashExtensions.Hash(usuario.HashedPassword);
                _DB.Usuarios.Add(usuario);
                _DB.SaveChanges();
                return Ok(new { success = true, usuario = usuario });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " | " + ex.StackTrace);
                return Ok(new { success = false, error = "Ha ocurrido un error en el servidor" });
            }
        }
    }
}
