using StartUp.Domain;
using StartUp.IExporterInterface;

namespace JSONExporter.cs
{
    // Este proyecto podria estar en otra solucion tranquilamente, solo necesito el dll
    // que resulta de compilar el proyecto para ponerlo en la carpeta Importers
    public class JSONImporter : IExporter
    {
        public string GetName()
        {
            return "Json Exporter";
        }

        // Aca obviamente va a leer de un JSON, no devolver algo hardcodeado
        public List<Medicine> ExportMedicines()
        {
            return new List<Medicine>() { new Medicine() { Id = 1, Name = "Medicine desde JSON Exporter" } };
        }
    }
}