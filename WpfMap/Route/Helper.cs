using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfMap.Route
{
    public class Helper
    {
        public class Base
        {
            //定义寻找范围
            public class Range
            {
                public Range()
                {
                    MinX = 0;
                    MinY = 0;
                    MaxX = double.MaxValue;
                    MaxY = double.MaxValue;
                }
                /// <summary>
                /// 最左边
                /// </summary>
                public double MinX;
                /// <summary>
                /// 最上边
                /// </summary>
                public double MinY;
                /// <summary>
                /// 最右边
                /// </summary>
                public double MaxX;
                /// <summary>
                /// 最下边
                /// </summary>
                public double MaxY;
            }
            /// <summary>
            /// 搜索标签
            /// </summary>
            public class FindRFID
            {
                /// <summary>
                /// 【向左】沿直线搜索标签
                /// </summary>
                /// <param name="index">当前标签索引</param>
                /// <param name="point">当前坐标</param>
                public static int Left(int index, Point point, Range range)
                {
                    //定义临时坐标
                    Point pt = new Point(point.X, point.Y);
                    //标签索引
                    int id = -1;
                    for (double i = pt.X; i > range.MinX; i -= 1)
                    {
                        //更新当前位置
                        pt.X = i;
                        //当前点在直线上
                        if (MapFunction.IsOnRouteLine(pt) != -1)
                        {
                            //当前点在标签上【非起点标签】
                            id = MapFunction.IsOnRFID(pt);
                            if (id != -1 && id != index)
                            {
                                //找到有效标签，结束搜索
                                break;
                            }
                        }
                        else
                            break;

                    }
                    if (id != -1 && id != index)
                    {
                        return id;
                    }
                    else
                        return -1;
                }
                /// <summary>
                /// 【向右】沿直线搜索标签
                /// </summary>
                /// <param name="index">当前标签索引</param>
                /// <param name="point">当前坐标</param>
                public static int Right(int index, Point point, Range range)
                {
                    //定义临时坐标
                    Point pt = new Point(point.X, point.Y);
                    //标签索引
                    int id = -1;
                    for (double i = pt.X; i < range.MaxX; i += 1)
                    {
                        //更新当前位置
                        pt.X = i;
                        //当前点在直线上
                        if (MapFunction.IsOnRouteLine(pt) != -1)
                        {
                            //当前点在标签上【非起点标签】
                            id = MapFunction.IsOnRFID(pt);
                            if (id != -1 && id != index)
                            {
                                //找到有效标签，结束搜索
                                break;
                            }
                        }
                        else
                            break;

                    }
                    if (id != -1 && id != index)
                    {
                        return id;
                    }
                    else
                        return -1;
                }
                /// <summary>
                /// 【向上】沿直线搜索标签
                /// </summary>
                /// <param name="index">当前标签索引</param>
                /// <param name="point">当前坐标</param>
                public static int Up(int index, Point point, Range range)
                {
                    //定义临时坐标
                    Point pt = new Point(point.X, point.Y);
                    //标签索引
                    int id = -1;
                    for (double i = pt.Y; i > range.MinY; i -= 1)
                    {
                        //更新当前位置
                        pt.Y = i;
                        //当前点在直线上
                        if (MapFunction.IsOnRouteLine(pt) != -1)
                        {
                            //当前点在标签上【非起点标签】
                            id = MapFunction.IsOnRFID(pt);
                            if (id != -1 && id != index)
                            {
                                //找到有效标签，结束搜索
                                break;
                            }
                        }
                        else
                            break;

                    }
                    if (id != -1 && id != index)
                    {
                        return id;
                    }
                    else
                        return -1;
                }
                /// <summary>
                /// 【向下】沿直线搜索标签
                /// </summary>
                /// <param name="index">当前标签索引</param>
                /// <param name="point">当前坐标</param>
                public static int Down(int index, Point point, Range range)
                {
                    //定义临时坐标
                    Point pt = new Point(point.X, point.Y);
                    //标签索引
                    int id = -1;
                    for (double i = pt.Y; i < range.MaxY; i += 1)
                    {
                        //更新当前位置
                        pt.Y = i;
                        //当前点在直线上
                        if (MapFunction.IsOnRouteLine(pt) != -1)
                        {
                            //当前点在标签上【非起点标签】
                            id = MapFunction.IsOnRFID(pt);
                            if (id != -1 && id != index)
                            {
                                //找到有效标签，结束搜索
                                break;
                            }
                        }
                        else
                            break;
                    }
                    if (id != -1 && id != index)
                    {
                        return id;
                    }
                    else
                        return -1;
                }
                /// <summary>
                /// 【在点上】沿直线搜索标签
                /// </summary>
                /// <param name="index">当前标签索引</param>
                /// <param name="point">当前坐标</param>
                public static int OnPoint(int index, Point point)
                {
                    //定义临时坐标
                    Point pt = new Point(point.X, point.Y);
                    //当前点在标签上
                    int id = MapFunction.IsOnRFID(pt);
                    if (id != -1 && id != index)
                    {
                        return id;
                    }
                    else
                        return -1;
                }
            }
            /// <summary>
            /// 搜索分叉
            /// </summary>
            public class FindForkLine
            {
                /// <summary>
                /// 【向左上】沿直线向左搜索朝上的分叉
                /// </summary>
                /// <param name="point">当前坐标</param>
                /// <param name="minX">搜索极限</param>
                /// <returns></returns>
                public static int LeftUp(Point point, double minX)
                {
                    if (MapElement.MapObject.ForkLines.Count == 0)
                        return -1;
                    //定义临时坐标
                    Point pt = new Point(point.X, point.Y);
                    //分叉索引
                    for (double i = pt.X; i > minX; i -= 1)
                    {
                        //更新当前位置
                        pt.X = i;
                        //在直线上
                        if (MapFunction.IsOnRouteLine(pt) != -1)
                        {
                            //当前点是某一个分叉的终点
                            foreach (var item in MapElement.MapObject.ForkLines)
                            {
                                if (item.EndPoint.Equals(pt))
                                {
                                    //找到，且起点在终点的左上
                                    if (item.StartPoint.X < item.EndPoint.X
                                        && item.StartPoint.Y < item.EndPoint.Y)
                                    {
                                        return MapElement.MapObject.ForkLines.IndexOf(item);
                                    }
                                }
                            }
                        }
                        else
                            return -1;
                    }
                    return -1;
                }
                /// <summary>
                /// 【向左下】沿直线向左搜索朝下的分叉
                /// </summary>
                /// <param name="point">当前坐标</param>
                /// <param name="minX">搜索极限</param>
                /// <returns></returns>
                public static int LeftDown(Point point, double minX)
                {
                    if (MapElement.MapObject.ForkLines.Count == 0)
                        return -1;
                    //定义临时坐标
                    Point pt = new Point(point.X, point.Y);
                    //分叉索引
                    for (double i = pt.X; i > minX; i -= 1)
                    {
                        //更新当前位置
                        pt.X = i;
                        //在直线上
                        if (MapFunction.IsOnRouteLine(pt) != -1)
                        {
                            //当前点是某一个分叉的终点
                            foreach (var item in MapElement.MapObject.ForkLines)
                            {
                                if (item.EndPoint.Equals(pt))
                                {
                                    //找到，且起点在终点的左下
                                    if (item.StartPoint.X < item.EndPoint.X
                                        && item.StartPoint.Y > item.EndPoint.Y)
                                    {
                                        return MapElement.MapObject.ForkLines.IndexOf(item);
                                    }
                                }
                            }
                        }
                        else
                            return -1;
                    }
                    return -1;
                }
                /// <summary>
                /// 【向右上】沿直线向右搜索朝上的分叉
                /// </summary>
                /// <param name="point">当前坐标</param>
                /// <param name="maxX">搜索极限</param>
                /// <returns></returns>
                public static int RightUp(Point point, double maxX)
                {
                    if (MapElement.MapObject.ForkLines.Count == 0)
                        return -1;
                    //定义临时坐标
                    Point pt = new Point(point.X, point.Y);
                    //分叉索引
                    for (double i = pt.X; i < maxX; i += 1)
                    {
                        //更新当前位置
                        pt.X = i;
                        //在直线上
                        if (MapFunction.IsOnRouteLine(pt) != -1)
                        {
                            //当前点是某一个分叉的终点
                            foreach (var item in MapElement.MapObject.ForkLines)
                            {
                                if (item.EndPoint.Equals(pt))
                                {
                                    //找到，且起点在终点的右上
                                    if (item.StartPoint.X > item.EndPoint.X
                                        && item.StartPoint.Y < item.EndPoint.Y)
                                    {
                                        return MapElement.MapObject.ForkLines.IndexOf(item);
                                    }
                                }
                            }
                        }
                        else
                            return -1;

                    }
                    return -1;
                }
                /// <summary>
                /// 【向右下】沿直线向右搜索朝下的分叉
                /// </summary>
                /// <param name="point">当前坐标</param>
                /// <param name="maxX">搜索极限</param>
                /// <returns></returns>
                public static int RightDown(Point point, double maxX)
                {
                    if (MapElement.MapObject.ForkLines.Count == 0)
                        return -1;
                    //定义临时坐标
                    Point pt = new Point(point.X, point.Y);
                    //分叉索引
                    for (double i = pt.X; i < maxX; i += 1)
                    {
                        //更新当前位置
                        pt.X = i;
                        //在直线上
                        if (MapFunction.IsOnRouteLine(pt) != -1)
                        {
                            //当前点是某一个分叉的终点
                            foreach (var item in MapElement.MapObject.ForkLines)
                            {
                                if (item.EndPoint.Equals(pt))
                                {
                                    //找到，且起点在终点的右下
                                    if (item.StartPoint.X > item.EndPoint.X
                                        && item.StartPoint.Y > item.EndPoint.Y)
                                    {
                                        return MapElement.MapObject.ForkLines.IndexOf(item);
                                    }
                                }
                            }
                        }
                        else
                            return -1;
                    }
                    return -1;
                }

                /// <summary>
                /// 【向上左】沿直线向上搜索朝左的分叉
                /// </summary>
                /// <param name="point">当前坐标</param>
                /// <param name="minY">搜索极限</param>
                /// <returns></returns>
                public static int UpLeft(Point point, double minY)
                {
                    if (MapElement.MapObject.ForkLines.Count == 0)
                        return -1;
                    //定义临时坐标
                    Point pt = new Point(point.X, point.Y);
                    //分叉索引
                    for (double i = pt.Y; i > minY; i -= 1)
                    {
                        //更新当前位置
                        pt.Y = i;
                        //在直线上
                        if (MapFunction.IsOnRouteLine(pt) != -1)
                        {
                            //当前点是某一个分叉的起点
                            foreach (var item in MapElement.MapObject.ForkLines)
                            {
                                if (item.StartPoint.Equals(pt))
                                {
                                    //找到，且终点在起点的左上
                                    if (item.EndPoint.X < item.StartPoint.X
                                        && item.EndPoint.Y < item.StartPoint.Y)
                                    {
                                        return MapElement.MapObject.ForkLines.IndexOf(item);
                                    }
                                }
                            }
                        }
                        else
                            return -1;
                    }
                    return -1;
                }
                /// <summary>
                /// 【向上右】沿直线向上搜索朝右的分叉
                /// </summary>
                /// <param name="point">当前坐标</param>
                /// <param name="minY">搜索极限</param>
                /// <returns></returns>
                public static int UpRight(Point point, double minY)
                {
                    if (MapElement.MapObject.ForkLines.Count == 0)
                        return -1;
                    //定义临时坐标
                    Point pt = new Point(point.X, point.Y);
                    //分叉索引
                    for (double i = pt.Y; i > minY; i -= 1)
                    {
                        //更新当前位置
                        pt.Y = i;
                        //在直线上
                        if (MapFunction.IsOnRouteLine(pt) != -1)
                        {
                            //当前点是某一个分叉的起点
                            foreach (var item in MapElement.MapObject.ForkLines)
                            {
                                if (item.StartPoint.Equals(pt))
                                {
                                    //找到，且终点在起点的右上
                                    if (item.EndPoint.X > item.StartPoint.X
                                        && item.EndPoint.Y < item.StartPoint.Y)
                                    {
                                        return MapElement.MapObject.ForkLines.IndexOf(item);
                                    }
                                }
                            }
                        }
                        else
                            return -1;
                    }
                    return -1;
                }
                /// <summary>
                /// 【向下左】沿直线向下搜索朝左的分叉
                /// </summary>
                /// <param name="point">当前坐标</param>
                /// <param name="maxY">搜索极限</param>
                /// <returns></returns>
                public static int DownLeft(Point point, double maxY)
                {
                    if (MapElement.MapObject.ForkLines.Count == 0)
                        return -1;
                    //定义临时坐标
                    Point pt = new Point(point.X, point.Y);
                    //分叉索引
                    for (double i = pt.Y; i < maxY; i += 1)
                    {
                        //更新当前位置
                        pt.Y = i;
                        //在直线上
                        if (MapFunction.IsOnRouteLine(pt) != -1)
                        {
                            //当前点是某一个分叉的起点
                            foreach (var item in MapElement.MapObject.ForkLines)
                            {
                                if (item.StartPoint.Equals(pt))
                                {
                                    //找到，且终点在起点的左下
                                    if (item.EndPoint.X < item.StartPoint.X
                                        && item.EndPoint.Y > item.StartPoint.Y)
                                    {
                                        return MapElement.MapObject.ForkLines.IndexOf(item);
                                    }
                                }
                            }
                        }
                        else
                            return -1;
                    }
                    return -1;
                }
                /// <summary>
                /// 【向下右】沿直线向下搜索朝右的分叉
                /// </summary>
                /// <param name="point">当前坐标</param>
                /// <param name="maxY">搜索极限</param>
                /// <returns></returns>
                public static int DownRight(Point point, double maxY)
                {
                    if (MapElement.MapObject.ForkLines.Count == 0)
                        return -1;
                    //定义临时坐标
                    Point pt = new Point(point.X, point.Y);
                    //分叉索引
                    for (double i = pt.Y; i < maxY; i += 1)
                    {
                        //更新当前位置
                        pt.Y = i;
                        //在直线上
                        if (MapFunction.IsOnRouteLine(pt) != -1)
                        {
                            //当前点是某一个分叉的起点
                            foreach (var item in MapElement.MapObject.ForkLines)
                            {
                                if (item.StartPoint.Equals(pt))
                                {
                                    //找到，且终点在起点的右下
                                    if (item.EndPoint.X > item.StartPoint.X
                                        && item.EndPoint.Y > item.StartPoint.Y)
                                    {
                                        return MapElement.MapObject.ForkLines.IndexOf(item);
                                    }
                                }
                            }
                        }
                        else
                            return -1;
                    }
                    return -1;
                }
            }
        }
        /// <summary>
        /// 流程图的状态位置
        /// 搜索原则，先上后下，先左后右
        /// </summary>
        public enum ProcessState
        {
            UpLeft,
            UpRight,
            DownLeft,
            DownRight,
            LeftUp,
            LeftDown,
            RightUp,
            RightDown
        }
        /// <summary>
        /// 流程状态机
        /// </summary>
        /// <param name="index"></param>
        /// <param name="point"></param>
        /// <param name="state"></param>
        /// <param name="range"></param>
        public static bool ProcessBool(int index, Point point, ProcessState state, Base.Range range)
        {
            int id = -1;
            Point pt = new Point(point.X, point.Y);
            //跳转到对应状态
            switch (state)
            {
                case ProcessState.UpLeft:
                    goto UpLeft;
                case ProcessState.UpRight:
                    goto UpRight;
                case ProcessState.DownLeft:
                    goto DownLeft;
                case ProcessState.DownRight:
                    goto DownRight;
                case ProcessState.LeftUp:
                    goto LeftUp;
                case ProcessState.LeftDown:
                    goto LeftDown;
                case ProcessState.RightUp:
                    goto RightUp;
                case ProcessState.RightDown:
                    goto RightDown;
                default:
                    return false;
            }


            #region 【向上找左分叉】
            UpLeft:
            id = Base.FindForkLine.UpLeft(pt, range.MinY);
            //找到分叉
            if (id != -1)
            {
                //设置选中
                MapFunction.SetForkLineIsSelected(id);
                MapOperate.SystemMsg.WriteLine("向上找到【{0}】号【左分叉】!", MapElement.MapObject.ForkLines[id].Num);
                //更新搜索起点
                pt = new Point(MapElement.MapObject.ForkLines[id].EndPoint.X, MapElement.MapObject.ForkLines[id].EndPoint.Y);
                //跳转【向左找标签】
                goto RFID_Left;
            }
            else
            {
                //【向上找右分叉】
                goto UpRight;
            }
            #endregion

            #region 【向上找右分叉】
            UpRight:
            id = Base.FindForkLine.UpRight(pt, range.MinY);
            //找到分叉
            if (id != -1)
            {
                //设置选中
                MapFunction.SetForkLineIsSelected(id);
                MapOperate.SystemMsg.WriteLine("向上找到【{0}】号【右分叉】!", MapElement.MapObject.ForkLines[id].Num);
                //更新搜索起点
                pt = new Point(MapElement.MapObject.ForkLines[id].EndPoint.X, MapElement.MapObject.ForkLines[id].EndPoint.Y);
                //跳转【向右找标签】
                goto RFID_Right;
            }
            else
            {
                return false;
            }
            #endregion

            #region 【向下找左分叉】
            DownLeft:
            id = Base.FindForkLine.DownLeft(pt, range.MaxY);
            //找到分叉
            if (id != -1)
            {
                //设置选中
                MapFunction.SetForkLineIsSelected(id);
                MapOperate.SystemMsg.WriteLine("向下找到【{0}】号【左分叉】!", MapElement.MapObject.ForkLines[id].Num);
                //更新搜索起点
                pt = new Point(MapElement.MapObject.ForkLines[id].EndPoint.X, MapElement.MapObject.ForkLines[id].EndPoint.Y);
                //跳转【向左找标签】
                goto RFID_Left;
            }
            else
            {
                //【向下找右分叉】
                goto DownRight;
            }
            #endregion

            #region 【向下找右分叉】
            DownRight:
            id = Base.FindForkLine.DownRight(pt, range.MaxY);
            //找到分叉
            if (id != -1)
            {
                //设置选中
                MapFunction.SetForkLineIsSelected(id);
                MapOperate.SystemMsg.WriteLine("向下找到【{0}】号【右分叉】!", MapElement.MapObject.ForkLines[id].Num);
                //更新搜索起点
                pt = new Point(MapElement.MapObject.ForkLines[id].EndPoint.X, MapElement.MapObject.ForkLines[id].EndPoint.Y);
                //跳转【向右找标签】
                goto RFID_Right;
            }
            else
            {
                //【完成】
                return false;
            }
            #endregion

            #region 【向左找上分叉】
            LeftUp:
            id = Base.FindForkLine.LeftUp(pt, range.MinX);
            //找到分叉
            if (id != -1)
            {
                //设置选中
                MapFunction.SetForkLineIsSelected(id);
                MapOperate.SystemMsg.WriteLine("向左找到【{0}】号【上分叉】!", MapElement.MapObject.ForkLines[id].Num);
                //更新搜索起点坐标
                pt = new Point(MapElement.MapObject.ForkLines[id].StartPoint.X, MapElement.MapObject.ForkLines[id].StartPoint.Y);
                //跳转【向上找标签】
                goto RFID_Up;
            }
            else
                //【向左找下分叉】
                goto LeftDown;
            #endregion

            #region 【向左找下分叉】
            LeftDown:
            id = Base.FindForkLine.LeftDown(pt, range.MinX);
            //找到分叉
            if (id != -1)
            {
                //设置选中
                MapFunction.SetForkLineIsSelected(id);
                MapOperate.SystemMsg.WriteLine("向左找到【{0}】号【下分叉】!", MapElement.MapObject.ForkLines[id].Num);
                //更新搜索起点坐标
                pt = new Point(MapElement.MapObject.ForkLines[id].StartPoint.X, MapElement.MapObject.ForkLines[id].StartPoint.Y);
                //跳转【向下找标签】
                goto RFID_Down;
            }
            else
                //【完成】
                return false;
            #endregion

            #region 【向右找上分叉】
            RightUp:
            id = Base.FindForkLine.RightUp(pt, range.MaxX);
            //找到分叉
            if (id != -1)
            {
                //设置选中
                MapFunction.SetForkLineIsSelected(id);
                MapOperate.SystemMsg.WriteLine("向右找到【{0}】号【上分叉】!", MapElement.MapObject.ForkLines[id].Num);
                //更新搜索起点坐标
                pt = new Point(MapElement.MapObject.ForkLines[id].StartPoint.X, MapElement.MapObject.ForkLines[id].StartPoint.Y);
                //跳转【向上找标签】
                goto RFID_Up;
            }
            else
                //跳转【向右找下分叉】
                goto RightDown;
            #endregion

            #region 【向右找下分叉】
            RightDown:
            id = Base.FindForkLine.RightDown(pt, range.MaxX);
            //找到分叉
            if (id != -1)
            {
                //设置选中
                MapFunction.SetForkLineIsSelected(id);
                MapOperate.SystemMsg.WriteLine("向右找到【{0}】号【下分叉】!", MapElement.MapObject.ForkLines[id].Num);
                //更新搜索起点坐标
                pt = new Point(MapElement.MapObject.ForkLines[id].StartPoint.X, MapElement.MapObject.ForkLines[id].StartPoint.Y);
                //跳转【向下找标签】
                goto RFID_Down;
            }
            else
                //【完成】
                return false;
            #endregion

            #region 【向上找标签】
            RFID_Up:
            id = Base.FindRFID.Up(index, pt, range);
            if (id != -1)
            {
                //设置选中
                MapFunction.SetRFIDIsSelected(id);
                MapOperate.SystemMsg.WriteLine("向上找到【{0}】号【标签】!", MapElement.MapObject.RFIDS[id].Num);
                //【完成】
                return true;
            }
            else
                //【向上找左分叉】
                goto UpLeft;
            #endregion

            #region 【向下找标签】
            RFID_Down:
            id = Base.FindRFID.Down(index, pt, range);
            if (id != -1)
            {
                //设置选中
                MapFunction.SetRFIDIsSelected(id);
                MapOperate.SystemMsg.WriteLine("向下找到【{0}】号【标签】!", MapElement.MapObject.RFIDS[id].Num);
                //【完成】
                return true;
            }
            else
                //【向下找左分叉】
                goto DownLeft;
            #endregion

            #region 【向左找标签】
            RFID_Left:
            id = Base.FindRFID.Left(index, pt, range);
            if (id != -1)
            {
                //设置选中
                MapFunction.SetRFIDIsSelected(id);
                MapOperate.SystemMsg.WriteLine("向左找到【{0}】号【标签】!", MapElement.MapObject.RFIDS[id].Num);
                //【完成】
                return true;
            }
            else
                //【向左找上分叉】
                goto LeftUp;
            #endregion

            #region 【向右找标签】
            RFID_Right:
            id = Base.FindRFID.Right(index, pt, range);
            if (id != -1)
            {
                //设置选中
                MapFunction.SetRFIDIsSelected(id);
                MapOperate.SystemMsg.WriteLine("向右找到【{0}】号【标签】!", MapElement.MapObject.RFIDS[id].Num);
                //【完成】
                return true;
            }
            else
                //【向右找上分叉】
                goto RightUp;
            #endregion
        }
        //返回List<String>类型结果
        //左上（分叉）
        //上右（分叉）
        //（向右直行）（到达）5（号标签）
        public static List<string> ProcessString(int index, Point point, ProcessState state, Base.Range range)
        {
            //第一次搜索标志，第一次搜索可能右范围限制，之后不再对搜索范围进行限制
            bool isFirst = true;
            List<string> vs = new List<string>();
            int id = -1;
            Point pt = new Point(point.X, point.Y);
            //跳转到对应状态
            switch (state)
            {
                case ProcessState.UpLeft:
                    goto UpLeft;
                case ProcessState.UpRight:
                    goto UpRight;
                case ProcessState.DownLeft:
                    goto DownLeft;
                case ProcessState.DownRight:
                    goto DownRight;
                case ProcessState.LeftUp:
                    goto LeftUp;
                case ProcessState.LeftDown:
                    goto LeftDown;
                case ProcessState.RightUp:
                    goto RightUp;
                case ProcessState.RightDown:
                    goto RightDown;
                default:
                    return null;
            }

            #region 【向上找左分叉】
            UpLeft:
            id = Base.FindForkLine.UpLeft(pt, range.MinY);
            //找到分叉
            if (id != -1)
            {
                //第一次搜索后不再对搜索范围进行限制
                if (isFirst)
                {
                    isFirst = false;
                    range = new Base.Range();
                }
                //记录
                vs.Add(string.Format("上左{0}", id));
                //设置选中
                MapFunction.SetForkLineIsSelected(id);
                MapOperate.SystemMsg.WriteLine("向上找到【{0}】号【左分叉】!", MapElement.MapObject.ForkLines[id].Num);
                //更新搜索起点
                pt = new Point(MapElement.MapObject.ForkLines[id].EndPoint.X, MapElement.MapObject.ForkLines[id].EndPoint.Y);
                //跳转【向左找标签】
                goto RFID_Left;
            }
            else
            {
                //【向上找右分叉】
                goto UpRight;
            }
            #endregion

            #region 【向上找右分叉】
            UpRight:
            id = Base.FindForkLine.UpRight(pt, range.MinY);
            //找到分叉
            if (id != -1)
            {
                //第一次搜索后不再对搜索范围进行限制
                if (isFirst)
                {
                    isFirst = false;
                    range = new Base.Range();
                }
                //记录
                vs.Add(string.Format("上右{0}", id));
                //设置选中
                MapFunction.SetForkLineIsSelected(id);
                MapOperate.SystemMsg.WriteLine("向上找到【{0}】号【右分叉】!", MapElement.MapObject.ForkLines[id].Num);
                //更新搜索起点
                pt = new Point(MapElement.MapObject.ForkLines[id].EndPoint.X, MapElement.MapObject.ForkLines[id].EndPoint.Y);
                //跳转【向右找标签】
                goto RFID_Right;
            }
            else
            {
                return null;
            }
            #endregion

            #region 【向下找左分叉】
            DownLeft:
            id = Base.FindForkLine.DownLeft(pt, range.MaxY);
            //找到分叉
            if (id != -1)
            {
                //第一次搜索后不再对搜索范围进行限制
                if (isFirst)
                {
                    isFirst = false;
                    range = new Base.Range();
                }
                //记录
                vs.Add(string.Format("下左{0}", id));
                //设置选中
                MapFunction.SetForkLineIsSelected(id);
                MapOperate.SystemMsg.WriteLine("向下找到【{0}】号【左分叉】!", MapElement.MapObject.ForkLines[id].Num);
                //更新搜索起点
                pt = new Point(MapElement.MapObject.ForkLines[id].EndPoint.X, MapElement.MapObject.ForkLines[id].EndPoint.Y);
                //跳转【向左找标签】
                goto RFID_Left;
            }
            else
            {
                //【向下找右分叉】
                goto DownRight;
            }
            #endregion

            #region 【向下找右分叉】
            DownRight:
            id = Base.FindForkLine.DownRight(pt, range.MaxY);
            //找到分叉
            if (id != -1)
            {
                //第一次搜索后不再对搜索范围进行限制
                if (isFirst)
                {
                    isFirst = false;
                    range = new Base.Range();
                }
                //记录
                vs.Add(string.Format("下右{0}", id));
                //设置选中
                MapFunction.SetForkLineIsSelected(id);
                MapOperate.SystemMsg.WriteLine("向下找到【{0}】号【右分叉】!", MapElement.MapObject.ForkLines[id].Num);
                //更新搜索起点
                pt = new Point(MapElement.MapObject.ForkLines[id].EndPoint.X, MapElement.MapObject.ForkLines[id].EndPoint.Y);
                //跳转【向右找标签】
                goto RFID_Right;
            }
            else
            {
                //【完成】
                return null;
            }
            #endregion

            #region 【向左找上分叉】
            LeftUp:
            id = Base.FindForkLine.LeftUp(pt, range.MinX);
            //找到分叉
            if (id != -1)
            {
                //第一次搜索后不再对搜索范围进行限制
                if (isFirst)
                {
                    isFirst = false;
                    range = new Base.Range();
                }
                //记录
                vs.Add(string.Format("左上{0}", id));
                //设置选中
                MapFunction.SetForkLineIsSelected(id);
                MapOperate.SystemMsg.WriteLine("向左找到【{0}】号【上分叉】!", MapElement.MapObject.ForkLines[id].Num);
                //更新搜索起点坐标
                pt = new Point(MapElement.MapObject.ForkLines[id].StartPoint.X, MapElement.MapObject.ForkLines[id].StartPoint.Y);
                //跳转【向上找标签】
                goto RFID_Up;
            }
            else
                //【向左找下分叉】
                goto LeftDown;
            #endregion

            #region 【向左找下分叉】
            LeftDown:
            id = Base.FindForkLine.LeftDown(pt, range.MinX);
            //找到分叉
            if (id != -1)
            {
                //第一次搜索后不再对搜索范围进行限制
                if (isFirst)
                {
                    isFirst = false;
                    range = new Base.Range();
                }
                //记录
                vs.Add(string.Format("左下{0}", id));
                //设置选中
                MapFunction.SetForkLineIsSelected(id);
                MapOperate.SystemMsg.WriteLine("向左找到【{0}】号【下分叉】!", MapElement.MapObject.ForkLines[id].Num);
                //更新搜索起点坐标
                pt = new Point(MapElement.MapObject.ForkLines[id].StartPoint.X, MapElement.MapObject.ForkLines[id].StartPoint.Y);
                //跳转【向下找标签】
                goto RFID_Down;
            }
            else
                //【完成】
                return null;
            #endregion

            #region 【向右找上分叉】
            RightUp:
            id = Base.FindForkLine.RightUp(pt, range.MaxX);
            //找到分叉
            if (id != -1)
            {
                //第一次搜索后不再对搜索范围进行限制
                if (isFirst)
                {
                    isFirst = false;
                    range = new Base.Range();
                }
                //记录
                vs.Add(string.Format("右上{0}", id));
                //设置选中
                MapFunction.SetForkLineIsSelected(id);
                MapOperate.SystemMsg.WriteLine("向右找到【{0}】号【上分叉】!", MapElement.MapObject.ForkLines[id].Num);
                //更新搜索起点坐标
                pt = new Point(MapElement.MapObject.ForkLines[id].StartPoint.X, MapElement.MapObject.ForkLines[id].StartPoint.Y);
                //跳转【向上找标签】
                goto RFID_Up;
            }
            else
                //跳转【向右找下分叉】
                goto RightDown;
            #endregion

            #region 【向右找下分叉】
            RightDown:
            id = Base.FindForkLine.RightDown(pt, range.MaxX);
            //找到分叉
            if (id != -1)
            {
                //第一次搜索后不再对搜索范围进行限制
                if (isFirst)
                {
                    isFirst = false;
                    range = new Base.Range();
                }
                //记录
                vs.Add(string.Format("右下{0}", id));
                //设置选中
                MapFunction.SetForkLineIsSelected(id);
                MapOperate.SystemMsg.WriteLine("向右找到【{0}】号【下分叉】!", MapElement.MapObject.ForkLines[id].Num);
                //更新搜索起点坐标
                pt = new Point(MapElement.MapObject.ForkLines[id].StartPoint.X, MapElement.MapObject.ForkLines[id].StartPoint.Y);
                //跳转【向下找标签】
                goto RFID_Down;
            }
            else
                //【完成】
                return null;
            #endregion

            #region 【向上找标签】
            RFID_Up:
            id = Base.FindRFID.Up(index, pt, range);
            if (id != -1)
            {
                //记录
                vs.Add(string.Format("{0}", id));
                //设置选中
                MapFunction.SetRFIDIsSelected(id);
                MapOperate.SystemMsg.WriteLine("向上找到【{0}】号【标签】!", MapElement.MapObject.RFIDS[id].Num);
                //【完成】
                return vs;
            }
            else
                //【向上找左分叉】
                goto UpLeft;
            #endregion

            #region 【向下找标签】
            RFID_Down:
            id = Base.FindRFID.Down(index, pt, range);
            if (id != -1)
            {
                //记录
                vs.Add(string.Format("{0}", id));
                //设置选中
                MapFunction.SetRFIDIsSelected(id);
                MapOperate.SystemMsg.WriteLine("向下找到【{0}】号【标签】!", MapElement.MapObject.RFIDS[id].Num);
                //【完成】
                return vs;
            }
            else
                //【向下找左分叉】
                goto DownLeft;
            #endregion

            #region 【向左找标签】
            RFID_Left:
            id = Base.FindRFID.Left(index, pt, range);
            if (id != -1)
            {
                //记录
                vs.Add(string.Format("{0}", id));
                //设置选中
                MapFunction.SetRFIDIsSelected(id);
                MapOperate.SystemMsg.WriteLine("向左找到【{0}】号【标签】!", MapElement.MapObject.RFIDS[id].Num);
                //【完成】
                return vs;
            }
            else
                //【向左找上分叉】
                goto LeftUp;
            #endregion

            #region 【向右找标签】
            RFID_Right:
            id = Base.FindRFID.Right(index, pt, range);
            if (id != -1)
            {
                //记录
                vs.Add(string.Format("{0}", id));
                //设置选中
                MapFunction.SetRFIDIsSelected(id);
                MapOperate.SystemMsg.WriteLine("向右找到【{0}】号【标签】!", MapElement.MapObject.RFIDS[id].Num);
                //【完成】
                return vs;
            }
            else
                //【向右找上分叉】
                goto RightUp;
            #endregion
        }

        /// <summary>
        /// 二次搜索
        /// </summary>
        /// <param name="index"></param>
        /// <param name="vs1"></param>
        public static void FindSecond(int index, List<string> vs1)
        {
            Base.Range range = new Base.Range();
            List<string> vs4 = null;
            Point pt = new Point();
            //第一个点不用处理【vs2和vs3已经处理过了】，最后一个点是终点标签，不用处理
            for (int i = 1; i < vs1.Count - 1; i++)
            {
                //获取分叉的起点和终点坐标
                int ids = int.Parse(vs1[i].Substring(2, vs1[i].Length - 2));
                Point startPt = MapElement.MapObject.ForkLines[ids].StartPoint;
                Point endPt = MapElement.MapObject.ForkLines[ids].EndPoint;
                switch (vs1[i].Substring(0, 2))
                {
                    case "左上":
                        {
                            //1.避开这个左上继续搜左上
                            pt = new Point(startPt.X, endPt.Y);
                            vs4 = ProcessString(index, pt, ProcessState.LeftUp, range);
                            //2.从终点开始搜左下
                            if (vs4 == null)
                            {
                                vs4 = ProcessString(index, endPt, ProcessState.LeftDown, range);
                                //3.从起点开始搜上右
                                if (vs4 == null)
                                {
                                    //向上找标签
                                    int id = Base.FindRFID.Up(index, startPt, new Base.Range());
                                    if (id.ToString() != vs1.Last())
                                        vs4 = ProcessString(index, startPt, ProcessState.UpRight, range);
                                }
                            }
                            if (vs4 != null && vs4.Last() != vs1.Last())
                                return;
                            else
                                break;
                        }
                    case "左下":
                        {
                            //1.避开这个左下继续搜左下
                            pt = new Point(startPt.X, endPt.Y);
                            vs4 = ProcessString(index, pt, ProcessState.LeftDown, range);
                            //2.从起点开始搜下右
                            if (vs4 == null)
                            {
                                //向下找标签
                                int id = Base.FindRFID.Down(index, startPt, new Base.Range());
                                if (id.ToString() != vs1.Last())
                                {
                                    vs4 = ProcessString(index, startPt, ProcessState.DownRight, range);
                                }
                            }
                            if (vs4 != null && vs4.Last() != vs1.Last())
                                return;
                            else
                                break;
                        }
                    case "右上":
                        {
                            //1.避开这个右上继续搜右上
                            pt = new Point(startPt.X, endPt.Y);
                            vs4 = ProcessString(index, pt, ProcessState.RightUp, range);
                            //2.从终点开始搜右下
                            if (vs4 == null)
                            {
                                vs4 = ProcessString(index, endPt, ProcessState.RightDown, range);
                                //3.从起点开始搜上右
                                if (vs4 == null)
                                {
                                    //向上找标签
                                    int id = Base.FindRFID.Up(index, startPt, new Base.Range());
                                    if (id.ToString() != vs1.Last())
                                        vs4 = ProcessString(index, startPt, ProcessState.UpRight, range);
                                }
                            }
                            if (vs4 != null && vs4.Last() != vs1.Last())
                                return;
                            else
                                break;
                        }
                    case "右下":
                        {
                            //1.避开这个右下继续搜右下
                            pt = new Point(startPt.X, endPt.Y);
                            vs4 = ProcessString(index, pt, ProcessState.RightDown, range);
                            //2.从起点开始搜下右
                            if (vs4 == null)
                            {
                                //向下找标签
                                int id = Base.FindRFID.Down(index, startPt, new Base.Range());
                                if (id.ToString() != vs1.Last())
                                    vs4 = ProcessString(index, startPt, ProcessState.DownRight, range);
                            }
                            if (vs4 != null && vs4.Last() != vs1.Last())
                                return;
                            else
                                break;
                        }
                    case "上左":
                        {
                            //1.避开这个上左继续搜上左
                            pt = new Point(startPt.X, endPt.Y);
                            vs4 = ProcessString(index, pt, ProcessState.UpLeft, range);
                            //2.从起点开始搜上右
                            if (vs4 == null)
                            {
                                vs4 = ProcessString(index, startPt, ProcessState.UpRight, range);
                                //3.从终点开始搜左下
                                if (vs4 == null)
                                {
                                    //向左找标签
                                    int id = Base.FindRFID.Left(index, endPt, new Base.Range());
                                    if (id.ToString() != vs1.Last())
                                        vs4 = ProcessString(index, endPt, ProcessState.LeftDown, range);
                                }
                            }
                            if (vs4 != null && vs4.Last() != vs1.Last())
                                return;
                            else
                                break;
                        }
                    case "下左":
                        {
                            //1.避开这个下左继续搜下左
                            pt = new Point(startPt.X, endPt.Y);
                            vs4 = ProcessString(index, pt, ProcessState.DownLeft, range);
                            //2.从起点开始搜下右
                            if (vs4 == null)
                            {
                                vs4 = ProcessString(index, startPt, ProcessState.DownRight, range);
                                //3.从终点开始搜左下
                                if (vs4 == null)
                                {
                                    //向左找标签
                                    int id = Base.FindRFID.Left(index, endPt, new Base.Range());
                                    if (id.ToString() != vs1.Last())
                                        vs4 = ProcessString(index, endPt, ProcessState.LeftDown, range);
                                }
                            }
                            if (vs4 != null && vs4.Last() != vs1.Last())
                                return;
                            else
                                break;
                        }
                    case "上右":
                        {
                            //1.避开这个上右继续搜上右
                            pt = new Point(startPt.X, endPt.Y);
                            vs4 = ProcessString(index, pt, ProcessState.UpRight, range);
                            //2.从终点开始搜右下
                            if (vs4 == null)
                            {
                                //向右找标签
                                int id = Base.FindRFID.Right(index, endPt, new Base.Range());
                                if (id.ToString() != vs1.Last())
                                    vs4 = ProcessString(index, endPt, ProcessState.RightDown, range);
                            }
                            if (vs4 != null && vs4.Last() != vs1.Last())
                                return;
                            else
                                break;
                        }
                    case "下右":
                        {
                            //1.避开这个下右继续搜下右
                            pt = new Point(startPt.X, endPt.Y);
                            vs4 = ProcessString(index, pt, ProcessState.DownRight, range);
                            //2.从终点开始搜右下
                            if (vs4 == null)
                            {
                                //向右找标签
                                int id = Base.FindRFID.Right(index, endPt, new Base.Range());
                                if (id.ToString() != vs1.Last())
                                    vs4 = ProcessString(index, endPt, ProcessState.RightDown, range);
                            }
                            if (vs4 != null && vs4.Last() != vs1.Last())
                                return;
                            else
                                break;
                        }
                    default:
                        break;
                }
            }
        }
        /// <summary>
        /// 生成一个点的邻接关系
        /// </summary>
        /// <param name="index">标签号</param>
        public static void GenerateNeighbour(int index)
        {
            MapElement.RFID rfid = MapElement.MapObject.RFIDS[index];
            //清除选中
            for (int i = 0; i < MapElement.MapObject.RFIDS.Count; i++)
            {
                MapFunction.SetRFIDIsNormal(i);
            }
            for (int i = 0; i < MapElement.MapObject.ForkLines.Count; i++)
            {
                MapFunction.SetRouteForkLineIsNormal(i);
            }
            #region 向左搜索
            /*-----------搜索【标签】----------------------*/
            int id = Base.FindRFID.Left(index, rfid.LeftPoint, new Base.Range());
            Base.Range range = new Base.Range();
            //搜到标签
            if (id != -1)
            {
                //设置选中
                MapFunction.SetRFIDIsSelected(id);
                MapOperate.SystemMsg.WriteLine("向左直行->到达【{0}】号标签!", MapElement.MapObject.RFIDS[id].Num);
                //设置搜索的x终点坐标【X的最小值为找的的标签的右侧中心坐标】
                range.MinX = MapElement.MapObject.RFIDS[id].RightPoint.X;
            }
            //向左找上分叉
            List<string> vs1 = ProcessString(index, rfid.LeftPoint, ProcessState.LeftUp, range);
            //找到了继续向左找下分叉
            if (vs1 != null)
            {
                //打印结果
                MapOperate.SystemMsg.WriteLine("=============");
                MapOperate.SystemMsg.WriteLine(String.Join("-", vs1.ToArray()));
                MapOperate.SystemMsg.WriteLine("=============");

                List<string> vs2 = null;
                //判断第一次通过状态机找到的是不是左上【如果不是左上那就是左下，接下来的搜索左下就不需要进行了】
                if (vs1.First().StartsWith("左上"))
                {
                    vs2 = ProcessString(index, rfid.LeftPoint, ProcessState.LeftDown, range);
                    //找到下分叉
                    if (vs2 != null)
                    {
                        //打印结果
                        MapOperate.SystemMsg.WriteLine("=============");
                        MapOperate.SystemMsg.WriteLine(String.Join("-", vs2.ToArray()));
                        MapOperate.SystemMsg.WriteLine("=============");
                    }
                }
                /*-----------------------------------------------------------------------
                 * 避开第一个和第二个分叉【如果没有第二个就不用处理】继续找
                 * ---------------------------------------------------------------------*/
                //获取第一个分叉的索引
                int idx1 = int.Parse(vs1.First().Substring(2, vs1.First().Length - 2));
                //获取x坐标【取起点是为了避免再次搜索到该分叉】
                double x1 = MapElement.MapObject.ForkLines[idx1].StartPoint.X;
                double x2 = double.MaxValue;
                //获取第二个分叉的索引
                if (vs2 != null)
                {
                    int idx2 = int.Parse(vs2.First().Substring(2, vs2.First().Length - 2));
                    x2 = MapElement.MapObject.ForkLines[idx2].StartPoint.X;
                }
                //获取y坐标【y坐标相同，取任意一个】
                double y = MapElement.MapObject.ForkLines[idx1].EndPoint.Y;
                //从比较靠左的分叉开始继续向左找分叉【针对三叉路口】
                Point pt = new Point(x1 < x2 ? x1 : x2, y);
                List<string> vs3 = ProcessString(index, pt, ProcessState.LeftUp, range);
                if (vs3 != null)
                {
                    //打印结果
                    MapOperate.SystemMsg.WriteLine("=============");
                    MapOperate.SystemMsg.WriteLine(String.Join("-", vs3.ToArray()));
                    MapOperate.SystemMsg.WriteLine("=============");
                }
                //判断后两步有没有找到，如果没有找到，再根据第一步搜索一遍
                //【系统采用先左后右，先上后下的原则，所有拐弯过多会出现搜索不到】
                if (vs2 == null && vs3 == null && vs1.Count > 2)
                {
                    FindSecond(index, vs1);
                }
            }

            #endregion

            #region 向右搜索
            /*-----------向右搜索【标签】----------------------*/
            id = Base.FindRFID.Right(index, rfid.RightPoint, new Base.Range());
            range = new Base.Range();
            //搜到标签
            if (id != -1)
            {
                //设置选中
                MapFunction.SetRFIDIsSelected(id);
                MapOperate.SystemMsg.WriteLine("向右找到【{0}】号标签!", MapElement.MapObject.RFIDS[id].Num);
                //设置搜索的x终点坐标【X的最大值为找的的标签的左侧中心坐标】
                range.MaxX = MapElement.MapObject.RFIDS[id].LeftPoint.X;
            }
            //向右找上分叉
            vs1 = ProcessString(index, rfid.RightPoint, ProcessState.RightUp, range);
            //找到了继续向右找下分叉
            if (vs1 != null)
            {
                //打印结果
                MapOperate.SystemMsg.WriteLine("=============");
                MapOperate.SystemMsg.WriteLine(String.Join("-", vs1.ToArray()));
                MapOperate.SystemMsg.WriteLine("=============");

                List<string> vs2 = null;
                //判断第一次通过状态机找到的是不是右上【如果不是右上那就是右下，接下来的搜索右下就不需要进行了】
                if (vs1.First().StartsWith("右上"))
                {
                    vs2 = ProcessString(index, rfid.RightPoint, ProcessState.RightDown, range);
                    //找到下分叉
                    if (vs2 != null)
                    {
                        //打印结果
                        MapOperate.SystemMsg.WriteLine("=============");
                        MapOperate.SystemMsg.WriteLine(String.Join("-", vs2.ToArray()));
                        MapOperate.SystemMsg.WriteLine("=============");
                    }
                }
                /*-----------------------------------------------------------------------
                 * 避开第一个和第二个分叉【如果没有第二个就不用处理】继续找
                 * ---------------------------------------------------------------------*/
                //获取第一个分叉的索引
                int idx1 = int.Parse(vs1.First().Substring(2, vs1.First().Length - 2));
                //获取x坐标【取起点是为了避免再次搜索到该分叉】
                double x1 = MapElement.MapObject.ForkLines[idx1].StartPoint.X;
                double x2 = double.MinValue;
                //获取第二个分叉的索引
                if (vs2 != null)
                {
                    int idx2 = int.Parse(vs2.First().Substring(2, vs2.First().Length - 2));
                    x2 = MapElement.MapObject.ForkLines[idx2].StartPoint.X;
                }
                //获取y坐标【y坐标相同，取任意一个】
                double y = MapElement.MapObject.ForkLines[idx1].EndPoint.Y;
                //从比较靠右的分叉开始继续向右找分叉【针对三叉路口】
                Point pt = new Point(x1 > x2 ? x1 : x2, y);
                List<string> vs3 = ProcessString(index, pt, ProcessState.RightUp, range);
                if (vs3 != null)
                {
                    //打印结果
                    MapOperate.SystemMsg.WriteLine("=============");
                    MapOperate.SystemMsg.WriteLine(String.Join("-", vs3.ToArray()));
                    MapOperate.SystemMsg.WriteLine("=============");
                }
                //判断后两步有没有找到，如果没有找到，再根据第一步搜索一遍
                //【系统采用先左后右，先上后下的原则，所有拐弯过多会出现搜索不到】
                if (vs2 == null && vs3 == null && vs1.Count > 2)
                {
                    FindSecond(index, vs1);
                }
            }
            #endregion

            #region 向上搜索
            /*-----------向上搜索【标签】----------------------*/
            id = Base.FindRFID.Up(index, rfid.UpPoint, new Base.Range());
            range = new Base.Range();
            //搜到标签
            if (id != -1)
            {
                //设置选中
                MapFunction.SetRFIDIsSelected(id);
                MapOperate.SystemMsg.WriteLine("向上找到【{0}】号标签!", MapElement.MapObject.RFIDS[id].Num);
                //设置搜索的y终点坐标【Y的最小值为找的的标签的下侧中心坐标】
                range.MinY = MapElement.MapObject.RFIDS[id].DownPoint.Y;
            }
            //向上找左分叉
            vs1 = ProcessString(index, rfid.UpPoint, ProcessState.UpLeft, range);
            //找到了继续向上找右分叉
            if (vs1 != null)
            {
                //打印结果
                MapOperate.SystemMsg.WriteLine("=============");
                MapOperate.SystemMsg.WriteLine(String.Join("-", vs1.ToArray()));
                MapOperate.SystemMsg.WriteLine("=============");

                List<string> vs2 = null;
                //判断第一次通过状态机找到的是不是上左【如果不是上左那就是上右，接下来的搜索上右就不需要进行了】
                if (vs1.First().StartsWith("上左"))
                {
                    vs2 = ProcessString(index, rfid.UpPoint, ProcessState.UpRight, range);
                    //找到分叉
                    if (vs2 != null)
                    {
                        //打印结果
                        MapOperate.SystemMsg.WriteLine("=============");
                        MapOperate.SystemMsg.WriteLine(String.Join("-", vs2.ToArray()));
                        MapOperate.SystemMsg.WriteLine("=============");
                    }
                }
                /*-----------------------------------------------------------------------
                 * 避开第一个和第二个分叉【如果没有第二个就不用处理】继续找
                 * ---------------------------------------------------------------------*/
                //获取第一个分叉的索引
                int idx1 = int.Parse(vs1.First().Substring(2, vs1.First().Length - 2));
                //获取Y坐标【取终点是为了避免再次搜索到该分叉】
                double y1 = MapElement.MapObject.ForkLines[idx1].EndPoint.Y;
                double y2 = double.MaxValue;
                //获取第二个分叉的索引
                if (vs2 != null)
                {
                    int idx2 = int.Parse(vs2.First().Substring(2, vs2.First().Length - 2));
                    y2 = MapElement.MapObject.ForkLines[idx2].EndPoint.Y;
                }
                //获取x坐标【x坐标相同，取任意一个】
                double x = MapElement.MapObject.ForkLines[idx1].StartPoint.X;
                //从比较靠上的分叉开始继续向上找分叉【针对三叉路口】
                Point pt = new Point(x, y1 < y2 ? y1 : y2);
                List<string> vs3 = ProcessString(index, pt, ProcessState.UpLeft, range);
                if (vs3 != null)
                {
                    //打印结果
                    MapOperate.SystemMsg.WriteLine("=============");
                    MapOperate.SystemMsg.WriteLine(String.Join("-", vs3.ToArray()));
                    MapOperate.SystemMsg.WriteLine("=============");
                }
                //判断后两步有没有找到，如果没有找到，再根据第一步搜索一遍
                //【系统采用先左后右，先上后下的原则，所有拐弯过多会出现搜索不到】
                if (vs2 == null && vs3 == null && vs1.Count > 2)
                {
                    FindSecond(index, vs1);
                }
            }
            #endregion

            #region 向下搜索
            /*-----------向下搜索【标签】----------------------*/
            id = Base.FindRFID.Down(index, rfid.DownPoint, new Base.Range());
            range = new Base.Range();
            //搜到标签
            if (id != -1)
            {
                //设置选中
                MapFunction.SetRFIDIsSelected(id);
                MapOperate.SystemMsg.WriteLine("向下找到【{0}】号标签!", MapElement.MapObject.RFIDS[id].Num);
                //设置搜索的y终点坐标【Y的最大值为找的的标签的上侧中心坐标】
                range.MaxY = MapElement.MapObject.RFIDS[id].UpPoint.Y;
            }
            //向下找左分叉
            vs1 = ProcessString(index, rfid.DownPoint, ProcessState.DownLeft, range);
            //找到了继续向下找右分叉
            if (vs1 != null)
            {
                //打印结果
                MapOperate.SystemMsg.WriteLine("=============");
                MapOperate.SystemMsg.WriteLine(String.Join("-", vs1.ToArray()));
                MapOperate.SystemMsg.WriteLine("=============");

                List<string> vs2 = null;
                //判断第一次通过状态机找到的是不是下左【如果不是下左那就是下右，接下来的搜索下右就不需要进行了】
                if (vs1.First().StartsWith("下左"))
                {
                    vs2 = ProcessString(index, rfid.DownPoint, ProcessState.DownRight, range);
                    //找到分叉
                    if (vs2 != null)
                    {
                        //打印结果
                        MapOperate.SystemMsg.WriteLine("=============");
                        MapOperate.SystemMsg.WriteLine(String.Join("-", vs2.ToArray()));
                        MapOperate.SystemMsg.WriteLine("=============");
                    }
                }
                /*-----------------------------------------------------------------------
                 * 避开第一个和第二个分叉【如果没有第二个就不用处理】继续找
                 * ---------------------------------------------------------------------*/
                //获取第一个分叉的索引
                int idx1 = int.Parse(vs1.First().Substring(2, vs1.First().Length - 2));
                //获取Y坐标【取终点是为了避免再次搜索到该分叉】
                double y1 = MapElement.MapObject.ForkLines[idx1].EndPoint.Y;
                double y2 = double.MinValue;
                //获取第二个分叉的索引
                if (vs2 != null)
                {
                    int idx2 = int.Parse(vs2.First().Substring(2, vs2.First().Length - 2));
                    y2 = MapElement.MapObject.ForkLines[idx2].EndPoint.Y;
                }
                //获取x坐标【x坐标相同，取任意一个】
                double x = MapElement.MapObject.ForkLines[idx1].StartPoint.X;
                //从比较靠上的分叉开始继续向上找分叉【针对三叉路口】
                Point pt = new Point(x, y1 > y2 ? y1 : y2);
                List<string> vs3 = ProcessString(index, pt, ProcessState.DownLeft, range);
                if (vs3 != null)
                {
                    //打印结果
                    MapOperate.SystemMsg.WriteLine("=============");
                    MapOperate.SystemMsg.WriteLine(String.Join("-", vs3.ToArray()));
                    MapOperate.SystemMsg.WriteLine("=============");
                }
                //判断后两步有没有找到，如果没有找到，再根据第一步搜索一遍
                //【系统采用先左后右，先上后下的原则，所有拐弯过多会出现搜索不到】
                if (vs2 == null && vs3 == null && vs1.Count > 2)
                {
                    FindSecond(index, vs1);
                }
            }
            #endregion

            MapOperate.SystemMsg.WriteLine("------------------------");
        }

    }
}
