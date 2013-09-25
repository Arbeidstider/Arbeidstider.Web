using Arbeidstider.Business.Logic.Domain;

namespace Arbeidstider.Business.Domain
{
    public class Workplace
    {
        public string Name { get; set; }
        public Employee Manager { get; set; }
    }
}