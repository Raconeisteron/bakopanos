using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows;
using System.Xml.Linq;
using ArchiCop.Library;

namespace ArchiCop.Starter
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class ShellWindow : Window
    {
        private readonly ObservableCollection<MsBuildTargetData> _msBuildTargets =
            new ObservableCollection<MsBuildTargetData>();
                
        private static readonly Func<XDocument, string, IEnumerable<XElement>> _getElements =
            (xDocument, elementName) => (from e in xDocument.Descendants(XNameSpace + elementName)
                                         select e);

        private static readonly XNamespace XNameSpace = "http://schemas.microsoft.com/developer/msbuild/2003";
        private const string MsBuildPath = @"c:\WINDOWS\Microsoft.NET\Framework\v3.5\msbuild.exe";
        
        public ShellWindow()
        {
            const string location = @"C:\ArchiCop";
            string[] projectFiles = Directory.GetFiles(location,"*.proj");

            foreach (string file in projectFiles)
            {                
                XDocument xdoc = XDocument.Load(file);

                IEnumerable<XElement> targets = _getElements(xdoc, "Target");//

                foreach (XElement target in targets)
                {
                    _msBuildTargets.Add(new MsBuildTargetData
                    {                        
                        ProjectName = file,
                        TargetName = target.Attribute("Name").Value
                    });               
                }                
            }

            InitializeComponent();
        }

        public ObservableCollection<MsBuildTargetData> MsBuildTargets
        {
            get
            {
                return _msBuildTargets;
            }
        }

        private void ExecuteTarget_Click(object sender, RoutedEventArgs e)
        {
            //
            //execute
            var selected = (MsBuildTargetData) TargetsView.SelectedItem;
            string arguments = string.Format("{0} /t:{1}", selected.ProjectName,selected.TargetName);

            var psi = new ProcessStartInfo
                          {
                              FileName = MsBuildPath,
                              Arguments = arguments
                          };
            
            // OS Check: XP or higher (Vista = 6)
            if (Environment.OSVersion.Version.Major > 5)
            {
                psi.Verb = "runas"; // elevate the application
                psi.UseShellExecute = true;
            }

            Process processMsBuild = Process.Start(psi);
        }
    }

    public class MsBuildTargetData
    {        
        public string ProjectName { get; set; }
        public string TargetName { get; set; }        
    }

}
