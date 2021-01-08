using System.Threading.Tasks;
using backend_aspnet_crud.Data;
using backend_aspnet_crud.Entities;
using System.Linq;

namespace backend_aspnet_crud.Repositories
{
    public class FileRepository: IFileRepository {
        private readonly DataContext _dataContext;
        public FileRepository(DataContext dataContext) {
            this._dataContext = dataContext;
        }

        public async void addFile(FileM file) {
            this._dataContext.Files.Add(file);
            await this._dataContext.SaveChangesAsync();
        }

        public FileM findFileByUserId(int user_id){
            return this._dataContext.Files.FirstOrDefault((file) => file.UserId == user_id);
        }
    }
}