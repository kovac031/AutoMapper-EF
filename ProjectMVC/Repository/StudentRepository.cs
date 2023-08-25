using AutoMapper;
using DAL;
using Model;
using Repository.Common;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class StudentRepository : IRepository
    {
        public readonly IMapper _mapper; // AutoMapper
        public EFContext Context { get; set; }
        public StudentRepository(EFContext context, IMapper mapper) // IMapper dodao za AutoMapper
        {
            Context = context;
            _mapper = mapper;
        }
        //----------------- GET ALL --------------
        public async Task<List<StudentDTO>> GetAllAsync()
        {
            IQueryable<Student> student = Context.Students;

            //List<StudentDTO> list = await student.Select(x => new StudentDTO()
            //{
            //    Id = x.Id,
            //    FirstName = x.FirstName,
            //    LastName = x.LastName,
            //    DateOfBirth = x.DateOfBirth,
            //    EmailAddress = x.EmailAddress,
            //    RegisteredOn = x.RegisteredOn
            //}).ToListAsync<StudentDTO>();

            //return list;

            return await _mapper.ProjectTo<StudentDTO>(student).ToListAsync();
        }
        //--------------- GET ONE BY ID --------------
        public async Task<StudentDTO> GetOneByIdAsync(Guid id)
        {
            Student student = await Context.Students.FirstOrDefaultAsync(s => s.Id == id);

            if (student == null) { return null; }

            return _mapper.Map<StudentDTO>(student);
        }
    }
}
