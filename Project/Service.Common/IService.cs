using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Common
{
    public interface IService
    {
        Task<List<StudentDTO>> GetAllAsync();
    }
}
