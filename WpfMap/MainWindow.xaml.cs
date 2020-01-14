using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace WpfMap
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            //绑定视图元素到界面
            bottomstackPanel.DataContext = GlobalVar.ViewInfo;
            //指定画布
            MapElement.CvGrid = cvGrid;//栅格
            MapElement.CvRFID = cvMap;//标签
            MapElement.CvRouteLine = cvMap;//直路线
            MapElement.CvForkLine = cvMap;//分叉路线

            //测试用代码，添加十个站点
            int num = 0;
            for (int i = 0; i < 5; i++)
            {
                num++;
                MapElement.RFID rfid = new MapElement.RFID();
                rfid.Num = num;
                rfid.ellipse.Margin = new Thickness(200 + i * 50, 100, 0, 0);
                MapElement.MapRFIDList.Add(rfid);
            }
            for (int i = 0; i < 5; i++)
            {
                num++;
                MapElement.RFID rfid = new MapElement.RFID();
                rfid.Num = num;
                rfid.ellipse.Margin = new Thickness(200 + i * 50, 200, 0, 0);
                MapElement.MapRFIDList.Add(rfid);
            }
            //画背景栅格，大小为20*20
            MapElement.DrawGrid(1024 * 2, 768 * 2);
            //绘制所有标签
            MapElement.DrawRFIDList();
        }

        private void UpdateBottomInfo()
        {
            //bottomInfo.ViewScale = string.Format("视图缩放: {0:0.0}", sfr.ScaleX);
            //bottomInfo.OriginPoint = string.Format("原点坐标：X:{0:0.0},Y:{1:0.0}", tlt.X, tlt.Y);
        }

        #region 鼠标事件
        //滚轮缩放
        private void image_PreviewMouseWheel(object sender, System.Windows.Input.MouseWheelEventArgs e)
        {
            //获取当前坐标
            System.Windows.Point point = e.GetPosition(gridDraw);
            //转换坐标到原始位置
            System.Windows.Point pt = this.gridDraw.RenderTransform.Inverse.Transform(point);
            //平移
            this.tlt.X = (point.X - pt.X) * this.sfr.ScaleX;
            this.tlt.Y = (point.Y - pt.Y) * this.sfr.ScaleY;

            this.sfr.CenterX = point.X;
            this.sfr.CenterY = point.Y;

            //获取缩放方向
            int scaleDir = Math.Abs(e.Delta) / e.Delta;
            //设置放大或缩小10%
            this.sfr.ScaleX += sfr.ScaleX * 0.1 * scaleDir;
            this.sfr.ScaleY += sfr.ScaleY * 0.1 * scaleDir;
            if (this.sfr.ScaleX < 0.4)
            {
                this.sfr.ScaleX = 0.4;
                this.sfr.ScaleY = 0.4;
            }
            //更新缩放比例,保留两位小数
            GlobalVar.ViewInfo.Scale = Math.Round(this.sfr.ScaleX, 0);
        }
        //右键按下
        private void imageRobot_PreviewMouseRightButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            //记录鼠标按下时的位置
            GlobalVar.mouseRightBtnDownPoint = e.GetPosition(cvMap);
            //获取当前坐标
            System.Windows.Point nowPoint = e.GetPosition(gridDraw);
            //设置为手型光标
            this.Cursor = Cursors.Hand;


        }
        //右键抬起
        private void imageRobot_PreviewMouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            //恢复移动视图的手形光标
            this.Cursor = Cursors.Arrow;
            //编辑属性
            if (GlobalVar.NowMode == GlobalVar.EnumMode.Edit)
            {
            }
            else
            //添加新元素
            if (GlobalVar.NowMode == GlobalVar.EnumMode.Add)
            {
                //结束添加
                GlobalVar.NowAddType = GlobalVar.EnumElementType.None;
                GlobalVar.NowMode = GlobalVar.EnumMode.Edit;
                return;
            }

        }
        //左键按下
        private void imageRobot_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //记录按下时的位置
            GlobalVar.mouseLeftBtnDownToMap = e.GetPosition(cvMap);
            GlobalVar.mouseLeftBtnDownToView = e.GetPosition(drawViewScroll);
            //编辑属性
            if (GlobalVar.NowMode == GlobalVar.EnumMode.Edit)
            {
                //判断光标是否在某个站点上
                int rs = MapFunction.IsOnRFID(GlobalVar.mouseLeftBtnDownToMap);
                if (rs != -1)
                    GlobalVar.NowSelectType = GlobalVar.EnumElementType.RFID;
                //如果本次未选中但之前有选中的，则清除之前
                if (rs == -1 && GlobalVar.NowSelectIndex != -1)
                {
                    if (GlobalVar.NowSelectType == GlobalVar.EnumElementType.RFID)
                    {
                        MapFunction.SetRFIDIsNormal(GlobalVar.NowSelectIndex);
                    }
                }
                else
                //当前选中的和之前选中的不一致，则清除之前的
                if (rs != -1 && rs != GlobalVar.NowSelectIndex)
                {
                    if (GlobalVar.NowSelectType == GlobalVar.EnumElementType.RFID)
                    {
                        //清除之前的选中状态
                        MapFunction.SetRFIDIsNormal(GlobalVar.NowSelectIndex);
                        //设置该点选中
                        MapFunction.SetRFIDIsSelected(rs);
                    }
                }
                else
                {
                    //设置该点选中
                    if (GlobalVar.NowSelectType == GlobalVar.EnumElementType.RFID)
                    {
                        MapFunction.SetRFIDIsSelected(rs);
                    }
                }
                //更新当前选中索引
                GlobalVar.NowSelectIndex = rs;
            }
            else
            //添加新元素
            if (GlobalVar.NowMode == GlobalVar.EnumMode.Add)
            {

            }
        }
        //左键抬起
        private void imageRobot_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            //记录按下时的位置
            GlobalVar.mouseLeftBtnDownToMap = e.GetPosition(cvMap);
            //编辑属性
            if (GlobalVar.NowMode == GlobalVar.EnumMode.Edit)
            {
                //bool rs = MapFunction.IsOnOneRFID(GlobalVar.mouseLeftBtnDownToMap, GlobalVar.NowSelectIndex);
                ////不在当前站点上
                //if (rs == false)
                //    GlobalVar.NowSelectIndex = -1;
            }
            else
            //添加新元素
            if (GlobalVar.NowMode == GlobalVar.EnumMode.Add)
            {
                //继续增加一个
                if (GlobalVar.NowAddType == GlobalVar.EnumElementType.RFID)
                {
                    //添加一个RFID
                    GlobalVar.NowSelectIndex = MapElement.AddRFIDAndShow();
                }
            }
        }
        //鼠标移动
        private void imageRobot_PreviewMouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            //获取当前坐标
            Point nowPoint = e.GetPosition(gridDraw);
            //显示当前坐标到界面
            GlobalVar.ViewInfo.View = new Point(Math.Round(nowPoint.X, 0), Math.Round(nowPoint.Y, 0));
            
            //移动视图【如果右键按下】
            if (e.RightButton == MouseButtonState.Pressed)
            {
                System.Windows.Point position = e.GetPosition(cvMap);
                System.Windows.Point position1 = e.GetPosition(drawViewScroll);

                tlt.X += position.X - GlobalVar.mouseRightBtnDownPoint.X;
                tlt.Y += position.Y - GlobalVar.mouseRightBtnDownPoint.Y;

                //更新圆点坐标,保留两位小数
                GlobalVar.ViewInfo.Origin = new Point(Math.Round(tlt.X, 0), Math.Round(tlt.Y, 0));
            }
            //编辑属性
            if (GlobalVar.NowMode == GlobalVar.EnumMode.Edit)
            {
                //左键按住移动位置【调整元素位置】
                if (e.LeftButton == MouseButtonState.Pressed && GlobalVar.NowSelectIndex != -1)
                {
                    MapFunction.MoveRFIDTo(GlobalVar.NowSelectIndex, nowPoint);
                }
            }
            else
            //添加新元素
            if (GlobalVar.NowMode == GlobalVar.EnumMode.Add)
            {
                MapFunction.MoveRFIDTo(GlobalVar.NowSelectIndex, nowPoint);
            }
        }
        private void drawViewScroll_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            //快捷键VF，重置视图
            if (e.Key == Key.F && GlobalVar.Userkey.Key == Key.V)
            {
                RestView();
            }
            //记录当前按键
            GlobalVar.Userkey.Key = e.Key;
            GlobalVar.Userkey.KeyState = KeyStates.Down;
        }
        private void drawViewScroll_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            //记录当前按键
            GlobalVar.Userkey.Key = e.Key;
            GlobalVar.Userkey.KeyState = KeyStates.None;
        }
        #endregion

        //重置窗口
        private void RestView()
        {
            sfr.ScaleX = 1;
            sfr.ScaleY = 1;

            tlt.X = 0;
            tlt.Y = 0;

            //更新圆点坐标,保留两位小数
            GlobalVar.ViewInfo.Origin = new Point(Math.Round(tlt.X, 0), Math.Round(tlt.Y, 0));
            //更新缩放比例,保留两位小数
            GlobalVar.ViewInfo.Scale = Math.Round(this.sfr.ScaleX, 0);
        }

        #region 添加元素按钮事件，点击后切换对应的类型
        private void Btn_Add_RFID_Click(object sender, RoutedEventArgs e)
        {
            //清除选中状态
            MapFunction.ClearAllSelect();
            GlobalVar.NowAddType = GlobalVar.EnumElementType.RFID;
            //添加一个RFID
            GlobalVar.NowSelectIndex = MapElement.AddRFIDAndShow();
            //进入添加模式
            GlobalVar.NowMode = GlobalVar.EnumMode.Add;
        }
        private void Btn_Add_RouteLine_Click(object sender, RoutedEventArgs e)
        {
            //清除选中状态
            MapFunction.ClearAllSelect();
            GlobalVar.NowAddType = GlobalVar.EnumElementType.RouteLine;
            //添加直线
            GlobalVar.NowSelectIndex = MapElement.AddRouteLineAndShow();
            //进入添加模式
            GlobalVar.NowMode = GlobalVar.EnumMode.Add;
        }
        private void Btn_Add_RouteForkLine_Click(object sender, RoutedEventArgs e)
        {
            //清除选中状态
            MapFunction.ClearAllSelect();
            GlobalVar.NowAddType = GlobalVar.EnumElementType.RouteForkLine;
            //添加分叉线
            GlobalVar.NowSelectIndex = MapElement.AddForkLineAndShow();
            //进入添加模式
            GlobalVar.NowMode = GlobalVar.EnumMode.Add;
        }
        #endregion
    }
}
