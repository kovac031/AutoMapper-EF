﻿using AutoMapper;
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
        //--------------- CREATE NEW --------------
        public async Task<bool> CreateAsync(StudentDTO student)
        {
            try
            {
                Student newStudent = _mapper.Map<Student>(student);

                newStudent.Id = Guid.NewGuid(); // mapper inace trazi sve, sto ne dobije bude null
                newStudent.RegisteredOn = DateTime.Now; // *

                Context.Students.Add(newStudent);

                await Context.SaveChangesAsync();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        //--------------- EDIT --------------
        public async Task<bool> EditAsync(StudentDTO student, Guid id)
        {
            try
            {
                Student existingStudent = await Context.Students.FindAsync(id);

                if (existingStudent == null) { return false; }

                _mapper.Map(student, existingStudent);

                // ovdje bi islo automatsko editiranje, npr editedBy ili timeEdited i sl

                await Context.SaveChangesAsync();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
