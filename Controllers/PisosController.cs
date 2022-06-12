using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PisoAppBackend.Models;
using System;
using System.Linq;

namespace PisoAppBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PisosController : ControllerBase
    {
        private readonly PisoAppContext _DB;
        public PisosController()
        {
            _DB = new PisoAppContext();
        }

        [HttpPost]
        [Route("GetUserPisos")]
        public IActionResult GetUserPisos([FromBody] Usuario usuario)
        {
            try
            {
                var pisosResult = _DB.IntegrantesPisos.Where(x => x.UserId == usuario.Id).Include(x => x.Piso.IntegrantesPisos).Select(x => x.Piso).ToList();
                return Ok(new { success = true, pisos = pisosResult });
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message + " | " + ex.StackTrace);
                return Ok(new { success = false, error = "Ha ocurrido un error en el servidor" });
            }
        }
    }
}
