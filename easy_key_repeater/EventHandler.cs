using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace easy_key_repeater
{
    partial class Form1
    {
        public event System.EventHandler isRunningChanged;
        void statusIsRunning()
        {
            if(isRunning)
                toolStripStatusLabel1.Text = "Status: Running";
            else
                toolStripStatusLabel1.Text = "Status: Stoped";
        }

        private void InitializeEventHandler()
        {
            
            //button
            exit_btn.Click += new EventHandler(exit_btn_click);
            start_btn.Click += new EventHandler(start_btn_click);
            stop_btn.Click += new EventHandler(stop_btn_click);
            new_button.Click += new EventHandler(new_btn_click);
            save_button.Click += new EventHandler(save_btn_click);
            add_row_btn.Click += new EventHandler(add_row_btn_click);
            clear_list_view_btn.Click += new EventHandler(clear_list_view_btn_click);
            move_up_row_btn.Click += new EventHandler(move_up_row_btn_click);
            move_down_row_btn.Click += new EventHandler(move_down_row_btn_click);
            delete_row_btn.Click += new EventHandler(delete_row_btn_click);
            update_row_btn.Click += new EventHandler(update_row_btn_click);
            clear_row_info_btn.Click += new EventHandler(clear_row_info_btn_click);

            //RadioButton
            repeat_unlimited.CheckedChanged += new EventHandler(radio_repeat_unlimited_check);
            repeat_once.CheckedChanged += new EventHandler(radio_repeat_once_check);
            repeat_number.CheckedChanged += new EventHandler(radio_repeat_number_check);
            desktop_radio.CheckedChanged += new EventHandler(radio_desktop_check);
            windows_radio.CheckedChanged += new EventHandler(radio_windows_check);

            //ComboBox
            select_window_cbB.DropDown += new EventHandler(select_window_cbB_DropDown);
            select_window_cbB.SelectedIndexChanged += new EventHandler(select_window_cbB_SelectedIndexChange);

            //ListView
            //main_view_listView.ControlAdded += new ControlEventHandler(ControlChanged);
            //main_view_listView.ControlRemoved += new ControlEventHandler(ControlChanged);
            main_view_listView.SelectedIndexChanged += new EventHandler(main_view_listView_SelectedIndexChanged);
        }
    }
}
