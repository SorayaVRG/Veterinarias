using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Veterinarias.Modelos
{
    public class Visitas
    {
        public int Id { get; set; }
        public int IdMascota { get; set; }
        public DateTime FechaVisita { get; set; } = DateTime.Now;
        public int IdVeterinario { get; set; }
        [DisplayFormat(DataFormatString = "{0:HH:mm}", ApplyFormatInEditMode = true)]
        public TimeSpan HoraIngreso { get; set; }
        public string Notas { get; set; } = string.Empty;
        public DateTime FechaRegistro { get; set; } = DateTime.Now;

        //[DisplayName("Foto")]
        //public string? Foto { get; set; }
        //[NotMapped]
        //public IFormFile FotoIFormFile { get; set; }
        //public string FotoURL => Foto == null ? "" : Foto;

        //public string FotoURL2
        //{
        //    get
        //    {
        //        if (string.IsNullOrEmpty(Foto))
        //        {
        //            return "";
        //        }
        //        else
        //        {
        //            return $"https://localhost:7149/{Foto}";
        //        }
        //    }
        //}
        [NotMapped]
        public string? PersonaDescripcion { get; set; }

        [NotMapped]
        public int IdAnimal { get; set; }

        [NotMapped]
        public int IdRaza { get; set; }

        [NotMapped]
        public int IdPersona { get; set; }
    }
}
