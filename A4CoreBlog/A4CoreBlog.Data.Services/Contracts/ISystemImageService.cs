namespace A4CoreBlog.Data.Services.Contracts
{
    public interface ISystemImageService
    {
        T Get<T>(int id);
        T AddOrUpdate<T>(T model);
    }
}
