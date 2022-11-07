using StartUp.Domain;

namespace StartUp.IExporterInterface;

public interface IExporter
{
    string GetName();
    void ExportMedicines(string routeName);

}
