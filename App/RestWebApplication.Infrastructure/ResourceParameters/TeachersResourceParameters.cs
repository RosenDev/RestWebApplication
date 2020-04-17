namespace RestWebApplication.Infrastructure.ResourceParameters
{
    
    //maybe can be a struct?
    public class TeachersResourceParameters
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string Fields { get; set; }

    }
}