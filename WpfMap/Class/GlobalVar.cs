using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Shapes;

namespace WpfMap
{
    public class GlobalVar
    {
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
            /// 编辑元素
            /// </summary>
            EditElement,
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
        /// 正在添加的线条的步骤，指示正在进行第几步，从1开始
        /// </summary>
        public static int AddRouteLineStep = 1;
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
        /// <summary>
        /// 鼠标左键按下移动历史值
        /// </summary>
        public static System.Windows.Point mouseLeftBtnDownMoveLast = new System.Windows.Point();
        /// <summary>
        /// 鼠标左键按下移动偏差，左键按下后清空
        /// </summary>
        public static System.Windows.Point mouseLeftBtnDownMoveDiff = new System.Windows.Point();

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
    }
}
