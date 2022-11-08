﻿using StartUp.IExporterInterface;
using StartUp.ModelsExporter;

namespace StartUp.WebApi.Exporters
{
    public class XMLExporter : IExporter
    {
        public string GetName()
        {
            return "XML Exporter";
        }

        public void ExportMedicines(string routeName, string format, List<MedicineModelExport> medicines)
        {
            foreach (MedicineModelExport medicine in medicines)
            {
                string fileName = $"{medicine.Name}.json";
                FileStream file = File.Create($"D:/Escritorio/ORT/5to/DA2/FileExporter/{medicine.Name}.json");
                //string jsonString = JsonSerializer.Serialize(medicine);
                //File.WriteAllText(fileName, jsonString);
            }
        }
    }
}