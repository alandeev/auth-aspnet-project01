using System.ComponentModel.DataAnnotations;

namespace backend_aspnet_crud.Entities
{
    public class User: IUser {

        [Key]
        public int id { get; set; }
        public string name { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string role { get; set; }
    }
}