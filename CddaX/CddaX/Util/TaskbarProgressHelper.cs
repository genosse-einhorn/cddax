using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.ComponentModel;
using System.Windows.Forms;

namespace CddaX.Util
{
    public class TaskbarProgressHelper : Component
    {
        private ITaskbarList4 m_taskbarList;

        public TaskbarProgressHelper()
        {
            try
            {
                m_taskbarList = (ITaskbarList4)new CTaskbarList();
            }
            catch (Exception)
            {
                // Taskbar list not found, will happen in Windows Pre-7
                // just ignore it
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (m_taskbarList != null)
            {
                Marshal.ReleaseComObject(m_taskbarList);
                m_taskbarList = null;
            }
            
            base.Dispose(disposing);
        }

        public void SetProgressState(Control window, TaskbarProgressFlag status)
        {
            if (m_taskbarList != null && window != null && window.IsHandleCreated)
            {
                m_taskbarList.SetProgressState(window.Handle, status);
            }
        }

        public void SetProgressValue(Control window, ulong value, ulong max)
        {
            if (m_taskbarList != null && window != null && window.IsHandleCreated)
            {
                m_taskbarList.SetProgressValue(window.Handle, value, max);
            }
        }

        [ComImportAttribute()]
        [GuidAttribute("c43dc798-95d1-4bea-9030-bb99e2983a1a")]
        [InterfaceTypeAttribute(ComInterfaceType.InterfaceIsIUnknown)]
        private interface ITaskbarList4
        {
            // ITaskbarList
            [PreserveSig]
            void HrInit();
            [PreserveSig]
            void AddTab(IntPtr hwnd);
            [PreserveSig]
            void DeleteTab(IntPtr hwnd);
            [PreserveSig]
            void ActivateTab(IntPtr hwnd);
            [PreserveSig]
            void SetActiveAlt(IntPtr hwnd);

            // ITaskbarList2
            [PreserveSig]
            void MarkFullscreenWindow(
                IntPtr hwnd,
                [MarshalAs(UnmanagedType.Bool)] bool fFullscreen);

            // ITaskbarList3
            [PreserveSig]
            void SetProgressValue(IntPtr hwnd, UInt64 ullCompleted, UInt64 ullTotal);
            [PreserveSig]
            void SetProgressState(IntPtr hwnd, TaskbarProgressFlag tbpFlags);
            [PreserveSig]
            void RegisterTab(IntPtr hwndTab, IntPtr hwndMDI);
            [PreserveSig]
            void UnregisterTab(IntPtr hwndTab);
            [PreserveSig]
            void SetTabOrder(IntPtr hwndTab, IntPtr hwndInsertBefore);
            [PreserveSig]
            void SetTabActive(IntPtr hwndTab, IntPtr hwndInsertBefore, uint dwReserved);
            void ThumbBarAddButtons(
                IntPtr hwnd,
                uint cButtons,
                IntPtr pButtons);
            void ThumbBarUpdateButtons(
                IntPtr hwnd,
                uint cButtons,
                IntPtr pButtons);
            [PreserveSig]
            void ThumbBarSetImageList(IntPtr hwnd, IntPtr himl);
            [PreserveSig]
            void SetOverlayIcon(
              IntPtr hwnd,
              IntPtr hIcon,
              [MarshalAs(UnmanagedType.LPWStr)] string pszDescription);
            [PreserveSig]
            void SetThumbnailTooltip(
                IntPtr hwnd,
                [MarshalAs(UnmanagedType.LPWStr)] string pszTip);
            [PreserveSig]
            void SetThumbnailClip(
                IntPtr hwnd,
                IntPtr prcClip);

            // ITaskbarList4
            void SetTabProperties(IntPtr hwndTab, int stpFlags);
        }

        [GuidAttribute("56FDF344-FD6D-11d0-958A-006097C9A090")]
        [ClassInterfaceAttribute(ClassInterfaceType.None)]
        [ComImportAttribute()]
        private class CTaskbarList { }
    }
    
    public enum TaskbarProgressFlag
    {
        NoProgress = 0,
        Indeterminate = 0x1,
        Normal = 0x2,
        Error = 0x4,
        Paused = 0x8
    }
}
