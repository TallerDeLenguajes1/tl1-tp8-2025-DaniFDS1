using System;
using System.Collections.Generic;
using System.Linq;

namespace ToDo
{
    public class Tarea
    {
        public int TareaID { get; set; }
        public string Descripcion { get; set; }
        private int _duracion;
        
        public int Duracion 
        { 
            get => _duracion;
            set 
            {
                if (value >= 10 && value <= 100)
                    _duracion = value;
                else
                    throw new ArgumentException("La duración debe estar entre 10 y 100 minutos");
            }
        }

        public Tarea(int id, string descripcion, int duracion)
        {
            TareaID = id;
            Descripcion = descripcion;
            Duracion = duracion; // Usa la propiedad para validar
        }

        public override string ToString()
        {
            return $"ID: {TareaID} - {Descripcion} ({Duracion} min)";
        }
    }

    class Program
    {
        private static List<Tarea> tareasPendientes = new List<Tarea>();
        private static List<Tarea> tareasRealizadas = new List<Tarea>();
        private static Random random = new Random();

        static void Main(string[] args)
        {
            Console.WriteLine("=== SISTEMA DE GESTIÓN DE TAREAS ===\n");
            
            // Generar tareas aleatorias al inicio
            GenerarTareasAleatorias();
            
            int opcion;
            do
            {
                MostrarMenu();
                opcion = LeerOpcion();
                ProcesarOpcion(opcion);
                
                if (opcion != 0)
                {
                    Console.WriteLine("\nPresione cualquier tecla para continuar...");
                    Console.ReadKey();
                }
                
            } while (opcion != 0);
        }

        static void MostrarMenu()
        {
            Console.Clear();
            Console.WriteLine("=== MENÚ PRINCIPAL ===");
            Console.WriteLine("1. Mover tarea pendiente a realizada");
            Console.WriteLine("2. Buscar tarea pendiente por descripción");
            Console.WriteLine("3. Mostrar todas las tareas");
            Console.WriteLine("4. Mostrar solo tareas pendientes");
            Console.WriteLine("5. Mostrar solo tareas realizadas");
            Console.WriteLine("6. Generar nuevas tareas aleatorias");
            Console.WriteLine("0. Salir");
            Console.Write("\nSeleccione una opción: ");
        }

        static int LeerOpcion()
        {
            string input = Console.ReadLine();
            if (int.TryParse(input, out int opcion))
                return opcion;
            return -1;
        }

        static void ProcesarOpcion(int opcion)
        {
            switch (opcion)
            {
                case 1:
                    MoverTareaPendienteARealizada();
                    break;
                case 2:
                    BuscarTareaPorDescripcion();
                    break;
                case 3:
                    MostrarTodasLasTareas();
                    break;
                case 4:
                    MostrarTareasPendientes();
                    break;
                case 5:
                    MostrarTareasRealizadas();
                    break;
                case 6:
                    GenerarTareasAleatorias();
                    break;
                case 0:
                    Console.WriteLine("¡Hasta luego!");
                    break;
                default:
                    Console.WriteLine("Opción no válida. Intente nuevamente.");
                    break;
            }
        }

        static void GenerarTareasAleatorias()
        {
            Console.Write("¿Cuántas tareas desea generar? ");
            if (int.TryParse(Console.ReadLine(), out int n) && n > 0)
            {
                string[] descripciones = {
                    "Revisar documentación",
                    "Actualizar base de datos",
                    "Preparar presentación",
                    "Llamar a cliente",
                    "Enviar informe mensual",
                    "Organizar reunión equipo",
                    "Revisar código fuente",
                    "Backup del sistema",
                    "Capacitación personal",
                    "Mantenimiento servidor",
                    "Analizar métricas",
                    "Planificar sprint",
                    "Testear nueva funcionalidad",
                    "Documentar proceso",
                    "Coordinar con proveedores"
                };

                int idInicial = tareasPendientes.Count + tareasRealizadas.Count + 1;
                
                for (int i = 0; i < n; i++)
                {
                    string descripcion = descripciones[random.Next(descripciones.Length)];
                    int duracion = random.Next(10, 101); // Entre 10 y 100
                    
                    try
                    {
                        Tarea nuevaTarea = new Tarea(idInicial + i, descripcion, duracion);
                        tareasPendientes.Add(nuevaTarea);
                    }
                    catch (ArgumentException ex)
                    {
                        Console.WriteLine($"Error al crear tarea: {ex.Message}");
                    }
                }
                
                Console.WriteLine($"Se generaron {n} tareas pendientes exitosamente.");
            }
            else
            {
                Console.WriteLine("Número no válido.");
            }
        }

