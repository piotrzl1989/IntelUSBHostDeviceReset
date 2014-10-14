using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ResetDevice
{
    public static class DisableHardware
    {
        const uint DIF_PROPERTYCHANGE = 0x12;
        const uint DICS_ENABLE = 1;
        const uint DICS_DISABLE = 2;
        const uint DICS_FLAG_GLOBAL = 1;
        const uint DIGCF_ALLCLASSES = 4;
        const uint DIGCF_PROFILE = 0x00000008;
        const uint DIGCF_DEVICEINTERFACE = 0x00000010;
        const uint DIGCF_PRESENT = 2;
        const uint ERROR_NO_MORE_ITEMS = 259;
        const uint ERROR_ELEMENT_NOT_FOUND = 1168;


        const uint CM_PROB_NOT_CONFIGURED = 0x00000001;   // no config for device
        const uint CM_PROB_DEVLOADER_FAILED = 0x00000002;   // service load failed
        const uint CM_PROB_OUT_OF_MEMORY = 0x00000003;   // out of memory
        const uint CM_PROB_ENTRY_IS_WRONG_TYPE = 0x00000004;   //
        const uint CM_PROB_LACKED_ARBITRATOR = 0x00000005;   //
        const uint CM_PROB_BOOT_CONFIG_CONFLICT = 0x00000006;   // boot config conflict
        const uint CM_PROB_FAILED_FILTER = 0x00000007;   //
        const uint CM_PROB_DEVLOADER_NOT_FOUND = 0x00000008;   // Devloader not found
        const uint CM_PROB_INVALID_DATA = 0x00000009;   // Invalid ID
        const uint CM_PROB_FAILED_START = 0x0000000A;   //
        const uint CM_PROB_LIAR = 0x0000000B;   //
        const uint CM_PROB_NORMAL_CONFLICT = 0x0000000C;   // config conflict
        const uint CM_PROB_NOT_VERIFIED = 0x0000000D;   //
        const uint CM_PROB_NEED_RESTART = 0x0000000E;   // requires restart
        const uint CM_PROB_REENUMERATION = 0x0000000F;   //
        const uint CM_PROB_PARTIAL_LOG_CONF = 0x00000010;   //
        const uint CM_PROB_UNKNOWN_RESOURCE = 0x00000011;   // unknown res type
        const uint CM_PROB_REINSTALL = 0x00000012;   //
        const uint CM_PROB_REGISTRY = 0x00000013;   //
        const uint CM_PROB_VXDLDR = 0x00000014;   // WINDOWS 95 ONLY
        const uint CM_PROB_WILL_BE_REMOVED = 0x00000015;   // devinst will remove
        const uint CM_PROB_DISABLED = 0x00000016;   // devinst is disabled
        const uint CM_PROB_DEVLOADER_NOT_READY = 0x00000017;   // Devloader not ready
        const uint CM_PROB_DEVICE_NOT_THERE = 0x00000018;   // device doesn't exist
        const uint CM_PROB_MOVED = 0x00000019;   //
        const uint CM_PROB_TOO_EARLY = 0x0000001A;   //
        const uint CM_PROB_NO_VALID_LOG_CONF = 0x0000001B;   // no valid log config
        const uint CM_PROB_FAILED_INSTALL = 0x0000001C;   // install failed
        const uint CM_PROB_HARDWARE_DISABLED = 0x0000001D;   // device disabled
        const uint CM_PROB_CANT_SHARE_IRQ = 0x0000001E;   // can't share IRQ
        const uint CM_PROB_FAILED_ADD = 0x0000001F;   // driver failed add
        const uint CM_PROB_DISABLED_SERVICE = 0x00000020;   // service's Start = 4
        const uint CM_PROB_TRANSLATION_FAILED = 0x00000021;   // resource translation failed
        const uint CM_PROB_NO_SOFTCONFIG = 0x00000022;   // no soft config
        const uint CM_PROB_BIOS_TABLE = 0x00000023;   // device missing in BIOS table
        const uint CM_PROB_IRQ_TRANSLATION_FAILED = 0x00000024;   // IRQ translator failed

        const uint CM_PROB_FAILED_DRIVER_ENTRY = 0x00000025;   // DriverEntry= ; failed.
        const uint CM_PROB_DRIVER_FAILED_PRIOR_UNLOAD = 0x00000026;   // Driver should have unloaded.
        const uint CM_PROB_DRIVER_FAILED_LOAD = 0x00000027;   // Driver load unsuccessful.
        const uint CM_PROB_DRIVER_SERVICE_KEY_INVALID = 0x00000028;   // Error accessing driver's service key
        const uint CM_PROB_LEGACY_SERVICE_NO_DEVICES = 0x00000029;   // Loaded legacy service created no devices
        const uint CM_PROB_DUPLICATE_DEVICE = 0x0000002A;   // Two devices were discovered with the same name
        const uint CM_PROB_FAILED_POST_START = 0x0000002B;   // The drivers set the device state to failed
        const uint CM_PROB_HALTED = 0x0000002C;   // This device was failed post start via usermode
        const uint CM_PROB_PHANTOM = 0x0000002D;   // The devinst currently exists only in the registry
        const uint CM_PROB_SYSTEM_SHUTDOWN = 0x0000002E;   // The system is shutting down
        const uint CM_PROB_HELD_FOR_EJECT = 0x0000002F;   // The device is offline awaiting removal
        const uint CM_PROB_DRIVER_BLOCKED = 0x00000030;   // One or more drivers is blocked from loading
        const uint CM_PROB_REGISTRY_TOO_LARGE = 0x00000031;   // System hive has grown too large
        const uint CM_PROB_SETPROPERTIES_FAILED = 0x00000032;   // Failed to apply one or more registry properties  

        //
        // Device Instance status flags, returned by call to CM_Get_DevInst_Status
        //
        const uint DN_ROOT_ENUMERATED = 0x00000001; // Was enumerated by ROOT
        const uint DN_DRIVER_LOADED = 0x00000002; // Has Register_Device_Driver
        const uint DN_ENUM_LOADED = 0x00000004; // Has Register_Enumerator
        const uint DN_STARTED = 0x00000008; // Is currently configured
        const uint DN_MANUAL = 0x00000010; // Manually installed
        const uint DN_NEED_TO_ENUM = 0x00000020; // May need reenumeration
        const uint DN_NOT_FIRST_TIME = 0x00000040; // Has received a config
        const uint DN_HARDWARE_ENUM = 0x00000080; // Enum generates hardware ID
        const uint DN_LIAR = 0x00000100; // Lied about can reconfig once
        const uint DN_HAS_MARK = 0x00000200; // Not CM_Create_DevInst lately
        const uint DN_HAS_PROBLEM = 0x00000400; // Need device installer
        const uint DN_FILTERED = 0x00000800; // Is filtered
        const uint DN_MOVED = 0x00001000; // Has been moved
        const uint DN_DISABLEABLE = 0x00002000; // Can be disabled
        const uint DN_REMOVABLE = 0x00004000; // Can be removed
        const uint DN_PRIVATE_PROBLEM = 0x00008000; // Has a private problem
        const uint DN_MF_PARENT = 0x00010000; // Multi function parent
        const uint DN_MF_CHILD = 0x00020000; // Multi function child
        const uint DN_WILL_BE_REMOVED = 0x00040000; // DevInst is being removed

        //
        // Windows 4 OPK2 Flags
        //
        const uint DN_NOT_FIRST_TIMEE = 0x00080000; // S: Has received a config enumerate
        const uint DN_STOP_FREE_RES = 0x00100000; // S: When child is stopped, free resources
        const uint DN_REBAL_CANDIDATE = 0x00200000; // S: Don't skip during rebalance
        const uint DN_BAD_PARTIAL = 0x00400000; // S: This devnode's log_confs do not have same resources
        const uint DN_NT_ENUMERATOR = 0x00800000; // S: This devnode's is an NT enumerator
        const uint DN_NT_DRIVER = 0x01000000; // S: This devnode's is an NT driver
        //
        // Windows 4.1 Flags
        //
        const uint DN_NEEDS_LOCKING = 0x02000000; // S: Devnode need lock resume processing
        const uint DN_ARM_WAKEUP = 0x04000000; // S: Devnode can be the wakeup device
        const uint DN_APM_ENUMERATOR = 0x08000000; // S: APM aware enumerator
        const uint DN_APM_DRIVER = 0x10000000; // S: APM aware driver
        const uint DN_SILENT_INSTALL = 0x20000000; // S: Silent install
        const uint DN_NO_SHOW_IN_DM = 0x40000000; // S: No show in device manager
        const uint DN_BOOT_LOG_PROB = 0x80000000; // S: Had a problem during preassignment of boot log conf



        static DEVPROPKEY DEVPKEY_Device_DeviceDesc;
        static DEVPROPKEY DEVPKEY_Device_HardwareIds;
        static DEVPROPKEY DEVPKEY_Device_Class;
        static DEVPROPKEY DEVPKEY_Device_ProblemCode;

        static DEVPROPKEY DEVPKEY_Device_DevNodeStatus;

        [StructLayout(LayoutKind.Sequential)]
        struct SP_CLASSINSTALL_HEADER
        {
            public UInt32 cbSize;
            public UInt32 InstallFunction;
        }

        [StructLayout(LayoutKind.Sequential)]
        struct SP_PROPCHANGE_PARAMS
        {
            public SP_CLASSINSTALL_HEADER ClassInstallHeader;
            public UInt32 StateChange;
            public UInt32 Scope;
            public UInt32 HwProfile;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct SP_DEVINFO_DATA
        {
            public UInt32 cbSize;
            public Guid classGuid;
            public UInt32 devInst;
            public UInt32 reserved;
        }

        [StructLayout(LayoutKind.Sequential)]
        struct DEVPROPKEY
        {
            public Guid fmtid;
            public UInt32 pid;
        }


        [StructLayout(LayoutKind.Sequential)]
        public struct SP_DEVICE_INTERFACE_DATA
        {
            public Int32 cbSize;
            public Guid interfaceClassGuid;
            public Int32 flags;
            private UIntPtr reserved;
        }
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto, Pack = 1)]
        struct NativeDeviceInterfaceDetailData
        {
            public int size;
            public char devicePath;
        }
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public struct SP_DEVICE_INTERFACE_DETAIL_DATA
        {
            public int cbSize;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
            public string DevicePath;
        }

        [DllImport("setupapi.dll", SetLastError = true)]
        static extern IntPtr SetupDiGetClassDevsW(
            [In] ref Guid ClassGuid,
            [MarshalAs(UnmanagedType.LPWStr)]
    string Enumerator,
            IntPtr parent,
            UInt32 flags);

        [DllImport("setupapi.dll", SetLastError = true)]
        static extern bool SetupDiDestroyDeviceInfoList(IntPtr handle);

        [DllImport("setupapi.dll", SetLastError = true)]
        static extern bool SetupDiEnumDeviceInfo(IntPtr deviceInfoSet,
            UInt32 memberIndex,
            [Out] out SP_DEVINFO_DATA deviceInfoData);

        [DllImport("setupapi.dll", SetLastError = true)]
        static extern bool SetupDiSetClassInstallParams(
            IntPtr deviceInfoSet,
            [In] ref SP_DEVINFO_DATA deviceInfoData,
            [In] ref SP_PROPCHANGE_PARAMS classInstallParams,
            UInt32 ClassInstallParamsSize);

        [DllImport("setupapi.dll", SetLastError = true)]
        static extern bool SetupDiChangeState(
            IntPtr deviceInfoSet,
            [In] ref SP_DEVINFO_DATA deviceInfoData);

        [DllImport("setupapi.dll", SetLastError = true)]
        static extern bool SetupDiGetDevicePropertyW(
                IntPtr deviceInfoSet,
                [In] ref SP_DEVINFO_DATA DeviceInfoData,
                [In] ref DEVPROPKEY propertyKey,
                [Out] out UInt32 propertyType,
                IntPtr propertyBuffer,
                UInt32 propertyBufferSize,
                out UInt32 requiredSize,
                UInt32 flags);

        [DllImport(@"setupapi.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern Boolean SetupDiGetDeviceInterfaceDetail(
           IntPtr hDevInfo,
           ref SP_DEVICE_INTERFACE_DATA deviceInterfaceData,
           ref SP_DEVICE_INTERFACE_DETAIL_DATA deviceInterfaceDetailData,
           UInt32 deviceInterfaceDetailDataSize,
           out UInt32 requiredSize,
           ref SP_DEVINFO_DATA deviceInfoData
        );

        [DllImport(@"setupapi.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern Boolean SetupDiEnumDeviceInterfaces(
           IntPtr hDevInfo,
           IntPtr devInfo,
           ref Guid interfaceClassGuid,
           UInt32 memberIndex,
           ref SP_DEVICE_INTERFACE_DATA deviceInterfaceData
        );



        static DisableHardware()
        {
            DisableHardware.DEVPKEY_Device_DeviceDesc = new DEVPROPKEY();
            DEVPKEY_Device_DeviceDesc.fmtid = new Guid(
                    0xa45c254e, 0xdf1c, 0x4efd, 0x80, 0x20, 0x67,
                    0xd1, 0x46, 0xa8, 0x50, 0xe0);
            DEVPKEY_Device_DeviceDesc.pid = 2;

            DEVPKEY_Device_HardwareIds = new DEVPROPKEY();
            DEVPKEY_Device_HardwareIds.fmtid = new Guid(
                0xa45c254e, 0xdf1c, 0x4efd, 0x80, 0x20, 0x67,
                0xd1, 0x46, 0xa8, 0x50, 0xe0);
            DEVPKEY_Device_HardwareIds.pid = 3;

            DEVPKEY_Device_Class = new DEVPROPKEY();
            DEVPKEY_Device_Class.fmtid = new Guid(
                0xa45c254e, 0xdf1c, 0x4efd, 0x80, 0x20, 0x67, 0xd1, 0x46, 0xa8, 0x50, 0xe0);
            DEVPKEY_Device_Class.pid = 9;

            DEVPKEY_Device_DevNodeStatus = new DEVPROPKEY();
            DEVPKEY_Device_DevNodeStatus.fmtid = new Guid(
                0x4340a6c5, 0x93fa, 0x4706, 0x97, 0x2c, 0x7b, 0x64, 0x80, 0x08, 0xa5, 0xa7);
            DEVPKEY_Device_DevNodeStatus.pid = 2;

            DEVPKEY_Device_ProblemCode = new DEVPROPKEY();
            DEVPKEY_Device_ProblemCode.fmtid = new Guid(
                0x4340a6c5, 0x93fa, 0x4706, 0x97, 0x2c, 0x7b, 0x64, 0x80, 0x08, 0xa5, 0xa7);
            DEVPKEY_Device_ProblemCode.pid = 3;


        }

        public static void Disable(IntPtr info, ref SP_DEVINFO_DATA devdata, bool isDisable)
        {
            SP_CLASSINSTALL_HEADER header = new SP_CLASSINSTALL_HEADER();
            header.cbSize = (UInt32)Marshal.SizeOf(header);
            header.InstallFunction = DIF_PROPERTYCHANGE;

            SP_PROPCHANGE_PARAMS propchangeparams = new SP_PROPCHANGE_PARAMS();
            propchangeparams.ClassInstallHeader = header;
            propchangeparams.StateChange = isDisable ? DICS_DISABLE : DICS_ENABLE;
            propchangeparams.Scope = DICS_FLAG_GLOBAL;
            propchangeparams.HwProfile = 0;

            SetupDiSetClassInstallParams(info,
                ref devdata,
                ref propchangeparams,
                (UInt32)Marshal.SizeOf(propchangeparams));
            CheckError("SetupDiSetClassInstallParams");

            SetupDiChangeState(
                info,
                ref devdata);
            CheckError("SetupDiChangeState");
        }

        public static void ResetErroredDevice(Func<string, bool> filter, String path)
        {
            IntPtr info = IntPtr.Zero;
            Guid NullGuid = Guid.Empty;
            string devicepath;

            try
            {
                info = SetupDiGetClassDevsW(
                    ref NullGuid,
                    null,
                    IntPtr.Zero,
                    DIGCF_ALLCLASSES /*| DIGCF_PRESENT | DIGCF_PROFILE | DIGCF_DEVICEINTERFACE*/);
                CheckError("SetupDiGetClassDevs");

                SP_DEVINFO_DATA devdata = new SP_DEVINFO_DATA();
                devdata.cbSize = (UInt32)Marshal.SizeOf(devdata);

                UInt32 problemCode;
                UInt32 statusNode;
                Log.Log.GetLogger().Info(String.Format("Search device {0}", path));

                for (uint i = 0; ; i++)
                {

                    SetupDiEnumDeviceInfo(info,
                        i,
                        out devdata);

                    if (Marshal.GetLastWin32Error() == ERROR_NO_MORE_ITEMS)
                        CheckError("No device found matching filter.", 0xcffff);
                    CheckError("SetupDiEnumDeviceInfo");

                    devicepath = GetStringPropertyForDevice(info,
                                               devdata, DEVPKEY_Device_HardwareIds);

                    problemCode = GetUint32PropertyForDevice(info,
                                               devdata, DEVPKEY_Device_ProblemCode);

                    statusNode = GetUint32PropertyForDevice(info,
                                               devdata, DEVPKEY_Device_DevNodeStatus);

                    if (devicepath != null && filter(devicepath))
                        break;

                }
                Log.Log.GetLogger().Info(String.Format("Found device {0} with error code {1}", devicepath, problemCode));

                //tylko z problemami startu
                if (problemCode == CM_PROB_FAILED_START || problemCode == CM_PROB_FAILED_POST_START || problemCode == CM_PROB_DISABLED)
                {

                    //zablokowanie device'a
                    Log.Log.GetLogger().Error(String.Format("Device {0} has problem {1}", devicepath, problemCode));

                    Disable(info, ref devdata, true);
                    Log.Log.GetLogger().Info(String.Format("Stop device {0}", devicepath, problemCode));

                    Thread.Sleep(500);

                    //odblokowanie device'a
                    Disable(info, ref devdata, false);
                    Log.Log.GetLogger().Info(String.Format("Start device {0}", devicepath, problemCode));

                    //sprawdzenie stanu po resecie
                    problemCode = GetUint32PropertyForDevice(info,
                           devdata, DEVPKEY_Device_ProblemCode);

                    if (problemCode == 0)
                    { 
                        Log.Log.GetLogger().Info(String.Format("Reset success device {0}", devicepath, problemCode));
                    }
                    else
                    {
                        Log.Log.GetLogger().Error(String.Format("Device {0} has problem {1}", devicepath, problemCode));
                    }


                }

            }
            finally
            {
                if (info != IntPtr.Zero)
                    SetupDiDestroyDeviceInfoList(info);
            }

        }



        public static void DisableDevice(Func<string, bool> filter, bool disable = true)
        {
            IntPtr info = IntPtr.Zero;
            Guid NullGuid = Guid.Empty;
            try
            {
                info = SetupDiGetClassDevsW(
                    ref NullGuid,
                    null,
                    IntPtr.Zero,
                    DIGCF_ALLCLASSES /*| DIGCF_PRESENT | DIGCF_PROFILE | DIGCF_DEVICEINTERFACE*/);
                CheckError("SetupDiGetClassDevs");

                SP_DEVINFO_DATA devdata = new SP_DEVINFO_DATA();
                devdata.cbSize = (UInt32)Marshal.SizeOf(devdata);

                // Get first device matching device criterion.
                for (uint i = 0; ; i++)
                {

                    SetupDiEnumDeviceInfo(info,
                        i,
                        out devdata);
                    // if no items match filter, throw
                    if (Marshal.GetLastWin32Error() == ERROR_NO_MORE_ITEMS)
                        CheckError("No device found matching filter.", 0xcffff);
                    CheckError("SetupDiEnumDeviceInfo");

                    string devicepath = GetStringPropertyForDevice(info,
                                               devdata, DEVPKEY_Device_HardwareIds);
                    UInt32 problemCode = GetUint32PropertyForDevice(info,
                                               devdata, DEVPKEY_Device_ProblemCode);

                    UInt32 statusNode = GetUint32PropertyForDevice(info,
                                               devdata, DEVPKEY_Device_DevNodeStatus);

                    if (devicepath != null && filter(devicepath))
                        break;

                }
                SP_DEVICE_INTERFACE_DATA dia = new SP_DEVICE_INTERFACE_DATA();
                dia.cbSize = Marshal.SizeOf(dia);
                // build a DevInfo Data structure
                SP_DEVINFO_DATA da = new SP_DEVINFO_DATA();
                da.cbSize = (uint)Marshal.SizeOf(da);

                //Guid guid = Guid.Parse("{3abf6f2d-71c4-462a-8a92-1e6861e6af27}");
                Guid guid = devdata.classGuid;
                for (uint dwCount = 0; SetupDiEnumDeviceInterfaces(info, IntPtr.Zero, ref guid, dwCount, ref dia); ++dwCount)
                {


                    // build a Device Interface Detail Data structure
                    SP_DEVICE_INTERFACE_DETAIL_DATA didd = new SP_DEVICE_INTERFACE_DETAIL_DATA();
                    if (IntPtr.Size == 8) // for 64 bit operating systems
                        didd.cbSize = 8;
                    else
                        didd.cbSize = 4 + Marshal.SystemDefaultCharSize; // for 32 bit systems

                    // now we can get some more detailed information
                    uint nRequiredSize = 0;
                    uint nBytes = 256;
                    if (SetupDiGetDeviceInterfaceDetail(info, ref dia, ref didd, nBytes, out nRequiredSize, ref da))
                    {
                    }

                }

                SP_CLASSINSTALL_HEADER header = new SP_CLASSINSTALL_HEADER();
                header.cbSize = (UInt32)Marshal.SizeOf(header);
                header.InstallFunction = DIF_PROPERTYCHANGE;

                SP_PROPCHANGE_PARAMS propchangeparams = new SP_PROPCHANGE_PARAMS();
                propchangeparams.ClassInstallHeader = header;
                propchangeparams.StateChange = disable ? DICS_DISABLE : DICS_ENABLE;
                propchangeparams.Scope = DICS_FLAG_GLOBAL;
                propchangeparams.HwProfile = 0;

                SetupDiSetClassInstallParams(info,
                    ref devdata,
                    ref propchangeparams,
                    (UInt32)Marshal.SizeOf(propchangeparams));
                CheckError("SetupDiSetClassInstallParams");

                SetupDiChangeState(
                    info,
                    ref devdata);
                CheckError("SetupDiChangeState");
            }
            finally
            {
                if (info != IntPtr.Zero)
                    SetupDiDestroyDeviceInfoList(info);
            }
        }
        private static void CheckError(string message, int lasterror = -1)
        {

            int code = lasterror == -1 ? Marshal.GetLastWin32Error() : lasterror;
            if (code != 0)
                throw new ApplicationException(
                    String.Format("Error disabling hardware device (Code {0}): {1}",
                        code, message));
        }

        private static UInt32 GetUint32PropertyForDevice(IntPtr info, SP_DEVINFO_DATA devdata,
            DEVPROPKEY key)
        {
            uint proptype, outsize;
            IntPtr buffer = IntPtr.Zero;
            UInt32 ret;
            try
            {
                uint buflen = 4;
                buffer = Marshal.AllocHGlobal((int)buflen);
                SetupDiGetDevicePropertyW(
                    info,
                    ref devdata,
                    ref key,
                    out proptype,
                    buffer,
                    buflen,
                    out outsize,
                    0);
                byte[] lbuffer = new byte[outsize];
                Marshal.Copy(buffer, lbuffer, 0, (int)outsize);

                int errcode = Marshal.GetLastWin32Error();
                if (errcode == ERROR_ELEMENT_NOT_FOUND) return (UInt32.MaxValue);
                CheckError("SetupDiGetDeviceProperty", errcode);

                return BitConverter.ToUInt32(lbuffer, 0);
            }
            finally
            {
                if (buffer != IntPtr.Zero)
                    Marshal.FreeHGlobal(buffer);
            }
        }

        private static string GetStringPropertyForDevice(IntPtr info, SP_DEVINFO_DATA devdata,
            DEVPROPKEY key)
        {
            uint proptype, outsize;
            IntPtr buffer = IntPtr.Zero;
            try
            {
                uint buflen = 512;
                buffer = Marshal.AllocHGlobal((int)buflen);
                SetupDiGetDevicePropertyW(
                    info,
                    ref devdata,
                    ref key,
                    out proptype,
                    buffer,
                    buflen,
                    out outsize,
                    0);
                byte[] lbuffer = new byte[outsize];
                Marshal.Copy(buffer, lbuffer, 0, (int)outsize);
                int errcode = Marshal.GetLastWin32Error();
                if (errcode == ERROR_ELEMENT_NOT_FOUND) return null;
                CheckError("SetupDiGetDeviceProperty", errcode);
                return Encoding.Unicode.GetString(lbuffer);
            }
            finally
            {
                if (buffer != IntPtr.Zero)
                    Marshal.FreeHGlobal(buffer);
            }
        }
    }
}
