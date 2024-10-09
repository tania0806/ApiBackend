namespace ApiBackend.Models
{
    public class RecetasModel
    {
        public int Id {get; set;}
        public string Nombre {get; set;}
        public int Estatus {get; set;}
        public string Fecha_crecion  {get;set;}
        public int Usuario_registra {get; set;}
    }
}