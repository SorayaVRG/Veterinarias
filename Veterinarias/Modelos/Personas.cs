using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Veterinarias.Modelos
{
    public class Personas
    {
        public int Id { get; set; }

        [DisplayName("Tipo Documento")]
        public string TipoDocumento { get; set; } = string.Empty;

        [DisplayName("Numero Documento")]
        public string NumeroDocumento { get; set; } = string.Empty;

        [DisplayName("Nombres")]
        public string Nombres { get; set; } = string.Empty;

        [DisplayName("Sexo")]
        public string Sexo { get; set; } = string.Empty;

        //[DisplayName("Fecha Nacimiento")]
        //public DateTime FechaNacimiento { get; set; } = DateTime.Now.Date;

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime FechaNacimiento { get; set; }

        [NotMapped]
        [DisplayName("Edad")]
        public int Edad { get; set; }

        [DisplayName("Estado")]
        public bool Estado { get; set; }

        [DisplayName("Foto")]
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