        static void MoverTareaPendienteARealizada()
        {
            if (tareasPendientes.Count == 0)
            {
                Console.WriteLine("No hay tareas pendientes para mover.");
                return;
            }

            Console.WriteLine("\n=== TAREAS PENDIENTES ===");
            for (int i = 0; i < tareasPendientes.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {tareasPendientes[i]}");
            }

            Console.Write("\nIngrese el número de la tarea a marcar como realizada: ");
            if (int.TryParse(Console.ReadLine(), out int indice) && indice >= 1 && indice <= tareasPendientes.Count)
            {
                Tarea tareaSeleccionada = tareasPendientes[indice - 1];
                tareasPendientes.RemoveAt(indice - 1);
                tareasRealizadas.Add(tareaSeleccionada);
                
                Console.WriteLine($"Tarea '{tareaSeleccionada.Descripcion}' marcada como realizada.");
            }
            else
            {
                Console.WriteLine("Número no válido.");
            }
        }

        static void BuscarTareaPorDescripcion()
        {
            if (tareasPendientes.Count == 0)
            {
                Console.WriteLine("No hay tareas pendientes para buscar.");
                return;
            }

            Console.Write("Ingrese la descripción a buscar: ");
            string busqueda = Console.ReadLine()?.ToLower() ?? "";

            if (string.IsNullOrWhiteSpace(busqueda))
            {
                Console.WriteLine("Debe ingresar una descripción válida.");
                return;
            }

            var tareasEncontradas = tareasPendientes
                .Where(t => t.Descripcion.ToLower().Contains(busqueda))
                .ToList();

            if (tareasEncontradas.Count > 0)
            {
                Console.WriteLine($"\n=== TAREAS ENCONTRADAS ({tareasEncontradas.Count}) ===");
                foreach (var tarea in tareasEncontradas)
                {
                    Console.WriteLine(tarea);
                }
            }
            else
            {
                Console.WriteLine("No se encontraron tareas con esa descripción.");
            }
        }

        static void MostrarTodasLasTareas()
        {
            Console.WriteLine("\n=== TODAS LAS TAREAS ===");
            
            if (tareasPendientes.Count > 0)
            {
                Console.WriteLine("\n--- TAREAS PENDIENTES ---");
                foreach (var tarea in tareasPendientes.OrderBy(t => t.TareaID))
                {
                    Console.WriteLine($"⏳ {tarea}");
                }
            }

            if (tareasRealizadas.Count > 0)
            {
                Console.WriteLine("\n--- TAREAS REALIZADAS ---");
                foreach (var tarea in tareasRealizadas.OrderBy(t => t.TareaID))
                {
                    Console.WriteLine($"✅ {tarea}");
                }
            }

            if (tareasPendientes.Count == 0 && tareasRealizadas.Count == 0)
            {
                Console.WriteLine("No hay tareas registradas.");
            }

            Console.WriteLine($"\nResumen: {tareasPendientes.Count} pendientes, {tareasRealizadas.Count} realizadas");
        }

        static void MostrarTareasPendientes()
        {
            Console.WriteLine("\n=== TAREAS PENDIENTES ===");
            
            if (tareasPendientes.Count > 0)
            {
                foreach (var tarea in tareasPendientes.OrderBy(t => t.TareaID))
                {
                    Console.WriteLine($"{tarea}");
                }
                Console.WriteLine($"\nTotal: {tareasPendientes.Count} tareas pendientes");
            }
            else
            {
                Console.WriteLine("No hay tareas pendientes.");
            }
        }

        static void MostrarTareasRealizadas()
        {
            Console.WriteLine("\n=== TAREAS REALIZADAS ===");
            
            if (tareasRealizadas.Count > 0)
            {
                foreach (var tarea in tareasRealizadas.OrderBy(t => t.TareaID))
                {
                    Console.WriteLine($"{tarea}");
                }
                Console.WriteLine($"\nTotal: {tareasRealizadas.Count} tareas realizadas");
            }
            else
            {
                Console.WriteLine("No hay tareas realizadas.");
            }
        }
    }
}