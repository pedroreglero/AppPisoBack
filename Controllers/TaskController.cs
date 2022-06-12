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
    public class TaskController : ControllerBase
    {
        private readonly PisoAppContext _DB;
        public TaskController()
        {
            _DB = new PisoAppContext();
        }

        
        [HttpPost]
        [Route("Create")]
        public IActionResult CreateTask([FromBody] Tarea tarea)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(tarea.Name))
                    return Ok(new { success = false, error = "Debes rellenar los campos obligatorios" });

                _DB.Tareas.Add(tarea);
                _DB.SaveChanges();
                return Ok(new { success = true, tarea = tarea });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " | " + ex.StackTrace);
                return Ok(new { success = false, error = "Ha ocurrido un error en el servidor" });
            }
        }

        [HttpPost]
        [Route("GetUserAssignedTasks")]
        public IActionResult GetUserAssignedTasks([FromBody] Usuario usuario)
        {
            try
            {
                return Ok(new { success = true, tareas = _DB.Tareas.Where(t => t.AsignadosTareas.Where(x => x.UserId == usuario.Id).Any()).ToList() });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " | " + ex.StackTrace);
                return Ok(new { success = false, error = "Ha ocurrido un error en el servidor" });
            }
        }

        [HttpPost]
        [Route("GetUserCreatedTasks")]
        public IActionResult GetUserCreatedTasks([FromBody] Usuario usuario)
        {
            try
            {
                return Ok(new { success = true, tareas = _DB.Tareas.Where(t => t.CreatedBy == usuario.Id).ToList() });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " | " + ex.StackTrace);
                return Ok(new { success = false, error = "Ha ocurrido un error en el servidor" });
            }
        }

        [HttpPost]
        [Route("AssignTask")]
        public IActionResult AssignTask([FromBody] Tarea tarea)
        {
            try
            {
                Tarea currentTarea = _DB.Tareas.Where(x => x.Id == tarea.Id).FirstOrDefault();
                if (currentTarea == null)
                    return Ok(new { success = false, error = "No se ha encontrado la tarea" });

                foreach (var asignado in tarea.AsignadosTareas)
                {
                    _DB.AsignadosTareas.Add(asignado);
                }
                _DB.SaveChanges();
                return Ok(new { success = true });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " | " + ex.StackTrace);
                return Ok(new { success = false, error = "Ha ocurrido un error en el servidor" });
            }
        }
    }
}
