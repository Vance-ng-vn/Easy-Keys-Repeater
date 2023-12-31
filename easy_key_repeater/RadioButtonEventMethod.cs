using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace easy_key_repeater
{
    partial class Form1
    {
        private void radio_repeat_unlimited_check(object sender, EventArgs e)
        {
            repeat_mode_config.Enabled = true;
            repeat_mode_unlimited_config.Enabled = true;
            repeat_number_textBox.Enabled = false;
        }
        private void radio_repeat_number_check(object sender, EventArgs e)
        {
            repeat_mode_config.Enabled = true;
            repeat_mode_unlimited_config.Enabled = false;
            repeat_number_textBox.Enabled = true;
        }
        private void radio_repeat_once_check(object sender, EventArgs e)
        {
            repeat_mode_config.Enabled = false;
        }
        private void radio_desktop_check(object sender, EventArgs e)
        {
            select_window_cbB.Enabled = false;
            label5.Enabled = false;
        }
        private void radio_windows_check(object sender, EventArgs e)
        {
            select_window_cbB.Enabled = true;
            label5.Enabled = true;
        }
    }
}
