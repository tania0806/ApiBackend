using System;
namespace reportesApi.Models
{
    public class DetalleEntradasModel
    {
        public int Id { get; set; }
        public int IdEntrada { get; set; }
        public string Insumo { get; set; }
          public string Cantidad { get; set; }

        public decimal Costo { get; set; }
    
    }
    
}
