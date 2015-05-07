using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GTA5ModManager
{
    class Functions
    {

        public void log(RichTextBox consoleBox, string message)
        {
            consoleBox.AppendText(message + "\n");
        }

     
    }
}
