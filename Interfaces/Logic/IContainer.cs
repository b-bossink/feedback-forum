using Interfaces.DTOs;

namespace Interfaces.Logic
{
    public interface IContainer<T> where T : DTO
	{
		public IEntity<T>[] GetAll();
		public IEntity<T> Get(int id);
		public CommunicationResult Delete(int id);
	}
}

