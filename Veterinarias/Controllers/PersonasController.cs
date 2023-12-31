﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting.Internal;
using Veterinarias.Data;
using Veterinarias.Modelos;
using Veterinarias.Models;
using static System.Net.Mime.MediaTypeNames;

namespace Veterinarias.Controllers
{
    public class PersonasController : Controller
    {
        //Conexion a la base de datos
        private readonly DataContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public PersonasController(DataContext dataContext, IWebHostEnvironment webHostEnvironment)
        {
            _context = dataContext;
            _webHostEnvironment = webHostEnvironment;
        }

        //INDEX PERSONAS 
        public IActionResult Index(string TipoDocumento, string Sexo)
        {
            var TiposDocumentos = new List<TiposDocumentos>();
            TiposDocumentos.Add(new TiposDocumentos { TipoDocumento = "", NombreDocumento = "Seleccionar" });
            TiposDocumentos.Add(new TiposDocumentos { TipoDocumento = "DNI", NombreDocumento = "DNI" });
            TiposDocumentos.Add(new TiposDocumentos { TipoDocumento = "CXE", NombreDocumento = "Carnet Extranjeria" });
            TiposDocumentos.Add(new TiposDocumentos { TipoDocumento = "PAS", NombreDocumento = "Pasaporte" });
            ViewBag.TipoDocumento = new SelectList(TiposDocumentos, "TipoDocumento", "NombreDocumento", TipoDocumento);

            var TiposSexo = new List<TiposSexo>();
            TiposSexo.Add(new TiposSexo { TipoSexo = "", NombreSexo = "Seleccionar" });
            TiposSexo.Add(new TiposSexo { TipoSexo = "F", NombreSexo = "Femenino" });
            TiposSexo.Add(new TiposSexo { TipoSexo = "M", NombreSexo = "Masculino" });
            ViewBag.TipoSexo = new SelectList(TiposSexo, "TipoSexo", "NombreSexo", Sexo);


            var personas = _context.PR_PERSONAS_S01.FromSqlRaw("exec PR_PERSONAS_S01 @P0, @P1", TipoDocumento, Sexo);
            //exec [PR_PERSONAS_S01] 'DNI', 'M'
            return View(personas);
        }

        //CREATE PERSONAS
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var TiposDocumentos = new List<TiposDocumentos>();
            TiposDocumentos.Add(new TiposDocumentos { TipoDocumento = "", NombreDocumento = "Seleccionar" });
            TiposDocumentos.Add(new TiposDocumentos { TipoDocumento = "DNI", NombreDocumento = "DNI" });
            TiposDocumentos.Add(new TiposDocumentos { TipoDocumento = "CXE", NombreDocumento = "Carnet Extranjeria" });
            TiposDocumentos.Add(new TiposDocumentos { TipoDocumento = "PAS", NombreDocumento = "Pasaporte" });

            ViewBag.TipoDocumento = new SelectList(TiposDocumentos, "TipoDocumento", "NombreDocumento");

            var TiposSexo = new List<TiposSexo>();
            TiposSexo.Add(new TiposSexo { TipoSexo = "", NombreSexo = "Seleccionar" });
            TiposSexo.Add(new TiposSexo { TipoSexo = "F", NombreSexo = "Femenino" });
            TiposSexo.Add(new TiposSexo { TipoSexo = "M", NombreSexo = "Masculino" });

            ViewBag.TipoSexo = new SelectList(TiposSexo, "TipoSexo", "NombreSexo");

            return PartialView();
        }

