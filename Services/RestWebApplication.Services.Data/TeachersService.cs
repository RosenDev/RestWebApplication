using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RestWebApplication.Common;
using RestWebApplication.Data.Common.Repositories;
using RestWebApplication.Data.Domain;
using RestWebApplication.Infrastructure.Helpers;
using RestWebApplication.Infrastructure.ResourceParameters;
using RestWebApplication.Services.Data.Contracts;
using RestWebApplication.Services.Mapping;
using RestWebApplication.Services.Mapping.AutoMapperExtensions;
using RestWebApplication.Services.Models;

namespace RestWebApplication.Services.Data
{
    public class TeachersService:ITeachersService
    {
        private readonly IRepository<Teacher> teachersRepository;


        public TeachersService(IRepository<Teacher> teachersRepository)
        {
            this.teachersRepository = teachersRepository;
        }
        public async Task<List<TeacherDto>> GetAllAsync(int maxResultsCount=100)
        {
            return await teachersRepository.All()
                .Take(maxResultsCount)
                .To<TeacherDto>()
                .ToListAsync();
        }

        public PagedList<TeacherDto> GetAll(TeachersResourceParameters teachersResourceParameters)
        {
            var allTeachers = teachersRepository.All().To<TeacherDto>();

            return PagedList<TeacherDto>.Create(allTeachers
                ,teachersResourceParameters.PageNumber
                ,teachersResourceParameters.PageSize);
        }

        public async Task<TeacherDto> GetAsync(string id)
        {
            ThrowHelper.ThrowIfNullEmptyOrWhitespace(id,nameof(id));

            var teacher = await teachersRepository.FindAsync(id);
            
            return teacher==null?null:teacher.To<TeacherDto>();
        }

        public async Task<string> CreateAsync(TeacherDto teacher)
        {
           var result =  teachersRepository.Add(teacher.To<Teacher>());

          await teachersRepository.SaveChangesAsync();
           return result.Id;
        }

        public async Task<bool> RemoveAsync(string id)
        {
            if (!await ExistsAsync(id))
            {
                return false;
            }
            var entityToDelete = await teachersRepository.FindAsync(id);
            
            teachersRepository.Delete(entityToDelete);
            
            await teachersRepository.SaveChangesAsync();
            return true;
        }

        public async Task<string> UpdateAsync(TeacherDto teacher)
        {
            ThrowHelper.ThrowIfNull(teacher,nameof(teacher));
            ThrowHelper.ThrowIfNullEmptyOrWhitespace(teacher.Id,nameof(teacher.Id));

            var entityForUpdate = teacher.To<Teacher>();
             teachersRepository.Update(entityForUpdate);

             await teachersRepository.SaveChangesAsync();
            
             return entityForUpdate.Id;
        }

        private async Task<bool> ExistsAsync(string id)
        {
            return await teachersRepository.FindAsync(id) != null;
        }
    }
}
