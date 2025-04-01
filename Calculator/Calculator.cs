using System;
using System.Collections.Generic;
using System.Linq;
using Calculator.Interfaces;
using Calculator.Operations;

namespace Calculator
{
    public class Calculator
    {
        private readonly Dictionary<string, IOperation> _operations;

        public Calculator()
        {
            _operations = new Dictionary<string, IOperation>
            {
                { "1", new Addition() },
                { "2", new Subtraction() },
                { "3", new Multiplication() },
                { "4", new Division() },
                { "5", new Cosine() },
                { "6", new Sine() },
                { "7", new Logarithm() },
                { "8", new Power() },
                { "9", new Factorial() }
            };
        }

        public void Start()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== Калькулятор ===");
                Console.WriteLine("Доступные операции:");
                foreach (var operation in _operations)
                {
                    Console.WriteLine($"{operation.Key}. {operation.Value.Name} - {operation.Value.Description}");
                }
                Console.WriteLine("0. Выход");
                Console.Write("\nВыберите операцию: ");

                string choice = Console.ReadLine();

                if (choice == "0")
                    break;

                if (!_operations.ContainsKey(choice))
                {
                    Console.WriteLine("Неверный выбор операции!");
                    Console.ReadKey();
                    continue;
                }

                try
                {
                    var operation = _operations[choice];
                    var operands = GetOperands(operation);
                    
                    if (operation.Validate(operands))
                    {
                        double result = operation.Execute(operands);
                        Console.WriteLine($"Результат: {result}");
                    }
                    else
                    {
                        Console.WriteLine("Ошибка: неверные параметры операции!");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка: {ex.Message}");
                }

                Console.WriteLine("\nНажмите любую клавишу для продолжения...");
                Console.ReadKey();
            }
        }

        private double[] GetOperands(IOperation operation)
        {
            var operands = new List<double>();
            Console.WriteLine($"\nОперация: {operation.Name}");
            Console.WriteLine($"Описание: {operation.Description}");

            while (true)
            {
                Console.Write("Введите число (или пустую строку для завершения): ");
                string input = Console.ReadLine();

                if (string.IsNullOrEmpty(input))
                    break;

                if (double.TryParse(input, out double number))
                {
                    operands.Add(number);
                }
                else
                {
                    Console.WriteLine("Ошибка: введите корректное число!");
                }
            }

            return operands.ToArray();
        }
    }
} 