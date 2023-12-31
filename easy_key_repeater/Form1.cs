using easy_key_repeater.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using easy_key_repeater;
using System.IO;

namespace easy_key_repeater
{
    public partial class Form1 : Form
    {
        const string form_title = "Easy Keys Repeater";
        IntPtr hWnd;
        bool _isRunning = false;
        bool isRunning
        {
            get
            {
                return _isRunning;
            }
            set
            {
                _isRunning = value;
                statusIsRunning();
            }
        }

        public static Panel choose_windows_type_panel;
        public static Panel repeat_mode_config;
        public static Panel repeat_mode_unlimited_config;

        public static GroupBox repeat_configs_group;
        public static GroupBox settings_group;
        public static GroupBox action_group;

        public static Button new_button;
        public static Button save_button;
        public static Button load_button;
        public static Button move_up_row_btn;
        public static Button move_down_row_btn;
        public static Button delete_row_btn;
        public static Button clear_list_view_btn;
        public static Button add_row_btn;
        public static Button update_row_btn;
        public static Button clear_row_info_btn;
        public static Button start_btn;
        public static Button stop_btn;
        public static Button exit_btn;

        public static RadioButton desktop_radio;
        public static RadioButton windows_radio;
        public static RadioButton repeat_unlimited;
        public static RadioButton repeat_once;
        public static RadioButton repeat_number;

        public static TextBox repeat_number_textBox;
        public static TextBox vk_key_delay_textBox;
        public static TextBox vk_key_tolerance_textBox;
        public static TextBox vk_key_random_percent_textBox;
        public static TextBox repeat_delay_textBox;

        public static ComboBox select_window_cbB;
        public static ComboBox vk_key_cbB;


        public static ListView main_view_listView;

        public static StatusStrip main_status_bottom;

        public Form1()
        {
            InitializeComponent();
            GUI_Init();
            InitializeEventHandler();
            if (File.Exists(FileUtility.tmpFile)){
                this.Text = form_title + " - Temporary saved";
                main_view_listView.Items.Clear();
                loadFromTmp();
                new_button.Enabled = true;
            }
            else
                NEW_INIT();
        }

