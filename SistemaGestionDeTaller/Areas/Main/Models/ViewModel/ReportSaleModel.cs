namespace SistemaGestionDeTaller.Areas.Main.Models.ViewModel
{
    public class ReportSaleModel
    {
        public Data DataConfig { set; get; } = null!;

        public Sale SaleObjetct { get; set; } = null!;

        public List<Concept> ConceptsList { get; set; } = null!;
    }
}