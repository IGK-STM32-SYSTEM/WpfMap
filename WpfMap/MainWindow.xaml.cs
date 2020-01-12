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
            int num = 0;
            for (int i = 0; i < 5; i++)
            {
                num++;
                GlobalVar.Station station = new GlobalVar.Station();
                station.Num = num;
                station.point = new Point(200 + i * 50, 100);
                GlobalVar.Stations.Add(station);
            }
            for (int i = 0; i < 5; i++)
            {
                num++;
                GlobalVar.Station station = new GlobalVar.Station();
                station.Num = num;
                station.point = new Point(200 + i * 50, 200);
                GlobalVar.Stations.Add(station);
            }

            //画背景栅格，大小为20*20
            DrawGrid(20);

            //画站点
            DrawStation();
        }


        //绘制栅格
        void DrawGrid(int unitSize)
        {
            //定义栅格区域大小
            int width = 1024;
            int height = 768;
            //横线数量
            int HorizontalNum = height / unitSize;
            //竖线数量
            int VerticalNum = width / unitSize;

            //画横线
            for (int i = 0; i < HorizontalNum; i++)
            {
                Line line = new Line();
                Point pointStart = new Point(0, i * unitSize);
                Point pointEnd = new Point(width, i * unitSize);

                //画方向线
                line.X1 = pointStart.X;
                line.Y1 = pointStart.Y;
                line.X2 = pointEnd.X;
                line.Y2 = pointEnd.Y;
                if (i % 2 == 0)
                    line.StrokeThickness = 0.4;
                else
                    line.StrokeThickness = 0.2;
                line.Stroke = System.Windows.Media.Brushes.LightGray;
                cvGrid.Children.Add(line);
            }
            //画竖线
            for (int i = 0; i < VerticalNum + 1; i++)
            {
                Line line = new Line();
                Point pointStart = new Point(i * unitSize, 0);
                Point pointEnd = new Point(i * unitSize, height);

                //画方向线
                line.X1 = pointStart.X;
                line.Y1 = pointStart.Y;
                line.X2 = pointEnd.X;
                line.Y2 = pointEnd.Y;
                if (i % 2 == 0)
                    line.StrokeThickness = 0.4;
                else
                    line.StrokeThickness = 0.2;
                line.Stroke = System.Windows.Media.Brushes.LightGray;
                cvGrid.Children.Add(line);
            }

        }

        //绘制站点
        void DrawStation()
        {
            foreach (var item in GlobalVar.Stations)
            {
                //站点
                float Radius = 20;
                //绘制
                item.ellipse.Height = Radius * 2;
                item.ellipse.Width = Radius * 2;
                item.ellipse.Margin = new Thickness(item.point.X - Radius, item.point.Y - Radius, 0, 0);
                item.ellipse.StrokeThickness = 1;
                item.ellipse.Fill = System.Windows.Media.Brushes.Yellow;
                item.ellipse.Stroke = System.Windows.Media.Brushes.Gray;
                cvMap.Children.Add(item.ellipse);
                //显示编号
                drawText(item.point.X - 4, item.point.Y - 7, item.Num.ToString(), Colors.DarkGray, cvMap, item.textBlock);
            }
        }

        private void drawText(double x, double y, string text, Color color, Canvas canvasObj, TextBlock textBlock)
        {
            textBlock.Text = text;
            textBlock.Foreground = new SolidColorBrush(color);
            Canvas.SetLeft(textBlock, x);
            Canvas.SetTop(textBlock, y);
            canvasObj.Children.Add(textBlock);
        }

        //记录用户按键
        class UserKeyClass
        {
            public Key Key;
            public KeyStates KeyState = KeyStates.None;
        }
        UserKeyClass Userkey = new UserKeyClass();
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
            if (this.sfr.ScaleX < 0.2)
            {
                this.sfr.ScaleX = 0.2;
                this.sfr.ScaleY = 0.2;
            }
            UpdateBottomInfo();
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
            if (GlobalVar.mouseFunction != GlobalVar.MouseFunctionEnum.RouteEdit)
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
            ////初始化机器人位置
            //if (GlobalVar.mouseFunction == GlobalVar.MouseFunctionEnum.ResetRobot)
            //{
            //    //绘制位置指示器
            //    //  DrawPostionCursor(GlobalVar.mouseLeftBtnDownToMap);
            //}
            //else
            ////手动设置机器人目标位置
            //if (GlobalVar.mouseFunction == GlobalVar.MouseFunctionEnum.SetTargetPose)
            //{
            //    //绘制位置指示器
            //    // DrawPostionCursor(GlobalVar.mouseLeftBtnDownToMap);
            //}
            ////else
            //站点编辑
            if (GlobalVar.mouseFunction == GlobalVar.MouseFunctionEnum.RouteEdit)
            {
                //判断光标是否在某个站点上
                for (int i = 0; i < GlobalVar.Stations.Count; i++)
                {
                    //获取站点坐标
                    //double x = GlobalVar.routeNow.stations[i].Pose.X;
                    //double y = GlobalVar.routeNow.stations[i].Pose.Y;
                    //System.Windows.Point point = RosHelper.WorldToView(new System.Windows.Point(x, y));
                    var distance = MathHelper.Distance(GlobalVar.Stations[i].point, GlobalVar.mouseLeftBtnDownToMap);
                    //在当前站点上
                    if (distance <= 20)
                    {
                        //将当前站点切换为绿色

                        GlobalVar.Stations[i].ellipse.Fill = Brushes.Green;

                        ////切换当前站点
                        //GlobalVar.routeConfig.StationNow = i;
                        ////更新界面
                        //DrawRoute();
                        ////更新信息
                        //RouteInfoUpdate();
                        return;
                    }
                }

                ////更新站点坐标
                //PointF pointf = RosHelper.ViewToWorld(GlobalVar.mouseLeftBtnDownToMap);
                //GlobalVar.routeNow.stations[GlobalVar.routeConfig.StationNow].Pose = new System.Windows.Point(pointf.X, pointf.Y);
                ////更新站点图标
                //DrawRoute();
                ////更新界面
                //RouteInfoUpdate();
            }
        }
        //左键抬起
        private void imageRobot_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            //重置机器人位置
            if (GlobalVar.mouseFunction == GlobalVar.MouseFunctionEnum.ResetRobot)
            {
                //清除标志
                GlobalVar.mouseFunction = GlobalVar.MouseFunctionEnum.None;
                ////清除指示器
                //ClearPostionCursor();
                ////界面坐标系转真实坐标系
                //PointF pointf = RosHelper.ViewToAxis(GlobalVar.mouseLeftBtnDownToMap);
                ////真实坐标系转世界坐标系
                //pointf = RosHelper.AxisToWorld(pointf, GlobalVar.SubHandle.mapGrid);

                ////获取当前坐标
                //System.Windows.Point nowPoint = e.GetPosition(gridDraw);
                ////计算角度
                //double angle = RosHelper.PointToAngle(RosHelper.ViewToAxisPoint(GlobalVar.mouseLeftBtnDownToMap), RosHelper.ViewToAxisPoint(nowPoint));

                ////发布目标位置话题
                //string resetId = GlobalVar.rosSocket.Advertise("/initialpose", MessageList.geometry_msgs.PoseWithCovarianceStamped);

                //GeometryPoseWithCovarianceStamped gps = new GeometryPoseWithCovarianceStamped();
                //gps.header.frame_id = "map";
                //gps.pose.pose.position.x = pointf.X;
                //gps.pose.pose.position.y = pointf.Y;
                //gps.pose.pose.orientation.z = (float)Math.Sin(angle / 2.0 * Math.PI / 180.0);
                //gps.pose.pose.orientation.w = (float)Math.Cos(angle / 2.0 * Math.PI / 180.0);
                //GlobalVar.rosSocket.Publish(resetId, gps);

                //Msg.Show(string.Format(
                //    "重置机器人坐标:\r\n" +
                //    "X:{0}\r\n " +
                //    "Y:{1}\r\n " +
                //    "Z:{2}\r\n " +
                //    "W:{3}",
                //    gps.pose.pose.position.x,
                //    gps.pose.pose.position.y,
                //    gps.pose.pose.orientation.z,
                //    gps.pose.pose.orientation.w
                //    ), System.Windows.Media.Brushes.Yellow);
            }
        }
        //鼠标移动
        private void imageRobot_PreviewMouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            //获取当前坐标
            //System.Windows.Point nowPoint = e.GetPosition(ViewUielement.Map.image);
            System.Windows.Point nowPoint = e.GetPosition(gridDraw);
            System.Windows.Point nowPointToView = e.GetPosition(drawViewScroll);

            ////更新底部坐标信息
            //bottomInfo.ViewAxis = string.Format("视图坐标: X:{0}, Y:{1}", (int)nowPoint.X, (int)nowPoint.Y);
            ////获取世界坐标
            //PointF worldPoint = RosHelper.ViewToWorld(new System.Windows.Point(nowPoint.X, nowPoint.Y));
            //bottomInfo.WordAxis = string.Format("世界坐标: X:{0}, Y:{1}", Math.Round(worldPoint.X, 3), Math.Round(worldPoint.Y, 3));

            ////发布位置
            //if (GlobalVar.mouseFunction == GlobalVar.MouseFunctionEnum.SetTargetPose)
            //{
            //    if (e.LeftButton == MouseButtonState.Pressed)
            //    {
            //        //计算角度
            //        double angle = RosHelper.PointToAngle(RosHelper.ViewToAxisPoint(GlobalVar.mouseLeftBtnDownToMap), RosHelper.ViewToAxisPoint(nowPoint));
            //        //绘制箭头
            //        DrawPostionCursor(GlobalVar.mouseLeftBtnDownToMap, (int)angle);
            //    }
            //}
            //else
            //移动视图
            if (GlobalVar.mouseFunction == GlobalVar.MouseFunctionEnum.MoveView)
            {
                if (e.RightButton == MouseButtonState.Pressed)
                {
                    System.Windows.Point position = e.GetPosition(cvMap);
                    System.Windows.Point position1 = e.GetPosition(drawViewScroll);

                    tlt.X += position.X - GlobalVar.mouseRightBtnDownPoint.X;
                    tlt.Y += position.Y - GlobalVar.mouseRightBtnDownPoint.Y;
                    //RosHelper.originPoint.X += position.X - GlobalVar.mouseRightBtnDownPoint.X;
                    //RosHelper.originPoint.Y += position.Y - GlobalVar.mouseRightBtnDownPoint.Y;
                    UpdateBottomInfo();
                    //DrawRobot();
                    //DrawMap();
                    //DrawGlobalPlan();
                    //UpdateBottomInfo();
                    //DrawRoute();
                }

            }
            else
            //重置机器人位置
            if (GlobalVar.mouseFunction == GlobalVar.MouseFunctionEnum.ResetRobot)
            {
                if (e.LeftButton == MouseButtonState.Pressed)
                {
                    ////计算角度
                    //double angle = RosHelper.PointToAngle(RosHelper.ViewToAxisPoint(GlobalVar.mouseLeftBtnDownToMap), RosHelper.ViewToAxisPoint(nowPoint));
                    ////绘制箭头
                    //DrawPostionCursor(GlobalVar.mouseLeftBtnDownToMap, (int)angle);
                }
            }
            else
            //编辑路径
            if (GlobalVar.mouseFunction == GlobalVar.MouseFunctionEnum.RouteEdit)
            {
                //手动调整时启动
                //左键移动位置
                if (e.LeftButton == MouseButtonState.Pressed)
                {
                    nowPoint.X -= 10;
                    nowPoint.Y -= 10;
                    Thickness thickness = GlobalVar.Stations[2].ellipse.Margin;
                    thickness.Left = nowPoint.X- nowPoint.X % 20;
                    thickness.Top = nowPoint.Y- nowPoint.Y % 20;
                    GlobalVar.Stations[2].ellipse.Margin = thickness;

                    //thickness = GlobalVar.Stations[2].textBlock.Margin;
                    //thickness.Left = nowPoint.X - nowPoint.X % 20;
                    //thickness.Top = nowPoint.Y - nowPoint.Y % 20;
                    //GlobalVar.Stations[2].textBlock.Margin = thickness;


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
            if (e.Key == Key.F && Userkey.Key == Key.V)
            {
                // RestView();
            }
            //记录当前按键
            Userkey.Key = e.Key;
            Userkey.KeyState = KeyStates.Down;
            //Msg.Show(Userkey.Key.ToString());
            //Msg.Show(Userkey.KeyStates.ToString());
        }
        private void drawViewScroll_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            //记录当前按键
            Userkey.Key = e.Key;
            Userkey.KeyState = KeyStates.None;
            //Msg.Show(Userkey.Key.ToString());
            //Msg.Show(Userkey.KeyState.ToString());
        }
        #endregion

        private void RadioLook_Click(object sender, RoutedEventArgs e)
        {
            GlobalVar.mouseFunction = GlobalVar.MouseFunctionEnum.None;
        }

        private void RadioEdit_Click(object sender, RoutedEventArgs e)
        {
            GlobalVar.mouseFunction = GlobalVar.MouseFunctionEnum.RouteEdit;
        }
    }
}
