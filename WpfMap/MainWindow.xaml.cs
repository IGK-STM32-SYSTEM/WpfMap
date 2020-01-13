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
            MapElement.DrawGrid(1024 * 2, 768 * 2, cvGrid);
            //绘制所有标签
            MapElement.DrawRFIDList(cvMap);
        }

        //当前编辑的站点
        int _editeRFIDIndex = -1;

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

            //编辑路径
            if (GlobalVar.mouseFunction == GlobalVar.MouseFunctionEnum.RouteEdit)
            {
                ////手动调整时启动
                //if (RadioButtonStationEditeMenul.IsChecked == true)
                //{
                //    //站点世界坐标转视图坐标
                //    double x = GlobalVar.routeNow.stations[GlobalVar.routeConfig.StationNow].Pose.X;
                //    double y = GlobalVar.routeNow.stations[GlobalVar.routeConfig.StationNow].Pose.Y;

                //    System.Windows.Point point = RosHelper.WorldToView(new System.Windows.Point(x, y));
                //    //计算角度
                //    double angle = RosHelper.PointToAngle(RosHelper.ViewToAxisPoint(point), RosHelper.ViewToAxisPoint(nowPoint));
                //    GlobalVar.routeNow.stations[GlobalVar.routeConfig.StationNow].Angle = angle;

                //    //更新站点图标
                //    DrawRoute();
                //    //更新界面
                //    RouteInfoUpdate();
                //}
            }
            else
            {
                GlobalVar.mouseFunction = GlobalVar.MouseFunctionEnum.MoveView;
                this.Cursor = Cursors.Hand;
            }
        }
        //右键抬起
        private void imageRobot_PreviewMouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (GlobalVar.mouseFunction == GlobalVar.MouseFunctionEnum.RouteEdit)
            {
                //如果是添加新元素
                if (GlobalVar.NowAddEditElementType != GlobalVar.EnumElementType.None)
                {
                    //结束添加
                    GlobalVar.NowAddEditElementType = GlobalVar.EnumElementType.None;
                    return;
                }
            }
            else
            {
                GlobalVar.mouseFunction = GlobalVar.MouseFunctionEnum.None;
                this.Cursor = Cursors.Arrow;
            }
        }
        //左键按下
        private void imageRobot_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //记录按下时的位置
            GlobalVar.mouseLeftBtnDownToMap = e.GetPosition(cvMap);
            GlobalVar.mouseLeftBtnDownToView = e.GetPosition(drawViewScroll);
            //站点编辑
            if (GlobalVar.mouseFunction == GlobalVar.MouseFunctionEnum.RouteEdit)
            {

                //如果不是在添加新元素
                if (GlobalVar.NowAddEditElementType == GlobalVar.EnumElementType.None)
                {
                    //判断光标是否在某个站点上，并赋给选中点的索引
                    int rs = MapFunction.IsOnRFID(GlobalVar.mouseLeftBtnDownToMap);
                    //如果未选中并且之前有选中，则清除之前
                    if (rs == -1 && _editeRFIDIndex != -1)
                    {
                        MapFunction.SetRFIDIsNormal(_editeRFIDIndex, cvMap);
                        _editeRFIDIndex = rs;
                    }
                    else
                    //当前选中的和之前选中的不一致，则清除之前的
                    if (rs != -1 && rs != _editeRFIDIndex)
                    {
                        MapFunction.SetRFIDIsNormal(_editeRFIDIndex, cvMap);
                        //设置该点选中
                        MapFunction.SetRFIDIsSelected(rs, cvMap);
                        _editeRFIDIndex = rs;

                    }
                    else
                    {
                        //设置该点选中
                        MapFunction.SetRFIDIsSelected(rs, cvMap);
                        _editeRFIDIndex = rs;
                    }
                }
            }
        }
        //左键抬起
        private void imageRobot_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            //记录按下时的位置
            GlobalVar.mouseLeftBtnDownToMap = e.GetPosition(cvMap);
            if (_editeRFIDIndex == -1)
                return;
            //如果是添加新元素
            if (GlobalVar.NowAddEditElementType != GlobalVar.EnumElementType.None)
            {
                //继续增加一个
                if (GlobalVar.NowAddEditElementType == GlobalVar.EnumElementType.RFID)
                {
                    //添加一个RFID
                    MapElement.RFID rfid = new MapElement.RFID();
                    rfid.Num = MapElement.MapRFIDList.Count + 1;
                    MapElement.MapRFIDList.Add(rfid);
                    //绘制到界面
                    MapElement.DrawRFID(MapElement.MapRFIDList.Count - 1, cvMap);
                    //设置该RFID为当前正在操作的RFID
                    _editeRFIDIndex = MapElement.MapRFIDList.Count - 1;
                }
                return;
            }
            bool rs = MapFunction.IsOnOneRFID(GlobalVar.mouseLeftBtnDownToMap, _editeRFIDIndex);
            //不在当前站点上
            if (rs == false)
                _editeRFIDIndex = -1;
        }
        //鼠标移动
        private void imageRobot_PreviewMouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            //获取当前坐标
            //System.Windows.Point nowPoint = e.GetPosition(ViewUielement.Map.image);
            System.Windows.Point nowPoint = e.GetPosition(gridDraw);
            System.Windows.Point nowPointToView = e.GetPosition(drawViewScroll);
            //显示当前坐标
            GlobalVar.ViewInfo.View = new Point(Math.Round(nowPoint.X, 0), Math.Round(nowPoint.Y, 0));
            //移动视图
            if (GlobalVar.mouseFunction == GlobalVar.MouseFunctionEnum.MoveView)
            {
                if (e.RightButton == MouseButtonState.Pressed)
                {
                    System.Windows.Point position = e.GetPosition(cvMap);
                    System.Windows.Point position1 = e.GetPosition(drawViewScroll);

                    tlt.X += position.X - GlobalVar.mouseRightBtnDownPoint.X;
                    tlt.Y += position.Y - GlobalVar.mouseRightBtnDownPoint.Y;

                    //更新圆点坐标,保留两位小数
                    GlobalVar.ViewInfo.Origin = new Point(Math.Round(tlt.X, 0), Math.Round(tlt.Y, 0));
                }

            }
            else
            //编辑路径
            if (GlobalVar.mouseFunction == GlobalVar.MouseFunctionEnum.RouteEdit)
            {
                //添加新元素
                if (GlobalVar.NowAddEditElementType != GlobalVar.EnumElementType.None)
                {
                    MapFunction.MoveRFIDTo(_editeRFIDIndex, nowPoint, cvMap);
                }
                else
                //左键移动位置
                if (e.LeftButton == MouseButtonState.Pressed && _editeRFIDIndex != -1)
                {
                    MapFunction.MoveRFIDTo(_editeRFIDIndex, nowPoint, cvMap);
                    //nowPoint.X -= 10;
                    //nowPoint.Y -= 10;

                    //Thickness thickness = GlobalVar.Stations[_editeRFIDIndex].ellipse.Margin;
                    //thickness.Left = nowPoint.X - nowPoint.X % 20;
                    //thickness.Top = nowPoint.Y - nowPoint.Y % 20;
                    //GlobalVar.Stations[_editeRFIDIndex].ellipse.Margin = thickness;

                    //thickness.Left += 15;
                    //thickness.Top += 10;
                    //GlobalVar.Stations[_editeRFIDIndex].textBlock.Margin = thickness;


                    //PointF pointf = RosHelper.ViewToWorld(nowPoint);

                    //GlobalVar.routeNow.stations[GlobalVar.routeConfig.StationNow].Pose = new System.Windows.Point(pointf.X, pointf.Y);
                    ////更新站点图标
                    //DrawRoute();
                    ////更新界面
                    //RouteInfoUpdate();
                }
                else
                //右键调整角度
                if (e.RightButton == MouseButtonState.Pressed)
                {
                    ////站点世界坐标转视图坐标
                    //double x = GlobalVar.routeNow.stations[GlobalVar.routeConfig.StationNow].Pose.X;
                    //double y = GlobalVar.routeNow.stations[GlobalVar.routeConfig.StationNow].Pose.Y;

                    //System.Windows.Point point = RosHelper.WorldToView(new System.Windows.Point(x, y));
                    ////计算角度
                    //double angle = RosHelper.PointToAngle(RosHelper.ViewToAxisPoint(point), RosHelper.ViewToAxisPoint(nowPoint));
                    //GlobalVar.routeNow.stations[GlobalVar.routeConfig.StationNow].Angle = angle;

                    ////更新站点图标
                    //DrawRoute();
                    ////更新界面
                    //RouteInfoUpdate();
                }
            }
            else
            {
                //左键按下旋转
                if (e.LeftButton == MouseButtonState.Pressed)
                {
                    //rotate.CenterX = RosHelper.viewSize.Width / 2;
                    //rotate.CenterY = RosHelper.viewSize.Height / 2;
                    //double angle = RosHelper.GetAngle(new System.Windows.Point(rotate.CenterX, rotate.CenterY), GlobalVar.mouseLeftBtnDownToView, nowPointToView);

                    //bottomInfo.RobotAngle = string.Format("角度:{0} ", angle);
                    //rotate.Angle = angletemp;
                    //rotate.Angle += angle;

                }
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
            //Msg.Show(Userkey.Key.ToString());
            //Msg.Show(Userkey.KeyState.ToString());
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

        #region 模式切换按钮事件
        //浏览模式
        private void RadioLook_Click(object sender, RoutedEventArgs e)
        {
            GlobalVar.mouseFunction = GlobalVar.MouseFunctionEnum.None;
        }
        //编辑模式
        private void RadioEdit_Click(object sender, RoutedEventArgs e)
        {
            GlobalVar.mouseFunction = GlobalVar.MouseFunctionEnum.RouteEdit;
            GlobalVar.NowAddEditElementType = GlobalVar.EnumElementType.None;
        }
        #endregion

        #region 添加元素按钮事件，点击后切换对应的类型
        private void Btn_Add_RFID_Click(object sender, RoutedEventArgs e)
        {
            //不是编辑模式，点击无效
            if (GlobalVar.mouseFunction != GlobalVar.MouseFunctionEnum.RouteEdit)
                return;
            GlobalVar.NowAddEditElementType = GlobalVar.EnumElementType.RFID;
            //添加一个RFID
            MapElement.RFID rfid = new MapElement.RFID();
            rfid.Num = MapElement.MapRFIDList.Count + 1;
            MapElement.MapRFIDList.Add(rfid);
            //绘制到界面
            MapElement.DrawRFID(MapElement.MapRFIDList.Count - 1, cvMap);
            //设置该RFID为当前正在操作的RFID
            _editeRFIDIndex = MapElement.MapRFIDList.Count - 1;
        }
        private void Btn_Add_RouteLine_Click(object sender, RoutedEventArgs e)
        {
            //不是编辑模式，点击无效
            if (GlobalVar.mouseFunction != GlobalVar.MouseFunctionEnum.RouteEdit)
                return;
            GlobalVar.NowAddEditElementType = GlobalVar.EnumElementType.RouteLine;
        }
        private void Btn_Add_RouteForkLine_Click(object sender, RoutedEventArgs e)
        {
            //不是编辑模式，点击无效
            if (GlobalVar.mouseFunction != GlobalVar.MouseFunctionEnum.RouteEdit)
                return;
            GlobalVar.NowAddEditElementType = GlobalVar.EnumElementType.RouteForkLine;
        }
        #endregion
    }
}
