using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace easy_key_repeater
{
    public class FormController
    {
        //ComboBox
        public static void load_window_selected()
        {
            Form1.select_window_cbB.DisplayMember = "Text";
            Form1.vk_key_cbB.ValueMember = "Value";
            Process[] processesRunning = Process.GetProcesses();
            foreach (Process process in processesRunning)
            {
                if (process.MainWindowTitle != "")
                {
                    Form1.select_window_cbB.Items.Add(new { Text = Form1.select_window_cbB.Items.Count + ". " + process.ProcessName + " - " + process.MainWindowTitle, Value = process.MainWindowHandle });
                }
            }
        }
        public static void load_vk_key()
        {
            Form1.vk_key_cbB.DisplayMember = "Text";
            Form1.vk_key_cbB.ValueMember = "Value";
            int index = 0;
            foreach (KeyBoardUtility.Key key in KeyBoardUtility.KeysList)
            {
                Form1.vk_key_cbB.Items.Add(new { Text = key.name, Value = index });
                index++;
            }
            Form1.vk_key_cbB.SelectedIndex = -1;
            //Form1.vk_key_cbB.SelectedItem = new { Text = "--Select--" };
        }
    }
}
