using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RestWebApplication.Infrastructure.Helpers;
using RestWebApplication.Infrastructure.ResourceParameters;
using RestWebApplication.Services.Models;

namespace RestWebApplication.Services.Data.Contracts
{
    public interface ITeachersService
    {
         Task<List<TeacherDto>> GetAllAsync(int maxResultsCount=100);

         PagedList<TeacherDto> GetAll(TeachersResourceParameters teachersResourceParameters);

         Task<TeacherDto> GetAsync(string id);
        
         Task<string> CreateAsync(TeacherDto teacher);

         Task<bool> RemoveAsync(string id);

         Task<string> UpdateAsync(TeacherDto teacher);
    }
}