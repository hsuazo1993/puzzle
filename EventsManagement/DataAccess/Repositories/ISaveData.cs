namespace DataAccess.Repositories
{
    public interface ISaveData<TDto> where TDto : class
    {
        Task<TDto> SaveAsync(TDto dto);
    }
}
