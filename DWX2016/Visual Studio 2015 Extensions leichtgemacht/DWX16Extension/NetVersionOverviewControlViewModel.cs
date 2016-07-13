using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Versioning;
using System.Windows.Input;
using EnvDTE;

namespace DWX16Extension
{
    public class NetVersionOverviewControlViewModel
    {
        private DTE dte;

        public ICommand AnalyzeSolution { get; }
        public ICommand UpgradeCommand { get; }
        public NetVersionOverviewControlViewModel()
        {
            dte = NetVersionOverviewCommand.Instance.ServiceProvider.GetService(typeof(DTE)) as DTE;
            AnalyzeSolution = new RelayCommand(Analyze);
            UpgradeCommand = new RelayCommand(Upgrade);
        }

        private void Upgrade()
        {
            var projects = dte.Solution.Projects;
            foreach (Project project in projects)
            {
                var framework =
                    project.Properties.OfType<Property>().FirstOrDefault(x => x.Name == "TargetFrameworkMoniker");
                framework.Value = new FrameworkName(".NETFramework", new Version(4, 5)).FullName;
            }
        }

        public ObservableCollection<ProjectViewModel> Projects { get; } = new ObservableCollection<ProjectViewModel>();

        private void Analyze()
        {
            var projects = dte.Solution.Projects;
            foreach (Project project in projects)
            {
                var targetFramework = project.Properties.OfType<Property>().FirstOrDefault(x => x.Name == "TargetFrameworkMoniker");
                Projects.Add(new ProjectViewModel
                {
                    Name = project.Name,
                    NETVersion = targetFramework?.Value?.ToString()
                });
            }
        }
    }

    public class ProjectViewModel
    {
        public string Name { get; set; }
        public string NETVersion { get; set; }
    }
}