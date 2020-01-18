using Microsoft.Win32;
using Newtonsoft.Json;
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

        /// <summary>
        /// RFID对象列表转JSON字符串
        /// </summary>
        public static string RFIDToJson()
        {
            //将标准对象转为Base
            foreach (var item in MapElement.MapObject.RFIDList)
            {
                item.baseTextBlock = SaveMap.Convert.TextBlockToBase(item.textBlock);
                item.baseEllipse = SaveMap.Convert.EllipseToBase(item.ellipse);
            }
            //转为json
            string str = JsonConvert.SerializeObject(MapElement.MapObject.RFIDList, Formatting.Indented);
            return str;
        }
        /// <summary>
        /// Json字符串转RFID对象，并显示
        /// </summary>
        public static void JsonToRFIDAndShow(string json)
        {
            MapElement.MapObject.RFIDList.Clear();
            //json 转为对象
            MapElement.MapObject.RFIDList = JsonConvert.DeserializeObject<List<MapElement.RFID>>(json);
            //将Base转为标准对象
            foreach (var item in MapElement.MapObject.RFIDList)
            {
                item.textBlock = SaveMap.Convert.BaseToTextBlock(item.baseTextBlock);
                item.ellipse = SaveMap.Convert.BaseToEllipse(item.baseEllipse);
            }
            //清空画布
            MapElement.CvRFID.Children.Clear();
            //绘制所有
            MapElement.DrawRFIDList();
        }
        /// <summary>
        /// 直线转JSON字符串
        /// </summary>
        public static string LineToJSON()
        {
            //将标准对象转为Base
            foreach (var item in MapElement.MapObject.LineList)
            {
                item.baseEndRect = SaveMap.Convert.RectangleToBase(item.EndRect);
                item.baseLine = SaveMap.Convert.LineToBase(item.line);
                item.baseSelectLine = SaveMap.Convert.LineToBase(item.SelectLine);
                item.baseStartRect = SaveMap.Convert.RectangleToBase(item.StartRect);
                item.baseTextBlock = SaveMap.Convert.TextBlockToBase(item.textBlock);
            }
            //转为json
            string str = JsonConvert.SerializeObject(MapElement.MapObject.LineList, Formatting.Indented);
            return str;
        }
        /// <summary>
        /// Json字符串转直线对象，并显示
        /// </summary>
        public static void JsonToLineAndShow(string str)
        {
            MapElement.MapObject.LineList.Clear();
            //json 转为对象
            MapElement.MapObject.LineList = JsonConvert.DeserializeObject<List<MapElement.RouteLine>>(str);
            //将Base转为标准对象
            foreach (var item in MapElement.MapObject.LineList)
            {
                item.EndRect = SaveMap.Convert.BaseToRectangle(item.baseEndRect);
                item.line = SaveMap.Convert.BaseToLine(item.baseLine);
                item.SelectLine = SaveMap.Convert.BaseToLine(item.baseSelectLine);
                item.StartRect = SaveMap.Convert.BaseToRectangle(item.baseStartRect);
                item.textBlock = SaveMap.Convert.BaseToTextBlock(item.baseTextBlock);
            }
            //清空画布
            MapElement.CvRouteLine.Children.Clear();
            //绘制所有
            MapElement.DrawLineList();
        }
        /// <summary>
        /// 分叉转JSON字符串
        /// </summary>
        public static string ForkLineToJson()
        {
            //将标准对象转为Base
            foreach (var item in  MapElement.MapObject.ForkLineList)
            {
                item.basePath = SaveMap.Convert.ForkLiePathToBase(item.Path);
                item.baseSelectPath = SaveMap.Convert.ForkLiePathToBase(item.SelectPath);
                item.baseStartRect = SaveMap.Convert.RectangleToBase(item.StartRect);
                item.baseEndRect = SaveMap.Convert.RectangleToBase(item.EndRect);
                item.baseTextBlock = SaveMap.Convert.TextBlockToBase(item.textBlock);
            }
            //转为json
            string str = JsonConvert.SerializeObject( MapElement.MapObject.ForkLineList, Formatting.Indented);
            return str;
        }
        /// <summary>
        /// Json字符串转分叉对象，并显示
        /// </summary>
        public static void JsonToForkLineAndShow(string str)
        {
             MapElement.MapObject.ForkLineList.Clear();
            //json 转为对象
             MapElement.MapObject.ForkLineList = JsonConvert.DeserializeObject<List<MapElement.RouteForkLine>>(str);
            //将Base转为标准对象
            foreach (var item in  MapElement.MapObject.ForkLineList)
            {
                item.Path = SaveMap.Convert.BaseToForkLiePath(item.basePath);
                item.SelectPath = SaveMap.Convert.BaseToForkLiePath(item.baseSelectPath);
                item.StartRect = SaveMap.Convert.BaseToRectangle(item.baseStartRect);
                item.EndRect = SaveMap.Convert.BaseToRectangle(item.baseEndRect);
                item.textBlock = SaveMap.Convert.BaseToTextBlock(item.baseTextBlock);
            }
            //清空画布
            MapElement.CvForkLine.Children.Clear();
            //绘制所有
            MapElement.DrawForkLineList();
        }


        /// <summary>
        /// 保存站点到文件
        /// </summary>
        public static void SaveRFIDToFile()
        {
            string str = RFIDToJson();
            //保存
            SaveMap.Helper.SaveToFile(str);
        }
        /// <summary>
        /// 从文件加载站点
        /// </summary>
        public static void LoadRFIDFromFile()
        {
            string str = SaveMap.Helper.LoadFromFile();
            JsonToRFIDAndShow(str);
        }
        /// <summary>
        /// 保存直线到文件
        /// </summary>
        public static void SaveLineToFile()
        {
            string str = LineToJSON();
            //保存
            SaveMap.Helper.SaveToFile(str);
        }
        /// <summary>
        /// 从文件加载直线
        /// </summary>
        public static void LoadLineFromFile()
        {
            string str = SaveMap.Helper.LoadFromFile();
            JsonToLineAndShow(str);
        }
        /// <summary>
        /// 保存分叉到文件
        /// </summary>
        public static void SaveForkLineToFile()
        {
            string str = ForkLineToJson();
            //保存
            SaveMap.Helper.SaveToFile(str);
        }
        /// <summary>
        /// 从文件加载分叉
        /// </summary>
        public static void LoadForkLineFromFile()
        {
            string str = SaveMap.Helper.LoadFromFile();
            JsonToForkLineAndShow(str);
        }
    }
}
