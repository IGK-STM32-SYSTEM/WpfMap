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
        /// <param name="str">内容</param>
        /// <param name="path">路径</param>
        public static void SaveToFile(string str, string path)
        {
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
        public static string LoadFromFile(string path)
        {
            string txt = string.Empty;
            using (StreamReader sr = new StreamReader(path, Encoding.Default))
            {
                txt = sr.ReadToEnd();
            }
            return txt;
        }

        #region 标准转Base
        /// <summary>
        /// 将标准【实际使用的】对象转换【映射】到基本对象【用于存储的对象】
        /// </summary>
        public class StandardToBase
        {
            public static void RFID(MapElement.RFID rfid)
            {
                rfid.baseTextBlock = SaveMap.Convert.TextBlockToBase(rfid.textBlock);
                rfid.baseEllipse = SaveMap.Convert.EllipseToBase(rfid.ellipse);
            }
            public static void RFID(List<MapElement.RFID> rfids)
            {
                foreach (var item in rfids)
                {
                    RFID(item);
                }
            }
            public static void Line(MapElement.RouteLine line)
            {
                line.baseEndRect = SaveMap.Convert.RectangleToBase(line.EndRect);
                line.baseLine = SaveMap.Convert.LineToBase(line.line);
                line.baseSelectLine = SaveMap.Convert.LineToBase(line.SelectLine);
                line.baseStartRect = SaveMap.Convert.RectangleToBase(line.StartRect);
                line.baseTextBlock = SaveMap.Convert.TextBlockToBase(line.textBlock);
            }
            public static void Line(List<MapElement.RouteLine> routeLines)
            {
                foreach (var item in routeLines)
                {
                    Line(item);
                }
            }
            public static void ForkLine(MapElement.RouteForkLine ForkLine)
            {
                ForkLine.basePath = SaveMap.Convert.ForkLiePathToBase(ForkLine.Path);
                ForkLine.baseSelectPath = SaveMap.Convert.ForkLiePathToBase(ForkLine.SelectPath);
                ForkLine.baseStartRect = SaveMap.Convert.RectangleToBase(ForkLine.StartRect);
                ForkLine.baseEndRect = SaveMap.Convert.RectangleToBase(ForkLine.EndRect);
                ForkLine.baseTextBlock = SaveMap.Convert.TextBlockToBase(ForkLine.textBlock);
            }
            public static void ForkLine(List<MapElement.RouteForkLine> routeForkLines)
            {
                foreach (var item in routeForkLines)
                {
                    ForkLine(item);
                }
            }
        }
        #endregion

        #region Base转标准
        /// <summary>
        /// 将基本对象【用于存储的对象】转换【映射】到标准【实际使用的】对象
        /// </summary>
        public class BaseToStandard
        {
            public static void RFID(MapElement.RFID rfid)
            {
                rfid.textBlock = SaveMap.Convert.BaseToTextBlock(rfid.baseTextBlock);
                rfid.ellipse = SaveMap.Convert.BaseToEllipse(rfid.baseEllipse);
            }
            public static void RFID(List<MapElement.RFID> rfidList)
            {
                foreach (var item in rfidList)
                {
                    RFID(item);
                }
            }
            public static void Line(MapElement.RouteLine line)
            {
                line.EndRect = SaveMap.Convert.BaseToRectangle(line.baseEndRect);
                line.line = SaveMap.Convert.BaseToLine(line.baseLine);
                line.SelectLine = SaveMap.Convert.BaseToLine(line.baseSelectLine);
                line.StartRect = SaveMap.Convert.BaseToRectangle(line.baseStartRect);
                line.textBlock = SaveMap.Convert.BaseToTextBlock(line.baseTextBlock);
            }
            public static void Line(List<MapElement.RouteLine> routeLines)
            {
                foreach (var item in routeLines)
                {
                    Line(item);
                }
            }
            public static void ForkLine(MapElement.RouteForkLine ForkLine)
            {
                ForkLine.Path = SaveMap.Convert.BaseToForkLiePath(ForkLine.basePath);
                ForkLine.SelectPath = SaveMap.Convert.BaseToForkLiePath(ForkLine.baseSelectPath);
                ForkLine.StartRect = SaveMap.Convert.BaseToRectangle(ForkLine.baseStartRect);
                ForkLine.EndRect = SaveMap.Convert.BaseToRectangle(ForkLine.baseEndRect);
                ForkLine.textBlock = SaveMap.Convert.BaseToTextBlock(ForkLine.baseTextBlock);
            }
            public static void ForkLine(List<MapElement.RouteForkLine> forkLines)
            {
                foreach (var item in forkLines)
                {
                    ForkLine(item);
                }
            }
        }

        #endregion

        /// <summary>
        /// 对象转Json字符串
        /// </summary>
        public class ObjToJson
        {
            /// <summary>
            /// RFID转JSON字符串
            /// </summary>
            public static string RFID(List<MapElement.RFID> rfidList)
            {
                //将标准对象转为Base
                StandardToBase.RFID(rfidList);
                //转为json
                string str = JsonConvert.SerializeObject(rfidList, Formatting.Indented);
                return str;
            }
            public static string RFID(MapElement.RFID rfid)
            {
                //将标准对象转为Base
                StandardToBase.RFID(rfid);
                //转为json
                string str = JsonConvert.SerializeObject(rfid, Formatting.Indented);
                return str;
            }
            /// <summary>
            /// Line转JSON字符串
            /// </summary>
            public static string Line(List<MapElement.RouteLine> routeLines)
            {
                //将标准对象转为Base
                StandardToBase.Line(routeLines);
                //转为json
                return JsonConvert.SerializeObject(routeLines, Formatting.Indented);
            }
            public static string Line(MapElement.RouteLine line)
            {
                //将标准对象转为Base
                StandardToBase.Line(line);
                //转为json
                return JsonConvert.SerializeObject(line, Formatting.Indented);
            }
            /// <summary>
            /// 分叉转JSON字符串
            /// </summary>
            public static string ForkLine(List<MapElement.RouteForkLine> routeForkLines)
            {
                //将标准对象转为Base
                StandardToBase.ForkLine(routeForkLines);
                //转为json
                return JsonConvert.SerializeObject(routeForkLines, Formatting.Indented);
            }
            public static string ForkLine(MapElement.RouteForkLine routeForkLine)
            {
                //将标准对象转为Base
                StandardToBase.ForkLine(routeForkLine);
                //转为json
                return JsonConvert.SerializeObject(routeForkLine, Formatting.Indented);
            }
            /// <summary>
            /// 地图对象转Json字符串
            /// </summary>
            /// <param name="mapObject"></param>
            /// <returns></returns>
            public static string MapOject(MapElement.MapObjectClass mapObject)
            {
                //保存地图【转换映射】
                SaveMap.Helper.StandardToBase.RFID(mapObject.RFIDS);
                SaveMap.Helper.StandardToBase.Line(mapObject.Lines);
                SaveMap.Helper.StandardToBase.ForkLine(mapObject.ForkLines);
                return JsonConvert.SerializeObject(mapObject, Formatting.Indented);
            }

        }
        /// <summary>
        /// Json字符串转对象
        /// </summary>
        public class JsonToObj
        {
            /// <summary>
            /// RFID
            /// </summary>
            public static MapElement.RFID RFID(string json)
            {
                MapElement.RFID rfid = new MapElement.RFID();
                //json 转为对象
                rfid = JsonConvert.DeserializeObject<MapElement.RFID>(json);
                //将Base转为标准对象
                BaseToStandard.RFID(rfid);
                return rfid;
            }
            public static List<MapElement.RFID> RFIDList(string json)
            {
                List<MapElement.RFID> rfidList = new List<MapElement.RFID>();
                //json 转为对象
                rfidList = JsonConvert.DeserializeObject<List<MapElement.RFID>>(json);
                //将Base转为标准对象
                BaseToStandard.RFID(rfidList);
                return rfidList;
            }
            /// <summary>
            /// Line
            /// </summary>
            public static MapElement.RouteLine Line(string str)
            {
                MapElement.RouteLine routeLine = new MapElement.RouteLine();
                //json 转为对象
                routeLine = JsonConvert.DeserializeObject<MapElement.RouteLine>(str);
                //将Base转为标准对象
                BaseToStandard.Line(routeLine);
                return routeLine;
            }
            public static List<MapElement.RouteLine> LineList(string str)
            {
                List<MapElement.RouteLine> routeLines = new List<MapElement.RouteLine>();
                //json 转为对象
                routeLines = JsonConvert.DeserializeObject<List<MapElement.RouteLine>>(str);
                //将Base转为标准对象
                BaseToStandard.Line(routeLines);
                return routeLines;
            }
            /// <summary>
            /// Json字符串转分叉对象，并显示
            /// </summary>
            public static MapElement.RouteForkLine ForkLine(string str)
            {
                MapElement.RouteForkLine routeForkLine = new MapElement.RouteForkLine();
                //json 转为对象
                routeForkLine = JsonConvert.DeserializeObject<MapElement.RouteForkLine>(str);
                //将Base转为标准对象
                BaseToStandard.ForkLine(routeForkLine);
                return routeForkLine;
            }
            public static List<MapElement.RouteForkLine> ForkLineList(string str)
            {
                List<MapElement.RouteForkLine> routeForkLines = new List<MapElement.RouteForkLine>();
                //json 转为对象
                routeForkLines = JsonConvert.DeserializeObject<List<MapElement.RouteForkLine>>(str);
                //将Base转为标准对象
                BaseToStandard.ForkLine(routeForkLines);
                return routeForkLines;
            }
            /// <summary>
            /// Json转地图对象
            /// </summary>
            /// <param name="str"></param>
            /// <returns></returns>
            public static MapElement.MapObjectClass MapObject(string str)
            {
                //json 转为对象
                MapElement.MapObjectClass mapObject = JsonConvert.DeserializeObject<MapElement.MapObjectClass>(str);
                if (mapObject == null)
                    return null;
                /*--------------RFID--------------------------------*/
                //将Base转为标准对象
                SaveMap.Helper.BaseToStandard.RFID(mapObject.RFIDS);
                /*--------------Line--------------------------------*/
                //将Base转为标准对象
                SaveMap.Helper.BaseToStandard.Line(mapObject.Lines);
                /*--------------ForkLine--------------------------------*/
                //将Base转为标准对象
                SaveMap.Helper.BaseToStandard.ForkLine(mapObject.ForkLines);
                return mapObject;
            }
        }
    }
}
