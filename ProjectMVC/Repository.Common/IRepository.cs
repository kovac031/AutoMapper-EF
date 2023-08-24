using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Common
{
    public interface IRepository
    {
        Task<List<StudentDTO>> GetAllAsync();
    }
}
