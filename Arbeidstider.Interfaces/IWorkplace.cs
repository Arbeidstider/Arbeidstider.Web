﻿namespace Arbeidstider.Interfaces
{
    public interface IWorkplace
    {
        int WorkplaceID { get; set; }
        string Name { get; set; }
        IEmployee Manager { get; set; }
    }
}
