using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Sc2Hack.Classes.BackEnds
{
    public class InteropCalls
    {
        #region kernel32-dll

        /* ReadProcessMemory */
        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool ReadProcessMemory(
            IntPtr hProcess,
            IntPtr lpBaseAddress,
            [Out] byte[] lpBuffer,
            int dwSize,
            out IntPtr lpNumberOfBytesRead
            );

        /* OpenProcess */
        [DllImport("kernel32.dll")]
        public static extern IntPtr OpenProcess(int dwDesiredAccess, bool bInheritHandle, int dwProcessId);

        /* CloseHandle */
        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool CloseHandle(IntPtr hObject);

        /* VirtualProtect */
        [DllImport("kernel32.dll")]
        public static extern bool VirtualProtect(IntPtr lpAddress, uint dwSize,
           uint flNewProtect, out uint lpflOldProtect);

       

        #endregion

        #region user32.dll

        /* GetAsyncKeyState */
        [DllImport("user32.dll")]
        public static extern short GetAsyncKeyState(Keys vKey);

        /* GetWindowLongPtr */
        [DllImport("user32.dll", EntryPoint = "GetWindowLong")]
        public static extern IntPtr GetWindowLongPtr(IntPtr hWnd, int nIndex);

        /* SetForegroundWindow */
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SetForegroundWindow(IntPtr hWnd);

        /* GetForegroundWindow */
        [DllImport("user32.dll")]
        public static extern IntPtr GetForegroundWindow();

        /* GetWindowLong */
        [DllImport("user32.dll", SetLastError = true)]
        public static extern UInt32 GetWindowLong(IntPtr hWnd, int nIndex);

        /* SetWindowLong */
        [DllImport("user32.dll")]
        public static extern int SetWindowLong(IntPtr hWnd, int nIndex, IntPtr dwNewLong);

        /* SendMessage */ 
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr SendMessage(IntPtr hWnd, UInt32 msg, IntPtr wParam, IntPtr lParam);

        /* GetActiveWindow */
        [DllImport("user32.dll")]
        public static extern IntPtr GetActiveWindow();

        /* SetActiveWindow */
        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr SetActiveWindow(IntPtr hWnd);

        #endregion

        #region dwmapi.dll

        [DllImport("dwmapi.dll", PreserveSig = false)]
        public static extern bool DwmIsCompositionEnabled();

        [DllImport("dwmapi.dll", EntryPoint = "DwmEnableComposition")]
        public static extern uint Win32DwmEnableComposition(uint uCompositionAction);

        #endregion

        [Flags]
        public enum ProcessAccess
        {
            /// <summary>
            /// Required to create a thread.
            /// </summary>
            CreateThread = 0x0002,

            /// <summary>
            ///
            /// </summary>
            SetSessionId = 0x0004,

            /// <summary>
            /// Required to perform an operation on the address space of a process
            /// </summary>
            VmOperation = 0x0008,

            /// <summary>
            /// Required to read memory in a process using ReadProcessMemory.
            /// </summary>
            VmRead = 0x0010,

            /// <summary>
            /// Required to write to memory in a process using WriteProcessMemory.
            /// </summary>
            VmWrite = 0x0020,

            /// <summary>
            /// Required to duplicate a handle using DuplicateHandle.
            /// </summary>
            DupHandle = 0x0040,

            /// <summary>
            /// Required to create a process.
            /// </summary>
            CreateProcess = 0x0080,

            /// <summary>
            /// Required to set memory limits using SetProcessWorkingSetSize.
            /// </summary>
            SetQuota = 0x0100,

            /// <summary>
            /// Required to set certain information about a process, such as its priority class (see SetPriorityClass).
            /// </summary>
            SetInformation = 0x0200,

            /// <summary>
            /// Required to retrieve certain information about a process, such as its token, exit code, and priority class (see OpenProcessToken).
            /// </summary>
            QueryInformation = 0x0400,

            /// <summary>
            /// Required to suspend or resume a process.
            /// </summary>
            SuspendResume = 0x0800,

            /// <summary>
            /// Required to retrieve certain information about a process (see GetExitCodeProcess, GetPriorityClass, IsProcessInJob, QueryFullProcessImageName).
            /// A handle that has the PROCESS_QUERY_INFORMATION access right is automatically granted PROCESS_QUERY_LIMITED_INFORMATION.
            /// </summary>
            QueryLimitedInformation = 0x1000,

            /// <summary>
            /// Required to wait for the process to terminate using the wait functions.
            /// </summary>
            Synchronize = 0x100000,

            /// <summary>
            /// Required to delete the object.
            /// </summary>
            Delete = 0x00010000,

            /// <summary>
            /// Required to read information in the security descriptor for the object, not including the information in the SACL.
            /// To read or write the SACL, you must request the ACCESS_SYSTEM_SECURITY access right. For more information, see SACL Access Right.
            /// </summary>
            ReadControl = 0x00020000,

            /// <summary>
            /// Required to modify the DACL in the security descriptor for the object.
            /// </summary>
            WriteDac = 0x00040000,

            /// <summary>
            /// Required to change the owner in the security descriptor for the object.
            /// </summary>
            WriteOwner = 0x00080000,

            StandardRightsRequired = 0x000F0000,

            /// <summary>
            /// All possible access rights for a process object.
            /// </summary>
            AllAccess = StandardRightsRequired | Synchronize | 0xFFFF
        }

        [Flags]
        public enum Protection
        {
            PageNoaccess = 0x01,
            PageReadonly = 0x02,
            PageReadwrite = 0x04,
            PageWritecopy = 0x08,
            PageExecute = 0x10,
            PageExecuteRead = 0x20,
            PageExecuteReadwrite = 0x40,
            PageExecuteWritecopy = 0x80,
            PageGuard = 0x100,
            PageNocache = 0x200,
            PageWritecombine = 0x400
        }

        [Flags]
        public enum Gwl
        {
            ExStyle = -20
        }

        [Flags]
        public enum Ws
        {
            Caption = 0x00C00000,
            Border = 0x00800000,
            ExLayered = 0x80000,
            Sysmenu = 0x00080000,
            Minimizebox = 0x00020000,
            ExTransparent = 0x20

        }

        [Flags]
        public enum WMessages
        {
            Lbuttondown = 0x201, //Left mousebutton down
            Lbuttonup = 0x202,  //Left mousebutton up
            Lbuttondblclk = 0x203, //Left mousebutton doubleclick
            Rbuttondown = 0x204, //Right mousebutton down
            Rbuttonup = 0x205,   //Right mousebutton up
            Rbuttondblclk = 0x206, //Right mousebutton doubleclick
            Keydown = 0x100,  //Key down
            Keyup = 0x101,   //Key up
        }

        [Flags]
        public enum DwmEc : uint
        {
            DisableComposition = 0,
            EnableComposition = 1,
        }

        public static byte[] Help_ReadProcessMemory(IntPtr handle, int address, int size)
        {
            IntPtr bytesRead;
            byte[] buffer = new byte[size];

            ReadProcessMemory(handle, (IntPtr) address, buffer, size, out bytesRead);

            return buffer;
        }

        public static IntPtr Help_OpenProcess(int dwDesiredAccess, bool bInheritHandle, string strProcessName)
        {
            if (!HelpFunctions.CheckProcess(strProcessName))
                return IntPtr.Zero;

            return OpenProcess(dwDesiredAccess, bInheritHandle, HelpFunctions.GetProcess(strProcessName).Id);
        }
    }
}
