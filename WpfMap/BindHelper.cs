using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using static System.Net.Mime.MediaTypeNames;

namespace WpfMap
{
    public class BindHelper
    {
        public abstract class BindableObject : INotifyPropertyChanged
        {
            public event PropertyChangedEventHandler PropertyChanged;
            protected void OnPropertyChanged(string propertyName)
            {
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
                }
            }

            protected void SetProperty<T>(ref T item, T value, [CallerMemberName] string propertyName = null)
            {
                if (!EqualityComparer<T>.Default.Equals(item, value))
                {
                    item = value;
                    OnPropertyChanged(propertyName);
                }
            }
        }

        public class DataGrid : BindableObject
        {
            private DataTable _dataTable = new DataTable();
            public DataGrid()
            {
                DataColumn ID = new DataColumn("ID");//第一列
                ID.DataType = System.Type.GetType("System.Int32");
                //ID.AutoIncrement = true; //自动递增ID号 
                _dataTable.Columns.Add(ID);
                //设置主键
                DataColumn[] keys = new DataColumn[1];
                keys[0] = ID;
                _dataTable.PrimaryKey = keys;

                _dataTable.Columns.Add(new DataColumn("RouteNum", typeof(string)));//第二列
                _dataTable.Columns.Add(new DataColumn("StationCount", typeof(string)));//第三列
                _dataTable.Columns.Add(new DataColumn("Des", typeof(string)));//第四列
            }


            public DataTable dataTable
            {
                get { return _dataTable; }
                set { SetProperty(ref _dataTable, value); }
            }
        }
        public class DataGridMsg : BindableObject
        {
            private DataTable _dataTable = new DataTable();
            public DataGridMsg()
            {
                DataColumn ID = new DataColumn("ID");//第一列
                ID.DataType = System.Type.GetType("System.Int32");
                //ID.AutoIncrement = true; //自动递增ID号 
                _dataTable.Columns.Add(ID);
                //设置主键
                DataColumn[] keys = new DataColumn[1];
                keys[0] = ID;
                _dataTable.PrimaryKey = keys;

                _dataTable.Columns.Add(new DataColumn("Time", typeof(string)));//第二列
                _dataTable.Columns.Add(new DataColumn("Msg", typeof(string)));//第三列
            }


            public DataTable dataTable
            {
                get { return _dataTable; }
                set { SetProperty(ref _dataTable, value); }
            }
        }

        #region Bottom 底部信息栏
        public class ViewInfoBound : BindableObject
        {
            private double _Scale = 1;
            private Point _Origin = new Point();
            private Point _Axis = new Point();

            /// <summary>
            /// 视图缩放比例
            /// </summary>
            public double Scale
            {
                get { return _Scale; }
                set { SetProperty(ref _Scale, value); }
            }
            /// <summary>
            /// 原点坐标
            /// </summary>
            public Point Origin
            {
                get { return _Origin; }
                set { SetProperty(ref _Origin, value); }
            }
            /// <summary>
            /// 视图坐标
            /// </summary>
            public Point View
            {
                get { return _Axis; }
                set { SetProperty(ref _Axis, value); }
            }
        }
        #endregion


        #region 系统消息
        public class SystemMsg : BindableObject
        {
            private string msg = string.Empty;
            /// <summary>
            /// 系统消息
            /// </summary>
            public string Msg
            {
                get { return msg; }
                set { SetProperty(ref msg, value); }
            }
            public void Write(String format, params object[] args)
            {
                string msg = string.Format(format, args);
                Msg += msg;
            }
            public void WriteLine(String format, params object[] args)
            {
                string msg = string.Format(format, args);
                Msg += msg + "\r\n";
            }
        }
        #endregion


        #region 路径编辑
        public class RouteEditBound : BindableObject
        {
            private string _RouteNow = "";
            private string _RouteDes = "";
            private string _StationTotal = "";
            private string _StationNow = "";
            private string _StationDes = "";
            private string _StationInfoX = "";
            private string _StationInfoY = "";
            private string _StationInfoAngel = "";
            private string _StationInfoStopTime = "";

            /// <summary>
            /// 当前路径
            /// </summary>
            public string RouteNow
            {
                get { return _RouteNow; }
                set { SetProperty(ref _RouteNow, value); }
            }
            /// <summary>
            /// 路径描述
            /// </summary>
            public string RouteDes
            {
                get { return _RouteDes; }
                set { SetProperty(ref _RouteDes, value); }
            }
            /// <summary>
            /// 站点总数
            /// </summary>
            public string StationTotal
            {
                get { return _StationTotal; }
                set { SetProperty(ref _StationTotal, value); }
            }
            /// <summary>
            /// 当前站点
            /// </summary>
            public string StationNow
            {
                get { return _StationNow; }
                set { SetProperty(ref _StationNow, value); }
            }
            /// <summary>
            /// 站点描述
            /// </summary>
            public string StationDes
            {
                get { return _StationDes; }
                set { SetProperty(ref _StationDes, value); }
            }
            /// <summary>
            /// X轴坐标
            /// </summary>
            public string StationInfoX
            {
                get { return _StationInfoX; }
                set { SetProperty(ref _StationInfoX, value); }
            }
            /// <summary>
            /// Y轴坐标
            /// </summary>
            public string StationInfoY
            {
                get { return _StationInfoY; }
                set { SetProperty(ref _StationInfoY, value); }
            }
            /// <summary>
            /// 机器人角度
            /// </summary>
            public string StationInfoAngel
            {
                get { return _StationInfoAngel; }
                set { SetProperty(ref _StationInfoAngel, value); }
            }
            /// <summary>
            /// 站点停留时间
            /// </summary>
            public string StationInfoStopTime
            {
                get { return _StationInfoStopTime; }
                set { SetProperty(ref _StationInfoStopTime, value); }
            }
        }
        #endregion
    }
}
