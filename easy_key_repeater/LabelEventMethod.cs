using easy_key_repeater.Properties;
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
        

        
        public static string GetWindowTitle(IntPtr hWnd)
        {
            var length = GetWindowTextLength(hWnd) + 1;
            var title = new StringBuilder(length);
            GetWindowText(hWnd, title, length);
            return title.ToString();
        }

        private void label5_MouseDown(object sender, MouseEventArgs e)
        {
            //label5.DoDragDrop(label5.Text, DragDropEffects.None);
            Cursor.Current = new Cursor(Resources.aim_cursor_32x32.GetHicon());
            label5.Visible = false;
        }
        private void label5_MouseUp(object sender, MouseEventArgs e)
        {

            label5.Visible = true;
            IntPtr hWnd = WindowFromPoint(Cursor.Position);
            this.hWnd = hWnd;
            while (GetParent(hWnd) != IntPtr.Zero)
            {
                hWnd = GetParent(hWnd);
            }

            string hWnd_title = GetWindowTitle(hWnd);
            select_window_cbB.SelectedIndex = -1;
            select_window_cbB.Text = hWnd.ToString() + " - " + hWnd_title;
        }

        private void label6_MouseDown(object sender, MouseEventArgs e)
        {
            Cursor.Current = Cursors.Cross;
            label6.Visible = false;
        }


        
        private void label6_MouseUp(object sender, MouseEventArgs e)
        {
            Rect current = new Rect();
            //IntPtr hWnd = WindowFromPoint(Cursor.Position);
            GetWindowRect(this.hWnd, ref current);

            System.Drawing.Point mouse_point = new System.Drawing.Point(Cursor.Position.X, Cursor.Position.Y);
            //System.Drawing.Point form_point = new System.Drawing.Point(mouse_point.X - this.Location.X, mouse_point.Y - this.Location.Y);
            //System.Drawing.Point leftToShift = PointToClient(this.Location);
            //System.Drawing.Point client_point = new System.Drawing.Point(form_point.X + leftToShift.X, form_point.Y + leftToShift.Y);
            System.Drawing.Point client_point = new System.Drawing.Point(mouse_point.X - current.Left, mouse_point.Y - current.Top);

            string inputType = "Click";
            string point = null;
            string delay = vk_key_delay_textBox.Text;
            string tolerance = vk_key_tolerance_textBox.Text;
            string randomPercent = vk_key_random_percent_textBox.Text;
            if (desktop_radio.Checked)
            {
                //mose_point
                //MessageBox.Show(mouse_point.ToString());
                string pointX = mouse_point.X.ToString();
                string pointY = mouse_point.Y.ToString();
                point = pointX + ',' + pointY;
            }
            else if (windows_radio.Checked)
            {
                //Client_point
                //MessageBox.Show(client_point.ToString());
                string potitionX = client_point.X.ToString();
                string potitionY = client_point.Y.ToString();
                point = potitionX + ',' + potitionY;
            }

            if (point != null)
            {
                if (!vk_key_cbB.Items.Contains("(" + point + ")"))
                {
                    vk_key_cbB.Items.Add(new { Text = "(" + point + ")", Value = point });
                }
                vk_key_cbB.Text = "(" + point + ")";
            }
            label6.Visible = true;
        }
    }
}
