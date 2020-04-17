using System;
using RestWebApplication.Data.Domain;
using RestWebApplication.Services.Mapping;
using RestWebApplication.Services.Mapping.Contracts;

namespace RestWebApplication.Services.Models
{
    public class TeacherDto:IMapFrom<Teacher>,IMapTo<Teacher>
    {
        public string Id { get; set; }
     
        public string Name { get; set; }

        public int Age { get; set; }

        public string Subject { get; set; }
        
    }
}