using ResetDevice;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IntelUSBHostDeviceReset
{
    class WinService : System.ServiceProcess.ServiceBase
    {
        static void Main(string[] args)
        {


            if (args.Contains("/install"))
            {
                try
                {
                    ServiceInstallerUtil.InstallAndStart("IntelUSBHostDeviceReset", "Intel USB Host Device Reset", System.Reflection.Assembly.GetEntryAssembly().Location);
                    Console.WriteLine("Service has been installed and started");
                }
                catch(Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                Console.ReadKey();
            }
            else if (args.Contains("/uninstall"))
            {
                try
                {
                    ServiceInstallerUtil.Uninstall("IntelUSBHostDeviceReset");
                    Console.WriteLine("Service has been uninstalled");
                }
                catch(Exception e)
                {
                    Console.WriteLine(e.Message);

                }
                Console.ReadKey();
            }
            else
            {
               // ResetDevices();
                System.ServiceProcess.ServiceBase[] ServicesToRun;
                ServicesToRun =
                  new System.ServiceProcess.ServiceBase[] { new WinService() };
                System.ServiceProcess.ServiceBase.Run(ServicesToRun);
            }
        }

        private void InitializeComponent()
        {
            this.ServiceName = "IntelUSBHostDeviceReset";
        }


        protected override void OnStart(string[] args)
        {

            ResetDevices();
        }

        /// <summary>
        /// Stop this service.
        /// </summary>
        protected override void OnStop()
        {
            Log.Log.GetLogger().CloseLogFile();
        }

        static void ResetDevices()
        {

            Log.Log.GetLogger().Info("Check devices for errors");

            String[] devicesPathes = (ConfigurationManager.AppSettings["devicespath"] ?? "").Split(',');

            foreach (var devicePath in devicesPathes)
            {

                try
                {
                    DisableHardware.ResetErroredDevice(n => n.ToUpperInvariant().Contains(devicePath.Trim()), devicePath.Trim());
                }
                catch (ApplicationException e)
                {
                    Log.Log.GetLogger().Error(e.Message);
                }

            }
        }

    }
}
