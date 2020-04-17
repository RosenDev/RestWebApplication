using RestWebApplication.Data.Common.Models;

namespace RestWebApplication.Data.Domain
{
    public class Teacher:BaseModel<string>
    {
        public string Name { get; set; }

        public int Age { get; set; }
        
        public string Subject { get; set; }
        
    }
}