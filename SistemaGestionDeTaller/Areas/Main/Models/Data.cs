using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SistemaGestionDeTaller.Areas.Main.Models;

public partial class Data
{
    public int Id { get; set; }

    [DisplayName("Calle")]
    [Required(ErrorMessage ="Ingresar calle.")]
    public string Street { get; set; } = null!;

    [DisplayName("Numero")]
    [Required(ErrorMessage = "Ingresar numero o altura.")]
    public int Number { get; set; }

    [DisplayName("Localidad")]
    [Required(ErrorMessage = "Ingresar localidad.")]
    public string Locality { get; set; } = null!;

    [DisplayName("Código postal")]
    [Required(ErrorMessage = "Ingresar numero de código postal.")]
    public int Cp { get; set; }
}
