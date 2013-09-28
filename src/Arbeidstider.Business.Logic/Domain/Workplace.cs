namespace Arbeidstider.Business.Logic.Domain
{
    public class Workplace
    {
        public string Name { get; set; }
        public Employee Manager { get; set; }
    }
}