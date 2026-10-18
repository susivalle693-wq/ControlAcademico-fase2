# Sistema de Control Académico

Proyecto de Cátedra — Fase 1 (DSP404, Universidad Don Bosco)

## Descripción

Sistema para el registro y control de estudiantes, docentes, cursos, matrículas y
calificaciones de una institución educativa. En esta primera fase se modelan las
clases principales del sistema en C# aplicando Programación Orientada a Objetos
(herencia, encapsulamiento, clase abstracta e interfaces), junto con un prototipo
funcional en consola. Más adelante, en la Fase 2, este mismo proyecto se ampliará
a una aplicación web completa en ASP.NET conectada a una base de datos.

## Tecnologías utilizadas

- C# (.NET Framework)
- Consola (prototipo Fase 1)
- Colecciones genéricas (`List<T>`)
- Git / GitHub
- Google Drive Video Presentación

## Estructura del proyecto

```
ControlAcademico/
├── README.md
├── src/
│   ├── Program.cs
│   └── Modelos/
│       ├── Persona.cs        (clase abstracta base)
│       ├── Estudiante.cs     (hereda de Persona)
│       ├── Docente.cs        (hereda de Persona)
│       ├── Curso.cs          (implementa IRegistrable)
│       ├── IRegistrable.cs
│       ├── Matricula.cs      (relación Estudiante-Curso)
│       └── Calificacion.cs   (implementa IEvaluable)
├── wireframes/
│   ├── pantalla-principal-dashboard.html
│   ├── pantalla-detalle-curso.html
│   ├── pantalla-matriculas.html
│   └── pantalla-calificaciones.html
└── docs/
    └── diagrama_clases_matricula.png
```

## Instrucciones de instalación y ejecución
    Opción 1: Visual Studio (recomendado)
    Clonar o descargar este repositorio.
    Abrir Visual Studio → Create a new project → Console App (.NET Framework).
    Nombrar el proyecto ControlAcademico y seleccionar .NET Framework 4.8.
    Borrar el Program.cs que Visual Studio crea por defecto.
    Clic derecho en el proyecto → Add → New Folder → nombrarla Modelos.
    Clic derecho en el proyecto → Add → Existing Item... → seleccionar src/Program.cs.
    Clic derecho en la carpeta Modelos → Add → Existing Item... → seleccionar los 7 archivos dentro de src/Modelos/ (Persona.cs, Estudiante.cs, Docente.cs, Curso.cs, IRegistrable.cs, Matricula.cs, Calificacion.cs).
    Presionar F5 para ejecutar.

## Integrantes del equipo

| # | Integrante | Clase en C# | Pantalla UI/UX |
|---|------------|--------------|-----------------|
| 1 | Marcelo Eduardo Guerrero Rodriguez - GR241968  | Persona (clase abstracta base) | Pantalla Principal / Dashboard |
| 2 | Daniela Jazmin Torres Ramos - TR232197 | Estudiante (hereda de Persona) | Pantalla de Listado |
| 3 | José Miguel Sosa Ayala - SA251597  | Docente (hereda de Persona) | Pantalla de Formulario |
| 4 | Susana Nicole Valle Méndez - VM253306  | Curso | Pantalla de Detalle |
| 5 | Alessandra Guadalupe González Burgos - GB253116  | Matrícula (relación Estudiante–Curso) | Pantalla de Matrículas |
| 6 | Alessandra Guadalupe González Burgos - GB253116  | Calificación (implementa IEvaluable) | Pantalla de Calificaciones |


## Video  
https://drive.google.com/file/d/1njLirsd0M4dwIirE8Bt67I102BxHhEUm/view?usp=sharing 
