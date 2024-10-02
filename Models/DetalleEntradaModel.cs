using System;
namespace reportesApi.Models
{
    public class DetalleEntradasModel
    {
        public int Id { get; set; }
        public int IdEntrada { get; set; }
        public string Insumo { get; set; }
          public string Cantidad { get; set; }
        public decimal SinCargo { get; set; }
        public decimal Costo { get; set; }
        public int Estatus { get; set; }
        public int Usuario_registra { get; set; }
        public string Fecha_registro { get; set; }

    }
}
