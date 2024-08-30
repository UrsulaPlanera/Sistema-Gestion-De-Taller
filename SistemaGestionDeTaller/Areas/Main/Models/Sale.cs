using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SistemaGestionDeTaller.Areas.Main.Models;

public partial class Sale
{
    [DisplayName("Numero de venta")]
    public int Id { get; set; }

    [DisplayName("Fecha y hora")]
    public DateTime Date { get; set; }

    [DisplayName("Total de venta")]
    public decimal TotalPrice { get; set; }

    [DisplayName("Detalle")]
    public virtual ICollection<Concept> Concepts { get; set; } = new List<Concept>();
}