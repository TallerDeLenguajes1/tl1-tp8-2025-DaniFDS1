using System;
using System.Collections.Generic;
using System.Linq;

namespace CalculadoraHistorial
{
    public class Calculadora
    {
        private double resultadoActual;
        private List<Operacion> historial;

        // Constructor
        public Calculadora()
        {
            resultadoActual = 0;
            historial = new List<Operacion>();
        }

        // Propiedad para acceder al resultado actual
        public double ResultadoActual
        {
            get { return resultadoActual; }
        }

        // Propiedad para acceder al historial (solo lectura)
        public IReadOnlyList<Operacion> Historial
        {
            get { return historial.AsReadOnly(); }
        }

        // Método para realizar suma
        public double Sumar(double valor)
        {
            var operacion = new Operacion(resultadoActual, valor, TipoOperacion.Suma);
            historial.Add(operacion);
            resultadoActual = operacion.Resultado;
            return resultadoActual;
        }

        // Método para realizar resta
        public double Restar(double valor)
        {
            var operacion = new Operacion(resultadoActual, valor, TipoOperacion.Resta);
            historial.Add(operacion);
            resultadoActual = operacion.Resultado;
            return resultadoActual;
        }

        // Método para realizar multiplicación
        public double Multiplicar(double valor)
        {
            var operacion = new Operacion(resultadoActual, valor, TipoOperacion.Multiplicacion);
            historial.Add(operacion);
            resultadoActual = operacion.Resultado;
            return resultadoActual;
        }

        // Método para realizar división
        public double Dividir(double valor)
        {
            if (valor == 0)
                throw new InvalidOperationException("No se puede dividir por cero");
            
            var operacion = new Operacion(resultadoActual, valor, TipoOperacion.Division);
            historial.Add(operacion);
            resultadoActual = operacion.Resultado;
            return resultadoActual;
        }

        // Método para limpiar el resultado actual
        public void Limpiar()
        {
            var operacion = new Operacion(TipoOperacion.Limpiar);
            historial.Add(operacion);
            resultadoActual = 0;
        }

        // Método para limpiar el historial completo
        public void LimpiarHistorial()
        {
            historial.Clear();
        }

        // Método para mostrar el historial
        public void MostrarHistorial()
        {
            if (historial.Count == 0)
            {
                Console.WriteLine("No hay operaciones en el historial.");
                return;
            }

            Console.WriteLine("\n=== HISTORIAL DE OPERACIONES ===");
            for (int i = 0; i < historial.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {historial[i]}");
            }
            Console.WriteLine("================================\n");
        }

        // Método para obtener la última operación
        public Operacion ObtenerUltimaOperacion()
        {
            return historial.LastOrDefault();
        }

        // Método para obtener el número de operaciones en el historial
        public int CantidadOperaciones()
        {
            return historial.Count;
        }
    }
}