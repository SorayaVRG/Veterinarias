using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Veterinarias.Data;

namespace Veterinarias.Controllers
{
    public class BandejaController : Controller
    {
        //Conexion a la base de datos
        private readonly DataContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public BandejaController(DataContext dataContext, IWebHostEnvironment webHostEnvironment)
        {
            _context = dataContext;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {
            //var bandeja = _context.PR_MASCOTAS_S01.FromSqlRaw("exec PR_MASCOTAS_S01");
            //return View(bandeja);

            var bandeja = _context.PR_MASCOTAS_S01.FromSqlRaw("EXEC PR_MASCOTAS_S01").ToList();
            var registrosFiltrados = bandeja.Where(r => r.Estado == "Registrado").ToList();
            return View(registrosFiltrados);
        }

        //PARA APROBAR Y RECHAZAR REGISTROS EN BANDEJA MASCOTAS
        public async Task<IActionResult> Activar(int id)
        {
            var veterinarios = await _context.Mascotas.FindAsync(id);
            veterinarios.Estado = "Aprob"; // Establecer el estado como activado
            _context.Update(veterinarios);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // Anular ANIMALES method GET
        public async Task<IActionResult> Anular(int id)
        {
            var veterinarios = await _context.Mascotas.FindAsync(id);
            veterinarios.Estado = "Recha"; // Establecer el estado como anulado
            _context.Update(veterinarios);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
