using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace SistemaGestionDeTaller.Areas.Main.Models;

public partial class Concept
{
    public int Id { get; set; }

    public int? SaleNumber { get; set; }

    [DisplayName("Producto")]
    public string NameProduct { get; set; } = null!;
    [DisplayName("Precio Unidad")]
    public decimal UnitPrice { get; set; }
    [DisplayName("Unidades")]
    public int Units { get; set; }
    [DisplayName("Precio total")]
    public decimal? TotalPrice { get; set; }

    public virtual Sale? SaleNumberNavigation { get; set; }
}
