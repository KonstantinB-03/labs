using System.Data;

namespace Errors
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string message = "error";
            bool error = false;

            Console.WriteLine("Программа по решению квадратного уравнения.\n");
            while (true)
            {
                error = false;

                try
                {
                    Console.WriteLine("Создайте в директории программы файл example.txt \n" +
                        "и через пробел укажите три коэфициенты a, b, c квадратного уравнения a*x*x+b*x+c=0.\n" +
                        "Нажмите любую кнопку, чтобы начать.");
                    Console.ReadKey();

                    //Читаем данные из файла
                    StreamReader reader = new StreamReader("example.txt");
                    string[] info = reader.ReadToEnd().Split(" ");
                    reader.Close();

                    //Проверка на корректность коэффициентов (возможно исключение из-за отсутвстия элементов массива)
                    if (!double.TryParse(info[0], out double a) | !double.TryParse(info[1], out double b) |
                        !double.TryParse(info[2], out double c))
                    {
                        throw new CoefficientException("Коэффициенты не являются числами!");
                    }

                    double dis = b * b - 4 * a * c;

                    //Проверка на корректность дискриминанта
                    if (dis > 0)
                    {
                        double x1 = (-b + Math.Sqrt(dis)) / 2;
                        double x2 = (-b - Math.Sqrt(dis)) / 2;
                        Console.WriteLine($"Result: x1 = {x1}, x2 = {x2}");
                    }
                    else if (dis == 0)
                    {
                        double x = (-b) / 2;
                        Console.WriteLine($"Result: x1 = {x}");
                        throw new DisZeroException("Дискриминант равен нуля, невозможно найти второй корень.");
                    }
                    else
                    {
                        throw new DisNegativeException("Дискриминант меньше нуля!");
                    }
                }
                catch (FileNotFoundException e)
                {
                    message = e.Message;
                    error = true;
                }
                catch (CoefficientException e)
                {
                    message = e.Message;
                    error = true;
                }
                catch (DisZeroException e)
                {
                    message = e.Message;
                    error = true;
                }
                catch (DisNegativeException e)
                {
                    message = e.Message;
                    error = true;
                }
                catch (Exception e)
                {
                    //Вероятная ошибка в массиве коэффициентов
                    message = e.Message;
                    error = true;
                }
                finally
                {
                    if (!error)
                    {
                        Console.WriteLine("Программа завершилась без ошибок.\n");
                    }
                    else
                    {
                        Console.WriteLine($"Программа завершилась c ошибкой: {message}\n");
                    }
                }

                Console.WriteLine("Нажмите любую кнопку, чтобы начать заново.\n");
                Console.ReadKey();
            }
        }

        class DisNegativeException : Exception
        {
            public DisNegativeException(string message) :base(message) { }

        }
        class DisZeroException : Exception
        {
            public DisZeroException(string message) : base(message) { }
        }

        class CoefficientException : Exception
        {
            public CoefficientException(string message) : base(message) { }
        }
    }
}