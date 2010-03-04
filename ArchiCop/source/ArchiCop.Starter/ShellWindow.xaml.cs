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
        private readonly ObservableCollection<MsBuildTarget> _msBuildTargets;
                
        private const string MsBuildPath = @"c:\WINDOWS\Microsoft.NET\Framework\v3.5\msbuild.exe";
        
        public ShellWindow()
        {
            const string location = @"C:\ArchiCop";

            var targets = new MsBuildTargetList(location);
            _msBuildTargets = new ObservableCollection<MsBuildTarget>(targets);

            InitializeComponent();
        }

        public ObservableCollection<MsBuildTarget> MsBuildTargets
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
            var selected = (MsBuildTarget) TargetsView.SelectedItem;
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

    

}
