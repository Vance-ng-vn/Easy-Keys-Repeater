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
        private void start_btn_click(object sender, EventArgs e)
        {
            init.start_button();
            uint loop;
            if (repeat_unlimited.Checked) loop = 0;
            else if (repeat_once.Checked) loop = 1;
            else loop = uint.Parse(repeat_number_textBox.Text);
            start_btn.Enabled = false;
            if (desktop_radio.Checked == true)
            {
                turnOnAutoDesktop(loop);
            }
            else if (windows_radio.Checked == true)
            {
                if (select_window_cbB.SelectedIndex != -1)
                {
                    //IntPtr hWnd = (select_window_cbB.SelectedItem as dynamic).Value;
                    turnOnAutoWindow(this.hWnd, loop);
                }
                else
                {
                    turnOnAutoWindow(this.hWnd, loop);
                }

            }
        }
        private void stop_btn_click(object sender, EventArgs e)
        {
            init.stop_button();
            isRunning = false;
        }
        private void exit_btn_click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void clear_list_view_btn_click(object sender, EventArgs e)
        {
            main_view_listView.Items.Clear();
            FileUtility.CreateTmp();
        }
        private void save_btn_click(object sender, EventArgs e)
        {

        }
        private void new_btn_click(object sender, EventArgs e)
        {
            NEW_INIT();
        }
        private void add_row_btn_click(object sender, EventArgs e)
        {
            string inputType = "";
            string keyInput = (vk_key_cbB.SelectedItem as dynamic).Text;
            var keyIndex = (vk_key_cbB.SelectedItem as dynamic).Value;

            string delay = vk_key_delay_textBox.Text;
            string tolerance = vk_key_tolerance_textBox.Text;
            string randomPercent = vk_key_random_percent_textBox.Text;
            if (keyInput != null)
            {
                if (keyIndex is string)
                {
                    inputType = "Click";
                    keyInput = keyIndex;
                    string[] line = { inputType, keyInput, delay, tolerance, randomPercent};
                    FileUtility.AppendRowKey(line);
                }
                else {
                    inputType = "Key";
                    uint wParam = KeyBoardUtility.KeysList[keyIndex].wParam; //*
                    uint scancode = KeyBoardUtility.KeysList[keyIndex].scancode; //*
                    string[] line = { inputType, keyInput, delay, tolerance, randomPercent, wParam.ToString(), scancode.ToString() };
                    FileUtility.AppendRowKey(line);
                }
            }
            
            //string[] row =
            //{
            //    main_view_listView.Items.Count.ToString(),
            //    inputType,
            //    keyInput,
            //    delay,
            //    tolerance,
            //    randomPercent
            //};
            //ListViewItem listViewItem = new ListViewItem(row);
            //main_view_listView.Items.Add(listViewItem)

            main_view_listView.Items.Clear();
            loadFromTmp();
        }

        private void update_row_btn_click(object sender, EventArgs e)
        {
            if (main_view_listView.Items.Count == 0) return;
            int current_index = main_view_listView.SelectedIndices[0];

            string inputType = "Click";
            string keyInput = (vk_key_cbB.SelectedItem as dynamic).Text;
            int keyIndex = (vk_key_cbB.SelectedItem as dynamic).Value;
            uint wParam = KeyBoardUtility.KeysList[keyIndex].wParam; //*
            uint scancode = KeyBoardUtility.KeysList[keyIndex].scancode; //*
            if (keyInput != null)
            {
                inputType = "Key";
            }
            string delay = vk_key_delay_textBox.Text;
            string tolerance = vk_key_tolerance_textBox.Text;
            string randomPercent = vk_key_random_percent_textBox.Text;
            string[] line = { inputType, keyInput, delay, tolerance, randomPercent, wParam.ToString(), scancode.ToString() };
            bool result = FileUtility.UpdateRow(current_index, line);
            if (result)
            {
                main_view_listView.Items.Clear();
                loadFromTmp();
                main_view_listView.Items[current_index].Selected = true;
                main_view_listView.Select();
            }
        }

        private void clear_row_info_btn_click(object sender, EventArgs e)
        {
            vk_key_cbB.SelectedIndex = -1;
            vk_key_delay_textBox.Text = "500";
            vk_key_tolerance_textBox.Text = "50";
            vk_key_random_percent_textBox.Text = "100";
        }
        private void move_up_row_btn_click(object sender, EventArgs e)
        {
            if (main_view_listView.Items.Count == 0) return;
            int current_index = main_view_listView.SelectedIndices[0];
            bool result = FileUtility.SwapLine(current_index, current_index - 1);
            if (result)
            {
                main_view_listView.Items.Clear();
                loadFromTmp();
                main_view_listView.Items[current_index - 1].Selected = true;
                main_view_listView.Select();
            }
        }
        private void move_down_row_btn_click(object sender, EventArgs e)
        {
            if (main_view_listView.Items.Count == 0) return;
            int current_index = main_view_listView.SelectedIndices[0];
            bool result = FileUtility.SwapLine(current_index, current_index + 1);
            if (result)
            {
                main_view_listView.Items.Clear();
                loadFromTmp();
                main_view_listView.Items[current_index + 1].Selected = true;
                main_view_listView.Select();
            }
            else main_view_listView.Select();
        }
        private void delete_row_btn_click(object sender, EventArgs e)
        {
            if (main_view_listView.Items.Count == 0) return;
            int current_index = main_view_listView.SelectedIndices[0];
            bool result = FileUtility.DeleteLine(current_index);
            if (result)
            {
                main_view_listView.Items.Clear();
                loadFromTmp();
                if(current_index < main_view_listView.Items.Count)
                    main_view_listView.Items[current_index].Selected = true;
                main_view_listView.Select();
            }
        }
    }
}
