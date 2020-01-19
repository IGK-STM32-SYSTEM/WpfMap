using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace WpfMap
{
    public class MapOperate
    {
        public class History
        {
            /// <summary>
            /// 记录对象
            /// </summary>
            public class Record
            {
                /// <summary>
                /// 数据
                /// </summary>
                public string Data { get; set; }
                /// <summary>
                /// 注释
                /// </summary>
                public string Note { get; set; }
            }
            /// <summary>
            /// 历史记录列表
            /// </summary>
            public static List<Record> Records = new List<Record>();
            /// <summary>
            /// 历史记录索引
            /// </summary>
            public static int Index { get; set; } = 0;
            /// <summary>
            /// 添加记录
            /// </summary>
            /// <param name="note">数据的注解和说明</param>
            public static void AddRecord(string note)
            {
                string str = SaveMap.Helper.ObjToJson.MapOject(MapElement.MapObject);
                //本次更改后的地图和之前的一致，不保存
                if (Records.Count > 0)
                {
                    if (str == Records.Last().Data)
                    {
                        Console.WriteLine("Note:{0}【和之前地图一致,不保存】", note);
                        return;
                    }
                }
                Record record = new Record();
                record.Data = str;
                record.Note = note;
                Records.Add(record);
                //索引指到当前位置
                Index = Records.Count - 1;
                //打印结果
                Console.WriteLine("Index:{0},Note:{1}", Index, record.Note);
            }
            /// <summary>
            /// 撤销
            /// </summary>
            public static void Undo()
            {
                if (Records.Count == 0)
                {
                    MessageBox.Show("还没有记录^-^");
                    return;
                }
                if (History.Index==0)
                {
                    MessageBox.Show("到低了,不能再撤销了^-^");
                    return;
                }
                History.Index--;
                //重载地图
                MapFunction.ReloadMap(Records[History.Index].Data);
            }
            /// <summary>
            /// 重做【恢复撤销】
            /// </summary>
            public static void Redo()
            {
                if (Records.Count == 0)
                {
                    MessageBox.Show("还没有记录^-^");
                    return;
                }
                if (History.Index == Records.Count-1)
                {
                    MessageBox.Show("到最后一步了^-^");
                    return;
                }
                History.Index++;
                //重载地图
                MapFunction.ReloadMap(Records[History.Index].Data);
            }
            /// <summary>
            /// 恢复到指定记录
            /// </summary>
            /// <param name="index">记录索引</param>
            public static void RecoverIndex(int index)
            {
                History.Index++;
            }
        }

        /// <summary>
        /// 直线调节模式式
        /// </summary>
        public enum EnumElementEditMode
        {
            /// <summary>
            /// 起点
            /// </summary>
            Start,
            /// <summary>
            /// 终点
            /// </summary>
            End,
            /// <summary>
            /// 整体
            /// </summary>
            All
        }
        /// <summary>
        /// 当前操作模式
        /// </summary>
        public enum EnumMode
        {
            /// <summary>
            /// 空闲
            /// </summary>
            None,
            /// <summary>
            /// 编辑单个元素【单选】【进入条件：直接左键在对象上单击】
            /// </summary>
            EditElement,
            /// <summary>
            /// 多选状态，只显示多选框，结束后如果有选中的，进入多编辑状态，否则恢复单个编辑模式
            /// </summary>
            MultiSelect,
            /// <summary>
            /// 多个编辑模式，进入该模式说明已经选中了多个元素
            /// </summary>
            MultiEdit,
            /// <summary>
            /// 粘贴模式
            /// </summary>
            Paste,
            /// <summary>
            /// 添加元素
            /// </summary>
            AddElement
        }
        /// <summary>
        /// 当前操作模式
        /// </summary>
        private static EnumMode nowMode = EnumMode.EditElement;
        public static EnumMode NowMode
        {
            get { return nowMode; }
            set
            {
                //Console.WriteLine("操作模式：{0}", Enum.GetName(typeof(EnumMode), value));
                nowMode = value;
            }
        }
        //元素类型枚举
        public enum EnumElementType
        {
            /// <summary>
            /// 无
            /// </summary>
            None,
            /// <summary>
            /// 标签
            /// </summary>
            RFID,
            /// <summary>
            /// 直线
            /// </summary>
            RouteLine,
            /// <summary>
            /// 分叉线
            /// </summary>
            RouteForkLine
        }
        /// <summary>
        /// 【添加通用】正在添加的线条的步骤，指示正在进行第几步，从1开始
        /// </summary>
        public static int AddStep = 1;
        /// <summary>
        /// 【调整直线用】编辑直线的方式，调节起点，调节终点，整体调节
        /// </summary>
        public static EnumElementEditMode ElementEditMode = EnumElementEditMode.All;

        /// <summary>
        /// 正在操作元素的索引
        /// </summary>
        public static int NowSelectIndex = -1;
        /// <summary>
        /// 当前选中的类型
        /// </summary>
        public static EnumElementType NowType = EnumElementType.None;

        //鼠标左键按下，记录按下的位置
        public static System.Windows.Point mouseLeftBtnDownToMap = new System.Windows.Point();
        //记录是否按住左键移动过
        public static bool MovedAfterLeftBtn = false;
        /// <summary>
        /// 鼠标左键按下移动偏差，左键按下后清空【整体移动直线时使用】
        /// </summary>
        public static System.Windows.Point mouseLeftBtnDownMoveDiff = new System.Windows.Point();

        /// <summary>
        /// 记录margin【整体移动时使用】
        /// </summary>
        public static Thickness ElementMarginLast = new Thickness();

        //右键按下
        public static System.Windows.Point mouseRightBtnDownPoint = new System.Windows.Point();
        //光标实时位置
        public static System.Windows.Point NowPoint = new System.Windows.Point();

        //记录用户按键
        public class UserKeyClass
        {
            public Key Key;
            public KeyStates KeyState = KeyStates.None;
        }
        public static UserKeyClass Userkey = new UserKeyClass();

        //底部消息栏
        public static BindHelper.ViewInfoBound ViewInfo = new BindHelper.ViewInfoBound();

        //选择框对象
        public static Rectangle SelectRectangle = new Rectangle();

        /// <summary>
        /// 已被选中对象【多选】
        /// </summary>
        public static MapElement.MapObjectClass MultiSelected = new MapElement.MapObjectClass();
        /// <summary>
        /// 刚粘贴的过程对象【过程对象】
        /// </summary>
        public static MapElement.MapObjectClass PastedObject = new MapElement.MapObjectClass();
        /// <summary>
        /// 剪贴板元素集合
        /// </summary>
        public static MapElement.MapObjectClass Clipboard = new MapElement.MapObjectClass();

        /// <summary>
        /// 绘制选择框
        /// </summary>
        /// <param name="point"></param>
        public static void DrawMultiSelectRect(Point point)
        {
            //取左键按下时到当前状态光标偏差
            double diffx = MapOperate.mouseLeftBtnDownMoveDiff.X;
            double diffy = MapOperate.mouseLeftBtnDownMoveDiff.Y;

            MapOperate.SelectRectangle.Fill = null;
            MapOperate.SelectRectangle.StrokeThickness = 2;
            MapOperate.SelectRectangle.Stroke = Brushes.Orange;
            MapOperate.SelectRectangle.Width = Math.Abs(diffx);
            MapOperate.SelectRectangle.Height = Math.Abs(diffy);
            Thickness thickness = new Thickness(MapOperate.mouseLeftBtnDownToMap.X, MapOperate.mouseLeftBtnDownToMap.Y, 0, 0);
            thickness.Left += diffx < 0 ? diffx : 0;
            thickness.Top += diffy < 0 ? diffy : 0;
            MapOperate.SelectRectangle.Margin = thickness;
            //显示虚线
            MapOperate.SelectRectangle.StrokeDashArray = new DoubleCollection() { 3, 3 };
            MapOperate.SelectRectangle.StrokeDashCap = PenLineCap.Triangle;

            if (MapElement.CvOperate.Children.Contains(MapOperate.SelectRectangle))
                return;
            MapElement.CvOperate.Children.Add(MapOperate.SelectRectangle);
        }
        /// <summary>
        /// 清除多选框
        /// </summary>
        public static void ClearMultiSelectRect()
        {
            MapElement.CvOperate.Children.Remove(MapOperate.SelectRectangle);
        }
    }
}
