using T072_RepositoryFlow.Data;

namespace T072_RepositoryFlow.Repositories
{
	public class UsersRepository(IConfiguration config)
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
	}
}
