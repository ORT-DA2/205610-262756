using StartUp.Domain;
using StartUp.IDataAccess;
using StartUp.IExporterInterface;
using System.Text.Json;

namespace JSONExporter.cs
{
    // Este proyecto podria estar en otra solucion tranquilamente, solo necesito el dll
    // que resulta de compilar el proyecto para ponerlo en la carpeta Importers
    public class JSONExporter : IExporter
    {
        private readonly IRepository<Pharmacy> _pharmacyRepositroy;
        public JSONExporter(IRepository<Pharmacy> pharmacyRepository)
        {
            _pharmacyRepositroy = pharmacyRepository;
        }
        public string GetName()
        {
            return "Json Exporter";
        }

        public void ExportMedicines(string pharmacyName)
        {
            Pharmacy pharmacy = _pharmacyRepositroy.GetOneByExpression(p=>p.Name == pharmacyName);
            List<Medicine> medicines = pharmacy.Stock;
            foreach (Medicine medicine in medicines)
            {
                string fileName = $"{medicine.Name}.json";
                FileStream file = File.Create($"D:/Escritorio/ORT/5to/DA2/FileExporter/{medicine.Name}.json");
                string jsonString = JsonSerializer.Serialize(medicine);
                File.WriteAllText(fileName, jsonString);
            }

        }
    }
}