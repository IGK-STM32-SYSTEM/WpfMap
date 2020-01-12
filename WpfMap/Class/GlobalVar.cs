using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace WpfMap
{
    public class GlobalVar
    {
        //鼠标当前功能功能
        public enum MouseFunctionEnum
        {
            /// <summary>
            /// 空闲
            /// </summary>
            None,
            /// <summary>
            /// 设置机器人位置
            /// </summary>
            ResetRobot,
            /// <summary>
            /// 发布机器人目标位置
            /// </summary>
            SetTargetPose,
            /// <summary>
            /// 移动视图
            /// </summary>
            MoveView,
            /// <summary>
            /// 缩放视图
            /// </summary>
            ZoomView,
            /// <summary>
            /// 旋转视图
            /// </summary>
            RotateView,
            /// <summary>
            /// 路径编辑模式，左键移动位置，右键调整方向
            /// </summary>
            RouteEdit,

        }
        public static MouseFunctionEnum mouseFunction = MouseFunctionEnum.None;
        //鼠标左键按下，记录按下的位置
        public static System.Windows.Point mouseLeftBtnDownToMap = new System.Windows.Point();
        public static System.Windows.Point mouseLeftBtnDownToView = new System.Windows.Point();

        //右键按下
        public static System.Windows.Point mouseRightBtnDownPoint = new System.Windows.Point();

        ////地图尺寸
        //public static System.Drawing.Size MapSize;

        //画布比例
        private double CanvasScale = 1;

        //站点
        public class Station
        {
            /// <summary>
            /// 坐标
            /// </summary>
            public System.Windows.Point point = new System.Windows.Point();
            /// <summary>
            /// 编号
            /// </summary>
            public int Num = 0;
            //绘图对象
            public Ellipse ellipse = new Ellipse();
            //绘文字对象
            public TextBlock textBlock = new TextBlock();
        }
        /// <summary>
        /// 站点列表
        /// </summary>
        public static List<Station> Stations = new List<Station>();
    }
}
