using StartUp.Domain;
using StartUp.Exceptions;
using StartUp.IBusinessLogic;
using StartUp.IDataAccess;
using StartUp.IExporterInterface;
using StartUp.ModelsExporter;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace StartUp.BusinessLogic
{
    public class ExporterService : IExporterService
    {
        private readonly ISessionService _sessionService;
        private readonly IRepository<Pharmacy> _pharmacyRepository;


        public ExporterService(ISessionService sessionService, IRepository<Pharmacy> pharmacyRepository)
        {
            _sessionService = sessionService;
            _pharmacyRepository = pharmacyRepository;

        }
        public List<string> GetAllExporters()
        {
            return GetExporterImplementations().Select(exporter => exporter.GetName()).ToList();
        }

        public void ExportMedicines(string routeName, string format)
        {
            List<IExporter> exporters = GetExporterImplementations();

            IExporter desiredImplementation = exporters.FirstOrDefault(i => i.GetName() == format);

            if (desiredImplementation == null)
                throw new ResourceNotFoundException("No se pudo encontrar el importador solicitado");

            Pharmacy pharmacy = _pharmacyRepository.GetOneByExpression(p => p.Name == _sessionService.UserLogged.Pharmacy.Name);
            List<MedicineModelExport> medicines = pharmacy.Stock.Select(m => new MedicineModelExport(m));
            desiredImplementation.ExportMedicines(routeName);
        }

        private List<IExporter> GetExporterImplementations()
        {
            List<IExporter> availableExporters = new List<IExporter>();
            // Va a estar adentro de WebApi, ya que mira relativo de donde se ejecuta el programa
            string exportersPath = "./Exporters";
            string[] filePaths = Directory.GetFiles(exportersPath);

            foreach (string filePath in filePaths)
            {
                if (filePath.EndsWith(".dll"))
                {
                    FileInfo fileInfo = new FileInfo(filePath);
                    Assembly assembly = Assembly.LoadFile(fileInfo.FullName);

                    foreach (Type type in assembly.GetTypes())
                    {
                        if (typeof(IExporter).IsAssignableFrom(type) && !type.IsInterface && !type.IsAbstract)
                        {
                            IExporter exporter = (IExporter)Activator.CreateInstance(type);
                            if (exporter != null)
                                availableExporters.Add(exporter);
                        }
                    }
                }
            }

            return availableExporters;
        }
    }
}

