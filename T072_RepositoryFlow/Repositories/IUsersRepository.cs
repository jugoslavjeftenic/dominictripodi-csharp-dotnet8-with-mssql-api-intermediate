namespace T072_RepositoryFlow.Repositories
{
	public interface IUsersRepository
	{
		void AddEntity<T>(T entityToAdd);
		void RemoveEntity<T>(T entityToRemove);
		bool SaveChanges();
	}
}