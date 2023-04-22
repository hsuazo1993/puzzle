namespace DataAccess.Repositories
{
    public interface IGetData<TDto> where TDto : class
    {
        Task<ICollection<TDto>> GetAllAsync();
        Task<TDto> GetByIdAsync(int id);
    }
}
