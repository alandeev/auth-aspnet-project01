using backend_aspnet_crud.Data;
using backend_aspnet_crud.Entities;
using backend_aspnet_crud.Entities.UserDTO;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;

namespace backend_aspnet_crud.Repositories {
    public interface IUserRepository {
        public Task<bool> AddAsync(User user);
        public Task<List<User>> GetUsers();
        public Task<User> findByUsername(string username);
    }    
}