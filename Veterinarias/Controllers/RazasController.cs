using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Veterinarias.Data;
using Veterinarias.Modelos;

namespace Veterinarias.Controllers
{
    public class RazasController : Controller
    {
        //conexion a la base de datos
        private readonly DataContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public RazasController(DataContext dataContext, IWebHostEnvironment webHostEnvironment)
        {
            _context = dataContext;
            _webHostEnvironment = webHostEnvironment;
        }

        //TOMARLO COMO PRUEBA PARA CREAR PAGINA DENTRO DE OTRA

        //INDEX RAZAS GET : /<Controller>/
        //Index
        public async Task<IActionResult> Index(int id)
        {
            var razas = await _context.PR_RAZAS_S01.FromSqlRaw("EXECUTE dbo.PR_RAZAS_S01 @idAnimal={0}", id).ToListAsync();
            var modelAnimales = await _context.Animales.FindAsync(id);
            ViewBag.Animales = modelAnimales;
            return View(razas);
        }

        //CREATE RAZAS
        public IActionResult Create(int idAnimal)
        {
            var raza = new Razas { IdAnimal = idAnimal };
            return PartialView(raza);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Razas Model)
        {
            await _context.AddAsync(Model);   //Insert into
            await _context.SaveChangesAsync();     //Commit a la bd
            return RedirectToAction("Index", new { id = Model.IdAnimal });
        }

        //EDITAR RAZAS
        public async Task<IActionResult> Edit(int id)
        {
            var raza = await _context.Razas.FindAsync(id);
            return PartialView(raza);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Razas model)
        {
            var modelOld = await _context.Razas.FindAsync(model.Id);
            modelOld.Nombre = model.Nombre;
            _context.Update(modelOld); // Actualizar el registro existente
            await _context.SaveChangesAsync(); // Guardar cambios en la base de datos
            return RedirectToAction("Index", new { id = modelOld.IdAnimal });
        }

        //PARA ACTIVAR Y DESACTIVAR RAZAS
        public async Task<IActionResult> Activar(int id)
        {
            var idAnimales = 0;
            var raza = await _context.Razas.FindAsync(id);
            if (raza != null)
            {
                idAnimales = raza.IdAnimal; // Guardar el ID del animal antes de anular la raza
                raza.Estado = true; // Establecer el estado como activado
                _context.Update(raza);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index), new { id = idAnimales });

        }

        // Anular RAZAS method GET
        public async Task<IActionResult> Anular(int id)
        {
            var idAnimales = 0;
            var raza = await _context.Razas.FindAsync(id);
            if (raza != null)
            {
                idAnimales = raza.IdAnimal; // Guardar el ID del animal antes de anular la raza
                raza.Estado = false; // Establecer el estado como anulado
                _context.Update(raza);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index), new { id = idAnimales });

        }


    }
}
