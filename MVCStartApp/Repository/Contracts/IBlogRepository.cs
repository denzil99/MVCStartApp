using MVCStartApp.Models.Db;

namespace MVCStartApp.Repository.Contracts
{
	public interface IBlogRepository
	{
		Task AddUser(User user);
		Task<User[]> GetUsers();
	}
}
