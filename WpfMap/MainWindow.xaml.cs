﻿using Microsoft.Win32;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
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
            MapElement.CvRFID = cvRFID;//标签
            MapElement.CvRouteLine = cvLine;//直路线
            MapElement.CvForkLine = cvForkLine;//分叉路线

            //画背景栅格，大小为20*20
            MapElement.DrawGrid(1024 * 2, 768 * 2);
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
            if (GlobalVar.NowMode == GlobalVar.EnumMode.EditElement)
            {
            }
            else
            //添加新元素
            if (GlobalVar.NowMode == GlobalVar.EnumMode.AddElement)
            {
                if (GlobalVar.NowType == GlobalVar.EnumElementType.RouteLine)
                {
                    //如果是第一步
                    if (GlobalVar.AddStep == 1)
                    {
                        //清除图形
                        MapFunction.ClearAllSelect();
                        //删除新增线条
                        MapElement.MapObject.MapLineList.RemoveAt(GlobalVar.NowSelectIndex);
                    }
                    else
                    if (GlobalVar.AddStep == 2)
                    {
                        //清除图形
                        MapFunction.ClearAllSelect();
                    }
                }
                else
                if (GlobalVar.NowType == GlobalVar.EnumElementType.RouteForkLine)
                {
                    //如果是第一步
                    if (GlobalVar.AddStep == 1)
                    {
                        //清除图形
                        MapFunction.ClearAllSelect();
                        //删除新增线条
                         MapElement.MapObject.MapForkLineList.RemoveAt(GlobalVar.NowSelectIndex);
                    }
                    else
                    if (GlobalVar.AddStep == 2)
                    {
                        //清除图形
                        MapFunction.ClearAllSelect();
                    }
                }
                //结束添加
                GlobalVar.NowType = GlobalVar.EnumElementType.None;
                GlobalVar.NowMode = GlobalVar.EnumMode.EditElement;
                GlobalVar.NowSelectIndex = -1;
                return;
            }

        }
        //左键按下
        private void imageRobot_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //记录按下时的位置
            GlobalVar.mouseLeftBtnDownToMap = e.GetPosition(cvMap);
            //更新左键按下移动上一次值
            GlobalVar.mouseLeftBtnDownMoveLast = GlobalVar.mouseLeftBtnDownToMap;
            //编辑属性
            if (GlobalVar.NowMode == GlobalVar.EnumMode.EditElement)
            {
                //情况0：如果目前是有选中的直线
                if (GlobalVar.NowSelectIndex != -1 && GlobalVar.NowType == GlobalVar.EnumElementType.RouteLine)
                {
                    //1.判断光标是否在直线的起点编辑器
                    if (MapFunction.IsOnOneLineStart(GlobalVar.NowSelectIndex, GlobalVar.mouseLeftBtnDownToMap))
                    {
                        //切换到调整直线起点
                        GlobalVar.ElementEditMode = GlobalVar.EnumElementEditMode.Start;
                        return;
                    }
                    else
                    //2.判断光标是否落在直线的终点编辑器
                    if (MapFunction.IsOnOneLineEnd(GlobalVar.NowSelectIndex, GlobalVar.mouseLeftBtnDownToMap))
                    {
                        //切换到调整直线终点
                        GlobalVar.ElementEditMode = GlobalVar.EnumElementEditMode.End;
                        return;
                    }
                    else
                        //切换到调整整体
                        GlobalVar.ElementEditMode = GlobalVar.EnumElementEditMode.All;
                }
                //情况1：如果目前是有选中的直线
                if (GlobalVar.NowSelectIndex != -1 && GlobalVar.NowType == GlobalVar.EnumElementType.RouteForkLine)
                {
                    //1.判断光标是否在起点编辑器
                    if (MapFunction.IsOnOneForkLineStart(GlobalVar.NowSelectIndex, GlobalVar.mouseLeftBtnDownToMap))
                    {
                        //切换到调整起点
                        GlobalVar.ElementEditMode = GlobalVar.EnumElementEditMode.Start;
                        return;
                    }
                    else
                    //2.判断光标是否落在终点编辑器
                    if (MapFunction.IsOnOneForkLineEnd(GlobalVar.NowSelectIndex, GlobalVar.mouseLeftBtnDownToMap))
                    {
                        //切换到调整终点
                        GlobalVar.ElementEditMode = GlobalVar.EnumElementEditMode.End;
                        return;
                    }
                    else
                        //切换到调整整体
                        GlobalVar.ElementEditMode = GlobalVar.EnumElementEditMode.All;
                }
                //情况2：判断光标是否在直线上
                int rs = MapFunction.IsOnRouteLine(GlobalVar.mouseLeftBtnDownToMap);
                if (rs != -1)
                {
                    //记录当前margin
                    GlobalVar.ElementMarginLast = MapElement.MapObject.MapLineList[rs].line.Margin;

                    //如果选中的和之前是同一个元素
                    if (rs == GlobalVar.NowSelectIndex && GlobalVar.NowType == GlobalVar.EnumElementType.RouteLine)
                    {
                        //不做任何处理
                        return;
                    }
                    else
                    {
                        //清除选中
                        MapFunction.ClearAllSelect();
                        //设置该点选中
                        MapFunction.SetRouteLineIsSelected(rs);
                        //更新当前选中索引
                        GlobalVar.NowSelectIndex = rs;
                        //更新当前元素
                        GlobalVar.NowType = GlobalVar.EnumElementType.RouteLine;
                        //设置调整模式为整体调节【默认】
                        GlobalVar.ElementEditMode = GlobalVar.EnumElementEditMode.All;
                        return;
                    }
                }
                //情况3：判断光标是否在分叉线【圆弧】上
                rs = MapFunction.IsOnForkLine(GlobalVar.mouseLeftBtnDownToMap);
                if (rs != -1)
                {
                    //记录当前margin
                    GlobalVar.ElementMarginLast =  MapElement.MapObject.MapForkLineList[rs].Path.Margin;

                    //如果选中的和之前是同一个元素
                    if (rs == GlobalVar.NowSelectIndex && GlobalVar.NowType == GlobalVar.EnumElementType.RouteForkLine)
                    {
                        //不做任何处理
                        return;
                    }
                    else
                    {
                        //清除选中
                        MapFunction.ClearAllSelect();
                        //设置该点选中
                        MapFunction.SetForkLineIsSelected(rs);
                        //更新当前选中索引
                        GlobalVar.NowSelectIndex = rs;
                        //更新当前元素
                        GlobalVar.NowType = GlobalVar.EnumElementType.RouteForkLine;
                        //设置调整模式为整体调节【默认】
                        GlobalVar.ElementEditMode = GlobalVar.EnumElementEditMode.All;
                        return;
                    }
                }
                //情况4：判断光标是否在标签上
                rs = MapFunction.IsOnRFID(GlobalVar.mouseLeftBtnDownToMap);
                if (rs != -1)
                {
                    //如果选中的和之前是同一个元素
                    if (rs == GlobalVar.NowSelectIndex && GlobalVar.NowType == GlobalVar.EnumElementType.RFID)
                    {
                        //不做任何处理
                        return;
                    }
                    else
                    {
                        //清除选中
                        MapFunction.ClearAllSelect();
                        //设置该点选中
                        MapFunction.SetRFIDIsSelected(rs);
                        //更新当前选中索引
                        GlobalVar.NowSelectIndex = rs;
                        //更新当前元素
                        GlobalVar.NowType = GlobalVar.EnumElementType.RFID;
                        return;
                    }
                }

                //清除选中
                MapFunction.ClearAllSelect();
                GlobalVar.NowSelectIndex = -1;
            }
        }
        //左键抬起
        private void imageRobot_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            //记录按下时的位置
            GlobalVar.mouseLeftBtnDownToMap = e.GetPosition(cvMap);
            //编辑属性
            if (GlobalVar.NowMode == GlobalVar.EnumMode.EditElement)
            {
            }
            else
            //添加新元素
            if (GlobalVar.NowMode == GlobalVar.EnumMode.AddElement)
            {
                switch (GlobalVar.NowType)
                {
                    case GlobalVar.EnumElementType.None:
                        break;
                    case GlobalVar.EnumElementType.RFID:
                        //添加RFID
                        {
                            //增加下一个
                            GlobalVar.NowSelectIndex = MapElement.AddRFIDAndShow();
                        }
                        break;
                    case GlobalVar.EnumElementType.RouteLine:
                        //添加直线
                        {
                            //如果是第一步
                            if (GlobalVar.AddStep == 1)
                            {
                                //【隐藏编辑器】【隐藏后再添加，避免编辑器出现在线条下发】
                                MapFunction.SetRouteLineIsNormal(GlobalVar.NowSelectIndex);
                                //显示直线,并设置起点坐标
                                MapElement.DrawRouteLine(GlobalVar.NowSelectIndex, GlobalVar.mouseLeftBtnDownToMap);
                                //显示起点编辑器
                                MapElement.RouteLineShowStart(GlobalVar.NowSelectIndex);
                                //显示终点编辑器
                                MapElement.RouteLineShowEnd(GlobalVar.NowSelectIndex);
                                //进入第二步
                                GlobalVar.AddStep = 2;
                            }
                            else
                            if (GlobalVar.AddStep == 2)
                            {
                                //直线添加完成【隐藏编辑器】
                                MapFunction.SetRouteLineIsNormal(GlobalVar.NowSelectIndex);
                                //添加新直线
                                GlobalVar.NowSelectIndex = MapElement.AddRouteLine();
                                //显示直线起点编辑器
                                MapElement.RouteLineShowStart(GlobalVar.NowSelectIndex);
                                //返回添加直线第一步
                                GlobalVar.AddStep = 1;
                            }
                        }
                        break;
                    case GlobalVar.EnumElementType.RouteForkLine:
                        //添加分叉线【圆弧】
                        {
                            //如果是第一步
                            if (GlobalVar.AddStep == 1)
                            {
                                //【隐藏编辑器】【隐藏后再添加，避免编辑器出现在线条下发】
                                MapFunction.SetRouteForkLineIsNormal(GlobalVar.NowSelectIndex);
                                //显示分叉【圆弧】,并设置起点坐标
                                MapElement.DrawForkLine(GlobalVar.NowSelectIndex, GlobalVar.mouseLeftBtnDownToMap);
                                //显示起点编辑器
                                MapElement.ForkLineShowStart(GlobalVar.NowSelectIndex);
                                //显示终点编辑器
                                MapElement.ForkLineShowEnd(GlobalVar.NowSelectIndex);
                                //进入第二步
                                GlobalVar.AddStep = 2;
                            }
                            else
                            if (GlobalVar.AddStep == 2)
                            {
                                //直线添加完成【隐藏编辑器】
                                MapFunction.SetRouteForkLineIsNormal(GlobalVar.NowSelectIndex);
                                //添加新直线
                                GlobalVar.NowSelectIndex = MapElement.AddForkLine();
                                //显示直线起点编辑器
                                MapElement.ForkLineShowStart(GlobalVar.NowSelectIndex);
                                //返回添加直线第一步
                                GlobalVar.AddStep = 1;
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
            if (GlobalVar.NowMode == GlobalVar.EnumMode.EditElement)
            {
                //左键按住移动位置【调整元素位置】
                if (e.LeftButton == MouseButtonState.Pressed && GlobalVar.NowSelectIndex != -1)
                {
                    //计算左键按下移动偏差
                    GlobalVar.mouseLeftBtnDownMoveDiff.X = nowPoint.X - GlobalVar.mouseLeftBtnDownMoveLast.X;
                    GlobalVar.mouseLeftBtnDownMoveDiff.Y = nowPoint.Y - GlobalVar.mouseLeftBtnDownMoveLast.Y;
                    //移动标签
                    if (GlobalVar.NowType == GlobalVar.EnumElementType.RFID)
                        MapFunction.MoveRFIDTo(GlobalVar.NowSelectIndex, nowPoint);
                    else
                    //移动直线
                    if (GlobalVar.NowType == GlobalVar.EnumElementType.RouteLine)
                    {
                        switch (GlobalVar.ElementEditMode)
                        {
                            case GlobalVar.EnumElementEditMode.Start:
                                MapFunction.MoveRouteLineStart(GlobalVar.NowSelectIndex, nowPoint);
                                break;
                            case GlobalVar.EnumElementEditMode.End:
                                MapFunction.MoveRouteLineEnd(GlobalVar.NowSelectIndex, nowPoint);
                                break;
                            case GlobalVar.EnumElementEditMode.All:
                                MapFunction.MoveRouteLineAll(GlobalVar.NowSelectIndex, nowPoint);
                                break;
                            default:
                                break;

                        }
                    }
                    else
                    //移动分叉【圆弧】
                    if (GlobalVar.NowType == GlobalVar.EnumElementType.RouteForkLine)
                    {
                        switch (GlobalVar.ElementEditMode)
                        {
                            case GlobalVar.EnumElementEditMode.Start:
                                MapFunction.MoveForkLineStartForAdd(GlobalVar.NowSelectIndex, nowPoint);
                                break;
                            case GlobalVar.EnumElementEditMode.End:
                                MapFunction.MoveForkLineEnd(GlobalVar.NowSelectIndex, nowPoint);
                                break;
                            case GlobalVar.EnumElementEditMode.All:
                                MapFunction.MoveForkLineAll(GlobalVar.NowSelectIndex, nowPoint);
                                break;
                            default:
                                break;

                        }
                    }
                }
            }
            else
            //添加新元素
            if (GlobalVar.NowMode == GlobalVar.EnumMode.AddElement)
            {
                if (GlobalVar.NowType == GlobalVar.EnumElementType.RFID)
                    MapFunction.MoveRFIDTo(GlobalVar.NowSelectIndex, nowPoint);
                else
                if (GlobalVar.NowType == GlobalVar.EnumElementType.RouteLine)
                {
                    if (GlobalVar.AddStep == 1)
                    {
                        MapFunction.MoveRouteLineStartForAdd(GlobalVar.NowSelectIndex, nowPoint);
                    }
                    else
                    {
                        MapFunction.MoveRouteLineEnd(GlobalVar.NowSelectIndex, nowPoint);
                    }
                }
                else
                if (GlobalVar.NowType == GlobalVar.EnumElementType.RouteForkLine)
                {
                    if (GlobalVar.AddStep == 1)
                    {
                        MapFunction.MoveForkLineStartForAdd(GlobalVar.NowSelectIndex, nowPoint);
                    }
                    else
                    {
                        MapFunction.MoveForkLineEnd(GlobalVar.NowSelectIndex, nowPoint);
                    }
                }
            }
        }
        #endregion

        #region 快捷键事件
        private void drawViewScroll_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            //快捷键VF，重置视图
            if (GlobalVar.Userkey.Key == Key.V && e.Key == Key.F)
            {
                RestView();
            }
            else
            if (e.Key == Key.Delete)
            {
                //是否有选中的
                if (GlobalVar.NowSelectIndex != -1)
                {
                    switch (GlobalVar.NowType)
                    {
                        case GlobalVar.EnumElementType.None:
                            break;
                        //选中了RFID
                        case GlobalVar.EnumElementType.RFID:
                            MapFunction.RemoveRFID(GlobalVar.NowSelectIndex);
                            GlobalVar.NowSelectIndex = -1;
                            break;
                        case GlobalVar.EnumElementType.RouteLine:
                            MapFunction.RemoveRouteLine(GlobalVar.NowSelectIndex);
                            GlobalVar.NowSelectIndex = -1;
                            break;
                        case GlobalVar.EnumElementType.RouteForkLine:
                            MapFunction.RemoveForkLine(GlobalVar.NowSelectIndex);
                            GlobalVar.NowSelectIndex = -1;
                            break;
                        default:
                            break;
                    }
                }
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
            GlobalVar.NowType = GlobalVar.EnumElementType.RFID;
            //添加一个RFID
            GlobalVar.NowSelectIndex = MapElement.AddRFIDAndShow();
            //进入添加模式
            GlobalVar.NowMode = GlobalVar.EnumMode.AddElement;
        }
        private void Btn_Add_RouteLine_Click(object sender, RoutedEventArgs e)
        {
            //清除选中状态
            MapFunction.ClearAllSelect();
            //选中类型为直线
            GlobalVar.NowType = GlobalVar.EnumElementType.RouteLine;
            //添加直线
            GlobalVar.NowSelectIndex = MapElement.AddRouteLine();
            //显示起点编辑器
            MapElement.RouteLineShowStart(GlobalVar.NowSelectIndex);
            //添加直线第一步
            GlobalVar.AddStep = 1;
            //进入添加模式
            GlobalVar.NowMode = GlobalVar.EnumMode.AddElement;
        }
        private void Btn_Add_RouteForkLine_Click(object sender, RoutedEventArgs e)
        {
            //清除选中状态
            MapFunction.ClearAllSelect();
            //选中类型为分叉线
            GlobalVar.NowType = GlobalVar.EnumElementType.RouteForkLine;
            //添加分叉线
            GlobalVar.NowSelectIndex = MapElement.AddForkLine();
            //显示起点编辑器
            MapElement.ForkLineShowStart(GlobalVar.NowSelectIndex);
            //开始第一步
            GlobalVar.AddStep = 1;
            //进入添加模式
            GlobalVar.NowMode = GlobalVar.EnumMode.AddElement;
        }
        #endregion

        private void Btn_SaveMap_Click(object sender, RoutedEventArgs e)
        {
            //保存地图【转换映射】
            SaveMap.Helper.RFIDToJson();
            SaveMap.Helper.LineToJSON();
            SaveMap.Helper.ForkLineToJson();
            string str = JsonConvert.SerializeObject(MapElement.MapObject,Formatting.Indented);
            //保存
            SaveMap.Helper.SaveToFile(str);

            //SaveMap.MapObject.MapRFIDList = MapElement.MapObject.MapRFIDList;
            //SaveMap.MapObject.MapForkLineList= MapElement.
            //获得RFID JSON 字符串
            //保存到数据库

        }

        private void Btn_LoadMap_Click(object sender, RoutedEventArgs e)
        {
            //读取
            string str = SaveMap.Helper.LoadFromFile();
            //json 转为对象
            MapElement.MapObject = JsonConvert.DeserializeObject<MapElement.MapObjectClass>(str);
            //转换映射并显示
            //将Base转为标准对象
            foreach (var item in MapElement.MapObject.MapRFIDList)
            {
                item.SelectRectangle = SaveMap.Convert.BaseToRectangle(item.baseSelectRectangle);
                item.textBlock = SaveMap.Convert.BaseToTextBlock(item.baseTextBlock);
                item.ellipse = SaveMap.Convert.BaseToEllipse(item.baseEllipse);
            }
            //清空画布
            MapElement.CvRFID.Children.Clear();
            //绘制所有
            MapElement.DrawRFIDList();
            foreach (var item in MapElement.MapObject.MapLineList)
            {
                item.EndRect = SaveMap.Convert.BaseToRectangle(item.baseEndRect);
                item.line = SaveMap.Convert.BaseToLine(item.baseLine);
                item.SelectLine = SaveMap.Convert.BaseToLine(item.baseSelectLine);
                item.StartRect = SaveMap.Convert.BaseToRectangle(item.baseStartRect);
                item.textBlock = SaveMap.Convert.BaseToTextBlock(item.baseTextBlock);
            }
            //清空画布
            MapElement.CvRouteLine.Children.Clear();
            //绘制所有
            MapElement.DrawLineList();
            //将Base转为标准对象
            foreach (var item in MapElement.MapObject.MapForkLineList)
            {
                item.Path = SaveMap.Convert.BaseToForkLiePath(item.basePath);
                item.SelectPath = SaveMap.Convert.BaseToForkLiePath(item.baseSelectPath);
                item.StartRect = SaveMap.Convert.BaseToRectangle(item.baseStartRect);
                item.EndRect = SaveMap.Convert.BaseToRectangle(item.baseEndRect);
                item.textBlock = SaveMap.Convert.BaseToTextBlock(item.baseTextBlock);
            }
            //清空画布
            MapElement.CvForkLine.Children.Clear();
            //绘制所有
            MapElement.DrawForkLineList();
        }
    }
}
