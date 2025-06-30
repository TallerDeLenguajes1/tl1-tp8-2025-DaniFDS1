using System;

namespace CalculadoraHistorial
{
    public class Operacion
    {
        private double resultadoAnterior; // Almacena el resultado previo al cálculo actual
        private double nuevoValor; // El valor con el que se opera sobre el resultadoAnterior
        private TipoOperacion operacion; // El tipo de operación realizada
        private double resultado; // Almacena el resultado calculado

        // Constructor
        public Operacion(double resultadoAnterior, double nuevoValor, TipoOperacion operacion)
        {
            this.resultadoAnterior = resultadoAnterior;
            this.nuevoValor = nuevoValor;
            this.operacion = operacion;
            this.resultado = CalcularResultado();
        }

        // Constructor para operación de limpiar
        public Operacion(TipoOperacion operacion)
        {
            if (operacion != TipoOperacion.Limpiar)
                throw new ArgumentException("Este constructor solo es válido para operaciones de tipo Limpiar");
            
            this.operacion = operacion;
            this.resultadoAnterior = 0;
            this.nuevoValor = 0;
            this.resultado = 0;
        }

        // Propiedad pública para acceder al resultado
        public double Resultado
        {
            get { return resultado; }
        }

        // Propiedad pública para acceder al nuevo valor utilizado en la operación
        public double NuevoValor
        {
            get { return nuevoValor; }
        }

        // Propiedad para acceder al resultado anterior
        public double ResultadoAnterior
        {
            get { return resultadoAnterior; }
        }

        // Propiedad para acceder al tipo de operación
        public TipoOperacion TipoOperacion
        {
            get { return operacion; }
        }

        // Método privado para calcular el resultado según el tipo de operación
        private double CalcularResultado()
        {
            switch (operacion)
            {
                case TipoOperacion.Suma:
                    return resultadoAnterior + nuevoValor;
                case TipoOperacion.Resta:
                    return resultadoAnterior - nuevoValor;
                case TipoOperacion.Multiplicacion:
                    return resultadoAnterior * nuevoValor;
                case TipoOperacion.Division:
                    if (nuevoValor == 0)
                        throw new InvalidOperationException("No se puede dividir por cero");
                    return resultadoAnterior / nuevoValor;
                case TipoOperacion.Limpiar:
                    return 0;
                default:
                    throw new InvalidOperationException("Tipo de operación no válida");
            }
        }

        // Método para obtener una representación en string de la operación
        public override string ToString()
        {
            if (operacion == TipoOperacion.Limpiar)
                return "Operación: Limpiar";

            string simbolo = operacion switch
            {
                TipoOperacion.Suma => "+",
                TipoOperacion.Resta => "-",
                TipoOperacion.Multiplicacion => "*",
                TipoOperacion.Division => "/",
                _ => "?"
            };

            return $"{resultadoAnterior} {simbolo} {nuevoValor} = {resultado}";
        }
    }
}