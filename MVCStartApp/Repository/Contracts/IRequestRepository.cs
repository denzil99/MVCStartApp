using MVCStartApp.Models.Db;

namespace MVCStartApp.Repository.Contracts
{
    public interface IRequestRepository
    {
        Task AddRequest(Request request);
        Task<Request[]> GetRequests();
    }
}
