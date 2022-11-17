using StartUp.IExporterInterface;
using StartUp.ModelsExporter;
using System.Text.Json;

namespace StartUp.WebApi.Exporters
{
    public class JSONExporter : IExporter
    {
        public string GetName()
        {
            return "JSON Exporter";
        }

        public void ExportMedicines(string routeName, string format, List<MedicineModelExport> medicines)
        {
            Directory.CreateDirectory($"{routeName}.JSON");

            string jsonString = JsonSerializer.Serialize(medicines);
            string pathString = Path.Combine($"{routeName}.JSON", "Medicines.json");
            File.WriteAllText(pathString, jsonString);
        }
    }
}