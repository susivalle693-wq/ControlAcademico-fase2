using ControlAcademico.Entidades;
using Microsoft.EntityFrameworkCore;

namespace ControlAcademico.Datos
{
    public class ControlAcademicoDbContext : DbContext
    {
        public ControlAcademicoDbContext(DbContextOptions<ControlAcademicoDbContext> options) : base(options) { }

        public DbSet<Estudiante> Estudiantes => Set<Estudiante>();
        public DbSet<Docente> Docentes => Set<Docente>();
        public DbSet<Curso> Cursos => Set<Curso>();
        public DbSet<Matricula> Matriculas => Set<Matricula>();
        public DbSet<Calificacion> Calificaciones => Set<Calificacion>();
        public DbSet<Actividad> Actividad => Set<Actividad>();

        protected override void OnModelCreating(ModelBuilder mb)
        {
            // ---------- Persona (clase abstracta) ----------
            // No tiene tabla propia: sus campos se repiten en Estudiantes y Docentes (TPC)
            mb.Entity<Persona>(p =>
            {
                p.UseTpcMappingStrategy();
                p.Property(x => x.Nombre).IsRequired().HasMaxLength(80);
                p.Property(x => x.Apellido).IsRequired().HasMaxLength(80);
                p.Property(x => x.Email).IsRequired().HasMaxLength(120);
                p.Property(x => x.Telefono).HasMaxLength(15).IsUnicode(false);
                p.Property(x => x.FechaNacimiento).HasColumnType("date");
            });

            // ---------- Estudiantes ----------
            mb.Entity<Estudiante>(e =>
            {
                e.ToTable("Estudiantes", t => t.HasCheckConstraint("CK_Estudiantes_CUM", "[CUM] >= 0 AND [CUM] <= 10"));
                e.Property(x => x.Carnet).IsRequired().HasMaxLength(10).IsUnicode(false);
                e.Property(x => x.Carrera).HasMaxLength(100);
                e.Property(x => x.Facultad).HasMaxLength(100);
                e.Property(x => x.EstadoAcademico).HasMaxLength(20);
                e.HasIndex(x => x.Carnet).IsUnique();
                e.HasIndex(x => x.Email).IsUnique();
                // Fase 1 guardaba las materias en una lista; ahora son la tabla Matriculas
                e.Ignore(x => x.MateriasInscritas);
            });

            // ---------- Docentes ----------
            mb.Entity<Docente>(d =>
            {
                d.ToTable("Docentes");
                d.Property(x => x.CodigoEmpleado).IsRequired().HasMaxLength(20).IsUnicode(false);
                d.Property(x => x.Especialidad).HasMaxLength(80);
                d.HasIndex(x => x.CodigoEmpleado).IsUnique();
                d.HasIndex(x => x.Email).IsUnique();
            });

            // ---------- Cursos ----------
            mb.Entity<Curso>(c =>
            {
                c.ToTable("Cursos", t =>
                {
                    t.HasCheckConstraint("CK_Cursos_Creditos", "[Creditos] BETWEEN 1 AND 6");
                    t.HasCheckConstraint("CK_Cursos_Cupo", "[CupoMaximo] > 0");
                });
                c.Property(x => x.Codigo).IsRequired().HasMaxLength(20).IsUnicode(false);
                c.Property(x => x.Nombre).IsRequired().HasMaxLength(120);
                c.HasIndex(x => x.Codigo).IsUnique();
                // Datos de la Fase 1 que ahora se calculan o vienen de otra tabla
                c.Ignore(x => x.Inscritos);
                c.Ignore(x => x.DocenteAsignado);
                c.HasOne(x => x.Docente).WithMany(d => d.Cursos)
                    .HasForeignKey(x => x.DocenteId).OnDelete(DeleteBehavior.Restrict);
            });

            // ---------- Matriculas ----------
            mb.Entity<Matricula>(m =>
            {
                m.ToTable("Matriculas");
                m.Property(x => x.CodigoMatricula).IsRequired().HasMaxLength(20).IsUnicode(false);
                m.Property(x => x.FechaRegistro).HasDefaultValueSql("GETDATE()");
                m.HasIndex(x => x.CodigoMatricula).IsUnique();
                // Una persona no se puede matricular dos veces en el mismo curso
                m.HasIndex(x => new { x.EstudianteId, x.CursoId }).IsUnique();
                m.HasOne(x => x.Estudiante).WithMany(e => e.Matriculas)
                    .HasForeignKey(x => x.EstudianteId).OnDelete(DeleteBehavior.Restrict);
                m.HasOne(x => x.Curso).WithMany(c => c.Matriculas)
                    .HasForeignKey(x => x.CursoId).OnDelete(DeleteBehavior.Restrict);
            });

            // ---------- Calificaciones ----------
            mb.Entity<Calificacion>(c =>
            {
                c.ToTable("Calificaciones", t => t.HasCheckConstraint("CK_Calificaciones_Nota", "[NotaFinal] >= 0 AND [NotaFinal] <= 10"));
                // La propiedad Nota se guarda en la columna NotaFinal decimal(4,2)
                c.Property(x => x.Nota).HasColumnName("NotaFinal").HasColumnType("decimal(4,2)").HasConversion<decimal>();
                c.Property(x => x.Estado).IsRequired().HasMaxLength(20);
                c.Property(x => x.FechaRegistro).HasDefaultValueSql("GETDATE()");
                // Alumno y Asignatura son de la Fase 1 (consola): no van a la base de datos
                c.Ignore(x => x.Alumno);
                c.Ignore(x => x.Asignatura);
                // Una matrícula tiene a lo sumo una nota (MatriculaId único)
                c.HasOne(x => x.Matricula).WithOne(m => m.Calificacion)
                    .HasForeignKey<Calificacion>(x => x.MatriculaId).OnDelete(DeleteBehavior.Restrict);
            });

            // ---------- Actividad ----------
            mb.Entity<Actividad>(a =>
            {
                a.ToTable("Actividad");
                a.Property(x => x.Descripcion).IsRequired().HasMaxLength(200);
                a.Property(x => x.Fecha).HasDefaultValueSql("GETDATE()");
            });
        }
    }
}
