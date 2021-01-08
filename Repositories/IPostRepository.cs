using backend_aspnet_crud.Data;
using backend_aspnet_crud.Entities;
using backend_aspnet_crud.Entities.UserDTO;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;

namespace backend_aspnet_crud.Repositories {
    public interface IPostRepository {
        public void AddAsync(Post post);
        public Task<List<Post>> GetPosts();
        public Task<List<Post>> getPostsByUserId(int user_id);
    }    
}