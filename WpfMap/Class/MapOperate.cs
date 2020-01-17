using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace WpfMap
{
    public class MapOperate
    {
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
            /// 编辑多个元素【多选】【进入条件：在空白处左键按下，并拖动框选，或按住Ctrl键在对象上单击】
            /// </summary>
            MultiEdit,
            /// <summary>
            /// 添加元素
            /// </summary>
            AddElement
        }
        /// <summary>
        /// 当前操作模式
        /// </summary>
        public static EnumMode NowMode = EnumMode.EditElement;
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
        public static System.Windows.Point mouseLeftBtnDownToView = new System.Windows.Point();
        //记录是否按住左键移动过
        public static bool MovedAfterLeftBtn = false;
        /// <summary>
        /// 鼠标左键按下移动历史值【整体移动直线时使用】
        /// </summary>
        public static System.Windows.Point mouseLeftBtnDownMoveLast = new System.Windows.Point();
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
            thickness.Left += diffx < 0 ? diffx  : 0;
            thickness.Top += diffy < 0 ? diffy  : 0;
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
