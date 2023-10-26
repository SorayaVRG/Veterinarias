using System.ComponentModel;

namespace Veterinarias.Modelos
{
    public class PR_RAZAS_S01
    {
        [DisplayName("IdRazas")]
        public int Id { get; set; }

        [DisplayName("NombreAnimal")]
        public string Animal { get; set; } = string.Empty;

        [DisplayName("NombreRaza")]
        public string Raza { get; set; } = string.Empty;
        public bool Estado { get; set; }
    }
}
