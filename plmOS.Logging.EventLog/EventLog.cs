/*  
  plmOS Logging provides .NET libraries for storing log messages to various targets.

  Copyright (C) 2015 Processwall Limited.

  This program is free software: you can redistribute it and/or modify
  it under the terms of the GNU Affero General Public License as published
  by the Free Software Foundation, either version 3 of the License, or
  (at your option) any later version.

  This program is distributed in the hope that it will be useful,
  but WITHOUT ANY WARRANTY; without even the implied warranty of
  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
  GNU Affero General Public License for more details.

  You should have received a copy of the GNU Affero General Public License
  along with this program.  If not, see http://opensource.org/licenses/AGPL-3.0.
 
  Company: Processwall Limited
  Address: The Winnowing House, Mill Lane, Askham Richard, York, YO23 3NW, United Kingdom
  Tel:     +44 113 815 3440
  Email:   support@processwall.com
*/

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
