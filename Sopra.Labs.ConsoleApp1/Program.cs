using System;

namespace Sopra.Labs.ConsoleApp1
{

    internal class Program
    {
        /// <summary>
        /// Método de inicio de la aplicación
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            while (true)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("".PadRight(56, '*'));
                Console.WriteLine("*  LABS Y EJERCICIOS".PadRight(55) + "*");
                Console.WriteLine("".PadRight(56, '*'));
                Console.WriteLine("*".PadRight(55) + "*");
                Console.WriteLine("*  1. Calcular letra DNI".PadRight(55) + "*");
                Console.WriteLine("*  2. Tabla de multiplicar".PadRight(55) + "*");
                Console.WriteLine("*  3. Secuencia numérica".PadRight(55) + "*");
                Console.WriteLine("*  4. Calcular valores".PadRight(55) + "*");
                Console.WriteLine("*  9. Salir".PadRight(55) + "*");
                Console.WriteLine("*".PadRight(55) + "*");
                Console.WriteLine("".PadRight(56, '*'));

                Console.WriteLine(Environment.NewLine);
                Console.Write("   Opción: ");

                Console.ForegroundColor = ConsoleColor.Cyan;

                int.TryParse(Console.ReadLine(), out int opcion);
                switch (opcion)
                {
                    case 1:
                        CalculaLetraDNI();
                        break;
                    case 2:
                        MostrarTablaMultiplicar();
                        break;
                    case 3:
                        SecuenciaNumerica();
                        break;
                    case 4:
                        CalcularValores();
                        break;
                    case 9:
                        return;
                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine(Environment.NewLine + $"La opción {opcion} no es valida.");
                        break;
                }

                Console.WriteLine(Environment.NewLine);
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("Pulsa una tecla para continuar...");
                Console.ReadKey();
            }

        }

        /// <summary>
        /// Calcular la letra del DNI
        /// </summary>
        static void CalculaLetraDNI()
        {
            char[] letras = { 'T', 'R', 'W', 'A', 'G', 'M', 'Y', 'F', 'P', 'D', 'X', 'B', 'N', 'J', 'Z', 'S', 'Q', 'V', 'H', 'L', 'C', 'K', 'E' };
            int numero;

            do
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("Número DNI: ");
                Console.ForegroundColor = ConsoleColor.White;
            } while(!int.TryParse(Console.ReadLine(), out numero));

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write($"DNI: ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"{numero}{letras[numero % 23]}");
            Console.ReadKey();
        }

        /// <summary>
        /// Mostar la tabla de multiplicar de un número
        /// </summary>
        static void MostrarTablaMultiplicar()
        {            
            int numero;

            do
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("Tabla de Multiplicar del número ? ");
                Console.ForegroundColor = ConsoleColor.White;
            } while (!int.TryParse(Console.ReadLine(), out numero));


            // Utiliza For
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(Environment.NewLine);
            Console.WriteLine($"===========================================================");
            Console.WriteLine($" Tabla del Multiplicar del {numero} // FOR");
            Console.WriteLine($"===========================================================");
            Console.ForegroundColor = ConsoleColor.White;

            for (int i = 1; i < 11; i++)
            {
                Console.WriteLine($" {i.ToString().PadLeft(5)} x {numero} = {(i * numero).ToString().PadLeft(8)}");
            }


            // Utiliza While
            Console.WriteLine(Environment.NewLine);
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(Environment.NewLine);
            Console.WriteLine($"===========================================================");
            Console.WriteLine($" Tabla del Multiplicar del {numero} // WHILE");
            Console.WriteLine($"===========================================================");
            Console.ForegroundColor = ConsoleColor.White;

            int contador = 0;
            while(++contador < 11)
            {
                Console.WriteLine($" {contador.ToString().PadLeft(5)} x {numero} = {(contador * numero).ToString().PadLeft(8)}");
            }

            Console.ReadKey();
        }

        /// <summary>
        /// Mostar los valores de un número a otro especificando el salto
        /// </summary>
        static void SecuenciaNumerica()
        {
            int start, end, step;

            do
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("Número de Inicio: ");
                Console.ForegroundColor = ConsoleColor.White;
            } while (!int.TryParse(Console.ReadLine(), out start));

            do
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("Número final: ");
                Console.ForegroundColor = ConsoleColor.White;
            } while (!int.TryParse(Console.ReadLine(), out end));

            do
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("Salto: ");
                Console.ForegroundColor = ConsoleColor.White;
            } while (!int.TryParse(Console.ReadLine(), out step));

            if (step < 0) step = step * -1;

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Clear();
            Console.WriteLine($"===========================================================");
            Console.WriteLine($" Del {start} al {end} de {step} en {step}");
            Console.WriteLine($"===========================================================");
            Console.ForegroundColor = ConsoleColor.White;

            if (start == end || step == 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("No se puede procesar.");
            }
            else
            {
                if(start < end) for (var i = start; i < end; i += step) Console.Write($" > {i}");
                else for (var i = start; i > end; i -= step) Console.Write($" > {i}");
                Console.WriteLine(Environment.NewLine);
            }
            Console.ReadKey(); 
        }

        /// <summary>
        /// Pregunta el número de elementos. Insertalos en un array y calcula el valor máximo, mínimo, la suma total y la media.
        /// </summary>
        static void CalcularValores()
        {
            int num;
            int max = 0, min = 0, sum = 0;

            do
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("Cuanto valores desea calcular ? ");
                Console.ForegroundColor = ConsoleColor.White;
            } while (!int.TryParse(Console.ReadLine(), out num));

            int[] values = new int[num];
            for (int i = 0; i < values.Length; i++)
            {
                do
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write($"Valor {i + 1}: ");
                    Console.ForegroundColor = ConsoleColor.White;
                } while (!int.TryParse(Console.ReadLine(), out values[i]));
            }

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Clear();
            Console.WriteLine($"===========================================================");
            Console.WriteLine($" RESULTADO DE CALCULOS");
            Console.WriteLine($"===========================================================");
            Console.ForegroundColor = ConsoleColor.White;

            for (int i = 0; i < values.Length; i++)
            { 
                sum += values[i];

                if (i == 0)
                {
                    max = values[i];
                    min = values[i];
                }
                else
                { 
                    if(values[i] > max) max = values[i];
                    if(values[i] < min) min = values[i];
                }
            }

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write($" {values.Length} Valores: ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(String.Join(',', values));

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write(" Valor Min: ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(min.ToString());
            Console.ForegroundColor = ConsoleColor.Cyan;

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write(" Valor Max: ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(max.ToString());

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write(" Suma: ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(sum.ToString());

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write(" Media: ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine((sum / values.Length).ToString());

            Console.ReadKey();
        }
    }
}
