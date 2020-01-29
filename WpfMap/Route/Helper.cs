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
                                    //找到，且起点在终点的左上
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
                                    //找到，且起点在终点的左下
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
                                    //找到，且终点在起点的上左
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
                                    //找到，且终点在起点的上左
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
                                    //找到，且终点在起点的上左
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
                                    //找到，且终点在起点的上左
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
            }
        }
        /// <summary>
        /// 流程图的状态位置
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
        public static bool Process(int index, Point point, ProcessState state, Base.Range range)
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
            //搜到标签
            if (id != -1)
            {
                //设置选中
                MapFunction.SetRFIDIsSelected(id);
                MapOperate.SystemMsg.WriteLine("向左直行->到达【{0}】号标签!", MapElement.MapObject.RFIDS[id].Num);
                //找上分叉
                id = Base.FindForkLine.LeftUp(rfid.LeftPoint, MapElement.MapObject.RFIDS[id].RightPoint.X);
                //找到上分叉
                if (id != -1)
                {
                    //设置选中
                    MapFunction.SetForkLineIsSelected(id);
                    MapOperate.SystemMsg.WriteLine("向左找到【{0}】号【上分叉】!", MapElement.MapObject.ForkLines[id].Num);
                    //沿分叉再向上找标签
                    id = Base.FindRFID.Up(index, MapElement.MapObject.ForkLines[id].StartPoint, new Base.Range());
                    //搜到标签
                    if (id != -1)
                    {
                        //设置选中
                        MapFunction.SetRFIDIsSelected(id);
                        MapOperate.SystemMsg.WriteLine("向上直行->到达【{0}】号标签!", MapElement.MapObject.RFIDS[id].Num);
                    }
                }
            }
            else
            //未搜到标签
            {

            }

            #endregion


            /*-----------向右搜索【标签】----------------------*/
            id = Base.FindRFID.Right(index, rfid.RightPoint, new Base.Range());
            if (id != -1)
            {
                //设置选中
                MapFunction.SetRFIDIsSelected(id);
                MapOperate.SystemMsg.WriteLine("向右找到【{0}】号标签!", MapElement.MapObject.RFIDS[id].Num);
            }
            /*-----------向上搜索【标签】----------------------*/
            id = Base.FindRFID.Up(index, rfid.UpPoint, new Base.Range());
            if (id != -1)
            {
                //设置选中
                MapFunction.SetRFIDIsSelected(id);
                MapOperate.SystemMsg.WriteLine("向上找到【{0}】号标签!", MapElement.MapObject.RFIDS[id].Num);
            }
            /*-----------向下搜索【标签】----------------------*/
            id = Base.FindRFID.Down(index, rfid.DownPoint, new Base.Range());
            if (id != -1)
            {
                //设置选中
                MapFunction.SetRFIDIsSelected(id);
                MapOperate.SystemMsg.WriteLine("向下找到【{0}】号标签!", MapElement.MapObject.RFIDS[id].Num);
            }

            ///*-----------向【左】搜索【上分叉】----------------------*/
            //id = Base.FindForkLine.LeftUp(pointLeft);
            ////找到分叉，继续找标签
            //if (id != -1)
            //{
            //    //设置选中
            //    MapFunction.SetForkLineIsSelected(id);
            //    MapOperate.SystemMsg.WriteLine("向左找到【{0}】号【上分叉】!", MapElement.MapObject.ForkLines[id].Num);
            //    //沿分叉再找标签
            //}
            ///*-----------向【左】搜索【下分叉】----------------------*/
            //id = Base.FindForkLine.LeftDown(pointLeft);
            ////找到分叉，继续找标签
            //if (id != -1)
            //{
            //    //设置选中
            //    MapFunction.SetForkLineIsSelected(id);
            //    MapOperate.SystemMsg.WriteLine("向左找到【{0}】号【下分叉】!", MapElement.MapObject.ForkLines[id].Num);
            //    //沿分叉再找标签
            //}
            ///*-----------向【右】搜索【上分叉】----------------------*/
            //id = Base.FindForkLine.RightUp(pointRight);
            ////找到分叉，继续找标签
            //if (id != -1)
            //{
            //    //设置选中
            //    MapFunction.SetForkLineIsSelected(id);
            //    MapOperate.SystemMsg.WriteLine("向右找到【{0}】号【上分叉】!", MapElement.MapObject.ForkLines[id].Num);
            //    //沿分叉再找标签
            //}
            ///*-----------向【右】搜索【下分叉】----------------------*/
            //id = Base.FindForkLine.RightDown(pointRight);
            ////找到分叉，继续找标签
            //if (id != -1)
            //{
            //    //设置选中
            //    MapFunction.SetForkLineIsSelected(id);
            //    MapOperate.SystemMsg.WriteLine("向右找到【{0}】号【下分叉】!", MapElement.MapObject.ForkLines[id].Num);
            //    //沿分叉再找标签
            //}


            ///*-----------向【上】搜索【左分叉】----------------------*/
            //id = Base.FindForkLine.UpLeft(pointUp);
            ////找到分叉，继续找标签
            //if (id != -1)
            //{
            //    //设置选中
            //    MapFunction.SetForkLineIsSelected(id);
            //    MapOperate.SystemMsg.WriteLine("向上找到【{0}】号【左分叉】!", MapElement.MapObject.ForkLines[id].Num);
            //    //沿分叉再找标签
            //}
            ///*-----------向【上】搜索【右分叉】----------------------*/
            //id = Base.FindForkLine.UpRight(pointUp);
            ////找到分叉，继续找标签
            //if (id != -1)
            //{
            //    //设置选中
            //    MapFunction.SetForkLineIsSelected(id);
            //    MapOperate.SystemMsg.WriteLine("向上找到【{0}】号【右分叉】!", MapElement.MapObject.ForkLines[id].Num);
            //    //沿分叉再找标签
            //}
            ///*-----------向【下】搜索【左分叉】----------------------*/
            //id = Base.FindForkLine.DownLeft(pointDown);
            ////找到分叉，继续找标签
            //if (id != -1)
            //{
            //    //设置选中
            //    MapFunction.SetForkLineIsSelected(id);
            //    MapOperate.SystemMsg.WriteLine("向下找到【{0}】号【左分叉】!", MapElement.MapObject.ForkLines[id].Num);
            //    //沿分叉再找标签
            //}
            ///*-----------向【下】搜索【右分叉】----------------------*/
            //id = Base.FindForkLine.DownRight(pointDown);
            ////找到分叉，继续找标签
            //if (id != -1)
            //{
            //    //设置选中
            //    MapFunction.SetForkLineIsSelected(id);
            //    MapOperate.SystemMsg.WriteLine("向下找到【{0}】号【右分叉】!", MapElement.MapObject.ForkLines[id].Num);
            //    //沿分叉再找标签
            //}


            MapOperate.SystemMsg.WriteLine("------------------------");
        }

    }
}