        public void GUI_Init()
        {
            //Panel
            choose_windows_type_panel = panel1;
            repeat_mode_config = panel2;
            repeat_mode_unlimited_config = panel3;
            //GroupBox
            action_group = groupBox2;
            repeat_configs_group = groupBox1;
            settings_group = groupBox3;
            //Button
            new_button = button15;
            save_button = button5;
            load_button = button6;
            move_up_row_btn = button2;
            move_down_row_btn = button3;
            delete_row_btn = button4;
            clear_list_view_btn = button14;
            add_row_btn = button1;
            update_row_btn = button12;
            clear_row_info_btn = button13;
            start_btn = button9;
            stop_btn = button10;
            exit_btn = button11;
            //RadioButton
            desktop_radio = radioButton1;
            windows_radio = radioButton2;
            repeat_unlimited = radioButton3;
            repeat_once = radioButton4;
            repeat_number = radioButton5;
            //TextBox
            repeat_number_textBox = textBox1;
            vk_key_delay_textBox = textBox2;
            vk_key_tolerance_textBox = textBox3;
            vk_key_random_percent_textBox = textBox4;
            repeat_delay_textBox = textBox5;
            //Combobox
            select_window_cbB = comboBox1;
            vk_key_cbB = comboBox2;
            //ListView
            main_view_listView = listView1;

            //startup_config
            desktop_radio.Checked = true;
            new_button.Enabled = false;
            save_button.Enabled = false;

            main_view_listView.Items.Clear();

            repeat_unlimited.Checked = true;
            repeat_mode_config.Enabled = true;
            repeat_mode_unlimited_config.Enabled = true;
            repeat_number_textBox.Enabled = false;
            //
            FormController.load_vk_key();
            //main_view_listView.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
        }
        private void loadFromTmp()
        {
            List<string[]> list = FileUtility.loadFromTmp();
            foreach(string[] line in list)
            {
                if (line.Length == 0) return;
                string[] row = {
                    main_view_listView.Items.Count.ToString(),
                    line[0],
                    line[1],
                    line[2],
                    line[3],
                    line[4]
                };
                ListViewItem item = new ListViewItem(row);
                main_view_listView.Items.Add(item);
            }
            main_view_listView.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
        }
        private void NEW_INIT()
        {
            this.Text = form_title + " - New File";
            FileUtility.CreateTmp();
            main_view_listView.Items.Clear();
        }
        private async void turnOnAutoWindow(IntPtr hWnd, uint loop)
        {
            KeyBoardUtility keyBoardUtility = new KeyBoardUtility();
            //GetListAction
            List<string[]> list = FileUtility.loadFromTmp();
            isRunning = true;
            if (loop == 0)
            {
                while (isRunning)
                {
                    if (hWnd != null)
                    {
                        //Foreach List Action
                        foreach (string[] values in list)
                        {
                            //KeyPress Action
                            if (values[0].Equals("Key"))
                            {

                                uint wParam = uint.Parse(values[5]);
                                uint scancode = uint.Parse(values[6]);
                                int delay = int.Parse(values[2]);
                                int tolerance = int.Parse(values[3]);
                                byte randomPercent = byte.Parse(values[4]);

                                Random rnd = new Random();
                                int rndNumber = rnd.Next(0, 100);
                                if (rndNumber <= randomPercent && isRunning)
                                {
                                    await TaskDelay.PutTaskDelay(rnd.Next(delay - tolerance, delay + tolerance));
                                    if (isRunning) keyBoardUtility.KeyPressWindow(hWnd, wParam, scancode);
                                    else return;
                                }

                            }
                            else if (values[0].Equals("Click")){
                                IntPtr Parent_hWnd = hWnd;
                                while (GetParent(Parent_hWnd) != IntPtr.Zero)
                                {
                                    Parent_hWnd = GetParent(Parent_hWnd);
                                }
                                string point_string = values[1];
                                int delay = int.Parse(values[2]);
                                int tolerance = int.Parse(values[3]);
                                byte randomPercent = byte.Parse(values[4]);
                                string[] point_string_array = values[1].Split(',');
                                Point point = new Point(int.Parse(point_string_array[0]), int.Parse(point_string_array[1]));
                                Random rnd = new Random();
                                int rndNumber = rnd.Next(0, 100);
                                if (rndNumber <= randomPercent && isRunning)
                                {
                                    await TaskDelay.PutTaskDelay(rnd.Next(delay - tolerance, delay + tolerance));
                                    if (isRunning)
                                    {
                                        Rect current = new Rect();
                                        GetWindowRect(this.hWnd, ref current);
                                        Cursor.Position = new Point(point.X + current.Left, point.Y + current.Top);
                                        await TaskDelay.PutTaskDelay(100);
                                        keyBoardUtility.ClickWindow(Parent_hWnd, hWnd, point);
                                    }
                                    else return;
                                }

                            }
                        }
                        //repeat delay
                        int repeatDelay = int.Parse(repeat_delay_textBox.Text);
                        if (repeatDelay > 0 && isRunning) await TaskDelay.PutTaskDelay(repeatDelay);
                    }
                }
            }
            else
            while (loop > 0)
            {
                if (hWnd != null)
                {
                    //Foreach List Action
                    foreach (string[] values in list)
                    {
                        //KeyPress Action
                        if (values[0].Equals("Key"))
                        {

                            uint wParam = uint.Parse(values[5]);
                            uint scancode = uint.Parse(values[6]);
                            int delay = int.Parse(values[2]);
                            int tolerance = int.Parse(values[3]);
                            byte randomPercent = byte.Parse(values[4]);

                            Random rnd = new Random();
                            int rndNumber = rnd.Next(0, 100);
                            if (rndNumber <= randomPercent)
                            {
                                await TaskDelay.PutTaskDelay(rnd.Next(delay - tolerance, delay + tolerance));
                                if(isRunning) keyBoardUtility.KeyPressWindow(hWnd, wParam, scancode);
                                else return;
                            }
                        }
                        else if (values[0].Equals("Click"))
                        {
                            IntPtr Parent_hWnd = hWnd;
                            while (GetParent(Parent_hWnd) != IntPtr.Zero)
                            {
                                Parent_hWnd = GetParent(Parent_hWnd);
                            }
                            string point_string = values[1];
                            int delay = int.Parse(values[2]);
                            int tolerance = int.Parse(values[3]);
                            byte randomPercent = byte.Parse(values[4]);
                            string[] point_string_array = values[1].Split(',');
                            Point point = new Point(int.Parse(point_string_array[0]), int.Parse(point_string_array[1]));
                            Random rnd = new Random();
                            int rndNumber = rnd.Next(0, 100);
                            if (rndNumber <= randomPercent && isRunning)
                            {
                                await TaskDelay.PutTaskDelay(rnd.Next(delay - tolerance, delay + tolerance));
                                    if (isRunning)
                                    {
                                        Rect current = new Rect();
                                        GetWindowRect(this.hWnd, ref current);
                                        Cursor.Position = new Point(point.X + current.Left, point.Y + current.Top);
                                        await TaskDelay.PutTaskDelay(100);
                                        keyBoardUtility.ClickWindow(Parent_hWnd, hWnd, point);
                                    }
                                    else return;
                            }

                        }
                    }
                    //repeat delay
                    int repeatDelay = int.Parse(repeat_delay_textBox.Text);
                    if (repeatDelay > 0 && isRunning) await TaskDelay.PutTaskDelay(repeatDelay);

                }
                loop--;
            }
            start_btn.Enabled = true;
            isRunning = false;
        }
        private async void turnOnAutoDesktop(uint loop)
        {
            KeyBoardUtility keyBoardUtility = new KeyBoardUtility();
            //GetListAction
            List<string[]> list = FileUtility.loadFromTmp();
            if (loop == 0)
            {
                isRunning = true;
                while (isRunning)
                {
                    IntPtr hWnd = GetForegroundWindow();
                    //MessageBox.Show(hWnd.ToString());
                    if (hWnd != null)
                    {
                        //Foreach List Action
                        foreach (string[] values in list)
                        {
                            //KeyPress Action
                            if (values[0].Equals("Key"))
                            {

                                uint wParam = uint.Parse(values[5]);
                                uint scancode = uint.Parse(values[6]);
                                int delay = int.Parse(values[2]);
                                int tolerance = int.Parse(values[3]);
                                byte randomPercent = byte.Parse(values[4]);

                                Random rnd = new Random();
                                int rndNumber = rnd.Next(0, 100);
                                if (rndNumber <= randomPercent && isRunning)
                                {
                                    await TaskDelay.PutTaskDelay(rnd.Next(delay - tolerance, delay + tolerance));
                                    keyBoardUtility.KeyPressWindow(hWnd, wParam, scancode);
                                }

                            }
                        }

                    }
                }
            }
            else
                while (loop > 0)
                {
                    IntPtr hWnd = GetForegroundWindow();
                    if (hWnd != null)
                    {
                        //Foreach List Action
                        foreach (string[] values in list)
                        {
                            //KeyPress Action
                            if (values[0].Equals("Key"))
                            {

                                uint wParam = uint.Parse(values[5]);
                                uint scancode = uint.Parse(values[6]);
                                int delay = int.Parse(values[2]);
                                int tolerance = int.Parse(values[3]);
                                byte randomPercent = byte.Parse(values[4]);

                                Random rnd = new Random();
                                int rndNumber = rnd.Next(0, 100);
                                if (rndNumber <= randomPercent)
                                {
                                    await TaskDelay.PutTaskDelay(rnd.Next(delay - tolerance, delay + tolerance));
                                    keyBoardUtility.KeyPressWindow(hWnd, wParam, scancode);
                                }

                            }
                        }

                    }
                    loop--;
                }
            start_btn.Enabled = true;
        }

        //ListView
        private void ControlChanged(object sender, ControlEventArgs e)
        {
            save_button.Enabled = true;
        }
        //ComboBox
        
        private void select_window_cbB_DropDown(object sender, EventArgs e)
        {
           
            select_window_cbB.Items.Clear();
            FormController.load_window_selected();
        }
        private void select_window_cbB_SelectedIndexChange(object sender, EventArgs e)
        {
            if(select_window_cbB.SelectedIndex != -1)
            {
                this.hWnd = (select_window_cbB.SelectedItem as dynamic).Value;
            }
        }
        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void main_view_listView_SelectedIndexChanged(object sender, EventArgs e)
        {
            //get item info
            if (main_view_listView.SelectedItems.Count == 0) return;
            ListViewItem current_item = main_view_listView.SelectedItems[0];
            vk_key_cbB.Text = current_item.SubItems[2].Text;
            vk_key_delay_textBox.Text = current_item.SubItems[3].Text;
            vk_key_tolerance_textBox.Text = current_item.SubItems[4].Text;
            vk_key_random_percent_textBox.Text = current_item.SubItems[5].Text;
        }
    }
}
