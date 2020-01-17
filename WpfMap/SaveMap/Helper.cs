using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfMap.SaveMap
{
    public class Helper
    {
        /// <summary>
        /// 保存到文本文件
        /// </summary>
        /// <param name="str"></param>
        public static void SaveToFile(string str)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.InitialDirectory = @"C:\Users\SpringRain\Desktop";
            sfd.Title = "请选择要保存的文件路径";
            sfd.Filter = "文本文件|*.txt|所有文件|*.*";
            sfd.ShowDialog();

            //获得用户要保存的文件的路径
            string path = sfd.FileName;
            if (path == "")
            {
                return;
            }
            using (FileStream fsWrite = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write))
            {
                byte[] buffer = Encoding.Default.GetBytes(str);
                fsWrite.Write(buffer, 0, buffer.Length);
            }
            MessageBox.Show("保存成功");
        }
        /// <summary>
        /// 从文件读取
        /// </summary>
        /// <returns></returns>
        public static string LoadFromFile()
        {
            string txt = string.Empty;
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Filter = "文本文件(*.txt)|*.txt";
            if (openFile.ShowDialog() == true)
            {
                using (StreamReader sr = new StreamReader(openFile.FileName, Encoding.Default))
                {
                    txt = sr.ReadToEnd();
                }
            }
            return txt;
        }
    }
}
