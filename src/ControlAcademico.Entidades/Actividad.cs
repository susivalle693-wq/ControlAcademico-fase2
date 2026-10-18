using System;

namespace ControlAcademico.Entidades
{
    /// <summary>
    /// Registro de una acción del sistema (alta, edición o baja).
    /// Alimenta la lista "Actividad reciente" del dashboard.
    /// </summary>
    public class Actividad
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public DateTime Fecha { get; set; } = DateTime.Now;
    }
}
