using System;
using Interfaces;
using Interfaces.DTOs;

namespace Logic
{
	public interface IEntity<T> where T : DTO
	{
		public CommunicationResult Create();
		public CommunicationResult Update();
		public T ToDTO();
	}
}

