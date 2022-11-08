using System.Collections.Generic;

namespace StartUp.IBusinessLogic
{
    public interface IExporterService
    {
        List<string> GetAllExporters();
        void ExportMedicines(string routeName, string format);
    }
}
