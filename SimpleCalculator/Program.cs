using System;

namespace SimpleCalculator
{
    class Program
    {
        static ILogger Logger { get; set; }

        static void Main(string[] args)
        {
            
            Console.WriteLine("Калькулятор может сложить два числа.");
            Console.WriteLine();

            Logger = new Logger();

            BaseCalculator baseCalculator = new BaseCalculator(Logger);

            int a = baseCalculator.ReadNumber("Введите первое число: "); // запрашиваем первое число
            int b = baseCalculator.ReadNumber("Введите второе число: "); // запрашиваем второе число

            int r = baseCalculator.Add(a, b); // складываем

            Console.WriteLine("Вот что у нас получилось: {0} + {1} = {2}", a, b, r);

            Console.ReadKey();
        }
    }

    public interface ICalculator
    {
        int Add(int a, int b);
    }

    public interface ILogger
    {
        void Event(string message);
        void Error(string message);
        void Question(string message);
    }

    /// <summary>
    /// Калькулятор с функцией сложения чисел
    /// </summary>
    public class BaseCalculator : ICalculator
    {
        ILogger Logger { get; }
        public BaseCalculator(ILogger logger)
        {
            Logger = logger;
        }

        /// <summary>
        /// Запросить число.
        /// </summary>
        /// <returns></returns>
        public int ReadNumber(string question)
        {
            int a = 0;
            bool c = true;

            do
            {
                Logger.Question(question);

                try
                {
                    a = Convert.ToInt32(Console.ReadLine());
                    c = true;
                }
                catch (FormatException)
                {
                    c = false;
                    Logger.Error("Введено некорректное значение!");
                }

            } while (c == false);

            return a;
        }

        /// <summary>
        /// Сложить два числа
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public int Add(int a, int b)
        {
            Logger.Event("Начинаем складывать числа...");
            return a + b;
        }
    }

    public class Logger : ILogger
    {
        public void Error(string message)
        {
            Console.BackgroundColor = ConsoleColor.Red;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine(message);
        }

        public void Event(string message)
        {
            Console.BackgroundColor = ConsoleColor.Green;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine(message);
        }

        public void Question(string message)
        {
            Console.BackgroundColor = ConsoleColor.Yellow;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine(message);
        }
    }
}
