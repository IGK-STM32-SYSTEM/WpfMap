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
            //是否打印调试信息
            bool debugPrint = false;
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
                if (debugPrint)
                {
                    MapOperate.SystemMsg.WriteLine("向上找到【{0}】号【左分叉】!", MapElement.MapObject.ForkLines[id].Num);
                }
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
                if (debugPrint)
                {
                    MapOperate.SystemMsg.WriteLine("向上找到【{0}】号【右分叉】!", MapElement.MapObject.ForkLines[id].Num);
                }
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
                if (debugPrint)
                {
                    MapOperate.SystemMsg.WriteLine("向下找到【{0}】号【左分叉】!", MapElement.MapObject.ForkLines[id].Num);
                }
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
                if (debugPrint)
                {
                    MapOperate.SystemMsg.WriteLine("向下找到【{0}】号【右分叉】!", MapElement.MapObject.ForkLines[id].Num);
                }
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
                if (debugPrint)
                {
                    MapOperate.SystemMsg.WriteLine("向左找到【{0}】号【上分叉】!", MapElement.MapObject.ForkLines[id].Num);
                }
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
                if (debugPrint)
                {
                    MapOperate.SystemMsg.WriteLine("向左找到【{0}】号【下分叉】!", MapElement.MapObject.ForkLines[id].Num);
                }
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
                if (debugPrint)
                {
                    MapOperate.SystemMsg.WriteLine("向右找到【{0}】号【上分叉】!", MapElement.MapObject.ForkLines[id].Num);
                }
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
                if (debugPrint)
                {
                    MapOperate.SystemMsg.WriteLine("向右找到【{0}】号【下分叉】!", MapElement.MapObject.ForkLines[id].Num);
                }
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
                if (debugPrint)
                {
                    MapOperate.SystemMsg.WriteLine("向上找到【{0}】号【标签】!", MapElement.MapObject.RFIDS[id].Num);
                }
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
                if (debugPrint)
                {
                    MapOperate.SystemMsg.WriteLine("向下找到【{0}】号【标签】!", MapElement.MapObject.RFIDS[id].Num);
                }
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
                if (debugPrint)
                {
                    MapOperate.SystemMsg.WriteLine("向左找到【{0}】号【标签】!", MapElement.MapObject.RFIDS[id].Num);
                }
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
                if (debugPrint)
                {
                    MapOperate.SystemMsg.WriteLine("向右找到【{0}】号【标签】!", MapElement.MapObject.RFIDS[id].Num);
                }
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
        public static void FindSecond(int index, List<string> vs1, ref List<string> vs2, ref List<string> vs3, Base.Range range)
        {
            vs2 = null;
            vs3 = null;
            Point pt = new Point();
            for (int i = 0; i < vs1.Count - 1; i++)
            {
                //获取分叉的起点和终点坐标
                int ids = int.Parse(vs1[i].Substring(2, vs1[i].Length - 2));
                Point startPt = MapElement.MapObject.ForkLines[ids].StartPoint;
                Point endPt = MapElement.MapObject.ForkLines[ids].EndPoint;
                switch (vs1[i].Substring(0, 2))
                {
                    case "左上":
                        {
                            /*--------按照当前方向搜索--------*/
                            //1.避开这个继续搜左上
                            pt = new Point(startPt.X, endPt.Y);
                            List<string> v1 = ProcessString(index, pt, ProcessState.LeftUp, range);
                            //将前面的部分补齐
                            if (v1 != null)
                            {
                                v1.InsertRange(0, vs1.GetRange(0, i + 1));
                                v1.RemoveAt(i);
                            }
                            //2.从终点开始搜左下
                            List<string> v2 = ProcessString(index, endPt, ProcessState.LeftDown, range);
                            if (v2 != null)
                            {
                                v2.InsertRange(0, vs1.GetRange(0, i + 1));
                                v2.RemoveAt(i);
                            }
                            /*--------改变方向搜索--------*/
                            //3.向上找标签
                            int id = Base.FindRFID.Up(index, startPt, new Base.Range());
                            //更新搜索范围
                            range = new Base.Range();
                            if (id != -1)
                                range.MinY = MapElement.MapObject.RFIDS[id].DownPoint.Y;
                            //从起点开始搜上左
                            List<string> v3 = ProcessString(index, startPt, ProcessState.UpLeft, range);
                            //将前面的部分补齐
                            if (v3 != null)
                                v3.InsertRange(0, vs1.GetRange(0, i + 1));
                            //4.从起点开始搜上右
                            List<string> v4 = ProcessString(index, startPt, ProcessState.UpRight, range);
                            if (v4 != null)
                                v4.InsertRange(0, vs1.GetRange(0, i + 1));
                            /*--------整理搜索结果--------*/
                            if (v1 != null && ListLastEquls(vs1, v1) == false)
                            {
                                vs2 = v1;
                                if (v2 != null && ListLastEquls(vs1, v2) == false && ListLastEquls(vs2, v2) == false)
                                    vs3 = v2;
                                else
                                if (v3 != null && ListLastEquls(vs1, v3) == false && ListLastEquls(vs2, v3) == false)
                                    vs3 = v3;
                                else
                                if (v4 != null && ListLastEquls(vs1, v4) == false && ListLastEquls(vs2, v4) == false)
                                    vs3 = v4;
                            }
                            else
                            if (v2 != null && ListLastEquls(vs1, v2) == false)
                            {
                                vs2 = v2;
                                if (v3 != null && ListLastEquls(vs1, v3) == false && ListLastEquls(vs2, v3) == false)
                                    vs3 = v3;
                                else
                                if (v4 != null && ListLastEquls(vs1, v4) == false && ListLastEquls(vs2, v4) == false)
                                    vs3 = v4;
                            }
                            else
                            if (v3 != null && ListLastEquls(vs1, v3) == false)
                            {
                                vs2 = v3;
                                if (v4 != null && ListLastEquls(vs1, v4) == false && ListLastEquls(vs2, v4) == false)
                                    vs3 = v4;
                            }
                            else
                            if (v4 != null && ListLastEquls(vs1, v4) == false)
                                vs2 = v4;
                            //有一个分叉就不再继续搜了
                            if (vs2 != null && ListLastEquls(vs1, vs2) == false)
                                return;
                            else
                                break;
                        }
                    case "左下":
                        {
                            /*--------按照当前方向搜索--------*/
                            //1.避开这个继续搜左下
                            pt = new Point(startPt.X, endPt.Y);
                            List<string> v1 = ProcessString(index, pt, ProcessState.LeftUp, range);
                            //将前面的部分补齐
                            if (v1 != null)
                            {
                                v1.InsertRange(0, vs1.GetRange(0, i + 1));
                                v1.RemoveAt(i);
                            }
                            /*--------改变方向搜索--------*/
                            //3. 向下找标签
                            int id = Base.FindRFID.Down(index, startPt, new Base.Range());
                            //更新搜索范围
                            range = new Base.Range();
                            if (id != -1)
                                range.MaxY = MapElement.MapObject.RFIDS[id].UpPoint.Y;
                            //从起点开始搜下左
                            List<string> v3 = ProcessString(index, startPt, ProcessState.DownLeft, range);
                            //将前面的部分补齐
                            if (v3 != null)
                                v3.InsertRange(0, vs1.GetRange(0, i + 1));
                            //4.从起点开始搜下右
                            List<string> v4 = ProcessString(index, startPt, ProcessState.DownRight, range);
                            if (v4 != null)
                                v4.InsertRange(0, vs1.GetRange(0, i + 1));
                            /*--------整理搜索结果--------*/
                            if (v1 != null && ListLastEquls(vs1, v1) == false)
                            {
                                vs2 = v1;
                                if (v3 != null && ListLastEquls(vs1, v3) == false && ListLastEquls(vs2, v3) == false)
                                    vs3 = v3;
                                else
                                if (v4 != null && ListLastEquls(vs1, v4) == false && ListLastEquls(vs2, v4) == false)
                                    vs3 = v4;
                            }
                            else
                            if (v3 != null && ListLastEquls(vs1, v3) == false)
                            {
                                vs2 = v3;
                                if (v4 != null && ListLastEquls(vs1, v4) == false && ListLastEquls(vs2, v4) == false)
                                    vs3 = v4;
                            }
                            else
                            if (v4 != null && ListLastEquls(vs1, v4) == false)
                                vs2 = v4;
                            //有一个分叉就不再继续搜了
                            if (vs2 != null && ListLastEquls(vs1, vs2) == false)
                                return;
                            else
                                break;
                        }
                    case "右上":
                        {
                            /*--------按照当前方向搜索--------*/
                            //1.避开这个继续搜右上
                            pt = new Point(startPt.X, endPt.Y);
                            List<string> v1 = ProcessString(index, pt, ProcessState.RightUp, range);
                            //将前面的部分补齐
                            if (v1 != null)
                            {
                                v1.InsertRange(0, vs1.GetRange(0, i + 1));
                                v1.RemoveAt(i);
                            }
                            //2.从终点开始搜右下
                            List<string> v2 = ProcessString(index, endPt, ProcessState.RightDown, range);
                            if (v2 != null)
                            {
                                v2.InsertRange(0, vs1.GetRange(0, i + 1));
                                v2.RemoveAt(i);
                            }
                            /*--------改变方向搜索--------*/
                            //3.向上找标签
                            int id = Base.FindRFID.Up(index, startPt, new Base.Range());
                            //更新搜索范围
                            range = new Base.Range();
                            if (id != -1)
                                range.MinY = MapElement.MapObject.RFIDS[id].DownPoint.Y;
                            //从起点开始搜上左
                            List<string> v3 = ProcessString(index, startPt, ProcessState.UpLeft, range);
                            //将前面的部分补齐
                            if (v3 != null)
                                v3.InsertRange(0, vs1.GetRange(0, i + 1));
                            //4.从起点开始搜上右
                            List<string> v4 = ProcessString(index, startPt, ProcessState.UpRight, range);
                            if (v4 != null)
                                v4.InsertRange(0, vs1.GetRange(0, i + 1));
                            /*--------整理搜索结果--------*/
                            if (v1 != null && ListLastEquls(vs1, v1) == false)
                            {
                                vs2 = v1;
                                if (v2 != null && ListLastEquls(vs1, v2) == false && ListLastEquls(vs2, v2) == false)
                                    vs3 = v2;
                                else
                                if (v3 != null && ListLastEquls(vs1, v3) == false && ListLastEquls(vs2, v3) == false)
                                    vs3 = v3;
                                else
                                if (v4 != null && ListLastEquls(vs1, v4) == false && ListLastEquls(vs2, v4) == false)
                                    vs3 = v4;
                            }
                            else
                            if (v2 != null && ListLastEquls(vs1, v2) == false)
                            {
                                vs2 = v2;
                                if (v3 != null && ListLastEquls(vs1, v3) == false && ListLastEquls(vs2, v3) == false)
                                    vs3 = v3;
                                else
                                if (v4 != null && ListLastEquls(vs1, v4) == false && ListLastEquls(vs2, v4) == false)
                                    vs3 = v4;
                            }
                            else
                            if (v3 != null && ListLastEquls(vs1, v3) == false)
                            {
                                vs2 = v3;
                                if (v4 != null && ListLastEquls(vs1, v4) == false && ListLastEquls(vs2, v4) == false)
                                    vs3 = v4;
                            }
                            else
                            if (v4 != null && ListLastEquls(vs1, v4) == false)
                                vs2 = v4;
                            //有一个分叉就不再继续搜了
                            if (vs2 != null && ListLastEquls(vs1, vs2) == false)
                                return;
                            else
                                break;
                        }
                    case "右下":
                        {
                            /*--------按照当前方向搜索--------*/
                            //1.避开这个继续搜右下
                            pt = new Point(startPt.X, endPt.Y);
                            List<string> v1 = ProcessString(index, pt, ProcessState.RightUp, range);
                            //将前面的部分补齐
                            if (v1 != null)
                            {
                                v1.InsertRange(0, vs1.GetRange(0, i + 1));
                                v1.RemoveAt(i);
                            }
                            /*--------改变方向搜索--------*/
                            //3. 向下找标签
                            int id = Base.FindRFID.Down(index, startPt, new Base.Range());
                            //更新搜索范围
                            range = new Base.Range();
                            if (id != -1)
                                range.MaxY = MapElement.MapObject.RFIDS[id].UpPoint.Y;
                            //从起点开始搜下左
                            List<string> v3 = ProcessString(index, startPt, ProcessState.DownLeft, range);
                            //将前面的部分补齐
                            if (v3 != null)
                                v3.InsertRange(0, vs1.GetRange(0, i + 1));
                            //4.从起点开始搜下右
                            List<string> v4 = ProcessString(index, startPt, ProcessState.DownRight, range);
                            if (v4 != null)
                                v4.InsertRange(0, vs1.GetRange(0, i + 1));
                            /*--------整理搜索结果--------*/
                            if (v1 != null && ListLastEquls(vs1, v1) == false)
                            {
                                vs2 = v1;
                                if (v3 != null && ListLastEquls(vs1, v3) == false && ListLastEquls(vs2, v3) == false)
                                    vs3 = v3;
                                else
                                if (v4 != null && ListLastEquls(vs1, v4) == false && ListLastEquls(vs2, v4) == false)
                                    vs3 = v4;
                            }
                            else
                            if (v3 != null && ListLastEquls(vs1, v3) == false)
                            {
                                vs2 = v3;
                                if (v4 != null && ListLastEquls(vs1, v4) == false && ListLastEquls(vs2, v4) == false)
                                    vs3 = v4;
                            }
                            else
                            if (v4 != null && ListLastEquls(vs1, v4) == false)
                                vs2 = v4;
                            //有一个分叉就不再继续搜了
                            if (vs2 != null && ListLastEquls(vs1, vs2) == false)
                                return;
                            else
                                break;
                        }
                    case "上左":
                        {
                            /*--------按照当前方向搜索--------*/
                            //1.避开这个上左继续搜上左
                            pt = new Point(startPt.X, endPt.Y);
                            List<string> v1 = ProcessString(index, pt, ProcessState.UpLeft, range);
                            //将前面的部分补齐
                            if (v1 != null)
                            {
                                v1.InsertRange(0, vs1.GetRange(0, i + 1));
                                v1.RemoveAt(i);
                            }
                            //2.从起点开始搜上右
                            List<string> v2 = ProcessString(index, startPt, ProcessState.UpRight, range);
                            if (v2 != null)
                            {
                                v2.InsertRange(0, vs1.GetRange(0, i + 1));
                                v2.RemoveAt(i);
                            }
                            /*--------改变方向搜索--------*/
                            //3.向左找标签，从终点开始搜左上
                            int id = Base.FindRFID.Left(index, endPt, new Base.Range());
                            //更新搜索范围
                            range = new Base.Range();
                            if (id != -1)
                                range.MinX = MapElement.MapObject.RFIDS[id].RightPoint.X;
                            List<string> v3 = ProcessString(index, endPt, ProcessState.LeftUp, range);
                            //将前面的部分补齐
                            if (v3 != null)
                                v3.InsertRange(0, vs1.GetRange(0, i + 1));
                            //4.从终点开始搜左下
                            List<string> v4 = ProcessString(index, endPt, ProcessState.LeftDown, range);
                            if (v4 != null)
                                v4.InsertRange(0, vs1.GetRange(0, i + 1));
                            /*--------改变方向搜索--------*/
                            //整理搜索结果
                            if (v1 != null && ListLastEquls(vs1, v1) == false)
                            {
                                vs2 = v1;
                                if (v2 != null && ListLastEquls(vs1, v2) == false && ListLastEquls(vs2, v2) == false)
                                    vs3 = v2;
                                else
                                if (v3 != null && ListLastEquls(vs1, v3) == false && ListLastEquls(vs2, v3) == false)
                                    vs3 = v3;
                                else
                                if (v4 != null && ListLastEquls(vs1, v4) == false && ListLastEquls(vs2, v4) == false)
                                    vs3 = v4;
                            }
                            else
                            if (v2 != null && ListLastEquls(vs1, v2) == false)
                            {
                                vs2 = v2;
                                if (v3 != null && ListLastEquls(vs1, v3) == false && ListLastEquls(vs2, v3) == false)
                                    vs3 = v3;
                                else
                                if (v4 != null && ListLastEquls(vs1, v4) == false && ListLastEquls(vs2, v4) == false)
                                    vs3 = v4;
                            }
                            else
                            if (v3 != null && ListLastEquls(vs1, v3) == false)
                            {
                                vs2 = v3;
                                if (v4 != null && ListLastEquls(vs1, v4) == false && ListLastEquls(vs2, v4) == false)
                                    vs3 = v4;
                            }
                            else
                            if (v4 != null && ListLastEquls(vs1, v4) == false)
                                vs2 = v4;
                            //有一个分叉就不再继续搜了
                            if (vs2 != null && ListLastEquls(vs1, vs2) == false)
                                return;
                            else
                                break;
                        }
                    case "下左":
                        {
                            /*--------按照当前方向搜索--------*/
                            //1.避开这个上左继续搜下左
                            pt = new Point(startPt.X, endPt.Y);
                            List<string> v1 = ProcessString(index, pt, ProcessState.DownLeft, range);
                            //将前面的部分补齐
                            if (v1 != null)
                            {
                                v1.InsertRange(0, vs1.GetRange(0, i + 1));
                                v1.RemoveAt(i);
                            }
                            //2.从起点开始搜下右
                            List<string> v2 = ProcessString(index, startPt, ProcessState.DownRight, range);
                            if (v2 != null)
                            {
                                v2.InsertRange(0, vs1.GetRange(0, i + 1));
                                v2.RemoveAt(i);
                            }
                            /*--------改变方向搜索--------*/
                            //3.向左找标签，从终点开始搜左上
                            int id = Base.FindRFID.Left(index, endPt, new Base.Range());
                            //更新搜索范围
                            range = new Base.Range();
                            if (id != -1)
                                range.MinX = MapElement.MapObject.RFIDS[id].RightPoint.X;
                            List<string> v3 = ProcessString(index, endPt, ProcessState.LeftUp, range);
                            //将前面的部分补齐
                            if (v3 != null)
                                v3.InsertRange(0, vs1.GetRange(0, i + 1));
                            //4.从终点开始搜左下
                            List<string> v4 = ProcessString(index, endPt, ProcessState.LeftDown, range);
                            if (v4 != null)
                                v4.InsertRange(0, vs1.GetRange(0, i + 1));
                            /*--------改变方向搜索--------*/
                            //整理搜索结果
                            if (v1 != null && ListLastEquls(vs1, v1) == false)
                            {
                                vs2 = v1;
                                if (v2 != null && ListLastEquls(vs1, v2) == false && ListLastEquls(vs2, v2) == false)
                                    vs3 = v2;
                                else
                                if (v3 != null && ListLastEquls(vs1, v3) == false && ListLastEquls(vs2, v3) == false)
                                    vs3 = v3;
                                else
                                if (v4 != null && ListLastEquls(vs1, v4) == false && ListLastEquls(vs2, v4) == false)
                                    vs3 = v4;
                            }
                            else
                            if (v2 != null && ListLastEquls(vs1, v2) == false)
                            {
                                vs2 = v2;
                                if (v3 != null && ListLastEquls(vs1, v3) == false && ListLastEquls(vs2, v3) == false)
                                    vs3 = v3;
                                else
                                if (v4 != null && ListLastEquls(vs1, v4) == false && ListLastEquls(vs2, v4) == false)
                                    vs3 = v4;
                            }
                            else
                            if (v3 != null && ListLastEquls(vs1, v3) == false)
                            {
                                vs2 = v3;
                                if (v4 != null && ListLastEquls(vs1, v4) == false && ListLastEquls(vs2, v4) == false)
                                    vs3 = v4;
                            }
                            else
                            if (v4 != null && ListLastEquls(vs1, v4) == false)
                                vs2 = v4;
                            //有一个分叉就不再继续搜了
                            if (vs2 != null && ListLastEquls(vs1, vs2) == false)
                                return;
                            else
                                break;
                        }
                    case "上右":
                        {
                            /*--------按照当前方向搜索--------*/
                            //1.避开这个上左继续搜上右
                            pt = new Point(startPt.X, endPt.Y);
                            List<string> v1 = ProcessString(index, pt, ProcessState.UpLeft, range);
                            //将前面的部分补齐
                            if (v1 != null)
                            {
                                v1.InsertRange(0, vs1.GetRange(0, i + 1));
                                v1.RemoveAt(i);
                            }
                            /*--------改变方向搜索--------*/
                            //3.向右找标签，从终点开始搜右下
                            int id = Base.FindRFID.Right(index, endPt, new Base.Range());
                            //更新搜索范围
                            range = new Base.Range();
                            if (id != -1)
                                range.MaxX = MapElement.MapObject.RFIDS[id].LeftPoint.X;
                            List<string> v3 = ProcessString(index, endPt, ProcessState.RightUp, range);
                            //将前面的部分补齐
                            if (v3 != null)
                                v3.InsertRange(0, vs1.GetRange(0, i + 1));
                            //4.从终点开始搜左下
                            List<string> v4 = ProcessString(index, endPt, ProcessState.RightDown, range);
                            if (v4 != null)
                                v4.InsertRange(0, vs1.GetRange(0, i + 1));
                            /*--------改变方向搜索--------*/
                            //整理搜索结果
                            if (v1 != null && ListLastEquls(vs1, v1) == false)
                            {
                                vs2 = v1;
                                if (v3 != null && ListLastEquls(vs1, v3) == false && ListLastEquls(vs2, v3) == false)
                                    vs3 = v3;
                                else
                                if (v4 != null && ListLastEquls(vs1, v4) == false && ListLastEquls(vs2, v4) == false)
                                    vs3 = v4;
                            }
                            else
                            if (v3 != null && ListLastEquls(vs1, v3) == false)
                            {
                                vs2 = v3;
                                if (v4 != null && ListLastEquls(vs1, v4) == false && ListLastEquls(vs2, v4) == false)
                                    vs3 = v4;
                            }
                            else
                            if (v4 != null && ListLastEquls(vs1, v4) == false)
                                vs2 = v4;
                            //有一个分叉就不再继续搜了
                            if (vs2 != null && ListLastEquls(vs1, vs2) == false)
                                return;
                            else
                                break;
                        }
                    case "下右":
                        {
                            /*--------按照当前方向搜索--------*/
                            //1.避开这个上左继续搜下右
                            pt = new Point(startPt.X, endPt.Y);
                            List<string> v1 = ProcessString(index, pt, ProcessState.DownLeft, range);
                            //将前面的部分补齐
                            if (v1 != null)
                            {
                                v1.InsertRange(0, vs1.GetRange(0, i + 1));
                                v1.RemoveAt(i);
                            }
                            /*--------改变方向搜索--------*/
                            //3.向右找标签，从终点开始搜右下
                            int id = Base.FindRFID.Right(index, endPt, new Base.Range());
                            //更新搜索范围
                            range = new Base.Range();
                            if (id != -1)
                                range.MaxX = MapElement.MapObject.RFIDS[id].LeftPoint.X;
                            List<string> v3 = ProcessString(index, endPt, ProcessState.RightUp, range);
                            //将前面的部分补齐
                            if (v3 != null)
                                v3.InsertRange(0, vs1.GetRange(0, i + 1));
                            //4.从终点开始搜左下
                            List<string> v4 = ProcessString(index, endPt, ProcessState.RightDown, range);
                            if (v4 != null)
                                v4.InsertRange(0, vs1.GetRange(0, i + 1));
                            /*--------改变方向搜索--------*/
                            //整理搜索结果
                            if (v1 != null && ListLastEquls(vs1, v1) == false)
                            {
                                vs2 = v1;
                                if (v3 != null && ListLastEquls(vs1, v3) == false && ListLastEquls(vs2, v3) == false)
                                    vs3 = v3;
                                else
                                if (v4 != null && ListLastEquls(vs1, v4) == false && ListLastEquls(vs2, v4) == false)
                                    vs3 = v4;
                            }
                            else
                            if (v3 != null && ListLastEquls(vs1, v3) == false)
                            {
                                vs2 = v3;
                                if (v4 != null && ListLastEquls(vs1, v4) == false && ListLastEquls(vs2, v4) == false)
                                    vs3 = v4;
                            }
                            else
                            if (v4 != null && ListLastEquls(vs1, v4) == false)
                                vs2 = v4;
                            //有一个分叉就不再继续搜了
                            if (vs2 != null && ListLastEquls(vs1, vs2) == false)
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
        /// 二次搜索
        /// </summary>
        /// <param name="index"></param>
        /// <param name="vs1"></param>
        public static void FindThird(int index, List<string> vs1, List<string> vs2, ref List<string> vs3)
        {
            vs3 = null;
            Point pt = new Point();
            Base.Range range = new Base.Range();
            for (int i = 0; i < vs2.Count - 1; i++)
            {
                //获取分叉的起点和终点坐标
                int ids = int.Parse(vs2[i].Substring(2, vs2[i].Length - 2));
                Point startPt = MapElement.MapObject.ForkLines[ids].StartPoint;
                Point endPt = MapElement.MapObject.ForkLines[ids].EndPoint;
                switch (vs2[i].Substring(0, 2))
                {
                    case "左上":
                        {
                            /*--------按照当前方向搜索--------*/
                            //1.避开这个继续搜左上
                            pt = new Point(startPt.X, endPt.Y);
                            List<string> v1 = ProcessString(index, pt, ProcessState.LeftUp, range);
                            //将前面的部分补齐
                            if (v1 != null)
                            {
                                v1.InsertRange(0, vs2.GetRange(0, i + 1));
                                v1.RemoveAt(i);
                            }
                            //2.从终点开始搜左下
                            List<string> v2 = ProcessString(index, endPt, ProcessState.LeftDown, range);
                            if (v2 != null)
                            {
                                v2.InsertRange(0, vs2.GetRange(0, i + 1));
                                v2.RemoveAt(i);
                            }
                            /*--------改变方向搜索--------*/
                            //3.向上找标签
                            int id = Base.FindRFID.Up(index, startPt, new Base.Range());
                            //更新搜索范围
                            range = new Base.Range();
                            if (id != -1)
                                range.MinY = MapElement.MapObject.RFIDS[id].DownPoint.Y;
                            //从起点开始搜上左
                            List<string> v3 = ProcessString(index, startPt, ProcessState.UpLeft, range);
                            //将前面的部分补齐
                            if (v3 != null)
                                v3.InsertRange(0, vs2.GetRange(0, i + 1));
                            //4.从起点开始搜上右
                            List<string> v4 = ProcessString(index, startPt, ProcessState.UpRight, range);
                            if (v4 != null)
                                v4.InsertRange(0, vs2.GetRange(0, i + 1));
                            /*--------整理搜索结果--------*/
                            if (v1 != null && ListLastEquls(vs1, v1) == false && ListLastEquls(vs2, v1) == false)
                                vs3 = v1;
                            else
                            if (v2 != null && ListLastEquls(vs1, v2) == false && ListLastEquls(vs2, v2) == false)
                                vs3 = v2;
                            else
                            if (v3 != null && ListLastEquls(vs1, v3) == false && ListLastEquls(vs2, v3) == false)
                                vs3 = v3;
                            else
                            if (v4 != null && ListLastEquls(vs1, v4) == false && ListLastEquls(vs2, v4) == false)
                                vs3 = v4;

                            if (vs3 != null)
                                return;
                            else
                                break;
                        }
                    case "左下":
                        {
                            /*--------按照当前方向搜索--------*/
                            //1.避开这个继续搜左下
                            pt = new Point(startPt.X, endPt.Y);
                            List<string> v1 = ProcessString(index, pt, ProcessState.LeftUp, range);
                            //将前面的部分补齐
                            if (v1 != null)
                            {
                                v1.InsertRange(0, vs2.GetRange(0, i + 1));
                                v1.RemoveAt(i);
                            }
                            /*--------改变方向搜索--------*/
                            //3. 向下找标签
                            int id = Base.FindRFID.Down(index, startPt, new Base.Range());
                            //更新搜索范围
                            range = new Base.Range();
                            if (id != -1)
                                range.MaxY = MapElement.MapObject.RFIDS[id].UpPoint.Y;
                            //从起点开始搜下左
                            List<string> v3 = ProcessString(index, startPt, ProcessState.DownLeft, range);
                            //将前面的部分补齐
                            if (v3 != null)
                                v3.InsertRange(0, vs2.GetRange(0, i + 1));
                            //4.从起点开始搜下右
                            List<string> v4 = ProcessString(index, startPt, ProcessState.DownRight, range);
                            if (v4 != null)
                                v4.InsertRange(0, vs2.GetRange(0, i + 1));
                            /*--------整理搜索结果--------*/
                            if (v1 != null && ListLastEquls(vs1, v1) == false && ListLastEquls(vs2, v1) == false)
                                vs3 = v1;
                            else
                            if (v3 != null && ListLastEquls(vs1, v3) == false && ListLastEquls(vs2, v3) == false)
                                vs3 = v3;
                            else
                            if (v4 != null && ListLastEquls(vs1, v4) == false && ListLastEquls(vs2, v4) == false)
                                vs3 = v4;

                            if (vs3 != null)
                                return;
                            else
                                break;
                        }
                    case "右上":
                        {
                            /*--------按照当前方向搜索--------*/
                            //1.避开这个继续搜右上
                            pt = new Point(startPt.X, endPt.Y);
                            List<string> v1 = ProcessString(index, pt, ProcessState.RightUp, range);
                            //将前面的部分补齐
                            if (v1 != null)
                            {
                                v1.InsertRange(0, vs2.GetRange(0, i + 1));
                                v1.RemoveAt(i);
                            }
                            //2.从终点开始搜右下
                            List<string> v2 = ProcessString(index, endPt, ProcessState.RightDown, range);
                            if (v2 != null)
                            {
                                v2.InsertRange(0, vs2.GetRange(0, i + 1));
                                v2.RemoveAt(i);
                            }
                            /*--------改变方向搜索--------*/
                            //3.向上找标签
                            int id = Base.FindRFID.Up(index, startPt, new Base.Range());
                            //更新搜索范围
                            range = new Base.Range();
                            if (id != -1)
                                range.MinY = MapElement.MapObject.RFIDS[id].DownPoint.Y;
                            //从起点开始搜上左
                            List<string> v3 = ProcessString(index, startPt, ProcessState.UpLeft, range);
                            //将前面的部分补齐
                            if (v3 != null)
                                v3.InsertRange(0, vs2.GetRange(0, i + 1));
                            //4.从起点开始搜上右
                            List<string> v4 = ProcessString(index, startPt, ProcessState.UpRight, range);
                            if (v4 != null)
                                v4.InsertRange(0, vs2.GetRange(0, i + 1));
                            /*--------整理搜索结果--------*/
                            if (v1 != null && ListLastEquls(vs1, v1) == false && ListLastEquls(vs2, v1) == false)
                                vs3 = v1;
                            else
                            if (v2 != null && ListLastEquls(vs1, v2) == false && ListLastEquls(vs2, v2) == false)
                                vs3 = v2;
                            else
                            if (v3 != null && ListLastEquls(vs1, v3) == false && ListLastEquls(vs2, v3) == false)
                                vs3 = v3;
                            else
                            if (v4 != null && ListLastEquls(vs1, v4) == false && ListLastEquls(vs2, v4) == false)
                                vs3 = v4;

                            if (vs3 != null)
                                return;
                            else
                                break;
                        }
                    case "右下":
                        {
                            /*--------按照当前方向搜索--------*/
                            //1.避开这个继续搜右下
                            pt = new Point(startPt.X, endPt.Y);
                            List<string> v1 = ProcessString(index, pt, ProcessState.RightUp, range);
                            //将前面的部分补齐
                            if (v1 != null)
                            {
                                v1.InsertRange(0, vs2.GetRange(0, i + 1));
                                v1.RemoveAt(i);
                            }
                            /*--------改变方向搜索--------*/
                            //3. 向下找标签
                            int id = Base.FindRFID.Down(index, startPt, new Base.Range());
                            //更新搜索范围
                            range = new Base.Range();
                            if (id != -1)
                                range.MaxY = MapElement.MapObject.RFIDS[id].UpPoint.Y;
                            //从起点开始搜下左
                            List<string> v3 = ProcessString(index, startPt, ProcessState.DownLeft, range);
                            //将前面的部分补齐
                            if (v3 != null)
                                v3.InsertRange(0, vs2.GetRange(0, i + 1));
                            //4.从起点开始搜下右
                            List<string> v4 = ProcessString(index, startPt, ProcessState.DownRight, range);
                            if (v4 != null)
                                v4.InsertRange(0, vs2.GetRange(0, i + 1));
                            /*--------整理搜索结果--------*/
                            if (v1 != null && ListLastEquls(vs1, v1) == false && ListLastEquls(vs2, v1) == false)
                                vs3 = v1;
                            else
                            if (v3 != null && ListLastEquls(vs1, v3) == false && ListLastEquls(vs2, v3) == false)
                                vs3 = v3;
                            else
                            if (v4 != null && ListLastEquls(vs1, v4) == false && ListLastEquls(vs2, v4) == false)
                                vs3 = v4;

                            if (vs3 != null)
                                return;
                            else
                                break;
                        }
                    case "上左":
                        {
                            /*--------按照当前方向搜索--------*/
                            //1.避开这个上左继续搜上左
                            pt = new Point(startPt.X, endPt.Y);
                            List<string> v1 = ProcessString(index, pt, ProcessState.UpLeft, range);
                            //将前面的部分补齐
                            if (v1 != null)
                            {
                                v1.InsertRange(0, vs2.GetRange(0, i + 1));
                                v1.RemoveAt(i);
                            }
                            //2.从起点开始搜上右
                            List<string> v2 = ProcessString(index, startPt, ProcessState.UpRight, range);
                            if (v2 != null)
                            {
                                v2.InsertRange(0, vs2.GetRange(0, i + 1));
                                v2.RemoveAt(i);
                            }
                            /*--------改变方向搜索--------*/
                            //3.向左找标签，从终点开始搜左上
                            int id = Base.FindRFID.Left(index, endPt, new Base.Range());
                            //更新搜索范围
                            range = new Base.Range();
                            if (id != -1)
                                range.MinX = MapElement.MapObject.RFIDS[id].RightPoint.X;
                            List<string> v3 = ProcessString(index, endPt, ProcessState.LeftUp, range);
                            //将前面的部分补齐
                            if (v3 != null)
                                v3.InsertRange(0, vs2.GetRange(0, i + 1));
                            //4.从终点开始搜左下
                            List<string> v4 = ProcessString(index, endPt, ProcessState.LeftDown, range);
                            if (v4 != null)
                                v4.InsertRange(0, vs2.GetRange(0, i + 1));
                            /*--------整理搜索结果--------*/
                            if (v1 != null && ListLastEquls(vs1, v1) == false && ListLastEquls(vs2, v1) == false)
                                vs3 = v1;
                            else
                            if (v2 != null && ListLastEquls(vs1, v2) == false && ListLastEquls(vs2, v2) == false)
                                vs3 = v2;
                            else
                            if (v3 != null && ListLastEquls(vs1, v3) == false && ListLastEquls(vs2, v3) == false)
                                vs3 = v3;
                            else
                            if (v4 != null && ListLastEquls(vs1, v4) == false && ListLastEquls(vs2, v4) == false)
                                vs3 = v4;

                            if (vs3 != null)
                                return;
                            else
                                break;
                        }
                    case "下左":
                        {
                            /*--------按照当前方向搜索--------*/
                            //1.避开这个上左继续搜下左
                            pt = new Point(startPt.X, endPt.Y);
                            List<string> v1 = ProcessString(index, pt, ProcessState.DownLeft, range);
                            //将前面的部分补齐
                            if (v1 != null)
                            {
                                v1.InsertRange(0, vs2.GetRange(0, i + 1));
                                v1.RemoveAt(i);
                            }
                            //2.从起点开始搜下右
                            List<string> v2 = ProcessString(index, startPt, ProcessState.DownRight, range);
                            if (v2 != null)
                            {
                                v2.InsertRange(0, vs2.GetRange(0, i + 1));
                                v2.RemoveAt(i);
                            }
                            /*--------改变方向搜索--------*/
                            //3.向左找标签，从终点开始搜左上
                            int id = Base.FindRFID.Left(index, endPt, new Base.Range());
                            //更新搜索范围
                            range = new Base.Range();
                            if (id != -1)
                                range.MinX = MapElement.MapObject.RFIDS[id].RightPoint.X;
                            List<string> v3 = ProcessString(index, endPt, ProcessState.LeftUp, range);
                            //将前面的部分补齐
                            if (v3 != null)
                                v3.InsertRange(0, vs2.GetRange(0, i + 1));
                            //4.从终点开始搜左下
                            List<string> v4 = ProcessString(index, endPt, ProcessState.LeftDown, range);
                            if (v4 != null)
                                v4.InsertRange(0, vs2.GetRange(0, i + 1));
                            /*--------整理搜索结果--------*/
                            if (v1 != null && ListLastEquls(vs1, v1) == false && ListLastEquls(vs2, v1) == false)
                                vs3 = v1;
                            else
                            if (v2 != null && ListLastEquls(vs1, v2) == false && ListLastEquls(vs2, v2) == false)
                                vs3 = v2;
                            else
                            if (v3 != null && ListLastEquls(vs1, v3) == false && ListLastEquls(vs2, v3) == false)
                                vs3 = v3;
                            else
                            if (v4 != null && ListLastEquls(vs1, v4) == false && ListLastEquls(vs2, v4) == false)
                                vs3 = v4;

                            if (vs3 != null)
                                return;
                            else
                                break;
                        }
                    case "上右":
                        {
                            /*--------按照当前方向搜索--------*/
                            //1.避开这个上左继续搜上右
                            pt = new Point(startPt.X, endPt.Y);
                            List<string> v1 = ProcessString(index, pt, ProcessState.UpLeft, range);
                            //将前面的部分补齐
                            if (v1 != null)
                            {
                                v1.InsertRange(0, vs2.GetRange(0, i + 1));
                                v1.RemoveAt(i);
                            }
                            /*--------改变方向搜索--------*/
                            //3.向右找标签，从终点开始搜右下
                            int id = Base.FindRFID.Right(index, endPt, new Base.Range());
                            //更新搜索范围
                            range = new Base.Range();
                            if (id != -1)
                                range.MaxX = MapElement.MapObject.RFIDS[id].LeftPoint.X;
                            List<string> v3 = ProcessString(index, endPt, ProcessState.RightUp, range);
                            //将前面的部分补齐
                            if (v3 != null)
                                v3.InsertRange(0, vs2.GetRange(0, i + 1));
                            //4.从终点开始搜左下
                            List<string> v4 = ProcessString(index, endPt, ProcessState.RightDown, range);
                            if (v4 != null)
                                v4.InsertRange(0, vs2.GetRange(0, i + 1));
                            /*--------整理搜索结果--------*/
                            if (v1 != null && ListLastEquls(vs1, v1) == false && ListLastEquls(vs2, v1) == false)
                                vs3 = v1;
                            else
                            if (v3 != null && ListLastEquls(vs1, v3) == false && ListLastEquls(vs2, v3) == false)
                                vs3 = v3;
                            else
                            if (v4 != null && ListLastEquls(vs1, v4) == false && ListLastEquls(vs2, v4) == false)
                                vs3 = v4;

                            if (vs3 != null)
                                return;
                            else
                                break;
                        }
                    case "下右":
                        {
                            /*--------按照当前方向搜索--------*/
                            //1.避开这个上左继续搜下右
                            pt = new Point(startPt.X, endPt.Y);
                            List<string> v1 = ProcessString(index, pt, ProcessState.DownLeft, range);
                            //将前面的部分补齐
                            if (v1 != null)
                            {
                                v1.InsertRange(0, vs2.GetRange(0, i + 1));
                                v1.RemoveAt(i);
                            }
                            /*--------改变方向搜索--------*/
                            //3.向右找标签，从终点开始搜右下
                            int id = Base.FindRFID.Right(index, endPt, new Base.Range());
                            //更新搜索范围
                            range = new Base.Range();
                            if (id != -1)
                                range.MaxX = MapElement.MapObject.RFIDS[id].LeftPoint.X;
                            List<string> v3 = ProcessString(index, endPt, ProcessState.RightUp, range);
                            //将前面的部分补齐
                            if (v3 != null)
                                v3.InsertRange(0, vs2.GetRange(0, i + 1));
                            //4.从终点开始搜左下
                            List<string> v4 = ProcessString(index, endPt, ProcessState.RightDown, range);
                            if (v4 != null)
                                v4.InsertRange(0, vs2.GetRange(0, i + 1));
                            /*--------整理搜索结果--------*/
                            if (v1 != null && ListLastEquls(vs1, v1) == false && ListLastEquls(vs2, v1) == false)
                                vs3 = v1;
                            else
                            if (v3 != null && ListLastEquls(vs1, v3) == false && ListLastEquls(vs2, v3) == false)
                                vs3 = v3;
                            else
                            if (v4 != null && ListLastEquls(vs1, v4) == false && ListLastEquls(vs2, v4) == false)
                                vs3 = v4;

                            if (vs3 != null)
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
        /// 判断列表的最后一项是否相等
        /// </summary>
        /// <param name="vs1"></param>
        /// <param name="vs2"></param>
        /// <returns></returns>
        public static bool ListLastEquls(List<string> vs1, List<string> vs2)
        {
            if (vs1 == null && vs2 == null)
                return true;
            if (vs1 == null || vs2 == null)
                return false;
            if (vs1.Last() == vs2.Last())
                return true;
            else
                return false;
        }


        /// <summary>
        /// 根据搜索结果解析成AGV可以识别的形式
        /// </summary>
        /// <param name="id">直行可以到达的标签索引</param>
        /// <param name="vs1">第一组搜索结果</param>
        /// <param name="vs2">第二组搜索结果</param>
        /// <param name="vs3">第三组搜索结果</param>
        /// <returns></returns>
        public static List<string> AnalyLeftResault(int id, List<string> vs1, List<string> vs2, List<string> vs3)
        {
            List<string> rs = new List<string>();
            //直行
            int Straight = -1;
            //上分叉
            int TurnUp = -1;
            //下分叉
            int TurnDown = -1;
            //有直接的直行标签
            if (id != -1)
            {
                Straight = MapElement.MapObject.RFIDS[id].Num;
                if (vs1 != null && vs2 != null && vs3 != null)
                {
                    MapOperate.SystemMsg.WriteLine("错误：单个方向不能出现三个以上标签！");
                    return null;
                }
                else
                //还有两个标签
                if (vs1 != null && vs2 != null)
                {
                    //排查错误
                    if (vs1.First().StartsWith("左上") && vs2.First().StartsWith("左上"))
                    {
                        MapOperate.SystemMsg.WriteLine("错误：上分叉出现了两个标签！");
                        return null;
                    }
                    else
                    if (vs1.First().StartsWith("左下") && vs2.First().StartsWith("左下"))
                    {
                        MapOperate.SystemMsg.WriteLine("错误：下分叉出现了两个标签！");
                        return null;
                    }
                    else
                    if (vs1.First().StartsWith("左上") && vs2.First().StartsWith("左下"))
                    {
                        TurnUp = MapElement.MapObject.RFIDS[int.Parse(vs1.Last())].Num;
                        TurnDown = MapElement.MapObject.RFIDS[int.Parse(vs2.Last())].Num;
                    }
                    else
                    if (vs1.First().StartsWith("左下") && vs2.First().StartsWith("左上"))
                    {
                        TurnDown = MapElement.MapObject.RFIDS[int.Parse(vs1.Last())].Num;
                        TurnUp = MapElement.MapObject.RFIDS[int.Parse(vs2.Last())].Num;
                    }
                    else
                    {
                        MapOperate.SystemMsg.WriteLine("错误：未知情况！");
                        return null;
                    }
                }
                else
                //还有一个标签
                if (vs1 != null)
                {
                    if (vs1.First().StartsWith("左上"))
                        TurnDown = MapElement.MapObject.RFIDS[int.Parse(vs1.Last())].Num;
                    else
                        TurnUp = MapElement.MapObject.RFIDS[int.Parse(vs1.Last())].Num;
                }
            }
            else
            //没有直行的标签
            {
                //有一个标签
                if (vs1 != null && vs2 == null && vs3 == null)
                {
                    Straight = MapElement.MapObject.RFIDS[int.Parse(vs1.Last())].Num;
                }
                else
                //有两个标签
                if (vs1 != null && vs2 != null && vs3 == null)
                {
                    //1.从起点开始删除相同部分
                    int i = 0;
                    for (i = 0; i < vs1.Count; i++)
                    {
                        if (vs1[i] != vs2[i])
                            break;
                    }
                    vs1.RemoveRange(0, i);
                    vs2.RemoveRange(0, i);
                    //情况1
                    if (vs1.First().StartsWith("左") && vs2.First().StartsWith("左"))
                    {
                        if (vs1.First().StartsWith("左上") && vs2.First().StartsWith("左下"))
                        {
                            TurnUp = MapElement.MapObject.RFIDS[int.Parse(vs1.Last())].Num;
                            TurnDown = MapElement.MapObject.RFIDS[int.Parse(vs2.Last())].Num;
                        }
                        else
                        if (vs1.First().StartsWith("左下") && vs2.First().StartsWith("左上"))
                        {
                            TurnDown = MapElement.MapObject.RFIDS[int.Parse(vs1.Last())].Num;
                            TurnUp = MapElement.MapObject.RFIDS[int.Parse(vs2.Last())].Num;
                        }
                        else
                        {
                            //获取分叉的坐标
                            int ids1 = int.Parse(vs1[i].Substring(2, vs1[i].Length - 2));
                            int ids2 = int.Parse(vs2[i].Substring(2, vs2[i].Length - 2));
                            Point pt1 = MapElement.MapObject.ForkLines[ids1].EndPoint;
                            Point pt2 = MapElement.MapObject.ForkLines[ids2].EndPoint;
                            if (vs1.First().StartsWith("左上") && vs2.First().StartsWith("左上"))
                            {
                                //靠左的是直行，靠右的上分叉
                                if (pt1.X < pt2.X)
                                {
                                    Straight = MapElement.MapObject.RFIDS[int.Parse(vs1.Last())].Num;
                                    TurnUp = MapElement.MapObject.RFIDS[int.Parse(vs2.Last())].Num;
                                }
                                else
                                {
                                    TurnUp = MapElement.MapObject.RFIDS[int.Parse(vs1.Last())].Num;
                                    Straight = MapElement.MapObject.RFIDS[int.Parse(vs2.Last())].Num;
                                }
                            }
                            else
                            if (vs1.First().StartsWith("左下") && vs2.First().StartsWith("左下"))
                            {
                                //靠左的是直行，靠右的下分叉
                                if (pt1.X < pt2.X)
                                {
                                    Straight = MapElement.MapObject.RFIDS[int.Parse(vs1.Last())].Num;
                                    TurnDown = MapElement.MapObject.RFIDS[int.Parse(vs2.Last())].Num;
                                }
                                else
                                {
                                    TurnDown = MapElement.MapObject.RFIDS[int.Parse(vs1.Last())].Num;
                                    Straight = MapElement.MapObject.RFIDS[int.Parse(vs2.Last())].Num;
                                }
                            }
                        }
                    }
                    else
                    //情况2
                    if (vs1.First().StartsWith("右") && vs2.First().StartsWith("右"))
                    {
                        if (vs1.First().StartsWith("右上") && vs2.First().StartsWith("右下"))
                        {
                            TurnDown = MapElement.MapObject.RFIDS[int.Parse(vs1.Last())].Num;
                            TurnUp = MapElement.MapObject.RFIDS[int.Parse(vs2.Last())].Num;
                        }
                        else
                        if (vs1.First().StartsWith("右下") && vs2.First().StartsWith("右上"))
                        {
                            TurnUp = MapElement.MapObject.RFIDS[int.Parse(vs1.Last())].Num;
                            TurnDown = MapElement.MapObject.RFIDS[int.Parse(vs2.Last())].Num;
                        }
                        else
                        {
                            //获取分叉的坐标
                            int ids1 = int.Parse(vs1[i].Substring(2, vs1[i].Length - 2));
                            int ids2 = int.Parse(vs2[i].Substring(2, vs2[i].Length - 2));
                            Point pt1 = MapElement.MapObject.ForkLines[ids1].EndPoint;
                            Point pt2 = MapElement.MapObject.ForkLines[ids2].EndPoint;
                            if (vs1.First().StartsWith("右上") && vs2.First().StartsWith("右上"))
                            {
                                //靠右的是直行，靠左的下分叉
                                if (pt1.X > pt2.X)
                                {
                                    Straight = MapElement.MapObject.RFIDS[int.Parse(vs1.Last())].Num;
                                    TurnDown = MapElement.MapObject.RFIDS[int.Parse(vs2.Last())].Num;
                                }
                                else
                                {
                                    TurnDown = MapElement.MapObject.RFIDS[int.Parse(vs1.Last())].Num;
                                    Straight = MapElement.MapObject.RFIDS[int.Parse(vs2.Last())].Num;
                                }
                            }
                            else
                            if (vs1.First().StartsWith("右下") && vs2.First().StartsWith("右下"))
                            {
                                //靠右的是直行，靠左的上分叉
                                if (pt1.X > pt2.X)
                                {
                                    Straight = MapElement.MapObject.RFIDS[int.Parse(vs1.Last())].Num;
                                    TurnUp = MapElement.MapObject.RFIDS[int.Parse(vs2.Last())].Num;
                                }
                                else
                                {
                                    TurnUp = MapElement.MapObject.RFIDS[int.Parse(vs1.Last())].Num;
                                    Straight = MapElement.MapObject.RFIDS[int.Parse(vs2.Last())].Num;
                                }
                            }
                        }
                    }
                    else
                    //情况3
                    if (vs1.First().StartsWith("上") && vs2.First().StartsWith("上"))
                    {
                        if (vs1.First().StartsWith("上左") && vs2.First().StartsWith("上右"))
                        {
                            TurnDown = MapElement.MapObject.RFIDS[int.Parse(vs1.Last())].Num;
                            TurnUp = MapElement.MapObject.RFIDS[int.Parse(vs2.Last())].Num;
                        }
                        else
                        if (vs1.First().StartsWith("上右") && vs2.First().StartsWith("上左"))
                        {
                            TurnUp = MapElement.MapObject.RFIDS[int.Parse(vs1.Last())].Num;
                            TurnDown = MapElement.MapObject.RFIDS[int.Parse(vs2.Last())].Num;
                        }
                        else
                        {
                            //获取分叉的坐标
                            int ids1 = int.Parse(vs1[i].Substring(2, vs1[i].Length - 2));
                            int ids2 = int.Parse(vs2[i].Substring(2, vs2[i].Length - 2));
                            Point pt1 = MapElement.MapObject.ForkLines[ids1].StartPoint;
                            Point pt2 = MapElement.MapObject.ForkLines[ids2].StartPoint;
                            if (vs1.First().StartsWith("上左") && vs2.First().StartsWith("上左"))
                            {
                                //靠上的是直行，靠下的下分叉
                                if (pt1.Y < pt2.Y)
                                {
                                    Straight = MapElement.MapObject.RFIDS[int.Parse(vs1.Last())].Num;
                                    TurnDown = MapElement.MapObject.RFIDS[int.Parse(vs2.Last())].Num;
                                }
                                else
                                {
                                    TurnDown = MapElement.MapObject.RFIDS[int.Parse(vs1.Last())].Num;
                                    Straight = MapElement.MapObject.RFIDS[int.Parse(vs2.Last())].Num;
                                }
                            }
                            else
                            if (vs1.First().StartsWith("上右") && vs2.First().StartsWith("上右"))
                            {
                                //靠上的是直行，靠下的上分叉
                                if (pt1.Y < pt2.Y)
                                {
                                    Straight = MapElement.MapObject.RFIDS[int.Parse(vs1.Last())].Num;
                                    TurnUp = MapElement.MapObject.RFIDS[int.Parse(vs2.Last())].Num;
                                }
                                else
                                {
                                    TurnUp = MapElement.MapObject.RFIDS[int.Parse(vs1.Last())].Num;
                                    Straight = MapElement.MapObject.RFIDS[int.Parse(vs2.Last())].Num;
                                }
                            }
                        }
                    }
                    else
                    //情况4
                    if (vs1.First().StartsWith("下") && vs2.First().StartsWith("下"))
                    {
                        if (vs1.First().StartsWith("下左") && vs2.First().StartsWith("下右"))
                        {
                            TurnUp = MapElement.MapObject.RFIDS[int.Parse(vs1.Last())].Num;
                            TurnDown = MapElement.MapObject.RFIDS[int.Parse(vs2.Last())].Num;
                        }
                        else
                        if (vs1.First().StartsWith("下右") && vs2.First().StartsWith("下左"))
                        {
                            TurnDown = MapElement.MapObject.RFIDS[int.Parse(vs1.Last())].Num;
                            TurnUp = MapElement.MapObject.RFIDS[int.Parse(vs2.Last())].Num;
                        }
                        else
                        {
                            //获取分叉的坐标
                            int ids1 = int.Parse(vs1[i].Substring(2, vs1[i].Length - 2));
                            int ids2 = int.Parse(vs2[i].Substring(2, vs2[i].Length - 2));
                            Point pt1 = MapElement.MapObject.ForkLines[ids1].StartPoint;
                            Point pt2 = MapElement.MapObject.ForkLines[ids2].StartPoint;
                            if (vs1.First().StartsWith("下左") && vs2.First().StartsWith("下左"))
                            {
                                //靠下的是直行，靠上的上分叉
                                if (pt1.Y > pt2.Y)
                                {
                                    Straight = MapElement.MapObject.RFIDS[int.Parse(vs1.Last())].Num;
                                    TurnUp = MapElement.MapObject.RFIDS[int.Parse(vs2.Last())].Num;
                                }
                                else
                                {
                                    TurnUp = MapElement.MapObject.RFIDS[int.Parse(vs1.Last())].Num;
                                    Straight = MapElement.MapObject.RFIDS[int.Parse(vs2.Last())].Num;
                                }
                            }
                            else
                            if (vs1.First().StartsWith("下右") && vs2.First().StartsWith("下右"))
                            {
                                //靠下的是直行，靠上的下分叉
                                if (pt1.Y > pt2.Y)
                                {
                                    Straight = MapElement.MapObject.RFIDS[int.Parse(vs1.Last())].Num;
                                    TurnDown = MapElement.MapObject.RFIDS[int.Parse(vs2.Last())].Num;
                                }
                                else
                                {
                                    TurnDown = MapElement.MapObject.RFIDS[int.Parse(vs1.Last())].Num;
                                    Straight = MapElement.MapObject.RFIDS[int.Parse(vs2.Last())].Num;
                                }
                            }
                        }
                    }
                    else
                    {
                        MapOperate.SystemMsg.WriteLine("错误：未知错误！");
                        return null;
                    }
                }
                else
                //有三个标签
                if (vs1 != null && vs2 != null && vs3 != null)
                {
                    //1.从起点开始删除相同部分
                    int i = 0;
                    for (i = 0; i < vs1.Count; i++)
                    {
                        if (vs1[i] != vs2[i] || vs1[i] != vs3[i])
                            break;
                    }
                    vs1.RemoveRange(0, i);
                    vs2.RemoveRange(0, i);
                    vs3.RemoveRange(0, i);
                    //情况1【直行可以到达某一个点】
                    if (vs1.Count == 1 || vs2.Count == 1 || vs3.Count == 1)
                    {
                        List<string> v1 = new List<string>();
                        List<string> v2 = new List<string>();
                        if (vs1.Count == 1)
                        {
                            //直行到达
                            Straight = MapElement.MapObject.RFIDS[int.Parse(vs1.Last())].Num;
                            v1 = vs2;
                            v2 = vs3;
                        }
                        else
                        if (vs2.Count == 1)
                        {
                            //直行到达
                            Straight = MapElement.MapObject.RFIDS[int.Parse(vs2.Last())].Num;
                            v1 = vs1;
                            v2 = vs3;
                        }
                        else
                        if (vs3.Count == 1)
                        {
                            //直行到达
                            Straight = MapElement.MapObject.RFIDS[int.Parse(vs3.Last())].Num;
                            v1 = vs1;
                            v2 = vs2;
                        }
                        //排查错误
                        if (v1.First().StartsWith("左上") && v2.First().StartsWith("左上"))
                        {
                            MapOperate.SystemMsg.WriteLine("错误：上分叉出现了两个标签！");
                            return null;
                        }
                        else
                        if (v1.First().StartsWith("左下") && v2.First().StartsWith("左下"))
                        {
                            MapOperate.SystemMsg.WriteLine("错误：下分叉出现了两个标签！");
                            return null;
                        }
                        else
                        if (v1.First().StartsWith("右上") && v2.First().StartsWith("右上"))
                        {
                            MapOperate.SystemMsg.WriteLine("错误：下分叉出现了两个标签！");
                            return null;
                        }
                        else
                        if (v1.First().StartsWith("右下") && v2.First().StartsWith("右下"))
                        {
                            MapOperate.SystemMsg.WriteLine("错误：下分叉出现了两个标签！");
                            return null;
                        }
                        else
                        if (v1.First().StartsWith("上左") && v2.First().StartsWith("上左"))
                        {
                            MapOperate.SystemMsg.WriteLine("错误：下分叉出现了两个标签！");
                            return null;
                        }
                        else
                        if (v1.First().StartsWith("上右") && v2.First().StartsWith("上右"))
                        {
                            MapOperate.SystemMsg.WriteLine("错误：下分叉出现了两个标签！");
                            return null;
                        }
                        else
                        if (v1.First().StartsWith("下左") && v2.First().StartsWith("下左"))
                        {
                            MapOperate.SystemMsg.WriteLine("错误：下分叉出现了两个标签！");
                            return null;
                        }
                        else
                        if (v1.First().StartsWith("下右") && v2.First().StartsWith("下右"))
                        {
                            MapOperate.SystemMsg.WriteLine("错误：下分叉出现了两个标签！");
                            return null;
                        }

                        //计算结果
                        if (v1.First().StartsWith("左") && v2.First().StartsWith("左"))
                        {
                            if (v1.First().StartsWith("左上") && v2.First().StartsWith("左下"))
                            {
                                TurnUp = MapElement.MapObject.RFIDS[int.Parse(v1.Last())].Num;
                                TurnDown = MapElement.MapObject.RFIDS[int.Parse(v2.Last())].Num;
                            }
                            else
                            if (v1.First().StartsWith("左下") && v2.First().StartsWith("左上"))
                            {
                                TurnDown = MapElement.MapObject.RFIDS[int.Parse(v1.Last())].Num;
                                TurnUp = MapElement.MapObject.RFIDS[int.Parse(v2.Last())].Num;
                            }
                        }
                        else
                        if (v1.First().StartsWith("右") && v2.First().StartsWith("右"))
                        {
                            if (v1.First().StartsWith("右上") && v2.First().StartsWith("右下"))
                            {
                                TurnDown = MapElement.MapObject.RFIDS[int.Parse(v1.Last())].Num;
                                TurnUp = MapElement.MapObject.RFIDS[int.Parse(v2.Last())].Num;
                            }
                            else
                            if (v1.First().StartsWith("右下") && v2.First().StartsWith("右上"))
                            {
                                TurnUp = MapElement.MapObject.RFIDS[int.Parse(v1.Last())].Num;
                                TurnDown = MapElement.MapObject.RFIDS[int.Parse(v2.Last())].Num;
                            }
                        }
                        else
                        if (v1.First().StartsWith("上") && v2.First().StartsWith("上"))
                        {
                            if (v1.First().StartsWith("上左") && v2.First().StartsWith("上右"))
                            {
                                TurnDown = MapElement.MapObject.RFIDS[int.Parse(v1.Last())].Num;
                                TurnUp = MapElement.MapObject.RFIDS[int.Parse(v2.Last())].Num;
                            }
                            else
                            if (v1.First().StartsWith("上右") && v2.First().StartsWith("上左"))
                            {
                                TurnUp = MapElement.MapObject.RFIDS[int.Parse(v1.Last())].Num;
                                TurnDown = MapElement.MapObject.RFIDS[int.Parse(v2.Last())].Num;
                            }
                        }
                        else
                        if (v1.First().StartsWith("下") && v2.First().StartsWith("下"))
                        {
                            if (v1.First().StartsWith("下左") && v2.First().StartsWith("下右"))
                            {
                                TurnUp = MapElement.MapObject.RFIDS[int.Parse(v1.Last())].Num;
                                TurnDown = MapElement.MapObject.RFIDS[int.Parse(v2.Last())].Num;
                            }
                            else
                            if (v1.First().StartsWith("下右") && v2.First().StartsWith("下左"))
                            {
                                TurnDown = MapElement.MapObject.RFIDS[int.Parse(v1.Last())].Num;
                                TurnUp = MapElement.MapObject.RFIDS[int.Parse(v2.Last())].Num;
                            }
                        }
                        else
                        {
                            MapOperate.SystemMsg.WriteLine("错误：未知情况！");
                            return null;
                        }
                    }
                    else
                    //情况2【都不是终点,根据位置拿到直行】
                    {
                        List<string> v1 = null;
                        List<string> v2 = null;
                        //情况1
                        if (vs1.First().StartsWith("左") && vs2.First().StartsWith("左") && vs3.First().StartsWith("左"))
                        {
                            //肯定是一个左上，两个左下，单独左下的就是左下，两个左上的需要单独处理
                            if (vs1.First().StartsWith("左上") && vs2.First().StartsWith("左下") && vs3.First().StartsWith("左下"))
                            {
                                TurnUp = MapElement.MapObject.RFIDS[int.Parse(vs1.Last())].Num;
                                v1 = vs2;
                                v2 = vs3;
                            }
                            else
                            if (vs1.First().StartsWith("左下") && vs2.First().StartsWith("左上") && vs3.First().StartsWith("左下"))
                            {
                                TurnUp = MapElement.MapObject.RFIDS[int.Parse(vs2.Last())].Num;
                                v1 = vs1;
                                v2 = vs3;
                            }
                            else
                            if (vs1.First().StartsWith("左下") && vs2.First().StartsWith("左下") && vs3.First().StartsWith("左上"))
                            {
                                TurnUp = MapElement.MapObject.RFIDS[int.Parse(vs3.Last())].Num;
                                v1 = vs1;
                                v2 = vs2;
                            }
                            else
                            if (vs1.First().StartsWith("左下") && vs2.First().StartsWith("左上") && vs3.First().StartsWith("左上"))
                            {
                                TurnDown = MapElement.MapObject.RFIDS[int.Parse(vs1.Last())].Num;
                                v1 = vs2;
                                v2 = vs3;
                            }
                            else
                            if (vs1.First().StartsWith("左上") && vs2.First().StartsWith("左下") && vs3.First().StartsWith("左上"))
                            {
                                TurnDown = MapElement.MapObject.RFIDS[int.Parse(vs2.Last())].Num;
                                v1 = vs1;
                                v2 = vs3;
                            }
                            else
                            if (vs1.First().StartsWith("左上") && vs2.First().StartsWith("左上") && vs3.First().StartsWith("左下"))
                            {
                                TurnDown = MapElement.MapObject.RFIDS[int.Parse(vs3.Last())].Num;
                                v1 = vs1;
                                v2 = vs2;
                            }
                        }
                        else
                        //情况2
                        if (vs1.First().StartsWith("右") && vs2.First().StartsWith("右") && vs3.First().StartsWith("右"))
                        {
                            if (vs1.First().StartsWith("右上") && vs2.First().StartsWith("右下") && vs3.First().StartsWith("右下"))
                            {
                                TurnDown = MapElement.MapObject.RFIDS[int.Parse(vs1.Last())].Num;
                                v1 = vs2;
                                v2 = vs3;
                            }
                            else
                            if (vs1.First().StartsWith("右下") && vs2.First().StartsWith("右上") && vs3.First().StartsWith("右下"))
                            {
                                TurnDown = MapElement.MapObject.RFIDS[int.Parse(vs2.Last())].Num;
                                v1 = vs1;
                                v2 = vs3;
                            }
                            else
                            if (vs1.First().StartsWith("右下") && vs2.First().StartsWith("右下") && vs3.First().StartsWith("右上"))
                            {
                                TurnDown = MapElement.MapObject.RFIDS[int.Parse(vs3.Last())].Num;
                                v1 = vs1;
                                v2 = vs2;
                            }
                            else
                            if (vs1.First().StartsWith("右下") && vs2.First().StartsWith("右上") && vs3.First().StartsWith("右上"))
                            {
                                TurnUp = MapElement.MapObject.RFIDS[int.Parse(vs1.Last())].Num;
                                v1 = vs2;
                                v2 = vs3;
                            }
                            else
                            if (vs1.First().StartsWith("右上") && vs2.First().StartsWith("右下") && vs3.First().StartsWith("右上"))
                            {
                                TurnUp = MapElement.MapObject.RFIDS[int.Parse(vs2.Last())].Num;
                                v1 = vs1;
                                v2 = vs3;
                            }
                            else
                            if (vs1.First().StartsWith("右上") && vs2.First().StartsWith("右上") && vs3.First().StartsWith("右下"))
                            {
                                TurnUp = MapElement.MapObject.RFIDS[int.Parse(vs3.Last())].Num;
                                v1 = vs1;
                                v2 = vs2;
                            }
                        }
                        else
                        //情况3
                        if (vs1.First().StartsWith("上") && vs2.First().StartsWith("上") && vs3.First().StartsWith("上"))
                        {
                            if (vs1.First().StartsWith("上左") && vs2.First().StartsWith("上右") && vs3.First().StartsWith("上右"))
                            {
                                TurnDown = MapElement.MapObject.RFIDS[int.Parse(vs1.Last())].Num;
                                v1 = vs2;
                                v2 = vs3;
                            }
                            else
                            if (vs1.First().StartsWith("上右") && vs2.First().StartsWith("上左") && vs3.First().StartsWith("上右"))
                            {
                                TurnDown = MapElement.MapObject.RFIDS[int.Parse(vs2.Last())].Num;
                                v1 = vs1;
                                v2 = vs3;
                            }
                            else
                            if (vs1.First().StartsWith("上右") && vs2.First().StartsWith("上右") && vs3.First().StartsWith("上左"))
                            {
                                TurnDown = MapElement.MapObject.RFIDS[int.Parse(vs3.Last())].Num;
                                v1 = vs1;
                                v2 = vs2;
                            }
                            else
                            if (vs1.First().StartsWith("上右") && vs2.First().StartsWith("上左") && vs3.First().StartsWith("上左"))
                            {
                                TurnUp = MapElement.MapObject.RFIDS[int.Parse(vs1.Last())].Num;
                                v1 = vs2;
                                v2 = vs3;
                            }
                            else
                            if (vs1.First().StartsWith("上左") && vs2.First().StartsWith("上右") && vs3.First().StartsWith("上左"))
                            {
                                TurnUp = MapElement.MapObject.RFIDS[int.Parse(vs2.Last())].Num;
                                v1 = vs1;
                                v2 = vs3;
                            }
                            else
                            if (vs1.First().StartsWith("上左") && vs2.First().StartsWith("上左") && vs3.First().StartsWith("上右"))
                            {
                                TurnUp = MapElement.MapObject.RFIDS[int.Parse(vs3.Last())].Num;
                                v1 = vs1;
                                v2 = vs2;
                            }
                        }
                        else
                        //情况4
                        if (vs1.First().StartsWith("下") && vs2.First().StartsWith("下") && vs3.First().StartsWith("下"))
                        {
                            if (vs1.First().StartsWith("下左") && vs2.First().StartsWith("下右") && vs3.First().StartsWith("下右"))
                            {
                                TurnUp = MapElement.MapObject.RFIDS[int.Parse(vs1.Last())].Num;
                                v1 = vs2;
                                v2 = vs3;
                            }
                            else
                              if (vs1.First().StartsWith("下右") && vs2.First().StartsWith("下左") && vs3.First().StartsWith("下右"))
                            {
                                TurnUp = MapElement.MapObject.RFIDS[int.Parse(vs2.Last())].Num;
                                v1 = vs1;
                                v2 = vs3;
                            }
                            else
                              if (vs1.First().StartsWith("下右") && vs2.First().StartsWith("下右") && vs3.First().StartsWith("下左"))
                            {
                                TurnUp = MapElement.MapObject.RFIDS[int.Parse(vs3.Last())].Num;
                                v1 = vs1;
                                v2 = vs2;
                            }
                            else
                              if (vs1.First().StartsWith("下右") && vs2.First().StartsWith("下左") && vs3.First().StartsWith("下左"))
                            {
                                TurnDown = MapElement.MapObject.RFIDS[int.Parse(vs1.Last())].Num;
                                v1 = vs2;
                                v2 = vs3;
                            }
                            else
                              if (vs1.First().StartsWith("下左") && vs2.First().StartsWith("下右") && vs3.First().StartsWith("下左"))
                            {
                                TurnDown = MapElement.MapObject.RFIDS[int.Parse(vs2.Last())].Num;
                                v1 = vs1;
                                v2 = vs3;
                            }
                            else
                              if (vs1.First().StartsWith("下左") && vs2.First().StartsWith("下左") && vs3.First().StartsWith("下右"))
                            {
                                TurnDown = MapElement.MapObject.RFIDS[int.Parse(vs3.Last())].Num;
                                v1 = vs1;
                                v2 = vs2;
                            }
                        }
                        if (v1 != null)
                        {
                            //1.从起点开始删除相同部分
                            i = 0;
                            for (i = 0; i < v1.Count; i++)
                            {
                                if (v1[i] != v2[i])
                                    break;
                            }
                            v1.RemoveRange(0, i);
                            v2.RemoveRange(0, i);
                            if (v1.First().StartsWith("左") && v2.First().StartsWith("左"))
                            {
                                //获取分叉的坐标
                                int ids1 = int.Parse(v1[i].Substring(2, v1[i].Length - 2));
                                int ids2 = int.Parse(v2[i].Substring(2, v2[i].Length - 2));
                                Point pt1 = MapElement.MapObject.ForkLines[ids1].EndPoint;
                                Point pt2 = MapElement.MapObject.ForkLines[ids2].EndPoint;
                                //靠左的是直行
                                if (pt1.X < pt2.X)
                                {
                                    Straight = MapElement.MapObject.RFIDS[int.Parse(v1.Last())].Num;
                                    if (TurnUp != -1)
                                        TurnDown = MapElement.MapObject.RFIDS[int.Parse(v2.Last())].Num;
                                    else
                                        TurnUp = MapElement.MapObject.RFIDS[int.Parse(v2.Last())].Num;
                                }
                                else
                                {
                                    if (TurnUp != -1)
                                        TurnDown = MapElement.MapObject.RFIDS[int.Parse(v1.Last())].Num;
                                    else
                                        TurnUp = MapElement.MapObject.RFIDS[int.Parse(v1.Last())].Num;
                                    Straight = MapElement.MapObject.RFIDS[int.Parse(v2.Last())].Num;
                                }
                            }
                            else
                            if (v1.First().StartsWith("右") && v2.First().StartsWith("右"))
                            {
                                //获取分叉的坐标
                                int ids1 = int.Parse(v1[i].Substring(2, v1[i].Length - 2));
                                int ids2 = int.Parse(v2[i].Substring(2, v2[i].Length - 2));
                                Point pt1 = MapElement.MapObject.ForkLines[ids1].EndPoint;
                                Point pt2 = MapElement.MapObject.ForkLines[ids2].EndPoint;
                                //靠右的是直行
                                if (pt1.X > pt2.X)
                                {
                                    Straight = MapElement.MapObject.RFIDS[int.Parse(v1.Last())].Num;
                                    if (TurnUp != -1)
                                        TurnDown = MapElement.MapObject.RFIDS[int.Parse(v2.Last())].Num;
                                    else
                                        TurnUp = MapElement.MapObject.RFIDS[int.Parse(v2.Last())].Num;
                                }
                                else
                                {
                                    if (TurnUp != -1)
                                        TurnDown = MapElement.MapObject.RFIDS[int.Parse(v1.Last())].Num;
                                    else
                                        TurnUp = MapElement.MapObject.RFIDS[int.Parse(v1.Last())].Num;
                                    Straight = MapElement.MapObject.RFIDS[int.Parse(v2.Last())].Num;
                                }
                            }
                            else
                            if (v1.First().StartsWith("上") && v2.First().StartsWith("上"))
                            {
                                //获取分叉的坐标
                                int ids1 = int.Parse(v1[i].Substring(2, v1[i].Length - 2));
                                int ids2 = int.Parse(v2[i].Substring(2, v2[i].Length - 2));
                                Point pt1 = MapElement.MapObject.ForkLines[ids1].StartPoint;
                                Point pt2 = MapElement.MapObject.ForkLines[ids2].StartPoint;
                                //靠上的是直行
                                if (pt1.Y < pt2.Y)
                                {
                                    Straight = MapElement.MapObject.RFIDS[int.Parse(v1.Last())].Num;
                                    if (TurnUp != -1)
                                        TurnDown = MapElement.MapObject.RFIDS[int.Parse(v2.Last())].Num;
                                    else
                                        TurnUp = MapElement.MapObject.RFIDS[int.Parse(v2.Last())].Num;
                                }
                                else
                                {
                                    if (TurnUp != -1)
                                        TurnDown = MapElement.MapObject.RFIDS[int.Parse(v1.Last())].Num;
                                    else
                                        TurnUp = MapElement.MapObject.RFIDS[int.Parse(v1.Last())].Num;
                                    Straight = MapElement.MapObject.RFIDS[int.Parse(v2.Last())].Num;
                                }
                            }
                            else
                            if (v1.First().StartsWith("下") && v2.First().StartsWith("下"))
                            {
                                //获取分叉的坐标
                                int ids1 = int.Parse(v1[i].Substring(2, v1[i].Length - 2));
                                int ids2 = int.Parse(v2[i].Substring(2, v2[i].Length - 2));
                                Point pt1 = MapElement.MapObject.ForkLines[ids1].StartPoint;
                                Point pt2 = MapElement.MapObject.ForkLines[ids2].StartPoint;
                                //靠下的是直行
                                if (pt1.Y > pt2.Y)
                                {
                                    Straight = MapElement.MapObject.RFIDS[int.Parse(v1.Last())].Num;
                                    if (TurnUp != -1)
                                        TurnDown = MapElement.MapObject.RFIDS[int.Parse(v2.Last())].Num;
                                    else
                                        TurnUp = MapElement.MapObject.RFIDS[int.Parse(v2.Last())].Num;
                                }
                                else
                                {
                                    if (TurnUp != -1)
                                        TurnDown = MapElement.MapObject.RFIDS[int.Parse(v1.Last())].Num;
                                    else
                                        TurnUp = MapElement.MapObject.RFIDS[int.Parse(v1.Last())].Num;
                                    Straight = MapElement.MapObject.RFIDS[int.Parse(v2.Last())].Num;
                                }
                            }
                        }
                        else
                        {
                            MapOperate.SystemMsg.WriteLine("错误：未知错误！");
                            return null;
                        }
                    }
                }
            }
            //打印结果
            MapOperate.SystemMsg.WriteLine("结果：上分叉【{0}】直行【{1}】下分叉【{2}】", TurnUp, Straight, TurnDown);
            return rs;
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
                MapOperate.SystemMsg.WriteLine("左直-{0}标签!", MapElement.MapObject.RFIDS[id].Num);
                //设置搜索的x终点坐标【X的最小值为找的的标签的右侧中心坐标】
                range.MinX = MapElement.MapObject.RFIDS[id].RightPoint.X;
            }
            //向左找上分叉
            List<string> vs1 = ProcessString(index, rfid.LeftPoint, ProcessState.LeftUp, range);
            List<string> vs2 = null;
            List<string> vs3 = null;
            //找到了继续向左找下分叉
            if (vs1 != null)
            {
                vs2 = null;
                vs3 = null;
                //第二次搜索
                if (vs1.Count > 1)
                    FindSecond(index, vs1, ref vs2, ref vs3, range);
                //第三次搜索
                if (!(vs1 != null && vs2 != null && id != -1))
                {
                    //不够三条路才进行第三次搜索
                    if (vs2 != null && vs3 == null)
                    {
                        List<string> vs = new List<string>();
                        bool p = false;
                        int num = 0;
                        for (int i = 0; i < vs2.Count; i++)
                        {

                            if (p == false)
                            {
                                if (vs2[i] == vs1[i])
                                    continue;
                                else
                                { p = true; num = i; }
                            }
                            if (p)
                                vs.Add(vs2[i]);
                        }
                        FindThird(index, vs1, vs, ref vs3);
                        //补齐前段
                        if (vs3 != null)
                            vs3.InsertRange(0, vs2.GetRange(0, num));
                        else
                        //如果第三次还是没有找到，用vs1的公共部分后半段再搜一次
                        if (vs3 == null)
                        {
                            vs = new List<string>();
                            p = false;
                            num = 0;
                            for (int i = 0; i < vs1.Count; i++)
                            {

                                if (p == false)
                                {
                                    if (vs1[i] == vs2[i])
                                        continue;
                                    else
                                    { p = true; num = i; }
                                }
                                if (p)
                                    vs.Add(vs1[i]);
                            }
                            FindThird(index, vs2, vs, ref vs3);
                            //补齐前段
                            if (vs3 != null)
                                vs3.InsertRange(0, vs1.GetRange(0, num));
                        }
                    }
                }
            }
            //打印结果
            if (vs1 != null)
                MapOperate.SystemMsg.WriteLine("vs1:" + String.Join("-", vs1.ToArray()) + "标签：" + MapElement.MapObject.RFIDS[int.Parse(vs1.Last())].Num.ToString());
            if (vs2 != null)
                MapOperate.SystemMsg.WriteLine("vs2:" + String.Join("-", vs2.ToArray()) + "标签：" + MapElement.MapObject.RFIDS[int.Parse(vs2.Last())].Num.ToString());
            if (vs3 != null)
                MapOperate.SystemMsg.WriteLine("vs3:" + String.Join("-", vs3.ToArray()) + "标签：" + MapElement.MapObject.RFIDS[int.Parse(vs3.Last())].Num.ToString());
            AnalyLeftResault(id, vs1, vs2, vs3);

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
                MapOperate.SystemMsg.WriteLine("右直-{0}标签!", MapElement.MapObject.RFIDS[id].Num);
                //设置搜索的x终点坐标【X的最大值为找的的标签的左侧中心坐标】
                range.MaxX = MapElement.MapObject.RFIDS[id].LeftPoint.X;
            }
            //向右找上分叉
            vs1 = ProcessString(index, rfid.RightPoint, ProcessState.RightUp, range);
            //找到了继续向右找下分叉
            if (vs1 != null)
            {
                vs2 = null;
                vs3 = null;
                //第二次搜索
                if (vs1.Count > 1)
                    FindSecond(index, vs1, ref vs2, ref vs3, range);
                //第三次搜索
                if (!(vs1 != null && vs2 != null && id != -1))
                {
                    //不够三条路才进行第三次搜索 
                    if (vs2 != null && vs3 == null)
                    {
                        List<string> vs = new List<string>();
                        bool p = false;
                        int num = 0;
                        for (int i = 0; i < vs2.Count; i++)
                        {

                            if (p == false)
                            {
                                if (vs2[i] == vs1[i])
                                    continue;
                                else
                                { p = true; num = i; }
                            }
                            if (p)
                                vs.Add(vs2[i]);
                        }
                        FindThird(index, vs1, vs, ref vs3);
                        //补齐前段
                        if (vs3 != null)
                            vs3.InsertRange(0, vs2.GetRange(0, num));
                        else
                        //如果第三次还是没有找到，用vs1的公共部分后半段再搜一次
                        if (vs3 == null)
                        {
                            vs = new List<string>();
                            p = false;
                            num = 0;
                            for (int i = 0; i < vs1.Count; i++)
                            {

                                if (p == false)
                                {
                                    if (vs1[i] == vs2[i])
                                        continue;
                                    else
                                    { p = true; num = i; }
                                }
                                if (p)
                                    vs.Add(vs1[i]);
                            }
                            FindThird(index, vs2, vs, ref vs3);
                            //补齐前段
                            if (vs3 != null)
                                vs3.InsertRange(0, vs1.GetRange(0, num));
                        }
                    }
                }
                //打印结果
                if (vs1 != null)
                    MapOperate.SystemMsg.WriteLine("vs1:" + String.Join("-", vs1.ToArray()) + "标签：" + MapElement.MapObject.RFIDS[int.Parse(vs1.Last())].Num.ToString());
                if (vs2 != null)
                    MapOperate.SystemMsg.WriteLine("vs2:" + String.Join("-", vs2.ToArray()) + "标签：" + MapElement.MapObject.RFIDS[int.Parse(vs2.Last())].Num.ToString());
                if (vs3 != null)
                    MapOperate.SystemMsg.WriteLine("vs3:" + String.Join("-", vs3.ToArray()) + "标签：" + MapElement.MapObject.RFIDS[int.Parse(vs3.Last())].Num.ToString());
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
                MapOperate.SystemMsg.WriteLine("上直-{0}标签!", MapElement.MapObject.RFIDS[id].Num);
                //设置搜索的y终点坐标【Y的最小值为找的的标签的下侧中心坐标】
                range.MinY = MapElement.MapObject.RFIDS[id].DownPoint.Y;
            }
            //向上找左分叉
            vs1 = ProcessString(index, rfid.UpPoint, ProcessState.UpLeft, range);
            //找到了继续向上找右分叉
            if (vs1 != null)
            {
                vs2 = null;
                vs3 = null;
                //第二次搜索
                if (vs1.Count > 1)
                    FindSecond(index, vs1, ref vs2, ref vs3, range);
                //第三次搜索
                if (!(vs1 != null && vs2 != null && id != -1))
                {
                    //不够三条路才进行第三次搜索 
                    if (vs2 != null && vs3 == null)
                    {
                        List<string> vs = new List<string>();
                        bool p = false;
                        int num = 0;
                        for (int i = 0; i < vs2.Count; i++)
                        {

                            if (p == false)
                            {
                                if (vs2[i] == vs1[i])
                                    continue;
                                else
                                { p = true; num = i; }
                            }
                            if (p)
                                vs.Add(vs2[i]);
                        }
                        FindThird(index, vs1, vs, ref vs3);
                        //补齐前段
                        if (vs3 != null)
                            vs3.InsertRange(0, vs2.GetRange(0, num));
                        else
                        //如果第三次还是没有找到，用vs1的公共部分后半段再搜一次
                        if (vs3 == null)
                        {
                            vs = new List<string>();
                            p = false;
                            num = 0;
                            for (int i = 0; i < vs1.Count; i++)
                            {

                                if (p == false)
                                {
                                    if (vs1[i] == vs2[i])
                                        continue;
                                    else
                                    { p = true; num = i; }
                                }
                                if (p)
                                    vs.Add(vs1[i]);
                            }
                            FindThird(index, vs2, vs, ref vs3);
                            //补齐前段
                            if (vs3 != null)
                                vs3.InsertRange(0, vs1.GetRange(0, num));
                        }
                    }
                }
                //打印结果
                if (vs1 != null)
                    MapOperate.SystemMsg.WriteLine("vs1:" + String.Join("-", vs1.ToArray()) + "标签：" + MapElement.MapObject.RFIDS[int.Parse(vs1.Last())].Num.ToString());
                if (vs2 != null)
                    MapOperate.SystemMsg.WriteLine("vs2:" + String.Join("-", vs2.ToArray()) + "标签：" + MapElement.MapObject.RFIDS[int.Parse(vs2.Last())].Num.ToString());
                if (vs3 != null)
                    MapOperate.SystemMsg.WriteLine("vs3:" + String.Join("-", vs3.ToArray()) + "标签：" + MapElement.MapObject.RFIDS[int.Parse(vs3.Last())].Num.ToString());
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
                MapOperate.SystemMsg.WriteLine("下直-{0}标签!", MapElement.MapObject.RFIDS[id].Num);
                //设置搜索的y终点坐标【Y的最大值为找的的标签的上侧中心坐标】
                range.MaxY = MapElement.MapObject.RFIDS[id].UpPoint.Y;
            }
            //向下找左分叉
            vs1 = ProcessString(index, rfid.DownPoint, ProcessState.DownLeft, range);
            //找到了继续向下找右分叉
            if (vs1 != null)
            {
                vs2 = null;
                vs3 = null;
                //第二次搜索
                if (vs1.Count > 1)
                    FindSecond(index, vs1, ref vs2, ref vs3, range);
                //第三次搜索
                if (!(vs1 != null && vs2 != null && id != -1))
                {
                    //不够三条路才进行第三次搜索
                    if (vs2 != null && vs3 == null)
                    {
                        List<string> vs = new List<string>();
                        bool p = false;
                        int num = 0;
                        for (int i = 0; i < vs2.Count; i++)
                        {

                            if (p == false)
                            {
                                if (vs2[i] == vs1[i])
                                    continue;
                                else
                                { p = true; num = i; }
                            }
                            if (p)
                                vs.Add(vs2[i]);
                        }
                        FindThird(index, vs1, vs, ref vs3);
                        //补齐前段
                        if (vs3 != null)
                            vs3.InsertRange(0, vs2.GetRange(0, num));
                        else
                        //如果第三次还是没有找到，用vs1的公共部分后半段再搜一次
                        if (vs3 == null)
                        {
                            vs = new List<string>();
                            p = false;
                            num = 0;
                            for (int i = 0; i < vs1.Count; i++)
                            {

                                if (p == false)
                                {
                                    if (vs1[i] == vs2[i])
                                        continue;
                                    else
                                    { p = true; num = i; }
                                }
                                if (p)
                                    vs.Add(vs1[i]);
                            }
                            FindThird(index, vs2, vs, ref vs3);
                            //补齐前段
                            if (vs3 != null)
                                vs3.InsertRange(0, vs1.GetRange(0, num));
                        }
                    }
                }

                //打印结果
                if (vs1 != null)
                    MapOperate.SystemMsg.WriteLine("vs1:" + String.Join("-", vs1.ToArray()) + "标签：" + MapElement.MapObject.RFIDS[int.Parse(vs1.Last())].Num.ToString());
                if (vs2 != null)
                    MapOperate.SystemMsg.WriteLine("vs2:" + String.Join("-", vs2.ToArray()) + "标签：" + MapElement.MapObject.RFIDS[int.Parse(vs2.Last())].Num.ToString());
                if (vs3 != null)
                    MapOperate.SystemMsg.WriteLine("vs3:" + String.Join("-", vs3.ToArray()) + "标签：" + MapElement.MapObject.RFIDS[int.Parse(vs3.Last())].Num.ToString());
            }
            #endregion

            MapOperate.SystemMsg.WriteLine("-----------END-------------");
        }

    }
}
