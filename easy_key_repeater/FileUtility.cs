using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace easy_key_repeater
{
    class FileUtility
    {
        static string tempPath
        {
            get
            {
                return Path.GetTempPath();
            }
        }
        //public static string tmpFile = tempPath + @"ekr.temp";
        public static string tmpFile = "tmp";
        public void CreateNew()
        {

        }
        public static void CreateTmp()
        {
            StreamWriter tmp;
            tmp = new StreamWriter(tmpFile);
            tmp.Close();
        }
        public static void AppendRowKey(string[] row)
        {
            if (row == null) return;
            StreamWriter tmp;
            tmp = new StreamWriter(tmpFile, true);
            foreach(string value in row)
            {
                tmp.Write(value);
                tmp.Write(";");
            }
            tmp.WriteLine();
            tmp.Close();
        }
        public static bool UpdateRow(int index, string[] row)
        {
            string[] lines = File.ReadAllLines(tmpFile);
            if (lines.Length <= 0) return false;
            else
            {
                if (index < 0 || index >= lines.Length) return false;
                else
                {
                    if (row == null) return false;
                    lines[index] = "";
                    foreach (string value in row)
                    {
                        lines[index] += @value;
                        lines[index] += ";";
                    }
                    if (WriteAllLine(lines)) return true;
                    else return false;
                }
            }
        }
        public static List<string[]> loadFromTmp()
        {
            List<string[]> list = new List<string[]>();
            string[] lines = File.ReadAllLines(tmpFile);
            foreach (string line in lines)
            {
                string[] values = line.Split(';');
                list.Add(values);
            }
            return list;
        }
        public static bool SwapLine(int index, int index2)
        {
            if (index == index2) return false;
            else
            {
                string[] lines = File.ReadAllLines(tmpFile);
                if (lines.Length <= 1) return false;
                else
                {
                    if (index < 0 || index2 >= lines.Length) return false;
                    else
                    {
                        string temp = lines[index];
                        lines[index] = lines[index2];
                        lines[index2] = temp;
                        if (WriteAllLine(lines)) return true;
                        else return false;
                    }
                }

            }
        }
        public static bool DeleteLine(int index)
        {
            string[] lines = File.ReadAllLines(tmpFile);
            if (lines.Length <= 0) return false;
            else
            {
                if (index < 0 || index >= lines.Length) return false;
                else
                {
                    List<string> temp = new List<string>(lines);
                    temp.RemoveAt(index);
                    lines = temp.ToArray();
                    if (WriteAllLine(lines)) return true;
                    else return false;
                }
            }
        }
        private static bool WriteAllLine(string[] lines)
        {
            try
            {
                StreamWriter tmp;
                tmp = new StreamWriter(tmpFile, false);
                foreach (string line in lines)
                {
                    tmp.WriteLine(line);
                }
                tmp.Close();
                return true;
            } catch(Exception e)
            {
                return false;
            }
        }
        public bool SaveAs(string path)
        {
            return true;
        }
        public bool LoadFromFile(string path)
        {
            return true;
        }
    }
}
