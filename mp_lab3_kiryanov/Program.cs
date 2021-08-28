using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Configuration;
using System.Data;
using System.Reflection;
using System.Threading;


namespace mp_lab3_kiryanov
{
    static class Program
    {
        private const string MutexName = "MUTEX_SINGLEINSTANCEANDNAMEDPIPE";
        private static bool _firstApplicationInstance;
        private static Mutex _mutexApplication;
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            if (IsApplicationFirstInstance())
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new Form1());
            }
            else
                Application.Exit();

        }

        private static bool IsApplicationFirstInstance()
        {
            // Allow for multiple runs but only try and get the mutex once
            if (_mutexApplication == null)
            {
                _mutexApplication = new Mutex(true, MutexName, out _firstApplicationInstance);
            }

            return _firstApplicationInstance;
        }

    }
}
