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
            foreach (MedicineModelExport medicine in medicines)
            {
                Directory.CreateDirectory($"{routeName}.JSON");
                Environment.CurrentDirectory = ($"{routeName}.JSON");

                string fileName = $"{medicine.Name}.json";
                string jsonString = JsonSerializer.Serialize(medicine);
                string pathString = Path.Combine($"{routeName}.JSON", $"{medicine.Name}.json");
                File.WriteAllText(fileName, jsonString);

                Environment.CurrentDirectory = @"D:\\Escritorio\\ORT\\5to\\DA2\\Obligatorio 2\\205610-262756\\StartUp\\WebApi";
            }
        }
    }
}