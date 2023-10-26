using Microsoft.AspNetCore.Mvc;
using Veterinarias.Data;
using Veterinarias.Modelos;

namespace Veterinarias.Controllers
{
    public class AnimalesController : Controller
    {
        //conexion a la base de datos
        private readonly DataContext _context;

        public AnimalesController(DataContext dataContext)
        {
            _context = dataContext;
        }

        //TOMARLO COMO PRUEBA PARA CREACIONES SENCILLAS

        //INDEX ANIMALES 
        public IActionResult Index()
        {
            var animales = _context.Animales.ToList();
            return View(animales);
        }

        //Create ANIMALES
        public IActionResult Create()
        {
            return PartialView();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Animales model)
        {
            await _context.AddAsync(model);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // edit
        public async Task<IActionResult> Edit(int id)
        {
            var animales = await _context.Animales.FindAsync(id); //select * from ANIMALES where PK = id
            return PartialView(animales);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Animales model)
        {
            var modelOld = await _context.Animales.FindAsync(model.Id);//select * from Zonas where PK = id
            modelOld.Nombre = model.Nombre;
            _context.Update(modelOld);//update ANIMALES set NOMBRES = model.NOMBRES
            await _context.SaveChangesAsync(); //commit a la base de datos
            return RedirectToAction(nameof(Index));
        }


        //PARA ACTIVAR Y DESACTIVAR ANIMALES
        public async Task<IActionResult> Activar(int id)
        {
            var animales = await _context.Animales.FindAsync(id);
            animales.Estado = true; // Establecer el estado como activado
            _context.Update(animales);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // Anular ANIMALES method GET
        public async Task<IActionResult> Anular(int id)
        {
            var animales = await _context.Animales.FindAsync(id);
            animales.Estado = false; // Establecer el estado como anulado
            _context.Update(animales);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}