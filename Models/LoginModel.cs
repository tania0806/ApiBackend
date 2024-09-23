using System;
namespace reportesApi.Models
{
    public class GetLoginModel
    {
        public int Id { get; set; }
        public string Nombres{ get; set; }
         public string Apellidos{ get; set; }
        public string Correo { get; set; }
        public string Contraseña { get; set; }
        public int NumeroTelefono { get; set; }
        public string Token { get; set; }
    }

     public class InsertRegistroModel 
    {
        public string Nombres { get; set; }
         public string Apellidos{ get; set; }
        public int NumeroTelefono{ get; set; }
        public string Correo { get; set; }
        public string Contraseña { get; set; }
    }

}