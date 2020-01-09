using System.Diagnostics;
using System.ServiceProcess;
using Task_4;
//using System.Timers;

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
            //Task_4.Program.Main(str);
            MainMethods.watcherCreated();
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
