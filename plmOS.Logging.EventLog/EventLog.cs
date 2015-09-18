using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace plmOS.Logging
{
    public class EventLog : ITarget
    {
        public String Location { get; private set; }

        public String Source { get; private set; }

        private static System.Diagnostics.EventLogEntryType ConvertLevel(plmOS.Log.Levels Level)
        {
            switch (Level)
            {
                case plmOS.Log.Levels.FAT:
                case plmOS.Log.Levels.ERR:
                    return System.Diagnostics.EventLogEntryType.Error;
                case plmOS.Log.Levels.WAR:
                    return System.Diagnostics.EventLogEntryType.Warning;
                default:
                    return System.Diagnostics.EventLogEntryType.Information;
            }
        }

        public void Store(Message Message)
        {
            System.Diagnostics.EventLog.WriteEntry(this.Source, Message.Text, ConvertLevel(Message.Level));
        }

        public void CreateSource()
        {
            if (!System.Diagnostics.EventLog.SourceExists(this.Source))
            {
                System.Diagnostics.EventLog.CreateEventSource(this.Source, this.Location);
            }
        }

        public void Dispose()
        {

        }

        public EventLog(String Location, String Source)
        {
            this.Location = Location;
            this.Source = Source;
        }
    }
}
