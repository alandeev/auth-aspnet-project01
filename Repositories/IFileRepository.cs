using backend_aspnet_crud.Entities;

namespace backend_aspnet_crud.Repositories
{
    public interface IFileRepository {
        public void addFile(FileM file);
        public FileM findFileByUserId(int user_id );
        public void deleteFile(FileM file);
    }
}