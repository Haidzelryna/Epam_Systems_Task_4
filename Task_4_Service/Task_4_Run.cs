using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using Task_4;
//using System.Timers;
using System.Diagnostics;

namespace Task_4_Service
{
    public partial class Task_4_Run : ServiceBase
    {
        private EventLog eventLog = new EventLog();

        public Task_4_Run()
        {
            InitializeComponent();

            EventLog eventLog = new EventLog();
            eventLog = new System.Diagnostics.EventLog();
            eventLog.Source = "MySource";
            eventLog.Log = "MyNewLog";
        }

        protected override void OnStart(string[] args)
        {
            eventLog.WriteEntry("In OnStart.");
            string[] str = new string[3];
            Task_4.Program.Main(str);
        }

        protected override void OnStop()
        {
            eventLog.WriteEntry("In OnStop.");
        }

        //public void OnTimer(object sender, ElapsedEventArgs args)
        //{
        //    // TODO: Insert monitoring activities here.
        //    eventLog.WriteEntry("Monitoring the System", EventLogEntryType.Information, eventId++);
        //}
    }
}
