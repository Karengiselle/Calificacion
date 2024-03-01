using System;
using System;

class Program
{
    class Calificacion
    {
        public int Nota { get; set; }
        public Calificacion Siguiente { get; set; }

        public Calificacion(int nota)
        {
            Nota = nota;
            Siguiente = null;
        }
    }

    class Estudiante
    {
        public string Nombre { get; set; }
        public Estudiante Siguiente { get; set; }
        public LinkedList<Calificacion> Calificaciones { get; set; }

        public Estudiante(string nombre)
        {
            Nombre = nombre;
            Siguiente = null;
            Calificaciones = new LinkedList<Calificacion>();
        }
    }

    static void Main()
    {
        Estudiante listaEstudiantes = null;
        string nombreArchivo = "karen";

        LeerDatosDesdeArchivo(nombreArchivo, ref listaEstudiantes);
        ImprimirListaEstudiantes(listaEstudiantes);
    }

    static void LeerDatosDesdeArchivo(string nombreArchivo, ref Estudiante listaEstudiantes)
    {
        try
        {
            using (StreamReader archivo = new StreamReader(nombreArchivo))
            {
                string linea;
                while ((linea = archivo.ReadLine()) != null)
                {
                    string[] partes = linea.Split(' ');
                    string nombreEstudiante = partes[0];

                    Estudiante nuevoEstudiante = new Estudiante(nombreEstudiante);

                    for (int i = 1; i < partes.Length && i <= 5; i++)
                    {
                        if (int.TryParse(partes[i], out int nota))
                        {
                            Calificacion nuevaCalificacion = new Calificacion(nota);
                            nuevoEstudiante.Calificaciones.AddLast(nuevaCalificacion);
                        }
                    }

                    nuevoEstudiante.Siguiente = listaEstudiantes;
                    listaEstudiantes = nuevoEstudiante;
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error al leer el archivo: " + ex.Message);
        }
    }

    static void ImprimirListaEstudiantes(Estudiante listaEstudiantes)
    {
        Estudiante actual = listaEstudiantes;

        while (actual != null)
        {
            Console.WriteLine($"Nombre: {actual.Nombre}");
            Console.Write("Calificaciones: ");

            foreach (Calificacion calificacionActual in actual.Calificaciones)
            {
                Console.Write($"{calificacionActual.Nota} ");
            }

            Console.WriteLine("\n");
            actual = actual.Siguiente;
        }
    }
}