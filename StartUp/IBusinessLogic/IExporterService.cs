using StartUp.Domain;
using System.Collections.Generic;

namespace StartUp.IBusinessLogic
{
    public interface IExporterService
    {
        List<string> GetAllExporters();
        List<Medicine> ExportMedicines(string exporterName);
    }
}
