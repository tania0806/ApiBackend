namespace ApiBackend.Models
{
    public class OrdenCompraModel
    {
        public int Id {get; set;}
         public int IdProveedor {get; set;}
        public int IdSucursal {get; set;}
         public int IdComprador {get; set;}
        public int Estatus {get; set;}
        public string FechaLLegada  {get;set;}
        public string Fecha  {get;set;}
        public int Usuario_registra {get; set;}
    }
}