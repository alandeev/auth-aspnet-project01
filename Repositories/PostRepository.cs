using backend_aspnet_crud.Data;
using backend_aspnet_crud.Entities;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using System;
using Microsoft.EntityFrameworkCore;

namespace backend_aspnet_crud.Repositories
{
    public class PostRepository: IPostRepository {
        private readonly DataContext _dataContext;
        public PostRepository(DataContext dataContext){
            this._dataContext = dataContext;
        }

        public async void AddAsync(Post post) {
            this._dataContext.Posts.Add(post);
            await this._dataContext.SaveChangesAsync();
        }

        public Task<List<Post>> GetPosts() {
            return this._dataContext.Posts.Include(x => x.User).Include(x => x.User.Image).ToListAsync();
        }

        public Task<List<Post>> getPostsByUserId(int user_id) {
            return this._dataContext.Posts.Where((post) => post.UserId == user_id).Include(x => x.User).Include(x => x.User.Image).ToListAsync();
        }
    }    
}