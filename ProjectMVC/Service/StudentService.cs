using Model;
using Repository.Common;
using Service.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class StudentService : IService
    {
        public IRepository Repository { get; set; }
        public StudentService(IRepository repository)
        {
            Repository = repository;
        }
        public async Task<List<StudentDTO>> GetAllAsync()
        {
            List<StudentDTO> list = await Repository.GetAllAsync();
            return list;
        }
        public async Task<StudentDTO> GetOneByIdAsync(Guid id)
        {
            StudentDTO student = await Repository.GetOneByIdAsync(id);
            return student;
        }
    }
}
