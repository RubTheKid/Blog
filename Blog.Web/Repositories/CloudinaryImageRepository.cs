namespace Blog.Web.Repositories;

public interface CloudinaryImageRepository : IImageRepository
{
    public Task<string> UploadAsync(IFormFile file)
    {
        throw new NotImplementedException();
    }

}
