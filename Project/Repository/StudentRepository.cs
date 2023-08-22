using DataAccessLayer;
using Model;
using Repository.Common;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class StudentRepository : IRepository
    {
        public EFContext Context { get; set; }
        public StudentRepository(EFContext context)
        {
            Context = context;
        }
        public async Task<List<StudentDTO>> GetAllAsync()
        {
            IQueryable<Student> student = Context.Students;

            List<StudentDTO> list = await student.Select(x => new StudentDTO()
            {
                Id = x.Id,
                FirstName = x.FirstName,
                LastName = x.LastName,                
                DateOfBirth = x.DateOfBirth,
                EmailAddress = x.EmailAddress,
                RegisteredOn = x.RegisteredOn
            }).ToListAsync<StudentDTO>();

            return list;
        }
    }
}
