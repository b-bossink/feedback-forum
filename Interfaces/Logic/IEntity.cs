using Interfaces.DTOs;

namespace Interfaces.Logic
{
    public interface IEntity<T> where T : DTO
	{
		public CommunicationResult Create();
		public CommunicationResult Update();
		public T ToDTO();
	}
}

