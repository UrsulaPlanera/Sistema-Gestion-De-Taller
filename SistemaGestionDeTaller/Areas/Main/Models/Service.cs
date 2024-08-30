using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace SistemaGestionDeTaller.Areas.Main.Models;

public partial class Service
{
    [DisplayName("Numero")]
    public int Id { get; set; }

    [DisplayName("Fecha")]
    public DateTime? Date { get; set; }

    [DisplayName("Patente")]
    public string Patent { get; set; } = null!;

    [DisplayName("Marca")]
    public string Brand { get; set; } = null!;

    [DisplayName("Modelo")]
    public string? Model { get; set; }

    [DisplayName("Nombre del Cliente")]
    public string? ClientName { get; set; }

    [DisplayName("Descripción")]
    public string? Description { get; set; }

    [DisplayName("Precio total")]
    public decimal TotalPrice { get; set; }
}
