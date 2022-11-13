using StartUp.IExporterInterface;
using StartUp.ModelsExporter;
using System.Xml;

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
            Directory.CreateDirectory($"{routeName}.XML");
            Environment.CurrentDirectory = ($"{routeName}.XML");
            XmlWriter xmlWriter = XmlWriter.Create("Medicines.XML");
            //string pathString = Path.Combine($"{routeName}.XML", xmlWriter.ToString());

            xmlWriter.WriteStartDocument();

            foreach (MedicineModelExport medicine in medicines)
            {
                xmlWriter.WriteStartElement(xmlWriter.ToString());
                xmlWriter.WriteAttributeString("Name", medicine.Name);
                xmlWriter.WriteAttributeString("Code", medicine.Code);
                xmlWriter.WriteAttributeString("Price", medicine.Price.ToString());
                xmlWriter.WriteAttributeString("Prescription", medicine.Prescription.ToString());
                xmlWriter.WriteAttributeString("Amount", medicine.Amount.ToString());
                xmlWriter.WriteAttributeString("Presentation", medicine.Presentation);

                foreach (var sym in medicine.Symptoms)
                {
                    xmlWriter.WriteAttributeString("Symptom Description", sym.SymptomDescription);
                }

                xmlWriter.WriteEndDocument();
                xmlWriter.Close();
                }
        }
        Environment.CurrentDirectory = @"D:\\Escritorio\\ORT\\5to\\DA2\\Obligatorio 2\\205610-262756\\StartUp\\WebApi";
           
    }
}