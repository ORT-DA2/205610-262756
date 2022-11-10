using StartUp.ModelsExporter;

namespace StartUp.IExporterInterface;

public interface IExporter
{
    string GetName();
    void ExportMedicines(string routeName, string format, List<MedicineModelExport> medicines);

}
