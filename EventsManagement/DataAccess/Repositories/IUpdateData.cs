namespace DataAccess.Repositories
{
    public interface IUpdateData<TDto> where TDto : class
    {
        Task<bool> UpdateAsync(int id, TDto dto);
    }
}
