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
using System.Collections.Concurrent;
using System.Threading;

namespace plmOS
{
    public class Log : IDisposable
    {
        private const int delay = 100;

        public enum Levels { FAT = 0, ERR = 1, WAR = 2, INF = 3, DEB = 4 };

        public ITarget Target { get; private set; }

        public Levels Level { get; set; }

        public void Add(Levels Level, String Text)
        {
            if (Level <= this.Level)
            {
                Message message = new Message(Level, Text);
                this.Messages.Enqueue(message);
            }
        }

        private Thread Worker;

        private ConcurrentQueue<Message> Messages;

        private void ProcessQueue()
        {
            while(true)
            {
                Message message = null;

                while(this.Messages.TryDequeue(out message))
                {
                    if (this.Target != null)
                    {
                        this.Target.Store(message);
                    }
                }

                Thread.Sleep(delay);
            }
        }
        
        public void Dispose()
        {
            // Stop Workder
            if (this.Worker != null)
            {
                this.Worker.Abort();
            }

            // Dispose Target
            if (this.Target != null)
            {
                this.Target.Dispose();
            }
        }

        public Log(ITarget Target)
        {
            this.Target = Target;
            this.Level = Levels.ERR;

            this.Messages = new ConcurrentQueue<Message>();

            this.Worker = new Thread(this.ProcessQueue);
            this.Worker.IsBackground = true;
            this.Worker.Start();
        }
    }
}
