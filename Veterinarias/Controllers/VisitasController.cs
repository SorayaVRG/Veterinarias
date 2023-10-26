using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using Veterinarias.Data;
using Veterinarias.Modelos;

namespace Veterinarias.Controllers
{
    public class VisitasController : Controller
    {
        //Conexion a la base de datos
        private readonly DataContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public VisitasController(DataContext dataContext, IWebHostEnvironment webHostEnvironment)
        {
            _context = dataContext;
            _webHostEnvironment = webHostEnvironment;
        }

        //BUSCAR PERSONAS
        public async Task<IActionResult> BuscarPersonas(string busqueda)
        {
            var personas = await _context.Personas.Where(t => t.Nombres.Contains(busqueda)).ToListAsync();
            return PartialView(personas);
        }
        //public IActionResult Index()
        //{
        //    var visitas = _context.PR_VISITAS_S01.FromSqlRaw("exec PR_VISITAS_S01");
        //    return View(visitas);
        //}

        public IActionResult Index(int IdAnimal, int IdRaza, string NombrePersona, int IdVeterinario)
        {
            Console.WriteLine($"IdAnimal: {IdAnimal}, IdRaza: {IdRaza}, NombrePersona: {NombrePersona}");
            // Obtener la lista de animales y razas desde la base de datos
            var Animales = _context.Animales.ToList();
            var Razas = _context.Razas.ToList();
            var Veterinarios = _context.Veterinarios.ToList();

            //AGREGAR LA OPCION SELECCIONAR A LAS LISTAS DE ANIMALES Y RAZAS
            Animales.Insert(0, new Animales { Id = 0, Nombre = "Seleccionar" });
            Razas.Insert(0, new Razas { Id = 0, Nombre = "Seleccionar" });
            Veterinarios.Insert(0, new Veterinarios { Id = 0, Nombres = "Seleccionar" });

            //CREAR LOS SELECTLIST PARA ENVIAR A LA VISTA
            ViewData["IdAnimal"] = new SelectList(Animales, "Id", "Nombre");
            ViewData["IdRaza"] = new SelectList(Razas, "Id", "Nombre");
            ViewData["IdVeterinario"] = new SelectList(Veterinarios, "Id", "Nombres");

            ViewBag.IdAnimal = new SelectList(Animales, "Id", "Nombre", IdAnimal);
            ViewBag.IdRaza = new SelectList(Razas, "Id", "Nombre", IdRaza);
            ViewBag.IdVeterinario = new SelectList(Veterinarios, "Id", "Nombres", IdVeterinario);

            var visitas = _context.PR_VISITAS_S01.FromSqlRaw("exec PR_VISITAS_S01 @p0, @p1, @p2, @p3", IdAnimal, IdRaza, NombrePersona, IdVeterinario).ToList();
            return View(visitas);
        }

        //CREATE ACTUALIZADO
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var veterinarios = await _context.Veterinarios.ToListAsync();
            ViewData["IdVeterinario"] = new SelectList(veterinarios, "Id", "Nombres");

            return PartialView();
        }

        [HttpGet]
        public async Task<JsonResult> CargarRazas(int id)
        {
            var razas = await _context.Razas.Where(r => r.IdAnimal == id).ToListAsync();
            return Json(razas);
        }

        //CREAR NUEVO REGISTRO
        [HttpPost]
        public async Task<IActionResult> Create(Visitas model)
        {
            _context.Add(model);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        //EDIT
        public async Task<IActionResult> Edit(int id)
        {
            var model = await _context.Visitas.FindAsync(id);

            var veterinarios = await _context.Veterinarios.ToListAsync();
            ViewData["IdVeterinario"] = new SelectList(veterinarios, "Id", "Nombres");

            return PartialView();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Visitas model)
        {
            var modelOld = await _context.Visitas.FindAsync(model.Id);

            modelOld.IdMascota = model.IdMascota; // Actualizar el campo IdMascota
            modelOld.FechaVisita = model.FechaVisita; // Actualizar el campo FechaVisita
            modelOld.IdVeterinario = model.IdVeterinario; // Actualizar el campo IdVeterinario
            modelOld.HoraIngreso = model.HoraIngreso; // Actualizar el campo HoraIngreso
            modelOld.Notas = model.Notas; // Actualizar el campo Notas

            _context.Update(modelOld);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

    }
}
