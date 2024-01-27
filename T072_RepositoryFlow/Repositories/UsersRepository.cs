using T072_RepositoryFlow.Data;
using T072_RepositoryFlow.Models;

namespace T072_RepositoryFlow.Repositories
{
	public class UsersRepository(IConfiguration config) : IUsersRepository
	{
		private readonly DataContextEF _ef = new(config);

		public bool SaveChanges()
		{
			return _ef.SaveChanges() > 0;
		}

		public void AddEntity<T>(T entityToAdd)
		{
			if (entityToAdd != null)
			{
				_ef.Add(entityToAdd);
			}
		}

		public void RemoveEntity<T>(T entityToRemove)
		{
			if (entityToRemove != null)
			{
				_ef.Add(entityToRemove);
			}
		}

		public IEnumerable<UserModel> GetUsers()
		{
			return _ef.Users.ToList();
		}

		public UserModel GetUser(int userId)
		{
			return _ef.Users
				.Where(u => u.UserId == userId)
				.FirstOrDefault() ?? throw new Exception($"Failed to Get User with ID: {userId}.");
		}

		public UserSalaryModel GetUserSalary(int userId)
		{
			return _ef.UserSalary
				.Where(u => u.UserId == userId)
				.FirstOrDefault() ?? throw new Exception($"Failed to Get User with ID: {userId}.");
		}

		public UserJobInfoModel GetUserJobInfo(int userId)
		{
			return _ef.UserJobInfo
				.Where(u => u.UserId == userId)
				.FirstOrDefault() ?? throw new Exception($"Failed to Get User with ID: {userId}.");
		}
	}
}
