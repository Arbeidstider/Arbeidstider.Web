namespace Arbeidstider.Business.Domain
{
    public class Workplace
    {
        public string Name { get; set; }
        public Employer Manager { get; set; }
    }
}