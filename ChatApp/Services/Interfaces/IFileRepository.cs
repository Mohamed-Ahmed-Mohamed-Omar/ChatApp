namespace ChatApp.Services.Interfaces
{
    public interface IFileRepository
    {
        public Task<string> UploadFile(string Location, IFormFile file);
    }
}
