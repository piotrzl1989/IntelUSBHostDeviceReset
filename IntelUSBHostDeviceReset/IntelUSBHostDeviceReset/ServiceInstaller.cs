using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelUSBHostDeviceReset
{
    [System.ComponentModel.RunInstaller(true)]
    public class ProjectInstaller : System.Configuration.Install.Installer
    {

        private System.ServiceProcess.ServiceInstaller serviceInstaller;
        private System.ServiceProcess.ServiceProcessInstaller 
                serviceProcessInstaller;

        public ProjectInstaller()
        {
            InitializeComponent();
        }


        private void InitializeComponent()
        {
            this.serviceInstaller = new System.ServiceProcess.ServiceInstaller();
            this.serviceProcessInstaller = 
              new System.ServiceProcess.ServiceProcessInstaller();

            this.serviceInstaller.Description = "Resetuje rozszerzone kontrolery hostów USB rodziny mikroukładów Intel(R) 6 Series/C200 Series";
            this.serviceInstaller.DisplayName = "Intel USB Host Device Reset";
            this.serviceInstaller.ServiceName = "IntelUSBHostDeviceReset";

            this.serviceProcessInstaller.Account =
              System.ServiceProcess.ServiceAccount.LocalSystem;
            this.serviceProcessInstaller.Password = null;
            this.serviceProcessInstaller.Username = null;

            this.Installers.AddRange(new System.Configuration.Install.Installer[] {
            this.serviceProcessInstaller,
            this.serviceInstaller});

        }

        public static void InstallService(string exeFilename)
        {
            string[] commandLineOptions = new string[1] { "/LogFile=install.log" };

            System.Configuration.Install.AssemblyInstaller installer = new System.Configuration.Install.AssemblyInstaller(exeFilename, commandLineOptions);

            installer.UseNewContext = true;
            installer.Install(null);
            installer.Commit(null);

        }

        public static void UninstallService(string exeFilename)
        {
            string[] commandLineOptions = new string[1] { "/LogFile=uninstall.log" };

            System.Configuration.Install.AssemblyInstaller installer = new System.Configuration.Install.AssemblyInstaller(exeFilename, commandLineOptions);

            installer.UseNewContext = true;
            installer.Uninstall(null);

        }

    }

}
