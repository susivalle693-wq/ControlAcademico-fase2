using System;

namespace ControlAcademico.Entidades
{
    // Interfaz IEvaluable: obliga a implementar la lógica de aprobación
    public interface IEvaluable
    {
        bool EstaAprobado();
        string ObtenerEstado();
    }

    /// <summary>
    /// Representa la nota de un estudiante en un curso.
    /// Implementa la interfaz IEvaluable.
    /// Desarrollado por: Persona 6
    /// </summary>
    public class Calificacion : IEvaluable
    {
        public int Id { get; set; }
        public Estudiante Alumno { get; set; }   // Fase 1 (consola); no se guarda en la BD
        public Curso Asignatura { get; set; }    // Fase 1 (consola); no se guarda en la BD
        public double Nota { get; set; }         // en la BD es la columna NotaFinal

        // Fase 2: la nota se guarda ligada a una matrícula
        public int MatriculaId { get; set; }
        public Matricula Matricula { get; set; }
        public string Estado { get; set; } = "Pendiente";   // lo calcula la capa de Negocio
        public DateTime FechaRegistro { get; set; } = DateTime.Now;

        // Constructor vacío: lo usan Entity Framework y los formularios
        public Calificacion()
        {
        }

        public Calificacion(int id, Estudiante alumno, Curso asignatura, double nota)
        {
            Id = id;
            Alumno = alumno;
            Asignatura = asignatura;

            if (nota < 0.0 || nota > 10.0)
                throw new ArgumentException("La nota debe estar entre 0.0 y 10.0");

            Nota = nota;
            Estado = ObtenerEstado();
        }

        // Métodos de la interfaz IEvaluable
        public bool EstaAprobado()
        {
            return Nota >= 6.0; // Nota mínima de aprobación
        }

        public string ObtenerEstado()
        {
            return EstaAprobado() ? "Aprobado" : "Reprobado";
        }

        public void MostrarDetalle()
        {
            Console.WriteLine($"ID: {Id} | Estudiante: {Alumno.ObtenerNombreCompleto()} | Curso: {Asignatura.Nombre} | Nota: {Nota} [{ObtenerEstado()}]");
        }
    }
}
