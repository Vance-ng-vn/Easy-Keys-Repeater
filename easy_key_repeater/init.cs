using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace easy_key_repeater
{
    public class init
    {
        public static void start_button()
        {
            Form1.new_button.Enabled = false;
            Form1.load_button.Enabled = false;
        }
        public static void stop_button()
        {
            Form1.start_btn.Enabled = true;
            Form1.new_button.Enabled = true;
            Form1.load_button.Enabled = true;
        }
    }
}
