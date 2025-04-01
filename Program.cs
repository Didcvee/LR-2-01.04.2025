using System;
using Calculator.Operations;
using Calculator.Interfaces;

class Program
{
    static void Main(string[] args)
    {
        var calculator = new Calculator.Calculator();
        calculator.Start();
    }
}
