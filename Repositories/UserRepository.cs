using backend_aspnet_crud.Data;
using backend_aspnet_crud.Entities;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using System;
using Microsoft.EntityFrameworkCore;

namespace backend_aspnet_crud.Repositories {
    public class UserRepository: IUserRepository {
        private readonly DataContext _dataContext;
        public UserRepository(DataContext dataContext){
            this._dataContext = dataContext;
        }

        public async Task<bool> AddAsync(User user){
            if(await this.findByUsername(user.username) != null){
                throw new Exception("user already exists");
            }
            this._dataContext.Users.Add(user);
            await this._dataContext.SaveChangesAsync();
            return true;
        }

        public async Task<User> findByUsername(string username){
            return await this._dataContext.Users
            .AsNoTracking()
            .FirstOrDefaultAsync((user) => user.username == username);
        }

        public async Task<List<User>> GetUsers() {
           return await this._dataContext.Users
            .AsNoTracking()
            .ToListAsync();
        }
    }    
}