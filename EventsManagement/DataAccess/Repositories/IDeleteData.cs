namespace DataAccess.Repositories
{
    public interface IDeleteData
    {
        Task<bool> DeleteAsync(int id);
    }
}
