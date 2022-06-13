using System.Collections.Generic;
using Interfaces.DTOs;

namespace Interfaces
{
	public interface IDAL<T> where T : DTO
	{
        public int Upload(T dto);
        public int Delete(int id);
        public int Update(T dto);
        public List<T> LoadAll();
    }
}

