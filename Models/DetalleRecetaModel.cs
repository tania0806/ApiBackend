namespace ApiBackend.Models
{
    public class DetalleRecetaModel
    {
        public int Id {get; set;}
        public int IdReceta {get; set;}
        public string Insumo {get; set;}
        public decimal Cantidad  {get;set;}
        public string Fecha_registro  {get;set;}
        public int Estatus {get; set;}
        public int Usuario_registra {get; set;}
    }
}