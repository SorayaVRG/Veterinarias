using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Veterinarias.Modelos
{
    public class Mascotas
    {
        public int Id { get; set; }

        [DisplayName("Raza")]
        public int IdRaza { get; set; }

        [DisplayName("Nombre")]
        public string Nombres { get; set; } = string.Empty;

        //[DisplayName("Fecha Nacimiento")]
        //public DateTime FechaNacimiento { get; set; } = DateTime.Now.Date;

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime FechaNacimiento { get; set; }

        [NotMapped]
        [DisplayName("Edad")]
        public int Edad { get; set; }

        [DisplayName("Color")]
        public string Color { get; set; } = string.Empty;

        [DisplayName("Persona")]
        public int IdPersona { get; set; }

        [DisplayName("Estado")]
        public string? Estado { get; set; }

        public string? Foto { get; set; }
        [NotMapped]
        public IFormFile? FotoIFormFile { get; set; }
        public string FotoURL => Foto == null ? "" : Foto;

        public string FotoURL2
        {
            get
            {
                if (string.IsNullOrEmpty(Foto))
                {
                    return "";
                }
                else
                {
                    return $"https://localhost:7149/{Foto}";
                }
            }
        }
        [NotMapped]
        public string? PersonaDescripcion { get; set; }

        [NotMapped]
        public int IdAnimal { get; set; }
    }
}