        //CREAR NUEVO REGISTRO
        [HttpPost]
        public async Task<IActionResult> Create(Personas model)
        {
            //PARA CREAR FOTO
            var prueba1 = model.FotoIFormFile;

            if (model.FotoIFormFile != null)
            {
                model.Foto = await CargarDocumento0(model.FotoIFormFile, "PersonasFoto");
            }

            _context.Add(model);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        //PARA CARGAR FOTO
        private async Task<string> CargarDocumento0(IFormFile FotoIFormFile, string ruta)
        {
            var guid = Guid.NewGuid().ToString();
            var fileName = guid + Path.GetExtension(FotoIFormFile.FileName);
            //Obtengo extension del documento
            var carga1 = Path.Combine(_webHostEnvironment.WebRootPath, "images", ruta);
            /*var carga = Path.Combine(_webHostEnvironment.WebRootPath, string.Format("images\\{0}", ruta));*/
            using (var fileStream = new FileStream(Path.Combine(carga1, fileName), FileMode.Create))
            {
                await FotoIFormFile.CopyToAsync(fileStream);
            }
            return string.Format("/images/{0}/{1}", ruta, fileName);
        }

        //EDIT PERSONAS
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var model = await _context.Personas.FindAsync(id);

            var TiposDocumentos = new List<TiposDocumentos>();
            TiposDocumentos.Add(new TiposDocumentos { TipoDocumento = "", NombreDocumento = "Seleccionar" });
            TiposDocumentos.Add(new TiposDocumentos { TipoDocumento = "DNI", NombreDocumento = "DNI" });
            TiposDocumentos.Add(new TiposDocumentos { TipoDocumento = "CXE", NombreDocumento = "Carnet Extranjeria" });
            TiposDocumentos.Add(new TiposDocumentos { TipoDocumento = "PAS", NombreDocumento = "Pasaporte" });
            ViewBag.TipoDocumento = new SelectList(TiposDocumentos, "TipoDocumento", "NombreDocumento", model.TipoDocumento);

            var TiposSexo = new List<TiposSexo>();
            TiposSexo.Add(new TiposSexo { TipoSexo = "", NombreSexo = "Seleccionar" });
            TiposSexo.Add(new TiposSexo { TipoSexo = "F", NombreSexo = "Femenino" });
            TiposSexo.Add(new TiposSexo { TipoSexo = "M", NombreSexo = "Masculino" });
            ViewBag.TipoSexo = new SelectList(TiposSexo, "TipoSexo", "NombreSexo", model.Sexo);

            return PartialView(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Personas model)
        {
            var modelOld = await _context.Personas.FindAsync(model.Id);
            modelOld.TipoDocumento = model.TipoDocumento; // Actualizar el campo TipoDocumento
            modelOld.NumeroDocumento = model.NumeroDocumento; // Actualizar el campo NumeroDocumento
            modelOld.Nombres = model.Nombres; // Actualizar el campo Nombres
            modelOld.FechaNacimiento = model.FechaNacimiento; // Actualizar el campo Fecha nac
            modelOld.Sexo = model.Sexo; // Actualizar el campo Sexo

            // Actualizar foto si se proporciona un nuevo archivo
            if (model.FotoIFormFile != null)
            {
                // Eliminar la foto anterior si existe
                if (!string.IsNullOrEmpty(modelOld.Foto))
                {
                    var fotoPath = Path.Combine(_webHostEnvironment.WebRootPath, modelOld.Foto.TrimStart('/'));
                    if (System.IO.File.Exists(fotoPath))
                    {
                        System.IO.File.Delete(fotoPath);
                    }
                }

                // Cargar la nueva foto
                modelOld.Foto = await CargarDocumento0(model.FotoIFormFile, "PersonasFoto");
            }

            _context.Update(modelOld);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        //PARA ACTIVAR Y DESACTIVAR PERSONAS
        public async Task<IActionResult> Activar(int id)
        {
            var personas = await _context.Personas.FindAsync(id);
            personas.Estado = true; // Establecer el estado como activado
            _context.Update(personas);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // Anular ANIMALES method GET
        public async Task<IActionResult> Anular(int id)
        {
            var personas = await _context.Personas.FindAsync(id);
            personas.Estado = false; // Establecer el estado como anulado
            _context.Update(personas);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

    }
}