using T072_RepositoryFlow.Models;

namespace T072_RepositoryFlow.Repositories
{
	public interface IUsersRepository
	{
		public bool SaveChanges();
		public void AddEntity<T>(T entityToAdd);
		public void RemoveEntity<T>(T entityToRemove);
		public IEnumerable<UserModel> GetUsers();
		public UserModel GetUser(int userId);
		public UserSalaryModel GetUserSalary(int userId);
		public UserJobInfoModel GetUserJobInfo(int userId);
	}
}