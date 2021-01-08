using backend_aspnet_crud.Data;
using backend_aspnet_crud.Entities;
using backend_aspnet_crud.Entities.UserDTO;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;

namespace backend_aspnet_crud.Repositories {
    public interface IUserRepository {
        Task<bool> AddAsync(User user);
        Task<List<User>> GetUsers();
        Task<User> findByUsername(string username);
    }    
}