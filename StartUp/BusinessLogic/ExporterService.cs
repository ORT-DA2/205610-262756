using StartUp.Domain;
using StartUp.Exceptions;
using StartUp.IBusinessLogic;
using StartUp.IExporterInterface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace StartUp.BusinessLogic
{
    public class ExporterService : IExporterService
    {
        public List<string> GetAllExporters()
        {
            return GetExporterImplementations().Select(exporter => exporter.GetName()).ToList();
        }

        public List<Medicine> ExportMedicines(string exporterName)
        {
            List<IExporter> exporters = GetExporterImplementations();

            IExporter? desiredImplementation = exporters.FirstOrDefault(i => i.GetName() == exporterName);

            if (desiredImplementation == null)
                throw new ResourceNotFoundException("No se pudo encontrar el importador solicitado");

            List<Medicine> exportedMovies = desiredImplementation.ExportMedicines();
            return exportedMovies;
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

