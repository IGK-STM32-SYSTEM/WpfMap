using Microsoft.Win32;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using Xceed.Wpf.AvalonDock.Layout.Serialization;

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
            bottomstackPanel.DataContext = MapOperate.ViewInfo;
            //指定画布
            MapElement.CvGrid = cvGrid;//栅格
            MapElement.CvRFID = cvRFID;//标签
            MapElement.CvRouteLine = cvLine;//直路线
            MapElement.CvForkLine = cvForkLine;//分叉路线
            MapElement.CvOperate = cvOperate;//操作层

            //画背景栅格，大小为20*20
            MapElement.DrawGrid(1024 * 4, 768 * 4);
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
            if (this.sfr.ScaleX < 0.5)
            {
                this.sfr.ScaleX = 0.5;
                this.sfr.ScaleY = 0.5;
            }
            //更新缩放比例,保留两位小数
            MapOperate.ViewInfo.Scale = Math.Round(this.sfr.ScaleX, 1);
        }
        //右键按下
        private void imageRobot_PreviewMouseRightButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            //记录鼠标按下时的位置
            MapOperate.mouseRightBtnDownPoint = e.GetPosition(cvMap);
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
            if (MapOperate.NowMode == MapOperate.EnumMode.EditElement)
            {
            }
            else
            //粘贴模式
            if (MapOperate.NowMode == MapOperate.EnumMode.Paste)
            {
                MapFunction.CancelPaste();
                //恢复默认
                MapOperate.NowMode = MapOperate.EnumMode.EditElement;
            }
            else
            //添加新元素
            if (MapOperate.NowMode == MapOperate.EnumMode.AddElement)
            {
                if (MapOperate.NowType == MapOperate.EnumElementType.RFID)
                {
                    MapFunction.RemoveRFID(MapElement.MapObject.RFIDS[MapOperate.NowSelectIndex]);
                }
                else
                if (MapOperate.NowType == MapOperate.EnumElementType.RouteLine)
                {
                    //如果是第一步
                    if (MapOperate.AddStep == 1)
                    {
                        //清除图形
                        MapFunction.ClearSelect();
                        //删除新增线条
                        MapElement.MapObject.Lines.RemoveAt(MapOperate.NowSelectIndex);
                    }
                    else
                    if (MapOperate.AddStep == 2)
                    {
                        //清除图形
                        MapFunction.ClearSelect();
                    }
                }
                else
                if (MapOperate.NowType == MapOperate.EnumElementType.RouteForkLine)
                {
                    //如果是第一步
                    if (MapOperate.AddStep == 1)
                    {
                        //清除图形
                        MapFunction.ClearSelect();
                        //删除新增线条
                        MapElement.MapObject.ForkLines.RemoveAt(MapOperate.NowSelectIndex);
                    }
                    else
                    if (MapOperate.AddStep == 2)
                    {
                        //清除图形
                        MapFunction.ClearSelect();
                    }
                }

                //结束添加
                MapOperate.NowType = MapOperate.EnumElementType.None;
                MapOperate.NowMode = MapOperate.EnumMode.EditElement;
                MapOperate.NowSelectIndex = -1;
                return;
            }

        }
        //左键按下
        private void imageRobot_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            LeftBtnDownStart:
            //记录按下时的位置
            MapOperate.mouseLeftBtnDownToMap = e.GetPosition(cvMap);
            //编辑单个元素
            if (MapOperate.NowMode == MapOperate.EnumMode.EditElement)
            {
                //情况0：如果目前是有选中的直线
                if (MapOperate.NowSelectIndex != -1 && MapOperate.NowType == MapOperate.EnumElementType.RouteLine)
                {
                    //1.判断光标是否在直线的起点编辑器
                    if (MapFunction.IsOnOneLineStart(MapOperate.NowSelectIndex, MapOperate.mouseLeftBtnDownToMap))
                    {
                        //切换到调整直线起点
                        MapOperate.ElementEditMode = MapOperate.EnumElementEditMode.Start;
                        return;
                    }
                    else
                    //2.判断光标是否落在直线的终点编辑器
                    if (MapFunction.IsOnOneLineEnd(MapOperate.NowSelectIndex, MapOperate.mouseLeftBtnDownToMap))
                    {
                        //切换到调整直线终点
                        MapOperate.ElementEditMode = MapOperate.EnumElementEditMode.End;
                        return;
                    }
                    else
                    {
                        //切换到调整整体
                        MapOperate.ElementEditMode = MapOperate.EnumElementEditMode.All;
                        //记录当前状态
                        MapOperate.History.AddRecord("编辑直线");
                    }
                }
                //情况1：如果目前是有选中的分叉线【圆弧】
                if (MapOperate.NowSelectIndex != -1 && MapOperate.NowType == MapOperate.EnumElementType.RouteForkLine)
                {
                    //1.判断光标是否在起点编辑器
                    if (MapFunction.IsOnOneForkLineStart(MapOperate.NowSelectIndex, MapOperate.mouseLeftBtnDownToMap))
                    {
                        //切换到调整起点
                        MapOperate.ElementEditMode = MapOperate.EnumElementEditMode.Start;
                        return;
                    }
                    else
                    //2.判断光标是否落在终点编辑器
                    if (MapFunction.IsOnOneForkLineEnd(MapOperate.NowSelectIndex, MapOperate.mouseLeftBtnDownToMap))
                    {
                        //切换到调整终点
                        MapOperate.ElementEditMode = MapOperate.EnumElementEditMode.End;
                        return;
                    }
                    else
                    {
                        //切换到调整整体
                        MapOperate.ElementEditMode = MapOperate.EnumElementEditMode.All;
                        //记录当前状态
                        MapOperate.History.AddRecord("编辑分叉");
                    }
                }
                //情况2：判断光标是否在直线上
                int rs = MapFunction.IsOnRouteLine(MapOperate.mouseLeftBtnDownToMap);
                if (rs != -1)
                {
                    //记录当前margin
                    MapOperate.ElementMarginLast = MapElement.MapObject.Lines[rs].line.Margin;

                    //如果选中的和之前是同一个元素
                    if (rs == MapOperate.NowSelectIndex && MapOperate.NowType == MapOperate.EnumElementType.RouteLine)
                    {
                        //不做任何处理
                        return;
                    }
                    else
                    {
                        //清除选中
                        MapFunction.ClearSelect();
                        //设置该点选中
                        MapFunction.SetRouteLineIsSelected(rs);
                        //更新当前选中索引
                        MapOperate.NowSelectIndex = rs;
                        //更新当前元素
                        MapOperate.NowType = MapOperate.EnumElementType.RouteLine;
                        //设置调整模式为整体调节【默认】
                        MapOperate.ElementEditMode = MapOperate.EnumElementEditMode.All;
                        return;
                    }
                }
                //情况3：判断光标是否在分叉线【圆弧】上
                rs = MapFunction.IsOnForkLine(MapOperate.mouseLeftBtnDownToMap);
                if (rs != -1)
                {
                    //记录当前margin
                    MapOperate.ElementMarginLast = MapElement.MapObject.ForkLines[rs].Path.Margin;

                    //如果选中的和之前是同一个元素
                    if (rs == MapOperate.NowSelectIndex && MapOperate.NowType == MapOperate.EnumElementType.RouteForkLine)
                    {
                        //不做任何处理
                        return;
                    }
                    else
                    {
                        //清除选中
                        MapFunction.ClearSelect();
                        //设置该点选中
                        MapFunction.SetForkLineIsSelected(rs);
                        //更新当前选中索引
                        MapOperate.NowSelectIndex = rs;
                        //更新当前元素
                        MapOperate.NowType = MapOperate.EnumElementType.RouteForkLine;
                        //设置调整模式为整体调节【默认】
                        MapOperate.ElementEditMode = MapOperate.EnumElementEditMode.All;
                        return;
                    }
                }
                //情况4：判断光标是否在标签上
                rs = MapFunction.IsOnRFID(MapOperate.mouseLeftBtnDownToMap);
                if (rs != -1)
                {
                    //如果选中的和之前是同一个元素
                    if (rs == MapOperate.NowSelectIndex && MapOperate.NowType == MapOperate.EnumElementType.RFID)
                    {
                        //不做任何处理
                        return;
                    }
                    else
                    {
                        //清除单个选中
                        MapFunction.ClearSelect();
                        //清除所有选中
                        MapFunction.ClearAllSelect(MapOperate.MultiSelected);
                        //设置该点选中
                        MapFunction.SetRFIDIsSelected(rs);
                        //更新当前选中索引
                        MapOperate.NowSelectIndex = rs;
                        //更新当前元素
                        MapOperate.NowType = MapOperate.EnumElementType.RFID;
                        return;
                    }
                }

                //清除选中
                MapFunction.ClearSelect();
                //清除所有选中
                MapFunction.ClearAllSelect(MapOperate.MultiSelected);
                MapOperate.NowSelectIndex = -1;
                //进入多选模式
                MapOperate.NowMode = MapOperate.EnumMode.MultiSelect;
                //清除移动状态【是否按住左键移动过】
                MapOperate.MovedAfterLeftBtn = false;
            }
            else
            //编辑多个元素
            if (MapOperate.NowMode == MapOperate.EnumMode.MultiEdit)
            {
                //判断光标是否在已经选中的标签上
                if (MapFunction.IsOnMultiSelected(MapOperate.mouseLeftBtnDownToMap) == false)
                {
                    //点击了空白处，退出多个编辑状态
                    MapOperate.NowMode = MapOperate.EnumMode.EditElement;
                    //清除所有选中
                    MapFunction.ClearAllSelect(MapOperate.MultiSelected);
                    //程序回退到左键按下处，重新执行
                    goto LeftBtnDownStart;
                }
            }
            else
            //粘贴模式
            if (MapOperate.NowMode == MapOperate.EnumMode.Paste)
            {
                //取消选中
                MapFunction.ClearAllSelect(MapOperate.PastedObject);
                //进入正常模式
                MapOperate.NowMode = MapOperate.EnumMode.EditElement;
                //粘贴完成，记录当前状态
                MapOperate.History.AddRecord("粘贴");
            }
        }
        //左键抬起
        private void imageRobot_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            //记录按下时的位置
            MapOperate.mouseLeftBtnDownToMap = e.GetPosition(cvMap);
            //编辑单个元素
            if (MapOperate.NowMode == MapOperate.EnumMode.EditElement)
            {
                //记录当前状态
                MapOperate.History.AddRecord("编辑单个");
            }
            else
            if (MapOperate.NowMode == MapOperate.EnumMode.MultiEdit)
            {
                //记录当前状态
                MapOperate.History.AddRecord("编辑多个");
            }
            else
            //多选状态
            if (MapOperate.NowMode == MapOperate.EnumMode.MultiSelect)
            {
                //是否按住左键移动过,如果没有移动就不能计算选中
                if (MapOperate.MovedAfterLeftBtn)
                {
                    //清除移动标志
                    MapOperate.MovedAfterLeftBtn = false;
                    //清除选中框
                    MapOperate.ClearMultiSelectRect();
                    //计算选中的元素
                    MapFunction.GetMultiSelectedObject();
                    //如果一个都没有选上，退出多选状态
                    if (MapOperate.MultiSelected.RFIDS.Count == 0
                        && MapOperate.MultiSelected.Lines.Count == 0
                        && MapOperate.MultiSelected.ForkLines.Count == 0)
                    {
                        //退出多选模式
                        MapOperate.NowMode = MapOperate.EnumMode.EditElement;
                    }
                    else
                    //有选中的就进入多个编辑状态
                    {
                        MapOperate.NowMode = MapOperate.EnumMode.MultiEdit;
                    }
                }
                //没有移动，直接退出多选
                else
                {
                    //恢复单个编辑模式
                    MapOperate.NowMode = MapOperate.EnumMode.EditElement;
                }
            }
            else
            //添加新元素
            if (MapOperate.NowMode == MapOperate.EnumMode.AddElement)
            {
                switch (MapOperate.NowType)
                {
                    case MapOperate.EnumElementType.None:
                        break;
                    case MapOperate.EnumElementType.RFID:
                        //添加RFID
                        {
                            //记录当前状态
                            MapOperate.History.AddRecord("增加RFID");
                            //增加下一个
                            MapOperate.NowSelectIndex = MapElement.AddRFIDAndShow();
                        }
                        break;
                    case MapOperate.EnumElementType.RouteLine:
                        //添加直线
                        {
                            //如果是第一步
                            if (MapOperate.AddStep == 1)
                            {
                                //【隐藏编辑器】【隐藏后再添加，避免编辑器出现在线条下发】
                                MapFunction.SetRouteLineIsNormal(MapOperate.NowSelectIndex);
                                //显示直线,并设置起点坐标
                                MapElement.DrawRouteLine(MapOperate.NowSelectIndex, MapOperate.mouseLeftBtnDownToMap);
                                //显示起点编辑器
                                MapElement.RouteLineShowStart(MapOperate.NowSelectIndex);
                                //显示终点编辑器
                                MapElement.RouteLineShowEnd(MapOperate.NowSelectIndex);
                                //进入第二步
                                MapOperate.AddStep = 2;
                            }
                            else
                            if (MapOperate.AddStep == 2)
                            {
                                //直线添加完成【隐藏编辑器】
                                MapFunction.SetRouteLineIsNormal(MapOperate.NowSelectIndex);
                                //记录当前状态
                                MapOperate.History.AddRecord("增加直线");
                                //添加新直线
                                MapOperate.NowSelectIndex = MapElement.AddRouteLine();
                                //显示直线起点编辑器
                                MapElement.RouteLineShowStart(MapOperate.NowSelectIndex);
                                //返回添加直线第一步
                                MapOperate.AddStep = 1;
                            }
                        }
                        break;
                    case MapOperate.EnumElementType.RouteForkLine:
                        //添加分叉线【圆弧】
                        {
                            //如果是第一步
                            if (MapOperate.AddStep == 1)
                            {
                                //【隐藏编辑器】【隐藏后再添加，避免编辑器出现在线条下发】
                                MapFunction.SetRouteForkLineIsNormal(MapOperate.NowSelectIndex);
                                //显示分叉【圆弧】,并设置起点坐标
                                MapElement.DrawForkLine(MapOperate.NowSelectIndex, MapOperate.mouseLeftBtnDownToMap);
                                //显示起点编辑器
                                MapElement.ForkLineShowStart(MapOperate.NowSelectIndex);
                                //显示终点编辑器
                                MapElement.ForkLineShowEnd(MapOperate.NowSelectIndex);
                                //进入第二步
                                MapOperate.AddStep = 2;
                            }
                            else
                            if (MapOperate.AddStep == 2)
                            {
                                //直线添加完成【隐藏编辑器】
                                MapFunction.SetRouteForkLineIsNormal(MapOperate.NowSelectIndex);
                                //记录当前状态
                                MapOperate.History.AddRecord("增加分叉");
                                //添加新直线
                                MapOperate.NowSelectIndex = MapElement.AddForkLine();
                                //显示直线起点编辑器
                                MapElement.ForkLineShowStart(MapOperate.NowSelectIndex);
                                //返回添加直线第一步
                                MapOperate.AddStep = 1;
                            }
                        }
                        break;
                    default:
                        break;
                }
            }
        }
        //鼠标移动
        private void imageRobot_PreviewMouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            //获取当前坐标
            Point nowPoint = e.GetPosition(gridDraw);
            MapOperate.NowPoint = nowPoint;
            //显示当前坐标到界面
            MapOperate.ViewInfo.View = new Point(Math.Round(nowPoint.X, 0), Math.Round(nowPoint.Y, 0));
            //计算左键按下移动偏差
            MapOperate.mouseLeftBtnDownMoveDiff.X = nowPoint.X - MapOperate.mouseLeftBtnDownToMap.X;
            MapOperate.mouseLeftBtnDownMoveDiff.Y = nowPoint.Y - MapOperate.mouseLeftBtnDownToMap.Y;

            //移动视图【如果右键按下】
            if (e.RightButton == MouseButtonState.Pressed)
            {
                System.Windows.Point position = e.GetPosition(cvMap);
                System.Windows.Point position1 = e.GetPosition(drawViewScroll);

                tlt.X += (position.X - MapOperate.mouseRightBtnDownPoint.X) * MapOperate.ViewInfo.Scale;
                tlt.Y += (position.Y - MapOperate.mouseRightBtnDownPoint.Y) * MapOperate.ViewInfo.Scale;

                //更新圆点坐标,保留两位小数
                MapOperate.ViewInfo.Origin = new Point(Math.Round(tlt.X, 0), Math.Round(tlt.Y, 0));
            }

            //编辑单个元素
            if (MapOperate.NowMode == MapOperate.EnumMode.EditElement)
            {
                //左键按住移动位置【调整元素位置】
                if (e.LeftButton == MouseButtonState.Pressed)
                {
                    //左键按下后，鼠标不移动也会进入移动事件，
                    //导致如果点击的位置不是中心，标签会动一下，
                    //很不美好，所以加坐标比较，没有变化就不进行移动
                    if (MapOperate.mouseLeftBtnDownToMap.X == nowPoint.X &&
                        MapOperate.mouseLeftBtnDownToMap.Y == nowPoint.Y)
                        return;
                    if (MapOperate.NowSelectIndex != -1)
                    {
                        //移动标签
                        if (MapOperate.NowType == MapOperate.EnumElementType.RFID)
                            MapFunction.MoveRFIDTo(MapOperate.NowSelectIndex, nowPoint);
                        else
                        //移动直线
                        if (MapOperate.NowType == MapOperate.EnumElementType.RouteLine)
                        {
                            switch (MapOperate.ElementEditMode)
                            {
                                case MapOperate.EnumElementEditMode.Start:
                                    MapFunction.MoveRouteLineStart(MapOperate.NowSelectIndex, nowPoint);
                                    break;
                                case MapOperate.EnumElementEditMode.End:
                                    MapFunction.MoveRouteLineEnd(MapOperate.NowSelectIndex, nowPoint);
                                    break;
                                case MapOperate.EnumElementEditMode.All:
                                    MapFunction.MoveRouteLineAll(MapOperate.NowSelectIndex, nowPoint);
                                    break;
                                default:
                                    break;

                            }
                        }
                        else
                        //移动分叉【圆弧】
                        if (MapOperate.NowType == MapOperate.EnumElementType.RouteForkLine)
                        {
                            switch (MapOperate.ElementEditMode)
                            {
                                case MapOperate.EnumElementEditMode.Start:
                                    MapFunction.MoveForkLineStartForAdd(MapOperate.NowSelectIndex, nowPoint);
                                    break;
                                case MapOperate.EnumElementEditMode.End:
                                    MapFunction.MoveForkLineEnd(MapOperate.NowSelectIndex, nowPoint);
                                    break;
                                case MapOperate.EnumElementEditMode.All:
                                    MapFunction.MoveForkLineAll(MapOperate.NowSelectIndex, nowPoint);
                                    break;
                                default:
                                    break;

                            }
                        }
                    }
                }
            }
            else
            //多选模式
            if (MapOperate.NowMode == MapOperate.EnumMode.MultiSelect)
            {
                //标记移动状态【是否按住左键移动过】
                MapOperate.MovedAfterLeftBtn = true;
                //绘制选择框   
                MapOperate.DrawMultiSelectRect(nowPoint);
            }
            else
            //多编辑模式
            if (MapOperate.NowMode == MapOperate.EnumMode.MultiEdit)
            {
                //如果按住左键，则移动对象
                if (e.LeftButton == MouseButtonState.Pressed)
                {
                    //移动所以选中的元素
                    MapFunction.MoveMultiSelected(nowPoint, MapOperate.MultiSelected);
                }
            }
            else
            //粘贴模式
            if (MapOperate.NowMode == MapOperate.EnumMode.Paste)
            {
                //移动所以选中的元素
                MapFunction.MoveMultiSelected(nowPoint, MapOperate.PastedObject);
            }
            else
            //添加新元素
            if (MapOperate.NowMode == MapOperate.EnumMode.AddElement)
            {
                if (MapOperate.NowType == MapOperate.EnumElementType.RFID)
                    MapFunction.MoveRFIDTo(MapOperate.NowSelectIndex, nowPoint);
                else
                if (MapOperate.NowType == MapOperate.EnumElementType.RouteLine)
                {
                    if (MapOperate.AddStep == 1)
                    {
                        MapFunction.MoveRouteLineStartForAdd(MapOperate.NowSelectIndex, nowPoint);
                    }
                    else
                    {
                        MapFunction.MoveRouteLineEnd(MapOperate.NowSelectIndex, nowPoint);
                    }
                }
                else
                if (MapOperate.NowType == MapOperate.EnumElementType.RouteForkLine)
                {
                    if (MapOperate.AddStep == 1)
                    {
                        MapFunction.MoveForkLineStartForAdd(MapOperate.NowSelectIndex, nowPoint);
                    }
                    else
                    {
                        MapFunction.MoveForkLineEnd(MapOperate.NowSelectIndex, nowPoint);
                    }
                }
            }
        }
        #endregion

        #region 快捷键事件
        private void drawViewScroll_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            //V+F【重置视图】
            if (MapOperate.Userkey.Key == Key.V && e.Key == Key.F)
            {
                RestView();
            }
            else
            //Delete【删除】
            if (e.Key == Key.Delete)
            {
                //编辑单个
                if (MapOperate.NowMode == MapOperate.EnumMode.EditElement)
                {
                    //是否有选中的
                    if (MapOperate.NowSelectIndex != -1)
                    {
                        switch (MapOperate.NowType)
                        {
                            case MapOperate.EnumElementType.None:
                                break;
                            //选中了RFID
                            case MapOperate.EnumElementType.RFID:
                                MapFunction.RemoveRFID(MapElement.MapObject.RFIDS[MapOperate.NowSelectIndex]);
                                MapOperate.NowSelectIndex = -1;
                                //记录当前状态
                                MapOperate.History.AddRecord("删除了一个RFID");
                                break;
                            case MapOperate.EnumElementType.RouteLine:
                                MapFunction.RemoveRouteLine(MapElement.MapObject.Lines[MapOperate.NowSelectIndex]);
                                MapOperate.NowSelectIndex = -1;
                                //记录当前状态
                                MapOperate.History.AddRecord("删除了一条直线");
                                break;
                            case MapOperate.EnumElementType.RouteForkLine:
                                MapFunction.RemoveForkLine(MapElement.MapObject.ForkLines[MapOperate.NowSelectIndex]);
                                MapOperate.NowSelectIndex = -1;
                                //记录当前状态
                                MapOperate.History.AddRecord("删除了一条分叉线");
                                break;
                            default:
                                break;
                        }
                    }
                }
                else
                //编辑多个
                if (MapOperate.NowMode == MapOperate.EnumMode.MultiEdit)
                {
                    //删除已选中所有元素
                    //RFID
                    foreach (var item in MapOperate.MultiSelected.RFIDS)
                    {
                        MapFunction.RemoveRFID(item);
                    }
                    //Line
                    foreach (var item in MapOperate.MultiSelected.Lines)
                    {
                        MapFunction.RemoveRouteLine(item);
                    }
                    //ForkLine
                    foreach (var item in MapOperate.MultiSelected.ForkLines)
                    {
                        MapFunction.RemoveForkLine(item);
                    }
                    //清除已选中
                    MapFunction.ClearAllSelect(MapOperate.MultiSelected);
                    //记录当前状态
                    MapOperate.History.AddRecord("批量删除");
                }
            }
            else
            //Ctrl+C【复制】
            if ((Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control && e.Key == Key.C)
            {
                //编辑单个
                if (MapOperate.NowMode == MapOperate.EnumMode.EditElement)
                {
                    if (MapOperate.NowSelectIndex != -1)
                    {
                        //清空剪切板
                        MapFunction.ClearClipBoard();
                        //将元素复制到剪贴板
                        switch (MapOperate.NowType)
                        {
                            case MapOperate.EnumElementType.None:
                                break;
                            case MapOperate.EnumElementType.RFID:
                                //列化深度复制
                                MapElement.RFID rfid = MapFunction.IgkClone.RFID(MapElement.MapObject.RFIDS[MapOperate.NowSelectIndex]);
                                //添加到剪切板
                                MapOperate.Clipboard.RFIDS.Add(rfid);
                                break;
                            case MapOperate.EnumElementType.RouteLine:
                                //列化深度复制
                                MapElement.RouteLine routeLine = MapFunction.IgkClone.Line(MapElement.MapObject.Lines[MapOperate.NowSelectIndex]);
                                //添加到剪切板
                                MapOperate.Clipboard.Lines.Add(routeLine);
                                break;
                            case MapOperate.EnumElementType.RouteForkLine:
                                //列化深度复制
                                MapElement.RouteForkLine routeForkLine = MapFunction.IgkClone.ForkLine(MapElement.MapObject.ForkLines[MapOperate.NowSelectIndex]);
                                //添加到剪切板
                                MapOperate.Clipboard.ForkLines.Add(routeForkLine);
                                break;
                            default:
                                break;
                        }
                    }
                }
                else
                //编辑多个
                if (MapOperate.NowMode == MapOperate.EnumMode.MultiEdit)
                {
                    //将元素复制到剪贴板
                    MapFunction.CopyMultiSelectToClipBoard();
                }
            }
            else
            //Ctrl+V【粘贴】
            if ((Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control && e.Key == Key.V)
            {
                //清除之前的所有选中
                MapFunction.ClearSelect();
                MapOperate.NowSelectIndex = -1;
                MapFunction.ClearAllSelect(MapOperate.MultiSelected);
                /*----将剪切板的元素复制到对应的地图列表----------------------------*/
                //RFID
                MapOperate.PastedObject.RFIDS.Clear();
                foreach (var item in MapOperate.Clipboard.RFIDS)
                {
                    //列化深度复制
                    MapElement.RFID rfid = MapFunction.IgkClone.RFID(item);
                    //修改编号
                    rfid.Num = MapElement.MapObject.RFIDS.Last().Num + 1;
                    //更新到文本
                    rfid.textBlock.Text = rfid.Num.ToString();
                    //添加到列表
                    MapElement.MapObject.RFIDS.Add(rfid);
                    //显示
                    MapElement.ShowRFID(rfid);
                    //设置为选中状态
                    MapFunction.SetRFIDIsSelected(rfid);
                    //添加到粘贴过程对象
                    MapOperate.PastedObject.RFIDS.Add(rfid);
                }
                //Line
                MapOperate.PastedObject.Lines.Clear();
                foreach (var item in MapOperate.Clipboard.Lines)
                {
                    //列化深度复制
                    MapElement.RouteLine line = MapFunction.IgkClone.Line(item);
                    //修改编号
                    line.Num = MapElement.MapObject.Lines.Last().Num + 1;
                    //更新到文本
                    line.textBlock.Text = line.Num.ToString();
                    //添加到列表
                    MapElement.MapObject.Lines.Add(line);
                    //显示
                    MapElement.ShowLine(line);
                    //设置为选中状态
                    MapFunction.SetRouteLineIsSelected(line);
                    //添加到粘贴过程对象
                    MapOperate.PastedObject.Lines.Add(line);
                }
                //ForkLine
                MapOperate.PastedObject.ForkLines.Clear();
                foreach (var item in MapOperate.Clipboard.ForkLines)
                {
                    //列化深度复制
                    MapElement.RouteForkLine forkLine = MapFunction.IgkClone.ForkLine(item);
                    //修改编号
                    forkLine.Num = MapElement.MapObject.ForkLines.Last().Num + 1;
                    //更新到文本
                    forkLine.textBlock.Text = forkLine.Num.ToString();
                    //添加到列表
                    MapElement.MapObject.ForkLines.Add(forkLine);
                    //显示
                    MapElement.ShowForkLine(forkLine);
                    //设置为选中状态
                    MapFunction.SetForkLineIsSelected(forkLine);
                    //添加到粘贴过程对象
                    MapOperate.PastedObject.ForkLines.Add(forkLine);
                }
                /*----使所有对象处于光标中心【优化效果】---------------------------*/
                //计算所有对象的中心
                Point center = MapFunction.GetCenterPoint(MapOperate.PastedObject);
                //将左键的按下值设置为中心值【移动是根据左键按下的位置计算偏差】
                MapOperate.mouseLeftBtnDownToMap = center;
                //计算左键按下移动偏差
                MapOperate.mouseLeftBtnDownMoveDiff.X = MapOperate.NowPoint.X - MapOperate.mouseLeftBtnDownToMap.X;
                MapOperate.mouseLeftBtnDownMoveDiff.Y = MapOperate.NowPoint.Y - MapOperate.mouseLeftBtnDownToMap.Y;
                //初始移动【增加粘贴动态效果】
                MapFunction.MoveMultiSelected(MapOperate.NowPoint, MapOperate.PastedObject);
                //进入粘贴模式
                MapOperate.NowMode = MapOperate.EnumMode.Paste;
            }
            else
            //Esc【取消】
            if (e.Key == Key.Escape)
            {
                //粘贴模式
                if (MapOperate.NowMode == MapOperate.EnumMode.Paste)
                {
                    MapFunction.CancelPaste();
                    //恢复默认
                    MapOperate.NowMode = MapOperate.EnumMode.EditElement;
                }
            }
            else
            //Ctrl+Shif+Z【重做】
            if ((Keyboard.Modifiers & (ModifierKeys.Control | ModifierKeys.Shift)) == (ModifierKeys.Control | ModifierKeys.Shift)
                && e.Key == Key.Z)
            {
                MapOperate.History.Redo();
            }
            else
           //Ctrl+Z【撤销】
           if ((Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control && e.Key == Key.Z)
            {
                MapOperate.History.Undo();
            }
            //记录当前按键
            MapOperate.Userkey.Key = e.Key;
            MapOperate.Userkey.KeyState = KeyStates.Down;
        }
        private void drawViewScroll_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            //记录当前按键
            MapOperate.Userkey.Key = e.Key;
            MapOperate.Userkey.KeyState = KeyStates.None;
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
            MapOperate.ViewInfo.Origin = new Point(Math.Round(tlt.X, 0), Math.Round(tlt.Y, 0));
            //更新缩放比例,保留两位小数
            MapOperate.ViewInfo.Scale = Math.Round(this.sfr.ScaleX, 0);
        }
        #region 添加元素按钮事件，点击后切换对应的类型
        private void Btn_Add_RFID_Click(object sender, RoutedEventArgs e)
        {
            //清除选中状态
            MapFunction.ClearSelect();
            MapOperate.NowType = MapOperate.EnumElementType.RFID;
            //添加一个RFID
            MapOperate.NowSelectIndex = MapElement.AddRFIDAndShow();
            //进入添加模式
            MapOperate.NowMode = MapOperate.EnumMode.AddElement;
        }
        private void Btn_Add_RouteLine_Click(object sender, RoutedEventArgs e)
        {
            //清除选中状态
            MapFunction.ClearSelect();
            //选中类型为直线
            MapOperate.NowType = MapOperate.EnumElementType.RouteLine;
            //添加直线
            MapOperate.NowSelectIndex = MapElement.AddRouteLine();
            //显示起点编辑器
            MapElement.RouteLineShowStart(MapOperate.NowSelectIndex);
            //添加直线第一步
            MapOperate.AddStep = 1;
            //进入添加模式
            MapOperate.NowMode = MapOperate.EnumMode.AddElement;
        }
        private void Btn_Add_RouteForkLine_Click(object sender, RoutedEventArgs e)
        {
            //清除选中状态
            MapFunction.ClearSelect();
            //选中类型为分叉线
            MapOperate.NowType = MapOperate.EnumElementType.RouteForkLine;
            //添加分叉线
            MapOperate.NowSelectIndex = MapElement.AddForkLine();
            //显示起点编辑器
            MapElement.ForkLineShowStart(MapOperate.NowSelectIndex);
            //开始第一步
            MapOperate.AddStep = 1;
            //进入添加模式
            MapOperate.NowMode = MapOperate.EnumMode.AddElement;
        }
        #endregion

        private void Btn_SaveMap_Click(object sender, RoutedEventArgs e)
        {
            //获取字符串地图
            string str = SaveMap.Helper.ObjToJson.MapOject(MapElement.MapObject);
            //保存
            SaveMap.Helper.SaveToFile(str);
        }

        private void Btn_LoadMap_Click(object sender, RoutedEventArgs e)
        {
            //读取
            string str = SaveMap.Helper.LoadFromFile();
            //重载
            MapFunction.ReloadMap(str);
            //记录当前状态
            MapOperate.History.AddRecord("加载地图");
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            using (var writer = new StreamWriter("AvalonDockSavedFile.txt"))
            {
                var layoutSerializer = new XmlLayoutSerializer(_dockingManager);
                layoutSerializer.Serialize(writer);
            }
        }

        private void LoadButton_Click(object sender, RoutedEventArgs e)
        {
            using (var reader = new StreamReader("AvalonDockSavedFile.txt"))
            {
                var layoutSerializer = new XmlLayoutSerializer(_dockingManager);
                layoutSerializer.Deserialize(reader);
            }
        }
    }
}
