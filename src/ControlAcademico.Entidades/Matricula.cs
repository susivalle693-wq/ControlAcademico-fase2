using System;

namespace ControlAcademico.Entidades
{
    /// <summary>
    /// Representa la relación entre un Estudiante y un Curso
    /// (una matrícula). Desarrollado por: Persona 5
    /// Fase 2: se agregan Id y llaves foráneas para guardarla en la base de datos.
    /// </summary>
    public class Matricula
    {
        public int Id { get; set; }
        public string CodigoMatricula { get; set; }

        public int EstudianteId { get; set; }
        public Estudiante Estudiante { get; set; }

        public int CursoId { get; set; }
        public Curso Curso { get; set; }

        public DateTime FechaRegistro { get; set; }

        // Una matrícula tiene a lo sumo una calificación
        public Calificacion Calificacion { get; set; }

        // Constructor vacío: lo usan Entity Framework y los formularios
        public Matricula()
        {
            FechaRegistro = DateTime.Now;
        }

        public Matricula(string codigoMatricula, Estudiante estudiante, Curso curso)
        {
            CodigoMatricula = !string.IsNullOrEmpty(codigoMatricula) ? codigoMatricula : throw new ArgumentException("ID inválido.");
            Estudiante = estudiante ?? throw new ArgumentNullException(nameof(estudiante));
            Curso = curso ?? throw new ArgumentNullException(nameof(curso));
            FechaRegistro = DateTime.Now;
        }

        public void MostrarDetalle()
        {
            Console.WriteLine($"[Matrícula] ID: {CodigoMatricula} | Fecha: {FechaRegistro.ToShortDateString()}");
            Console.WriteLine($" -> Estudiante: {Estudiante.ObtenerNombreCompleto()}");
            Console.WriteLine($" -> Curso: {Curso.Nombre}");
        }
    }
}
