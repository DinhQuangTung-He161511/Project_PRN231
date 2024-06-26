

namespace Service.Common
{
    public interface IStorageService
    {
        public string GetFileUrl(string filename);
        public Task SaveFileAsync(Stream mediaBinaryStream, string filename);
        public Task DeleteFileAsync(string filename);

    }
}
