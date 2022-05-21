using System;
using System.Collections;
using System.Collections.Generic;

namespace Iterador_GoF
{
    class Program
    {
        int posicionActual = 0;
        static void Main(string[] args)
        {
            Console.WriteLine("ITERADOR NUMERICO \n");
           //ITERADOR NUMERICO EJEMPLO CORTO.
            int[] numbers = { 1, 2, 3, 4, 5 };

            IEnumerator iterator = numbers.GetEnumerator();

            while (iterator.MoveNext())
            {
                Console.WriteLine(iterator.Current);
            }

            Console.WriteLine("\n ITERADOR POR ALUMNOS \n");
            IRegistroAlumnos registro = new RegistroAlumnos();

            // Insertamos unos cuantos elementos
            registro.InsertarAlumnos("Ana", "Valencia", "Activo");
            registro.InsertarAlumnos("Cecilia", "Garcia", "Activo");
            registro.InsertarAlumnos("Petronila", "Perez", "Inactivo");
            registro.InsertarAlumnos("Fulanito", "Caseres", "Inactivo");
            registro.InsertarAlumnos("Benedicto", "Terron", "Activo");

            // Obtenemos el iterator
            IIteratorAlumno iterador = registro.ObtenerIterator();

            // Mientras queden elementos
            while (iterador.QuedanElementos())
            {
                // Obtenemos el siguiente elemento
                Alumno a = iterador.Siguiente();

                // Mostramos su contenido
                Console.WriteLine(a.Nombre + " " + a.Apellido + " Día Consulta: " + a.FechaConsulta.ToShortDateString() + " " + a.Activo);
            }


            Console.ReadKey();

        }

     
    }

        public class Alumno
        {
            public string Nombre { get; set; }
            public string Apellido { get; set; }
            public DateTime FechaConsulta { get; set; }
            public string Activo { get; set; }

            public Alumno(string nombre, string apellido,
                DateTime FechaConsulta, string activo)
            {
                this.Nombre = nombre;
                this.Apellido = apellido;
                this.FechaConsulta = FechaConsulta;
                this.Activo = activo;
            }

            public string InfoAlumno()
            {
                return Nombre + " " + Apellido + " - Inscrito en: " +
                    FechaConsulta.ToShortDateString() + " Estado: " +
                    Activo;
            }
        }

        public interface IRegistroAlumnos
        {
            void InsertarAlumnos(string nombre, string apellido, string activo);
            Alumno MostrarInformacionAlumno(int indice);
            IIteratorAlumno ObtenerIterator();
        }


        public class RegistroAlumnos : IRegistroAlumnos
        {
            public ArrayList listaAlumnos;

            public RegistroAlumnos()
            {
                this.listaAlumnos = new ArrayList();
            }

        

            public void InsertarAlumnos(string nombre, string apellido, string activo)
            {
                Alumno a = new Alumno(nombre, apellido, DateTime.Now, activo);
                listaAlumnos.Add(a);
            }

            public Alumno MostrarInformacionAlumno(int indice)
            {
                return (Alumno)listaAlumnos[indice];
            }
            public IIteratorAlumno ObtenerIterator()
            {
                return new IteratorAlumno(listaAlumnos);
            }
        }


        public interface IIteratorAlumno
        {
            void Primero();
            Alumno Actual();
            Alumno Siguiente();
            bool QuedanElementos();
          
        }

        public class IteratorAlumno : IIteratorAlumno
        {
            // Referencia al listado completo
            public ArrayList alumno;

            // Almacenamiento en el índice en el que se encuentra el iterador
            private int posicionActual = -1;

            
            public IteratorAlumno(ArrayList listado)
            {
                this.alumno = listado;
            }




            // Reinicio del índice
            public void Primero()
            {
                this.posicionActual = -1;
            }

            // Acceso al elemento actual
            public Alumno Actual()
            {
                // validaciones de indices
                if ((this.alumno == null) ||  (this.alumno.Count == 0) || (posicionActual > this.alumno.Count - 1) ||(this.posicionActual < 0))
                    return null;

                // Devolvemos el elemento actual
                else
                    return (Alumno)this.alumno[posicionActual];
            }

            // Operación 3: Acceso al siguiente elemento
            public Alumno Siguiente()
            {
                  // validaciones de indices

                if ((this.alumno == null) || (this.alumno.Count == 0) || (posicionActual + 1 > this.alumno.Count - 1))
                    return null;

                // Aumentamos el índice en una unidad y devolvemos ese elemento
                else
                    return (Alumno)this.alumno[++posicionActual];
            }

            // Operación 4: Comprobación de si existen elementos en la colección
            public bool QuedanElementos()
            {
                // validaciones de indices
                // máximo índice aceptable (número de elementos del array - 1).
            return (posicionActual + 1 <= this.alumno.Count - 1);
            }
          
        }
      
    }
   
