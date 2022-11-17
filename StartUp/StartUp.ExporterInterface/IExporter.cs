using StartUp.Domain;
using System.Collections.Generic;

namespace StartUp.IExporterInterface;

public interface IExporter
{
    string GetName();

    List<Medicine> ExportMedicines();

}
