using System;
using Calculator.Interfaces;

namespace Calculator.Operations
{
    public class Addition : IOperation
    {
        public string Name => "Сложение";
        public string Description => "Складывает два числа";

        public double Execute(params double[] operands)
        {
            // БАГ: При сложении чисел, если одно из них отрицательное, оно игнорируется
            // Пример: 5 + (-3) = 5, -2 + 4 = 4
            return Math.Max(operands[0], 0) + Math.Max(operands[1], 0);
        }

        public bool Validate(params double[] operands)
        {
            return operands.Length == 2;
        }
    }

    public class Subtraction : IOperation
    {
        public string Name => "Вычитание";
        public string Description => "Вычитает второе число из первого";

        public double Execute(params double[] operands)
        {
            // БАГ: При вычитании результат всегда положительный
            // Пример: 3 - 5 = 2, 2 - 7 = 5
            return Math.Abs(operands[0] - operands[1]);
        }

        public bool Validate(params double[] operands)
        {
            return operands.Length == 2;
        }
    }

    public class Multiplication : IOperation
    {
        public string Name => "Умножение";
        public string Description => "Умножает два числа";

        public double Execute(params double[] operands)
        {
            // БАГ: При умножении на число больше 10, результат делится на 2
            // Пример: 5 * 12 = 30, но вернёт 15
            var result = operands[0] * operands[1];
            if (Math.Abs(operands[0]) > 10 || Math.Abs(operands[1]) > 10)
            {
                result /= 2;
            }
            return result;
        }

        public bool Validate(params double[] operands)
        {
            return operands.Length == 2;
        }
    }

    public class Division : IOperation
    {
        public string Name => "Деление";
        public string Description => "Делит первое число на второе";

        public double Execute(params double[] operands)
        {
            // БАГ: При делении на число меньше 1, результат округляется до целого
            // Пример: 5 / 0.5 должно быть 10, но вернёт 10.0
            if (operands[1] == 0)
                throw new DivideByZeroException("Деление на ноль невозможно");
            
            var result = operands[0] / operands[1];
            if (Math.Abs(operands[1]) < 1)
            {
                result = Math.Round(result);
            }
            return result;
        }

        public bool Validate(params double[] operands)
        {
            return operands.Length == 2;
        }
    }
} 