using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations.Schema;

namespace Veterinarias.Modelos
{
    public class PR_VISITAS_S01
    {
        public int Id { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime FechaVisita { get; set; }

        [DisplayName("Animal")]
        public string Animal { get; set; } = string.Empty;

        [DisplayName("Raza")]
        public string Raza { get; set; } = string.Empty;

        [DisplayName("Nombre")]
        public string Nombres { get; set; } = string.Empty;

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
                    return $"https://localhost:7149/{Foto}";
                }
            }
        }

        [DisplayName("Dueño")]
        public string Dueño { get; set; } = string.Empty;

        [DisplayName("Veterinario")]
        public string Veterinario { get; set; } = string.Empty;

        public string HoraIngreso { get; set; }


        [DisplayName("Atencion")]
        public string Atencion { get; set; } = string.Empty;
    }
}
