using System;

namespace Calculator.Interfaces
{
    public interface IOperation
    {
        string Name { get; }
        string Description { get; }
        double Execute(params double[] operands);
        bool Validate(params double[] operands);
    }
} 