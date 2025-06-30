using System;

namespace CalculadoraHistorial
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== CALCULADORA CON HISTORIAL ===");
            Console.WriteLine("Bienvenido a la calculadora con historial de operaciones\n");

            Calculadora calculadora = new Calculadora();
            bool continuar = true;

            while (continuar)
            {
                MostrarMenu();
                Console.WriteLine($"Resultado actual: {calculadora.ResultadoActual}");
                Console.Write("Seleccione una opción (1-8): ");
                
                string opcion = Console.ReadLine();

                try
                {
                    switch (opcion)
                    {
                        case "1":
                            RealizarOperacion(calculadora, TipoOperacion.Suma);
                            break;
                        case "2":
                            RealizarOperacion(calculadora, TipoOperacion.Resta);
                            break;
                        case "3":
                            RealizarOperacion(calculadora, TipoOperacion.Multiplicacion);
                            break;
                        case "4":
                            RealizarOperacion(calculadora, TipoOperacion.Division);
                            break;
                        case "5":
                            calculadora.Limpiar();
                            Console.WriteLine("Resultado limpiado.");
                            break;
                        case "6":
                            calculadora.MostrarHistorial();
                            break;
                        case "7":
                            calculadora.LimpiarHistorial();
                            Console.WriteLine("Historial limpiado.");
                            break;
                        case "8":
                            continuar = false;
                            Console.WriteLine("¡Gracias por usar la calculadora!");
                            break;
                        default:
                            Console.WriteLine("Opción no válida. Por favor, seleccione una opción del 1 al 8.");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }

                if (continuar)
                {
                    Console.WriteLine("\nPresione cualquier tecla para continuar...");
                    Console.ReadKey();
                    Console.Clear();
                }
            }
        }

        static void MostrarMenu()
        {
            Console.WriteLine("=== MENÚ DE OPCIONES ===");
            Console.WriteLine("1. Sumar");
            Console.WriteLine("2. Restar");
            Console.WriteLine("3. Multiplicar");
            Console.WriteLine("4. Dividir");
            Console.WriteLine("5. Limpiar resultado");
            Console.WriteLine("6. Mostrar historial");
            Console.WriteLine("7. Limpiar historial");
            Console.WriteLine("8. Salir");
            Console.WriteLine("========================");
        }

        static void RealizarOperacion(Calculadora calculadora, TipoOperacion tipo)
        {
            string nombreOperacion = tipo switch
            {
                TipoOperacion.Suma => "sumar",
                TipoOperacion.Resta => "restar",
                TipoOperacion.Multiplicacion => "multiplicar",
                TipoOperacion.Division => "dividir",
                _ => "operar"
            };

            Console.Write($"Ingrese el valor a {nombreOperacion}: ");
            string input = Console.ReadLine();

            if (double.TryParse(input, out double valor))
            {
                double resultado = tipo switch
                {
                    TipoOperacion.Suma => calculadora.Sumar(valor),
                    TipoOperacion.Resta => calculadora.Restar(valor),
                    TipoOperacion.Multiplicacion => calculadora.Multiplicar(valor),
                    TipoOperacion.Division => calculadora.Dividir(valor),
                    _ => throw new InvalidOperationException("Operación no válida")
                };

                Console.WriteLine($"Resultado: {resultado}");
            }
            else
            {
                Console.WriteLine("Por favor, ingrese un número válido.");
            }
        }
    }
}