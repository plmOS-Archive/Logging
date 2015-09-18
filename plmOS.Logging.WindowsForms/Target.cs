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
using System.Windows.Forms;

namespace plmOS.Logging.WindowsForms
{
    public class Target : ITarget
    {
        public TextBox TextBox { get; private set; }

        public void Store(Message Message)
        {
            String messagestring = Message.Date.ToString("yyyy-MM-dd HH:mm:ss") + " " + Message.Level.ToString() + " " + Message.Text + Environment.NewLine;

            if (this.TextBox.InvokeRequired)
            {
                this.TextBox.Invoke(new Action(() => this.TextBox.AppendText(messagestring)));
            }
            else
            {
                this.TextBox.AppendText(messagestring);
            }
        }

        public void Dispose()
        {

        }

        public Target(TextBox TextBox)
        {
            this.TextBox = TextBox;
        }
    }
}
