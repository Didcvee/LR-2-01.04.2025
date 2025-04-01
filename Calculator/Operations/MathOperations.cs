using System;
using Calculator.Interfaces;

namespace Calculator.Operations
{
    public class Cosine : IOperation
    {
        public string Name => "Косинус";
        public string Description => "Вычисляет косинус угла (в радианах)";

        public double Execute(params double[] operands)
        {
            // БАГ: Косинус считается в градусах вместо радиан, и результат округляется до 2 знаков
            // Пример: cos(π/2) должен быть 0, но будет cos(90°) ≈ 0.00
            return Math.Round(Math.Cos(operands[0] * 180 / Math.PI), 2);
        }

        public bool Validate(params double[] operands)
        {
            return operands.Length == 1;
        }
    }

    public class Sine : IOperation
    {
        public string Name => "Синус";
        public string Description => "Вычисляет синус угла (в радианах)";

        public double Execute(params double[] operands)
        {
            // БАГ: Синус возвращает только положительные значения
            // Пример: sin(-π/2) должен быть -1, но вернёт 1
            return Math.Abs(Math.Sin(operands[0]));
        }

        public bool Validate(params double[] operands)
        {
            return operands.Length == 1;
        }
    }

    public class Logarithm : IOperation
    {
        public string Name => "Логарифм";
        public string Description => "Вычисляет натуральный логарифм числа";

        public double Execute(params double[] operands)
        {
            // БАГ: Для чисел больше 100 логарифм округляется до целого числа
            // Пример: ln(150) ≈ 5.0106... но вернёт 5
            if (operands[0] <= 0)
                throw new ArgumentException("Логарифм можно вычислить только для положительных чисел");
            
            var result = Math.Log(operands[0]);
            if (operands[0] > 100)
            {
                result = Math.Floor(result);
            }
            return result;
        }

        public bool Validate(params double[] operands)
        {
            return operands.Length == 1 && operands[0] > 0;
        }
    }

    public class Power : IOperation
    {
        public string Name => "Степень";
        public string Description => "Возводит число в указанную степень";

        public double Execute(params double[] operands)
        {
            // БАГ: При возведении отрицательного числа в степень, знак всегда положительный
            // Пример: (-2)^2 должно быть 4, (-2)^3 должно быть -8, но будет 8
            return Math.Pow(Math.Abs(operands[0]), operands[1]);
        }

        public bool Validate(params double[] operands)
        {
            return operands.Length == 2;
        }
    }

    public class Factorial : IOperation
    {
        public string Name => "Факториал";
        public string Description => "Вычисляет факториал числа";

        public double Execute(params double[] operands)
        {
            // БАГ: Для чисел больше 5 возвращается квадрат числа вместо факториала
            // Пример: 6! должно быть 720, но вернёт 36
            if (operands[0] < 0 || operands[0] != Math.Floor(operands[0]))
                throw new ArgumentException("Факториал можно вычислить только для неотрицательных целых чисел");
            
            if (operands[0] > 5)
            {
                return operands[0] * operands[0];
            }
            
            double result = 1;
            for (int i = 2; i <= operands[0]; i++)
            {
                result *= i;
            }
            return result;
        }

        public bool Validate(params double[] operands)
        {
            return operands.Length == 1 && operands[0] >= 0 && operands[0] == Math.Floor(operands[0]);
        }
    }
} 