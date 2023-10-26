using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Veterinarias.Modelos
{
    public class PR_MASCOTAS_S01
    {
        public int Id { get; set; }

        [DisplayName("Animal")]
        public string Animal { get; set; } = string.Empty;

        [DisplayName("Raza")]
        public string Raza { get; set; } = string.Empty;

        [DisplayName("Nombre")]
        public string Nombres { get; set; } = string.Empty;

        //[DisplayName("Fecha Nacimiento")]
        //public DateTime FechaNacimiento { get; set; } = DateTime.Now.Date;

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime FechaNacimiento { get; set; }

        [DisplayName("Edad")]
        public int Edad { get; set; }

        [DisplayName("Color")]
        public string Color { get; set; } = string.Empty;

        [DisplayName("Dueño")]
        public string Dueño { get; set; } = string.Empty;

        [DisplayName("Estado")]
        public string Estado { get; set; } = string.Empty;

        public string? Foto { get; set; }
        [NotMapped]
        public IFormFile FotoIFormFile { get; set; }
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
                    return $"http://localhost:7149/{Foto}";
                }
            }
        }
    }
}