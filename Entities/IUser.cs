namespace backend_aspnet_crud.Entities
{
    public interface IUser {
        int id { get; set; }
        string name { get; set; }
        string username { get; set; }
        string password { get; set; }
        string role { get; set; }
    }
}