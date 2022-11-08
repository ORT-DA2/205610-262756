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
                //string fileName = $"{medicine.Name}.json";
                //string jsonString = JsonSerializer.Serialize(medicine);
                //File.WriteAllText(fileName, jsonString);

                string fileName = $"{medicine.Name}.json";
                FileStream createStream = File.Create(fileName);
                string jsonString = JsonSerializer.Serialize(medicine);
                File.WriteAllText(fileName, jsonString);
            }
        }
    }
}