using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SistemaGestionDeTaller.Areas.Main.Models;

public partial class Product
{
    public int Id { get; set; }

    [DisplayName("Nombre")]
    public string Name { get; set; } = null!;

    [DisplayName("Descripcion")]
    public string? DescriptionProduct { get; set; }

    [DisplayName("Dinero Invertido")]
    public decimal InvestmentPrice { get; set; }

    [DisplayName("Precio de venta")]
    public decimal SalePrice { get; set; }

    [DisplayName("Stock")]
    public int Stock { get; set; }
}
