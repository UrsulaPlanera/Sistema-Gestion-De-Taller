using System.ComponentModel;

namespace SistemaGestionDeTaller.Areas.Main.Models.ViewModel
{
    public class ProductCart
    {
        public int IdCart { get; set; }

        [DisplayName("Nombre")]
        public string NameProduct { get; set; } = null!;
        [DisplayName("Precio")]
        public decimal Price { get; set; }
        [DisplayName("Unidades")]
        public int Units { get; set; }
    }
}
