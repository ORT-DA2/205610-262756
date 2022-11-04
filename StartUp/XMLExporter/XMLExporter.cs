using StartUp.Domain;
using StartUp.IExporterInterface;

namespace XMLExporter
{
    public class XMLExporter : IExporter
    {
        public string GetName()
        {
            return "XML Exporter";
        }

        // Aca obviamente va a leer de un JSON, no devolver algo hardcodeado
        public List<Medicine> ExportMedicines()
        {
            return new List<Medicine>() { new Medicine() { Id = 2, Name = "Medicine desde XML Exporter" } };
        }
    }
}