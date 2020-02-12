using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Windows.Shapes;

namespace WpfMap.Route
{
    public class Helper
    {
        public class Base
        {
            #region 四个方向的分叉定义
            /// <summary>
            /// 向左行驶
            /// </summary>
            public class Left
            {
                public Left()
                {
                    Straight = -1;
                    Up = -1;
                    Down = -1;
                }
                /// <summary>
                /// 直行可到达目标索引
                /// </summary>
                public int Straight;
                /// <summary>
                /// 向上分叉可到达目标索引
                /// </summary>
                public int Up;
                /// <summary>
                /// 向下分叉可到达目标索引
                /// </summary>
                public int Down;
                /// <summary>
                /// 路径集合1
                /// </summary>
                public List<string> V1 = null;
                /// <summary>
                /// 路径集合2
                /// </summary>
                public List<string> V2 = null;
                /// <summary>
                /// 路径集合3
                /// </summary>
                public List<string> V3 = null;
            }
            /// <summary>
            /// 向右行驶
            /// </summary>
            public class Right
            {
                public Right()
                {
                    Straight = -1;
                    Up = -1;
                    Down = -1;
                }
                /// <summary>
                /// 直行可到达目标索引
                /// </summary>
                public int Straight;
                /// <summary>
                /// 向上分叉可到达目标索引
                /// </summary>
                public int Up;
                /// <summary>
                /// 向下分叉可到达目标索引
                /// </summary>
                public int Down;
                /// <summary>
                /// 路径集合1
                /// </summary>
                public List<string> V1 = null;
                /// <summary>
                /// 路径集合2
                /// </summary>
                public List<string> V2 = null;
                /// <summary>
                /// 路径集合3
                /// </summary>
                public List<string> V3 = null;
            }
            /// <summary>
            /// 向上行驶
            /// </summary>
            public class Up
            {
                public Up()
                {
                    Straight = -1;
                    Left = -1;
                    Right = -1;
                }
                /// <summary>
                /// 直行可到达目标索引
                /// </summary>
                public int Straight;
                /// <summary>
                /// 向左分叉可到达目标索引
                /// </summary>
                public int Left;
                /// <summary>
                /// 向右分叉可到达目标索引
                /// </summary>
                public int Right;
                /// <summary>
                /// 路径集合1
                /// </summary>
                public List<string> V1 = null;
                /// <summary>
                /// 路径集合2
                /// </summary>
                public List<string> V2 = null;
                /// <summary>
                /// 路径集合3
                /// </summary>
                public List<string> V3 = null;
            }
            /// <summary>
            /// 向下行驶
            /// </summary>
            public class Down
            {
                public Down()
                {
                    Straight = -1;
                    Left = -1;
                    Right = -1;
                }
                /// <summary>
                /// 直行可到达目标索引
                /// </summary>
                public int Straight;
                /// <summary>
                /// 向左分叉可到达目标索引
                /// </summary>
                public int Left;
                /// <summary>
                /// 向右分叉可到达目标索引
                /// </summary>
                public int Right;
                /// <summary>
                /// 路径集合1
                /// </summary>
                public List<string> V1 = null;
                /// <summary>
                /// 路径集合2
                /// </summary>
                public List<string> V2 = null;
                /// <summary>
                /// 路径集合3
                /// </summary>
                public List<string> V3 = null;
            }
            #endregion


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
                    for (double i = pt.X; i > range.MinX; i -= MapElement.GridSize)
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
                    for (double i = pt.X; i < range.MaxX; i += MapElement.GridSize)
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
                    for (double i = pt.Y; i > range.MinY; i -= MapElement.GridSize)
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
                    for (double i = pt.Y; i < range.MaxY; i += MapElement.GridSize)
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
                    for (double i = pt.X; i > minX; i -= MapElement.GridSize)
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
                    for (double i = pt.X; i > minX; i -= MapElement.GridSize)
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
                    for (double i = pt.X; i < maxX; i += MapElement.GridSize)
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
                    for (double i = pt.X; i < maxX; i += MapElement.GridSize)
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
                    for (double i = pt.Y; i > minY; i -= MapElement.GridSize)
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
                    for (double i = pt.Y; i > minY; i -= MapElement.GridSize)
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
                    for (double i = pt.Y; i < maxY; i += MapElement.GridSize)
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
                    for (double i = pt.Y; i < maxY; i += MapElement.GridSize)
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
        /// 车头方向枚举
        /// </summary>
        public enum HeadDirections
        {
            /// <summary>
            /// 未定义
            /// </summary>
            None = 0,
            Up,
            Right,
            Down,
            Left
        }

        /// <summary>
        /// 车头方向类
        /// </summary>
        public class HeadDirection
        {
            public HeadDirection()
            {
                Dir = HeadDirections.None;
            }
            /// <summary>
            /// 车头方向
            /// </summary>
            public HeadDirections Dir;
            /// <summary>
            /// 搜索标记【是否通过该点搜索过】
            /// </summary>
            public bool Searched = false;
            /// <summary>
            /// 顺时针旋转
            /// </summary>
            public void Clockwise()
            {
                switch (Dir)
                {
                    case HeadDirections.None:
                        break;
                    case HeadDirections.Up:
                        Dir = HeadDirections.Right;
                        break;
                    case HeadDirections.Right:
                        Dir = HeadDirections.Down;
                        break;
                    case HeadDirections.Down:
                        Dir = HeadDirections.Left;
                        break;
                    case HeadDirections.Left:
                        Dir = HeadDirections.Up;
                        break;
                    default:
                        break;
                }
            }
            /// <summary>
            /// 逆时针旋转
            /// </summary>
            public void Counterclockwise()
            {
                switch (Dir)
                {
                    case HeadDirections.None:
                        break;
                    case HeadDirections.Up:
                        Dir = HeadDirections.Left;
                        break;
                    case HeadDirections.Right:
                        Dir = HeadDirections.Up;
                        break;
                    case HeadDirections.Down:
                        Dir = HeadDirections.Right;
                        break;
                    case HeadDirections.Left:
                        Dir = HeadDirections.Down;
                        break;
                    default:
                        break;
                }
            }
        }

        /// <summary>
        /// 地图上标签的邻接关系类
        /// </summary>
        public class MapNeighbour
        {
            public MapNeighbour()
            {
                Index = -1;
            }
            /// <summary>
            /// 当前点标签索引
            /// </summary>
            public int Index;
            /// <summary>
            /// 向左行驶可到达目标索引集合
            /// </summary>
            public Base.Left Left = new Base.Left();
            /// <summary>
            /// 向右行驶可到达目标索引集合
            /// </summary>
            public Base.Right Right = new Base.Right();
            /// <summary>
            /// 向上行驶可到达目标索引集合
            /// </summary>
            public Base.Up Up = new Base.Up();
            /// <summary>
            /// 向下行驶可到达目标索引集合
            /// </summary>
            public Base.Down Down = new Base.Down();

            public override string ToString()
            {
                if (Index == -1)
                    return "Null";
                else
                {
                    string str = String.Empty;
                    str += string.Format("---------【{0}】----------\r\n", MapElement.MapObject.RFIDS[Index].Num);

                    if (Left.Up != -1)
                        str += string.Format("左上：【{0}】\r\n", MapElement.MapObject.RFIDS[Left.Up].Num);
                    if (Left.Straight != -1)
                        str += string.Format("左直：【{0}】\r\n", MapElement.MapObject.RFIDS[Left.Straight].Num);
                    if (Left.Down != -1)
                        str += string.Format("左下：【{0}】\r\n", MapElement.MapObject.RFIDS[Left.Down].Num);

                    if (Right.Up != -1)
                        str += string.Format("右上：【{0}】\r\n", MapElement.MapObject.RFIDS[Right.Up].Num);
                    if (Right.Straight != -1)
                        str += string.Format("右直：【{0}】\r\n", MapElement.MapObject.RFIDS[Right.Straight].Num);
                    if (Right.Down != -1)
                        str += string.Format("右下：【{0}】\r\n", MapElement.MapObject.RFIDS[Right.Down].Num);


                    if (Up.Left != -1)
                        str += string.Format("上左：【{0}】\r\n", MapElement.MapObject.RFIDS[Up.Left].Num);
                    if (Up.Straight != -1)
                        str += string.Format("上直：【{0}】\r\n", MapElement.MapObject.RFIDS[Up.Straight].Num);
                    if (Up.Right != -1)
                        str += string.Format("上右：【{0}】\r\n", MapElement.MapObject.RFIDS[Up.Right].Num);

                    if (Down.Left != -1)
                        str += string.Format("下左：【{0}】\r\n", MapElement.MapObject.RFIDS[Down.Left].Num);
                    if (Down.Straight != -1)
                        str += string.Format("下直：【{0}】\r\n", MapElement.MapObject.RFIDS[Down.Straight].Num);
                    if (Down.Right != -1)
                        str += string.Format("下右：【{0}】\r\n", MapElement.MapObject.RFIDS[Down.Right].Num);
                    return str;
                }
            }
        }

        /// <summary>
        /// AGV上标签的邻接关系类
        /// </summary>
        public class AGVNeighbour
        {
            /// <summary>
            /// 当前点标签编号
            /// </summary>
            public int NowNum;
            public Go go = new Go();
            public Back back = new Back();
            public class Go
            {
                /// <summary>
                /// 左分叉
                /// </summary>
                public int LeftFork = -1;
                /// <summary>
                /// 直行
                /// </summary>
                public int Straight = -1;
                /// <summary>
                /// 右分叉
                /// </summary>
                public int RightFork = -1;
                /// <summary>
                /// 左旋
                /// </summary>
                public int TurnLeft = -1;
                /// <summary>
                /// 左旋角度
                /// </summary>
                public int AngleLeft = -1;
                /// <summary>
                /// 右旋
                /// </summary>
                public int TurnRight = -1;
                /// <summary>
                /// 右旋角度
                /// </summary>
                public int AngleRight = -1;
            }
            public class Back
            {
                /// <summary>
                /// 左分叉
                /// </summary>
                public int LeftFork = -1;
                /// <summary>
                /// 直行
                /// </summary>
                public int Straight = -1;
                /// <summary>
                /// 右分叉
                /// </summary>
                public int RightFork = -1;
                /// <summary>
                /// 左旋
                /// </summary>
                public int TurnLeft = -1;
                /// <summary>
                /// 左旋角度
                /// </summary>
                public int AngleLeft = -1;
                /// <summary>
                /// 右旋
                /// </summary>
                public int TurnRight = -1;
                /// <summary>
                /// 右旋角度
                /// </summary>
                public int AngleRight = -1;
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

        //返回List<String>类型结果
        //左上（分叉）
        //上右（分叉）
        //（向右直行）（到达）5（号标签）
        public static List<string> ProcessString(int index, Point point, ProcessState state, Base.Range range)
        {
            //是否打印调试信息
            bool debugPrint = false;
            //是否设置选中
            bool debugSetSelected = false;
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
                if (debugSetSelected)
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
                if (debugSetSelected)
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
                if (debugSetSelected)
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
                if (debugSetSelected)
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
                if (debugSetSelected)
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
                if (debugSetSelected)
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
                if (debugSetSelected)
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
                if (debugSetSelected)
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
                if (debugSetSelected)
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
                if (debugSetSelected)
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
                if (debugSetSelected)
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
                if (debugSetSelected)
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
        /// 三次搜索
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

        #region 搜索结果解析
        /// <summary>
        /// 将向左搜索结果解析成AGV可以识别的形式
        /// </summary>
        /// <param name="id">直行可以到达的标签索引</param>
        /// <param name="vs1">第一组搜索结果</param>
        /// <param name="vs2">第二组搜索结果</param>
        /// <param name="vs3">第三组搜索结果</param>
        /// <returns>0:向上分叉可到达的标签索引，1:直行可到达的标签索引，2:向下分叉可到达的标签索引</returns>
        public static List<int> AnalyLeftResault(int id, List<string> va1, List<string> va2, List<string> va3)
        {
            List<string> vs1 = va1 == null ? null : new List<string>(va1.ToArray());
            List<string> vs2 = va2 == null ? null : new List<string>(va2.ToArray());
            List<string> vs3 = va3 == null ? null : new List<string>(va3.ToArray());
            bool debug = false;
            //直行
            int Straight = -1;
            //上分叉
            int TurnUp = -1;
            //下分叉
            int TurnDown = -1;
            //有直接的直行标签
            if (id != -1)
            {
                Straight = id;
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
                        TurnUp = int.Parse(vs1.Last());
                        TurnDown = int.Parse(vs2.Last());
                    }
                    else
                    if (vs1.First().StartsWith("左下") && vs2.First().StartsWith("左上"))
                    {
                        TurnDown = int.Parse(vs1.Last());
                        TurnUp = int.Parse(vs2.Last());
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
                        TurnUp = int.Parse(vs1.Last());
                    else
                        TurnDown = int.Parse(vs1.Last());
                }
            }
            else
            //没有直行的标签
            {
                //有一个标签
                if (vs1 != null && vs2 == null && vs3 == null)
                {
                    Straight = int.Parse(vs1.Last());
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
                            TurnUp = int.Parse(vs1.Last());
                            TurnDown = int.Parse(vs2.Last());
                        }
                        else
                        if (vs1.First().StartsWith("左下") && vs2.First().StartsWith("左上"))
                        {
                            TurnDown = int.Parse(vs1.Last());
                            TurnUp = int.Parse(vs2.Last());
                        }
                        else
                        {
                            //获取分叉的坐标
                            int ids1 = int.Parse(vs1.First().Substring(2, vs1.First().Length - 2));
                            int ids2 = int.Parse(vs2.First().Substring(2, vs2.First().Length - 2));
                            Point pt1 = MapElement.MapObject.ForkLines[ids1].EndPoint;
                            Point pt2 = MapElement.MapObject.ForkLines[ids2].EndPoint;
                            if (vs1.First().StartsWith("左上") && vs2.First().StartsWith("左上"))
                            {
                                //靠左的是直行，靠右的上分叉
                                if (pt1.X < pt2.X)
                                {
                                    Straight = int.Parse(vs1.Last());
                                    TurnUp = int.Parse(vs2.Last());
                                }
                                else
                                {
                                    TurnUp = int.Parse(vs1.Last());
                                    Straight = int.Parse(vs2.Last());
                                }
                            }
                            else
                            if (vs1.First().StartsWith("左下") && vs2.First().StartsWith("左下"))
                            {
                                //靠左的是直行，靠右的下分叉
                                if (pt1.X < pt2.X)
                                {
                                    Straight = int.Parse(vs1.Last());
                                    TurnDown = int.Parse(vs2.Last());
                                }
                                else
                                {
                                    TurnDown = int.Parse(vs1.Last());
                                    Straight = int.Parse(vs2.Last());
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
                            TurnDown = int.Parse(vs1.Last());
                            TurnUp = int.Parse(vs2.Last());
                        }
                        else
                        if (vs1.First().StartsWith("右下") && vs2.First().StartsWith("右上"))
                        {
                            TurnUp = int.Parse(vs1.Last());
                            TurnDown = int.Parse(vs2.Last());
                        }
                        else
                        {
                            //获取分叉的坐标
                            int ids1 = int.Parse(vs1.First().Substring(2, vs1.First().Length - 2));
                            int ids2 = int.Parse(vs2.First().Substring(2, vs2.First().Length - 2));
                            Point pt1 = MapElement.MapObject.ForkLines[ids1].EndPoint;
                            Point pt2 = MapElement.MapObject.ForkLines[ids2].EndPoint;
                            if (vs1.First().StartsWith("右上") && vs2.First().StartsWith("右上"))
                            {
                                //靠右的是直行，靠左的下分叉
                                if (pt1.X > pt2.X)
                                {
                                    Straight = int.Parse(vs1.Last());
                                    TurnDown = int.Parse(vs2.Last());
                                }
                                else
                                {
                                    TurnDown = int.Parse(vs1.Last());
                                    Straight = int.Parse(vs2.Last());
                                }
                            }
                            else
                            if (vs1.First().StartsWith("右下") && vs2.First().StartsWith("右下"))
                            {
                                //靠右的是直行，靠左的上分叉
                                if (pt1.X > pt2.X)
                                {
                                    Straight = int.Parse(vs1.Last());
                                    TurnUp = int.Parse(vs2.Last());
                                }
                                else
                                {
                                    TurnUp = int.Parse(vs1.Last());
                                    Straight = int.Parse(vs2.Last());
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
                            TurnDown = int.Parse(vs1.Last());
                            TurnUp = int.Parse(vs2.Last());
                        }
                        else
                        if (vs1.First().StartsWith("上右") && vs2.First().StartsWith("上左"))
                        {
                            TurnUp = int.Parse(vs1.Last());
                            TurnDown = int.Parse(vs2.Last());
                        }
                        else
                        {
                            //获取分叉的坐标
                            int ids1 = int.Parse(vs1.First().Substring(2, vs1.First().Length - 2));
                            int ids2 = int.Parse(vs2.First().Substring(2, vs2.First().Length - 2));
                            Point pt1 = MapElement.MapObject.ForkLines[ids1].StartPoint;
                            Point pt2 = MapElement.MapObject.ForkLines[ids2].StartPoint;
                            if (vs1.First().StartsWith("上左") && vs2.First().StartsWith("上左"))
                            {
                                //靠上的是直行，靠下的下分叉
                                if (pt1.Y < pt2.Y)
                                {
                                    Straight = int.Parse(vs1.Last());
                                    TurnDown = int.Parse(vs2.Last());
                                }
                                else
                                {
                                    TurnDown = int.Parse(vs1.Last());
                                    Straight = int.Parse(vs2.Last());
                                }
                            }
                            else
                            if (vs1.First().StartsWith("上右") && vs2.First().StartsWith("上右"))
                            {
                                //靠上的是直行，靠下的上分叉
                                if (pt1.Y < pt2.Y)
                                {
                                    Straight = int.Parse(vs1.Last());
                                    TurnUp = int.Parse(vs2.Last());
                                }
                                else
                                {
                                    TurnUp = int.Parse(vs1.Last());
                                    Straight = int.Parse(vs2.Last());
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
                            TurnUp = int.Parse(vs1.Last());
                            TurnDown = int.Parse(vs2.Last());
                        }
                        else
                        if (vs1.First().StartsWith("下右") && vs2.First().StartsWith("下左"))
                        {
                            TurnDown = int.Parse(vs1.Last());
                            TurnUp = int.Parse(vs2.Last());
                        }
                        else
                        {
                            //获取分叉的坐标
                            int ids1 = int.Parse(vs1.First().Substring(2, vs1.First().Length - 2));
                            int ids2 = int.Parse(vs2.First().Substring(2, vs2.First().Length - 2));
                            Point pt1 = MapElement.MapObject.ForkLines[ids1].StartPoint;
                            Point pt2 = MapElement.MapObject.ForkLines[ids2].StartPoint;
                            if (vs1.First().StartsWith("下左") && vs2.First().StartsWith("下左"))
                            {
                                //靠下的是直行，靠上的上分叉
                                if (pt1.Y > pt2.Y)
                                {
                                    Straight = int.Parse(vs1.Last());
                                    TurnUp = int.Parse(vs2.Last());
                                }
                                else
                                {
                                    TurnUp = int.Parse(vs1.Last());
                                    Straight = int.Parse(vs2.Last());
                                }
                            }
                            else
                            if (vs1.First().StartsWith("下右") && vs2.First().StartsWith("下右"))
                            {
                                //靠下的是直行，靠上的下分叉
                                if (pt1.Y > pt2.Y)
                                {
                                    Straight = int.Parse(vs1.Last());
                                    TurnDown = int.Parse(vs2.Last());
                                }
                                else
                                {
                                    TurnDown = int.Parse(vs1.Last());
                                    Straight = int.Parse(vs2.Last());
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
                            Straight = int.Parse(vs1.Last());
                            v1 = vs2;
                            v2 = vs3;
                        }
                        else
                        if (vs2.Count == 1)
                        {
                            //直行到达
                            Straight = int.Parse(vs2.Last());
                            v1 = vs1;
                            v2 = vs3;
                        }
                        else
                        if (vs3.Count == 1)
                        {
                            //直行到达
                            Straight = int.Parse(vs3.Last());
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
                                TurnUp = int.Parse(v1.Last());
                                TurnDown = int.Parse(v2.Last());
                            }
                            else
                            if (v1.First().StartsWith("左下") && v2.First().StartsWith("左上"))
                            {
                                TurnDown = int.Parse(v1.Last());
                                TurnUp = int.Parse(v2.Last());
                            }
                        }
                        else
                        if (v1.First().StartsWith("右") && v2.First().StartsWith("右"))
                        {
                            if (v1.First().StartsWith("右上") && v2.First().StartsWith("右下"))
                            {
                                TurnDown = int.Parse(v1.Last());
                                TurnUp = int.Parse(v2.Last());
                            }
                            else
                            if (v1.First().StartsWith("右下") && v2.First().StartsWith("右上"))
                            {
                                TurnUp = int.Parse(v1.Last());
                                TurnDown = int.Parse(v2.Last());
                            }
                        }
                        else
                        if (v1.First().StartsWith("上") && v2.First().StartsWith("上"))
                        {
                            if (v1.First().StartsWith("上左") && v2.First().StartsWith("上右"))
                            {
                                TurnDown = int.Parse(v1.Last());
                                TurnUp = int.Parse(v2.Last());
                            }
                            else
                            if (v1.First().StartsWith("上右") && v2.First().StartsWith("上左"))
                            {
                                TurnUp = int.Parse(v1.Last());
                                TurnDown = int.Parse(v2.Last());
                            }
                        }
                        else
                        if (v1.First().StartsWith("下") && v2.First().StartsWith("下"))
                        {
                            if (v1.First().StartsWith("下左") && v2.First().StartsWith("下右"))
                            {
                                TurnUp = int.Parse(v1.Last());
                                TurnDown = int.Parse(v2.Last());
                            }
                            else
                            if (v1.First().StartsWith("下右") && v2.First().StartsWith("下左"))
                            {
                                TurnDown = int.Parse(v1.Last());
                                TurnUp = int.Parse(v2.Last());
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
                                TurnUp = int.Parse(vs1.Last());
                                v1 = vs2;
                                v2 = vs3;
                            }
                            else
                            if (vs1.First().StartsWith("左下") && vs2.First().StartsWith("左上") && vs3.First().StartsWith("左下"))
                            {
                                TurnUp = int.Parse(vs2.Last());
                                v1 = vs1;
                                v2 = vs3;
                            }
                            else
                            if (vs1.First().StartsWith("左下") && vs2.First().StartsWith("左下") && vs3.First().StartsWith("左上"))
                            {
                                TurnUp = int.Parse(vs3.Last());
                                v1 = vs1;
                                v2 = vs2;
                            }
                            else
                            if (vs1.First().StartsWith("左下") && vs2.First().StartsWith("左上") && vs3.First().StartsWith("左上"))
                            {
                                TurnDown = int.Parse(vs1.Last());
                                v1 = vs2;
                                v2 = vs3;
                            }
                            else
                            if (vs1.First().StartsWith("左上") && vs2.First().StartsWith("左下") && vs3.First().StartsWith("左上"))
                            {
                                TurnDown = int.Parse(vs2.Last());
                                v1 = vs1;
                                v2 = vs3;
                            }
                            else
                            if (vs1.First().StartsWith("左上") && vs2.First().StartsWith("左上") && vs3.First().StartsWith("左下"))
                            {
                                TurnDown = int.Parse(vs3.Last());
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
                                TurnDown = int.Parse(vs1.Last());
                                v1 = vs2;
                                v2 = vs3;
                            }
                            else
                            if (vs1.First().StartsWith("右下") && vs2.First().StartsWith("右上") && vs3.First().StartsWith("右下"))
                            {
                                TurnDown = int.Parse(vs2.Last());
                                v1 = vs1;
                                v2 = vs3;
                            }
                            else
                            if (vs1.First().StartsWith("右下") && vs2.First().StartsWith("右下") && vs3.First().StartsWith("右上"))
                            {
                                TurnDown = int.Parse(vs3.Last());
                                v1 = vs1;
                                v2 = vs2;
                            }
                            else
                            if (vs1.First().StartsWith("右下") && vs2.First().StartsWith("右上") && vs3.First().StartsWith("右上"))
                            {
                                TurnUp = int.Parse(vs1.Last());
                                v1 = vs2;
                                v2 = vs3;
                            }
                            else
                            if (vs1.First().StartsWith("右上") && vs2.First().StartsWith("右下") && vs3.First().StartsWith("右上"))
                            {
                                TurnUp = int.Parse(vs2.Last());
                                v1 = vs1;
                                v2 = vs3;
                            }
                            else
                            if (vs1.First().StartsWith("右上") && vs2.First().StartsWith("右上") && vs3.First().StartsWith("右下"))
                            {
                                TurnUp = int.Parse(vs3.Last());
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
                                TurnDown = int.Parse(vs1.Last());
                                v1 = vs2;
                                v2 = vs3;
                            }
                            else
                            if (vs1.First().StartsWith("上右") && vs2.First().StartsWith("上左") && vs3.First().StartsWith("上右"))
                            {
                                TurnDown = int.Parse(vs2.Last());
                                v1 = vs1;
                                v2 = vs3;
                            }
                            else
                            if (vs1.First().StartsWith("上右") && vs2.First().StartsWith("上右") && vs3.First().StartsWith("上左"))
                            {
                                TurnDown = int.Parse(vs3.Last());
                                v1 = vs1;
                                v2 = vs2;
                            }
                            else
                            if (vs1.First().StartsWith("上右") && vs2.First().StartsWith("上左") && vs3.First().StartsWith("上左"))
                            {
                                TurnUp = int.Parse(vs1.Last());
                                v1 = vs2;
                                v2 = vs3;
                            }
                            else
                            if (vs1.First().StartsWith("上左") && vs2.First().StartsWith("上右") && vs3.First().StartsWith("上左"))
                            {
                                TurnUp = int.Parse(vs2.Last());
                                v1 = vs1;
                                v2 = vs3;
                            }
                            else
                            if (vs1.First().StartsWith("上左") && vs2.First().StartsWith("上左") && vs3.First().StartsWith("上右"))
                            {
                                TurnUp = int.Parse(vs3.Last());
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
                                TurnUp = int.Parse(vs1.Last());
                                v1 = vs2;
                                v2 = vs3;
                            }
                            else
                              if (vs1.First().StartsWith("下右") && vs2.First().StartsWith("下左") && vs3.First().StartsWith("下右"))
                            {
                                TurnUp = int.Parse(vs2.Last());
                                v1 = vs1;
                                v2 = vs3;
                            }
                            else
                              if (vs1.First().StartsWith("下右") && vs2.First().StartsWith("下右") && vs3.First().StartsWith("下左"))
                            {
                                TurnUp = int.Parse(vs3.Last());
                                v1 = vs1;
                                v2 = vs2;
                            }
                            else
                              if (vs1.First().StartsWith("下右") && vs2.First().StartsWith("下左") && vs3.First().StartsWith("下左"))
                            {
                                TurnDown = int.Parse(vs1.Last());
                                v1 = vs2;
                                v2 = vs3;
                            }
                            else
                              if (vs1.First().StartsWith("下左") && vs2.First().StartsWith("下右") && vs3.First().StartsWith("下左"))
                            {
                                TurnDown = int.Parse(vs2.Last());
                                v1 = vs1;
                                v2 = vs3;
                            }
                            else
                              if (vs1.First().StartsWith("下左") && vs2.First().StartsWith("下左") && vs3.First().StartsWith("下右"))
                            {
                                TurnDown = int.Parse(vs3.Last());
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
                            //获取分叉索引
                            int ids1 = int.Parse(v1.First().Substring(2, v1.First().Length - 2));
                            int ids2 = int.Parse(v2.First().Substring(2, v2.First().Length - 2));
                            if (v1.First().StartsWith("左") && v2.First().StartsWith("左"))
                            {
                                Point pt1 = MapElement.MapObject.ForkLines[ids1].EndPoint;
                                Point pt2 = MapElement.MapObject.ForkLines[ids2].EndPoint;
                                //靠左的是直行
                                if (pt1.X < pt2.X)
                                {
                                    Straight = int.Parse(v1.Last());
                                    if (v2.First().StartsWith("左下") && TurnDown != -1)
                                    {
                                        MapOperate.SystemMsg.WriteLine("错误：出现了两个左下！");
                                        return null;
                                    }
                                    else
                                    if (v2.First().StartsWith("左上") && TurnUp != -1)
                                    {
                                        MapOperate.SystemMsg.WriteLine("错误：出现了两个左上！");
                                        return null;
                                    }
                                    else
                                    {
                                        if (TurnUp != -1)
                                            TurnDown = int.Parse(v2.Last());
                                        else
                                            TurnUp = int.Parse(v2.Last());
                                    }
                                }
                                else
                                {
                                    if (v1.First().StartsWith("左下") && TurnDown != -1)
                                    {
                                        MapOperate.SystemMsg.WriteLine("错误：出现了两个左下！");
                                        return null;
                                    }
                                    else
                                    if (v1.First().StartsWith("左上") && TurnUp != -1)
                                    {
                                        MapOperate.SystemMsg.WriteLine("错误：出现了两个左上！");
                                        return null;
                                    }
                                    else
                                    {
                                        if (TurnUp != -1)
                                            TurnDown = int.Parse(v1.Last());
                                        else
                                            TurnUp = int.Parse(v1.Last());
                                    }
                                    Straight = int.Parse(v2.Last());
                                }
                            }
                            else
                            if (v1.First().StartsWith("右") && v2.First().StartsWith("右"))
                            {
                                //获取分叉的坐标
                                Point pt1 = MapElement.MapObject.ForkLines[ids1].EndPoint;
                                Point pt2 = MapElement.MapObject.ForkLines[ids2].EndPoint;
                                //靠右的是直行
                                if (pt1.X > pt2.X)
                                {
                                    Straight = int.Parse(v1.Last());
                                    //右下对应左上
                                    if (v2.First().StartsWith("右下") && TurnUp != -1)
                                    {
                                        MapOperate.SystemMsg.WriteLine("错误：出现了两个左上！");
                                        return null;
                                    }
                                    else
                                    if (v2.First().StartsWith("右上") && TurnDown != -1)
                                    {
                                        MapOperate.SystemMsg.WriteLine("错误：出现了两个左下！");
                                        return null;
                                    }
                                    else
                                    {
                                        if (TurnUp != -1)
                                            TurnDown = int.Parse(v2.Last());
                                        else
                                            TurnUp = int.Parse(v2.Last());
                                    }
                                }
                                else
                                {
                                    //右下对应左上
                                    if (v1.First().StartsWith("右下") && TurnUp != -1)
                                    {
                                        MapOperate.SystemMsg.WriteLine("错误：出现了两个左上！");
                                        return null;
                                    }
                                    else
                                    if (v1.First().StartsWith("右上") && TurnDown != -1)
                                    {
                                        MapOperate.SystemMsg.WriteLine("错误：出现了两个左下！");
                                        return null;
                                    }
                                    else
                                    {
                                        if (TurnUp != -1)
                                            TurnDown = int.Parse(v1.Last());
                                        else
                                            TurnUp = int.Parse(v1.Last());
                                    }
                                    Straight = int.Parse(v2.Last());
                                }
                            }
                            else
                            if (v1.First().StartsWith("上") && v2.First().StartsWith("上"))
                            {
                                //获取分叉的坐标
                                Point pt1 = MapElement.MapObject.ForkLines[ids1].StartPoint;
                                Point pt2 = MapElement.MapObject.ForkLines[ids2].StartPoint;
                                //靠上的是直行
                                if (pt1.Y < pt2.Y)
                                {
                                    Straight = int.Parse(v1.Last());
                                    //上左对应左下
                                    if (v2.First().StartsWith("上左") && TurnDown != -1)
                                    {
                                        MapOperate.SystemMsg.WriteLine("错误：出现了两个左下！");
                                        return null;
                                    }
                                    else
                                    if (v2.First().StartsWith("上右") && TurnUp != -1)
                                    {
                                        MapOperate.SystemMsg.WriteLine("错误：出现了两个左上！");
                                        return null;
                                    }
                                    else
                                    {
                                        if (TurnUp != -1)
                                            TurnDown = int.Parse(v2.Last());
                                        else
                                            TurnUp = int.Parse(v2.Last());
                                    }
                                }
                                else
                                {
                                    //上左对应左下
                                    if (v1.First().StartsWith("上左") && TurnDown != -1)
                                    {
                                        MapOperate.SystemMsg.WriteLine("错误：出现了两个左下！");
                                        return null;
                                    }
                                    else
                                    if (v1.First().StartsWith("上右") && TurnUp != -1)
                                    {
                                        MapOperate.SystemMsg.WriteLine("错误：出现了两个左上！");
                                        return null;
                                    }
                                    else
                                    {
                                        if (TurnUp != -1)
                                            TurnDown = int.Parse(v1.Last());
                                        else
                                            TurnUp = int.Parse(v1.Last());
                                    }
                                    Straight = int.Parse(v2.Last());
                                }
                            }
                            else
                            if (v1.First().StartsWith("下") && v2.First().StartsWith("下"))
                            {
                                Point pt1 = MapElement.MapObject.ForkLines[ids1].StartPoint;
                                Point pt2 = MapElement.MapObject.ForkLines[ids2].StartPoint;
                                //靠下的是直行
                                if (pt1.Y > pt2.Y)
                                {
                                    Straight = int.Parse(v1.Last());
                                    //下左对应左上
                                    if (v2.First().StartsWith("下左") && TurnUp != -1)
                                    {
                                        MapOperate.SystemMsg.WriteLine("错误：出现了两个左上！");
                                        return null;
                                    }
                                    else
                                    if (v2.First().StartsWith("下右") && TurnDown != -1)
                                    {
                                        MapOperate.SystemMsg.WriteLine("错误：出现了两个左下！");
                                        return null;
                                    }
                                    else
                                    {
                                        if (TurnUp != -1)
                                            TurnDown = int.Parse(v2.Last());
                                        else
                                            TurnUp = int.Parse(v2.Last());
                                    }
                                }
                                else
                                {
                                    //下左对应左上
                                    if (v1.First().StartsWith("下左") && TurnUp != -1)
                                    {
                                        MapOperate.SystemMsg.WriteLine("错误：出现了两个左上！");
                                        return null;
                                    }
                                    else
                                    if (v1.First().StartsWith("下右") && TurnDown != -1)
                                    {
                                        MapOperate.SystemMsg.WriteLine("错误：出现了两个左下！");
                                        return null;
                                    }
                                    else
                                    {
                                        if (TurnUp != -1)
                                            TurnDown = int.Parse(v1.Last());
                                        else
                                            TurnUp = int.Parse(v1.Last());
                                    }
                                    Straight = int.Parse(v2.Last());
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
            if (debug)
                MapOperate.SystemMsg.WriteLine("向左：上分叉【{0}】直行【{1}】下分叉【{2}】",
                    TurnUp == -1 ? TurnUp : MapElement.MapObject.RFIDS[TurnUp].Num,
                    Straight == -1 ? Straight : MapElement.MapObject.RFIDS[Straight].Num,
                    TurnDown == -1 ? TurnDown : MapElement.MapObject.RFIDS[TurnDown].Num);
            List<int> rs = new List<int>();
            rs.Add(TurnUp);
            rs.Add(Straight);
            rs.Add(TurnDown);
            return rs;
        }
        /// <summary>
        /// 将向右搜索结果解析成AGV可以识别的形式
        /// </summary>
        /// <param name="id">直行可以到达的标签索引</param>
        /// <param name="vs1">第一组搜索结果</param>
        /// <param name="vs2">第二组搜索结果</param>
        /// <param name="vs3">第三组搜索结果</param>
        /// <returns>0:向上分叉可到达的标签索引，1:直行可到达的标签索引，2:向下分叉可到达的标签索引</returns>
        public static List<int> AnalyRightResault(int id, List<string> va1, List<string> va2, List<string> va3)
        {
            List<string> vs1 = va1 == null ? null : new List<string>(va1.ToArray());
            List<string> vs2 = va2 == null ? null : new List<string>(va2.ToArray());
            List<string> vs3 = va3 == null ? null : new List<string>(va3.ToArray());
            bool debug = false;
            //直行
            int Straight = -1;
            //上分叉
            int TurnUp = -1;
            //下分叉
            int TurnDown = -1;
            //有直接的直行标签
            if (id != -1)
            {
                Straight = id;
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
                    if (vs1.First().StartsWith("右上") && vs2.First().StartsWith("右上"))
                    {
                        MapOperate.SystemMsg.WriteLine("错误：上分叉出现了两个标签！");
                        return null;
                    }
                    else
                    if (vs1.First().StartsWith("右下") && vs2.First().StartsWith("右下"))
                    {
                        MapOperate.SystemMsg.WriteLine("错误：下分叉出现了两个标签！");
                        return null;
                    }
                    else
                    if (vs1.First().StartsWith("右上") && vs2.First().StartsWith("右下"))
                    {
                        TurnUp = int.Parse(vs1.Last());
                        TurnDown = int.Parse(vs2.Last());
                    }
                    else
                    if (vs1.First().StartsWith("右下") && vs2.First().StartsWith("右上"))
                    {
                        TurnDown = int.Parse(vs1.Last());
                        TurnUp = int.Parse(vs2.Last());
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
                    if (vs1.First().StartsWith("右上"))
                        TurnUp = int.Parse(vs1.Last());
                    else
                        TurnDown = int.Parse(vs1.Last());
                }
            }
            else
            //没有直行的标签
            {
                //有一个标签
                if (vs1 != null && vs2 == null && vs3 == null)
                {
                    Straight = int.Parse(vs1.Last());
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
                            TurnDown = int.Parse(vs1.Last());
                            TurnUp = int.Parse(vs2.Last());
                        }
                        else
                        if (vs1.First().StartsWith("左下") && vs2.First().StartsWith("左上"))
                        {
                            TurnUp = int.Parse(vs1.Last());
                            TurnDown = int.Parse(vs2.Last());
                        }
                        else
                        {
                            //获取分叉的坐标
                            int ids1 = int.Parse(vs1.First().Substring(2, vs1.First().Length - 2));
                            int ids2 = int.Parse(vs2.First().Substring(2, vs2.First().Length - 2));
                            Point pt1 = MapElement.MapObject.ForkLines[ids1].EndPoint;
                            Point pt2 = MapElement.MapObject.ForkLines[ids2].EndPoint;
                            if (vs1.First().StartsWith("左上") && vs2.First().StartsWith("左上"))
                            {
                                //靠左的是直行，靠右的下分叉
                                //靠左的是直行，靠右的下分叉
                                if (pt1.X < pt2.X)
                                {
                                    Straight = int.Parse(vs1.Last());
                                    TurnDown = int.Parse(vs2.Last());
                                }
                                else
                                {
                                    TurnDown = int.Parse(vs1.Last());
                                    Straight = int.Parse(vs2.Last());
                                }
                            }
                            else
                            if (vs1.First().StartsWith("左下") && vs2.First().StartsWith("左下"))
                            {
                                if (pt1.X < pt2.X)
                                {
                                    Straight = int.Parse(vs1.Last());
                                    TurnUp = int.Parse(vs2.Last());
                                }
                                else
                                {
                                    TurnUp = int.Parse(vs1.Last());
                                    Straight = int.Parse(vs2.Last());
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
                            TurnUp = int.Parse(vs1.Last());
                            TurnDown = int.Parse(vs2.Last());
                        }
                        else
                        if (vs1.First().StartsWith("右下") && vs2.First().StartsWith("右上"))
                        {
                            TurnDown = int.Parse(vs1.Last());
                            TurnUp = int.Parse(vs2.Last());
                        }
                        else
                        {
                            //获取分叉的坐标
                            int ids1 = int.Parse(vs1.First().Substring(2, vs1.First().Length - 2));
                            int ids2 = int.Parse(vs2.First().Substring(2, vs2.First().Length - 2));
                            Point pt1 = MapElement.MapObject.ForkLines[ids1].EndPoint;
                            Point pt2 = MapElement.MapObject.ForkLines[ids2].EndPoint;
                            if (vs1.First().StartsWith("右上") && vs2.First().StartsWith("右上"))
                            {
                                //靠右的是直行，靠左的上分叉
                                if (pt1.X > pt2.X)
                                {
                                    Straight = int.Parse(vs1.Last());
                                    TurnUp = int.Parse(vs2.Last());
                                }
                                else
                                {
                                    TurnUp = int.Parse(vs1.Last());
                                    Straight = int.Parse(vs2.Last());
                                }
                            }
                            else
                            if (vs1.First().StartsWith("右下") && vs2.First().StartsWith("右下"))
                            {
                                //靠右的是直行，靠左的下分叉
                                if (pt1.X > pt2.X)
                                {
                                    Straight = int.Parse(vs1.Last());
                                    TurnDown = int.Parse(vs2.Last());
                                }
                                else
                                {
                                    TurnDown = int.Parse(vs1.Last());
                                    Straight = int.Parse(vs2.Last());
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
                            TurnUp = int.Parse(vs1.Last());
                            TurnDown = int.Parse(vs2.Last());
                        }
                        else
                        if (vs1.First().StartsWith("上右") && vs2.First().StartsWith("上左"))
                        {
                            TurnDown = int.Parse(vs1.Last());
                            TurnUp = int.Parse(vs2.Last());
                        }
                        else
                        {
                            //获取分叉的坐标
                            int ids1 = int.Parse(vs1.First().Substring(2, vs1.First().Length - 2));
                            int ids2 = int.Parse(vs2.First().Substring(2, vs2.First().Length - 2));
                            Point pt1 = MapElement.MapObject.ForkLines[ids1].StartPoint;
                            Point pt2 = MapElement.MapObject.ForkLines[ids2].StartPoint;
                            if (vs1.First().StartsWith("上左") && vs2.First().StartsWith("上左"))
                            {
                                //靠上的是直行，靠下的上分叉
                                if (pt1.Y < pt2.Y)
                                {
                                    Straight = int.Parse(vs1.Last());
                                    TurnUp = int.Parse(vs2.Last());
                                }
                                else
                                {
                                    TurnUp = int.Parse(vs1.Last());
                                    Straight = int.Parse(vs2.Last());
                                }
                            }
                            else
                            if (vs1.First().StartsWith("上右") && vs2.First().StartsWith("上右"))
                            {
                                //靠上的是直行，靠下的下分叉
                                if (pt1.Y < pt2.Y)
                                {
                                    Straight = int.Parse(vs1.Last());
                                    TurnDown = int.Parse(vs2.Last());
                                }
                                else
                                {
                                    TurnDown = int.Parse(vs1.Last());
                                    Straight = int.Parse(vs2.Last());
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
                            TurnDown = int.Parse(vs1.Last());
                            TurnUp = int.Parse(vs2.Last());
                        }
                        else
                        if (vs1.First().StartsWith("下右") && vs2.First().StartsWith("下左"))
                        {
                            TurnUp = int.Parse(vs1.Last());
                            TurnDown = int.Parse(vs2.Last());
                        }
                        else
                        {
                            //获取分叉的坐标
                            int ids1 = int.Parse(vs1.First().Substring(2, vs1.First().Length - 2));
                            int ids2 = int.Parse(vs2.First().Substring(2, vs2.First().Length - 2));
                            Point pt1 = MapElement.MapObject.ForkLines[ids1].StartPoint;
                            Point pt2 = MapElement.MapObject.ForkLines[ids2].StartPoint;
                            if (vs1.First().StartsWith("下左") && vs2.First().StartsWith("下左"))
                            {
                                //靠下的是直行，靠上的下分叉
                                if (pt1.Y > pt2.Y)
                                {
                                    Straight = int.Parse(vs1.Last());
                                    TurnDown = int.Parse(vs2.Last());
                                }
                                else
                                {
                                    TurnDown = int.Parse(vs1.Last());
                                    Straight = int.Parse(vs2.Last());
                                }
                            }
                            else
                            if (vs1.First().StartsWith("下右") && vs2.First().StartsWith("下右"))
                            {
                                //靠下的是直行，靠上的上分叉
                                if (pt1.Y > pt2.Y)
                                {
                                    Straight = int.Parse(vs1.Last());
                                    TurnUp = int.Parse(vs2.Last());
                                }
                                else
                                {
                                    TurnUp = int.Parse(vs1.Last());
                                    Straight = int.Parse(vs2.Last());
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
                            Straight = int.Parse(vs1.Last());
                            v1 = vs2;
                            v2 = vs3;
                        }
                        else
                        if (vs2.Count == 1)
                        {
                            //直行到达
                            Straight = int.Parse(vs2.Last());
                            v1 = vs1;
                            v2 = vs3;
                        }
                        else
                        if (vs3.Count == 1)
                        {
                            //直行到达
                            Straight = int.Parse(vs3.Last());
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
                                TurnDown = int.Parse(v1.Last());
                                TurnUp = int.Parse(v2.Last());
                            }
                            else
                            if (v1.First().StartsWith("左下") && v2.First().StartsWith("左上"))
                            {
                                TurnUp = int.Parse(v1.Last());
                                TurnDown = int.Parse(v2.Last());
                            }
                        }
                        else
                        if (v1.First().StartsWith("右") && v2.First().StartsWith("右"))
                        {
                            if (v1.First().StartsWith("右上") && v2.First().StartsWith("右下"))
                            {
                                TurnUp = int.Parse(v1.Last());
                                TurnDown = int.Parse(v2.Last());
                            }
                            else
                            if (v1.First().StartsWith("右下") && v2.First().StartsWith("右上"))
                            {
                                TurnDown = int.Parse(v1.Last());
                                TurnUp = int.Parse(v2.Last());
                            }
                        }
                        else
                        if (v1.First().StartsWith("上") && v2.First().StartsWith("上"))
                        {
                            if (v1.First().StartsWith("上左") && v2.First().StartsWith("上右"))
                            {
                                TurnUp = int.Parse(v1.Last());
                                TurnDown = int.Parse(v2.Last());
                            }
                            else
                            if (v1.First().StartsWith("上右") && v2.First().StartsWith("上左"))
                            {
                                TurnDown = int.Parse(v1.Last());
                                TurnUp = int.Parse(v2.Last());
                            }
                        }
                        else
                        if (v1.First().StartsWith("下") && v2.First().StartsWith("下"))
                        {
                            if (v1.First().StartsWith("下左") && v2.First().StartsWith("下右"))
                            {
                                TurnDown = int.Parse(v1.Last());
                                TurnUp = int.Parse(v2.Last());
                            }
                            else
                            if (v1.First().StartsWith("下右") && v2.First().StartsWith("下左"))
                            {
                                TurnUp = int.Parse(v1.Last());
                                TurnDown = int.Parse(v2.Last());
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
                                TurnDown = int.Parse(vs1.Last());
                                v1 = vs2;
                                v2 = vs3;
                            }
                            else
                            if (vs1.First().StartsWith("左下") && vs2.First().StartsWith("左上") && vs3.First().StartsWith("左下"))
                            {
                                TurnDown = int.Parse(vs2.Last());
                                v1 = vs1;
                                v2 = vs3;
                            }
                            else
                            if (vs1.First().StartsWith("左下") && vs2.First().StartsWith("左下") && vs3.First().StartsWith("左上"))
                            {
                                TurnDown = int.Parse(vs3.Last());
                                v1 = vs1;
                                v2 = vs2;
                            }
                            else
                            if (vs1.First().StartsWith("左下") && vs2.First().StartsWith("左上") && vs3.First().StartsWith("左上"))
                            {
                                TurnUp = int.Parse(vs1.Last());
                                v1 = vs2;
                                v2 = vs3;
                            }
                            else
                            if (vs1.First().StartsWith("左上") && vs2.First().StartsWith("左下") && vs3.First().StartsWith("左上"))
                            {
                                TurnUp = int.Parse(vs2.Last());
                                v1 = vs1;
                                v2 = vs3;
                            }
                            else
                            if (vs1.First().StartsWith("左上") && vs2.First().StartsWith("左上") && vs3.First().StartsWith("左下"))
                            {
                                TurnUp = int.Parse(vs3.Last());
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
                                TurnUp = int.Parse(vs1.Last());
                                v1 = vs2;
                                v2 = vs3;
                            }
                            else
                            if (vs1.First().StartsWith("右下") && vs2.First().StartsWith("右上") && vs3.First().StartsWith("右下"))
                            {
                                TurnUp = int.Parse(vs2.Last());
                                v1 = vs1;
                                v2 = vs3;
                            }
                            else
                            if (vs1.First().StartsWith("右下") && vs2.First().StartsWith("右下") && vs3.First().StartsWith("右上"))
                            {
                                TurnUp = int.Parse(vs3.Last());
                                v1 = vs1;
                                v2 = vs2;
                            }
                            else
                            if (vs1.First().StartsWith("右下") && vs2.First().StartsWith("右上") && vs3.First().StartsWith("右上"))
                            {
                                TurnDown = int.Parse(vs1.Last());
                                v1 = vs2;
                                v2 = vs3;
                            }
                            else
                            if (vs1.First().StartsWith("右上") && vs2.First().StartsWith("右下") && vs3.First().StartsWith("右上"))
                            {
                                TurnDown = int.Parse(vs2.Last());
                                v1 = vs1;
                                v2 = vs3;
                            }
                            else
                            if (vs1.First().StartsWith("右上") && vs2.First().StartsWith("右上") && vs3.First().StartsWith("右下"))
                            {
                                TurnDown = int.Parse(vs3.Last());
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
                                TurnUp = int.Parse(vs1.Last());
                                v1 = vs2;
                                v2 = vs3;
                            }
                            else
                            if (vs1.First().StartsWith("上右") && vs2.First().StartsWith("上左") && vs3.First().StartsWith("上右"))
                            {
                                TurnUp = int.Parse(vs2.Last());
                                v1 = vs1;
                                v2 = vs3;
                            }
                            else
                            if (vs1.First().StartsWith("上右") && vs2.First().StartsWith("上右") && vs3.First().StartsWith("上左"))
                            {
                                TurnUp = int.Parse(vs3.Last());
                                v1 = vs1;
                                v2 = vs2;
                            }
                            else
                            if (vs1.First().StartsWith("上右") && vs2.First().StartsWith("上左") && vs3.First().StartsWith("上左"))
                            {
                                TurnDown = int.Parse(vs1.Last());
                                v1 = vs2;
                                v2 = vs3;
                            }
                            else
                            if (vs1.First().StartsWith("上左") && vs2.First().StartsWith("上右") && vs3.First().StartsWith("上左"))
                            {
                                TurnDown = int.Parse(vs2.Last());
                                v1 = vs1;
                                v2 = vs3;
                            }
                            else
                            if (vs1.First().StartsWith("上左") && vs2.First().StartsWith("上左") && vs3.First().StartsWith("上右"))
                            {
                                TurnDown = int.Parse(vs3.Last());
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
                                TurnDown = int.Parse(vs1.Last());
                                v1 = vs2;
                                v2 = vs3;
                            }
                            else
                              if (vs1.First().StartsWith("下右") && vs2.First().StartsWith("下左") && vs3.First().StartsWith("下右"))
                            {
                                TurnDown = int.Parse(vs2.Last());
                                v1 = vs1;
                                v2 = vs3;
                            }
                            else
                              if (vs1.First().StartsWith("下右") && vs2.First().StartsWith("下右") && vs3.First().StartsWith("下左"))
                            {
                                TurnDown = int.Parse(vs3.Last());
                                v1 = vs1;
                                v2 = vs2;
                            }
                            else
                              if (vs1.First().StartsWith("下右") && vs2.First().StartsWith("下左") && vs3.First().StartsWith("下左"))
                            {
                                TurnUp = int.Parse(vs1.Last());
                                v1 = vs2;
                                v2 = vs3;
                            }
                            else
                              if (vs1.First().StartsWith("下左") && vs2.First().StartsWith("下右") && vs3.First().StartsWith("下左"))
                            {
                                TurnUp = int.Parse(vs2.Last());
                                v1 = vs1;
                                v2 = vs3;
                            }
                            else
                              if (vs1.First().StartsWith("下左") && vs2.First().StartsWith("下左") && vs3.First().StartsWith("下右"))
                            {
                                TurnUp = int.Parse(vs3.Last());
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
                            //获取分叉索引
                            int ids1 = int.Parse(v1.First().Substring(2, v1.First().Length - 2));
                            int ids2 = int.Parse(v2.First().Substring(2, v2.First().Length - 2));
                            if (v1.First().StartsWith("左") && v2.First().StartsWith("左"))
                            {
                                Point pt1 = MapElement.MapObject.ForkLines[ids1].EndPoint;
                                Point pt2 = MapElement.MapObject.ForkLines[ids2].EndPoint;
                                //靠左的是直行
                                if (pt1.X < pt2.X)
                                {
                                    Straight = int.Parse(v1.Last());
                                    if (v2.First().StartsWith("左下") && TurnUp != -1)
                                    {
                                        MapOperate.SystemMsg.WriteLine("错误：出现了两个右上！");
                                        return null;
                                    }
                                    else
                                    if (v2.First().StartsWith("左上") && TurnDown != -1)
                                    {
                                        MapOperate.SystemMsg.WriteLine("错误：出现了两个右下！");
                                        return null;
                                    }
                                    else
                                    {
                                        if (TurnUp != -1)
                                            TurnDown = int.Parse(v2.Last());
                                        else
                                            TurnUp = int.Parse(v2.Last());
                                    }
                                }
                                else
                                {
                                    if (v1.First().StartsWith("左下") && TurnUp != -1)
                                    {
                                        MapOperate.SystemMsg.WriteLine("错误：出现了两个右上！");
                                        return null;
                                    }
                                    else
                                    if (v1.First().StartsWith("左上") && TurnDown != -1)
                                    {
                                        MapOperate.SystemMsg.WriteLine("错误：出现了两个右下！");
                                        return null;
                                    }
                                    else
                                    {
                                        if (TurnUp != -1)
                                            TurnDown = int.Parse(v1.Last());
                                        else
                                            TurnUp = int.Parse(v1.Last());
                                    }
                                    Straight = int.Parse(v2.Last());
                                }
                            }
                            else
                            if (v1.First().StartsWith("右") && v2.First().StartsWith("右"))
                            {
                                //获取分叉的坐标
                                Point pt1 = MapElement.MapObject.ForkLines[ids1].EndPoint;
                                Point pt2 = MapElement.MapObject.ForkLines[ids2].EndPoint;
                                //靠右的是直行
                                if (pt1.X > pt2.X)
                                {
                                    Straight = int.Parse(v1.Last());
                                    if (v2.First().StartsWith("右下") && TurnDown != -1)
                                    {
                                        MapOperate.SystemMsg.WriteLine("错误：出现了两个右下！");
                                        return null;
                                    }
                                    else
                                    if (v2.First().StartsWith("右上") && TurnUp != -1)
                                    {
                                        MapOperate.SystemMsg.WriteLine("错误：出现了两个右上！");
                                        return null;
                                    }
                                    else
                                    {
                                        if (TurnUp != -1)
                                            TurnDown = int.Parse(v2.Last());
                                        else
                                            TurnUp = int.Parse(v2.Last());
                                    }
                                }
                                else
                                {
                                    if (v1.First().StartsWith("右下") && TurnDown != -1)
                                    {
                                        MapOperate.SystemMsg.WriteLine("错误：出现了两个右下！");
                                        return null;
                                    }
                                    else
                                     if (v1.First().StartsWith("右上") && TurnUp != -1)
                                    {
                                        MapOperate.SystemMsg.WriteLine("错误：出现了两个右上！");
                                        return null;
                                    }
                                    else
                                    {
                                        if (TurnUp != -1)
                                            TurnDown = int.Parse(v1.Last());
                                        else
                                            TurnUp = int.Parse(v1.Last());
                                    }
                                    Straight = int.Parse(v2.Last());
                                }
                            }
                            else
                            if (v1.First().StartsWith("上") && v2.First().StartsWith("上"))
                            {
                                //获取分叉的坐标
                                Point pt1 = MapElement.MapObject.ForkLines[ids1].StartPoint;
                                Point pt2 = MapElement.MapObject.ForkLines[ids2].StartPoint;
                                //靠上的是直行
                                if (pt1.Y < pt2.Y)
                                {
                                    Straight = int.Parse(v1.Last());
                                    if (v2.First().StartsWith("上左") && TurnUp != -1)
                                    {
                                        MapOperate.SystemMsg.WriteLine("错误：出现了两个右上！");
                                        return null;
                                    }
                                    else
                                    if (v2.First().StartsWith("上右") && TurnDown != -1)
                                    {
                                        MapOperate.SystemMsg.WriteLine("错误：出现了两个右下！");
                                        return null;
                                    }
                                    else
                                    {
                                        if (TurnUp != -1)
                                            TurnDown = int.Parse(v2.Last());
                                        else
                                            TurnUp = int.Parse(v2.Last());
                                    }
                                }
                                else
                                {
                                    if (v1.First().StartsWith("上左") && TurnUp != -1)
                                    {
                                        MapOperate.SystemMsg.WriteLine("错误：出现了两个右上！");
                                        return null;
                                    }
                                    else
                                    if (v1.First().StartsWith("上右") && TurnDown != -1)
                                    {
                                        MapOperate.SystemMsg.WriteLine("错误：出现了两个右下！");
                                        return null;
                                    }
                                    else
                                    {
                                        if (TurnUp != -1)
                                            TurnDown = int.Parse(v1.Last());
                                        else
                                            TurnUp = int.Parse(v1.Last());
                                    }
                                    Straight = int.Parse(v2.Last());
                                }
                            }
                            else
                            if (v1.First().StartsWith("下") && v2.First().StartsWith("下"))
                            {
                                Point pt1 = MapElement.MapObject.ForkLines[ids1].StartPoint;
                                Point pt2 = MapElement.MapObject.ForkLines[ids2].StartPoint;
                                //靠下的是直行
                                if (pt1.Y > pt2.Y)
                                {
                                    Straight = int.Parse(v1.Last());
                                    if (v2.First().StartsWith("下左") && TurnDown != -1)
                                    {
                                        MapOperate.SystemMsg.WriteLine("错误：出现了两个右下！");
                                        return null;
                                    }
                                    else
                                    if (v2.First().StartsWith("下右") && TurnUp != -1)
                                    {
                                        MapOperate.SystemMsg.WriteLine("错误：出现了两个右上！");
                                        return null;
                                    }
                                    else
                                    {
                                        if (TurnUp != -1)
                                            TurnDown = int.Parse(v2.Last());
                                        else
                                            TurnUp = int.Parse(v2.Last());
                                    }
                                }
                                else
                                {
                                    if (v1.First().StartsWith("下左") && TurnDown != -1)
                                    {
                                        MapOperate.SystemMsg.WriteLine("错误：出现了两个右下！");
                                        return null;
                                    }
                                    else
                                    if (v1.First().StartsWith("下右") && TurnUp != -1)
                                    {
                                        MapOperate.SystemMsg.WriteLine("错误：出现了两个右上！");
                                        return null;
                                    }
                                    else
                                    {
                                        if (TurnUp != -1)
                                            TurnDown = int.Parse(v1.Last());
                                        else
                                            TurnUp = int.Parse(v1.Last());
                                    }
                                    Straight = int.Parse(v2.Last());
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
            if (debug)
                MapOperate.SystemMsg.WriteLine("向右：上分叉【{0}】直行【{1}】下分叉【{2}】",
                 TurnUp == -1 ? TurnUp : MapElement.MapObject.RFIDS[TurnUp].Num,
                 Straight == -1 ? Straight : MapElement.MapObject.RFIDS[Straight].Num,
                 TurnDown == -1 ? TurnDown : MapElement.MapObject.RFIDS[TurnDown].Num);

            List<int> rs = new List<int>();
            rs.Add(TurnUp);
            rs.Add(Straight);
            rs.Add(TurnDown);
            return rs;
        }
        /// <summary>
        /// 将向上搜索结果解析成AGV可以识别的形式
        /// </summary>
        /// <param name="id">直行可以到达的标签索引</param>
        /// <param name="vs1">第一组搜索结果</param>
        /// <param name="vs2">第二组搜索结果</param>
        /// <param name="vs3">第三组搜索结果</param>
        /// <returns>0:向左分叉可到达的标签索引，1:直行可到达的标签索引，2:向右分叉可到达的标签索引</returns>
        public static List<int> AnalyUpResault(int id, List<string> va1, List<string> va2, List<string> va3)
        {
            List<string> vs1 = va1 == null ? null : new List<string>(va1.ToArray());
            List<string> vs2 = va2 == null ? null : new List<string>(va2.ToArray());
            List<string> vs3 = va3 == null ? null : new List<string>(va3.ToArray());
            bool debug = false;
            //直行
            int Straight = -1;
            //左分叉
            int TurnLeft = -1;
            //右分叉
            int TurnRight = -1;
            //有直接的直行标签
            if (id != -1)
            {
                Straight = id;
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
                    if (vs1.First().StartsWith("上左") && vs2.First().StartsWith("上左"))
                    {
                        MapOperate.SystemMsg.WriteLine("错误：上分叉出现了两个标签！");
                        return null;
                    }
                    else
                    if (vs1.First().StartsWith("上右") && vs2.First().StartsWith("上右"))
                    {
                        MapOperate.SystemMsg.WriteLine("错误：下分叉出现了两个标签！");
                        return null;
                    }
                    else
                    if (vs1.First().StartsWith("上左") && vs2.First().StartsWith("上右"))
                    {
                        TurnLeft = int.Parse(vs1.Last());
                        TurnRight = int.Parse(vs2.Last());
                    }
                    else
                    if (vs1.First().StartsWith("上右") && vs2.First().StartsWith("上左"))
                    {
                        TurnRight = int.Parse(vs1.Last());
                        TurnLeft = int.Parse(vs2.Last());
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
                    if (vs1.First().StartsWith("上左"))
                        TurnLeft = int.Parse(vs1.Last());
                    else
                        TurnRight = int.Parse(vs1.Last());
                }
            }
            else
            //没有直行的标签
            {
                //有一个标签
                if (vs1 != null && vs2 == null && vs3 == null)
                {
                    Straight = int.Parse(vs1.Last());
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
                            TurnRight = int.Parse(vs1.Last());
                            TurnLeft = int.Parse(vs2.Last());
                        }
                        else
                        if (vs1.First().StartsWith("左下") && vs2.First().StartsWith("左上"))
                        {
                            TurnLeft = int.Parse(vs1.Last());
                            TurnRight = int.Parse(vs2.Last());
                        }
                        else
                        {
                            //获取分叉的坐标
                            int ids1 = int.Parse(vs1.First().Substring(2, vs1.First().Length - 2));
                            int ids2 = int.Parse(vs2.First().Substring(2, vs2.First().Length - 2));
                            Point pt1 = MapElement.MapObject.ForkLines[ids1].EndPoint;
                            Point pt2 = MapElement.MapObject.ForkLines[ids2].EndPoint;
                            if (vs1.First().StartsWith("左上") && vs2.First().StartsWith("左上"))
                            {
                                //靠左的是直行，靠右的右分叉
                                if (pt1.X < pt2.X)
                                {
                                    Straight = int.Parse(vs1.Last());
                                    TurnRight = int.Parse(vs2.Last());
                                }
                                else
                                {
                                    TurnRight = int.Parse(vs1.Last());
                                    Straight = int.Parse(vs2.Last());
                                }
                            }
                            else
                            if (vs1.First().StartsWith("左下") && vs2.First().StartsWith("左下"))
                            {
                                //靠左的是直行，靠右的左分叉
                                if (pt1.X < pt2.X)
                                {
                                    Straight = int.Parse(vs1.Last());
                                    TurnLeft = int.Parse(vs2.Last());
                                }
                                else
                                {
                                    TurnLeft = int.Parse(vs1.Last());
                                    Straight = int.Parse(vs2.Last());
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
                            TurnLeft = int.Parse(vs1.Last());
                            TurnRight = int.Parse(vs2.Last());
                        }
                        else
                        if (vs1.First().StartsWith("右下") && vs2.First().StartsWith("右上"))
                        {
                            TurnRight = int.Parse(vs1.Last());
                            TurnLeft = int.Parse(vs2.Last());
                        }
                        else
                        {
                            //获取分叉的坐标
                            int ids1 = int.Parse(vs1.First().Substring(2, vs1.First().Length - 2));
                            int ids2 = int.Parse(vs2.First().Substring(2, vs2.First().Length - 2));
                            Point pt1 = MapElement.MapObject.ForkLines[ids1].EndPoint;
                            Point pt2 = MapElement.MapObject.ForkLines[ids2].EndPoint;
                            if (vs1.First().StartsWith("右上") && vs2.First().StartsWith("右上"))
                            {
                                //靠右的是直行，靠左的左分叉
                                if (pt1.X > pt2.X)
                                {
                                    Straight = int.Parse(vs1.Last());
                                    TurnLeft = int.Parse(vs2.Last());
                                }
                                else
                                {
                                    TurnLeft = int.Parse(vs1.Last());
                                    Straight = int.Parse(vs2.Last());
                                }
                            }
                            else
                            if (vs1.First().StartsWith("右下") && vs2.First().StartsWith("右下"))
                            {
                                //靠右的是直行，靠左的右分叉
                                if (pt1.X > pt2.X)
                                {
                                    Straight = int.Parse(vs1.Last());
                                    TurnRight = int.Parse(vs2.Last());
                                }
                                else
                                {
                                    TurnRight = int.Parse(vs1.Last());
                                    Straight = int.Parse(vs2.Last());
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
                            TurnLeft = int.Parse(vs1.Last());
                            TurnRight = int.Parse(vs2.Last());
                        }
                        else
                        if (vs1.First().StartsWith("上右") && vs2.First().StartsWith("上左"))
                        {
                            TurnRight = int.Parse(vs1.Last());
                            TurnLeft = int.Parse(vs2.Last());
                        }
                        else
                        {
                            //获取分叉的坐标
                            int ids1 = int.Parse(vs1.First().Substring(2, vs1.First().Length - 2));
                            int ids2 = int.Parse(vs2.First().Substring(2, vs2.First().Length - 2));
                            Point pt1 = MapElement.MapObject.ForkLines[ids1].StartPoint;
                            Point pt2 = MapElement.MapObject.ForkLines[ids2].StartPoint;
                            if (vs1.First().StartsWith("上左") && vs2.First().StartsWith("上左"))
                            {
                                //靠上的是直行，靠下的左分叉
                                if (pt1.Y < pt2.Y)
                                {
                                    Straight = int.Parse(vs1.Last());
                                    TurnLeft = int.Parse(vs2.Last());
                                }
                                else
                                {
                                    TurnLeft = int.Parse(vs1.Last());
                                    Straight = int.Parse(vs2.Last());
                                }
                            }
                            else
                            if (vs1.First().StartsWith("上右") && vs2.First().StartsWith("上右"))
                            {
                                //靠上的是直行，靠下的右分叉
                                if (pt1.Y < pt2.Y)
                                {
                                    Straight = int.Parse(vs1.Last());
                                    TurnRight = int.Parse(vs2.Last());
                                }
                                else
                                {
                                    TurnRight = int.Parse(vs1.Last());
                                    Straight = int.Parse(vs2.Last());
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
                            TurnRight = int.Parse(vs1.Last());
                            TurnLeft = int.Parse(vs2.Last());
                        }
                        else
                        if (vs1.First().StartsWith("下右") && vs2.First().StartsWith("下左"))
                        {
                            TurnLeft = int.Parse(vs1.Last());
                            TurnRight = int.Parse(vs2.Last());
                        }
                        else
                        {
                            //获取分叉的坐标
                            int ids1 = int.Parse(vs1.First().Substring(2, vs1.First().Length - 2));
                            int ids2 = int.Parse(vs2.First().Substring(2, vs2.First().Length - 2));
                            Point pt1 = MapElement.MapObject.ForkLines[ids1].StartPoint;
                            Point pt2 = MapElement.MapObject.ForkLines[ids2].StartPoint;
                            if (vs1.First().StartsWith("下左") && vs2.First().StartsWith("下左"))
                            {
                                //靠下的是直行，靠上的右分叉
                                if (pt1.Y > pt2.Y)
                                {
                                    Straight = int.Parse(vs1.Last());
                                    TurnRight = int.Parse(vs2.Last());
                                }
                                else
                                {
                                    TurnRight = int.Parse(vs1.Last());
                                    Straight = int.Parse(vs2.Last());
                                }
                            }
                            else
                            if (vs1.First().StartsWith("下右") && vs2.First().StartsWith("下右"))
                            {
                                //靠下的是直行，靠上的左分叉
                                if (pt1.Y > pt2.Y)
                                {
                                    Straight = int.Parse(vs1.Last());
                                    TurnLeft = int.Parse(vs2.Last());
                                }
                                else
                                {
                                    TurnLeft = int.Parse(vs1.Last());
                                    Straight = int.Parse(vs2.Last());
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
                            Straight = int.Parse(vs1.Last());
                            v1 = vs2;
                            v2 = vs3;
                        }
                        else
                        if (vs2.Count == 1)
                        {
                            //直行到达
                            Straight = int.Parse(vs2.Last());
                            v1 = vs1;
                            v2 = vs3;
                        }
                        else
                        if (vs3.Count == 1)
                        {
                            //直行到达
                            Straight = int.Parse(vs3.Last());
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
                                TurnRight = int.Parse(v1.Last());
                                TurnLeft = int.Parse(v2.Last());
                            }
                            else
                            if (v1.First().StartsWith("左下") && v2.First().StartsWith("左上"))
                            {
                                TurnLeft = int.Parse(v1.Last());
                                TurnRight = int.Parse(v2.Last());
                            }
                        }
                        else
                        if (v1.First().StartsWith("右") && v2.First().StartsWith("右"))
                        {
                            if (v1.First().StartsWith("右上") && v2.First().StartsWith("右下"))
                            {
                                TurnLeft = int.Parse(v1.Last());
                                TurnRight = int.Parse(v2.Last());
                            }
                            else
                            if (v1.First().StartsWith("右下") && v2.First().StartsWith("右上"))
                            {
                                TurnRight = int.Parse(v1.Last());
                                TurnLeft = int.Parse(v2.Last());
                            }
                        }
                        else
                        if (v1.First().StartsWith("上") && v2.First().StartsWith("上"))
                        {
                            if (v1.First().StartsWith("上左") && v2.First().StartsWith("上右"))
                            {
                                TurnLeft = int.Parse(v1.Last());
                                TurnRight = int.Parse(v2.Last());
                            }
                            else
                            if (v1.First().StartsWith("上右") && v2.First().StartsWith("上左"))
                            {
                                TurnRight = int.Parse(v1.Last());
                                TurnLeft = int.Parse(v2.Last());
                            }
                        }
                        else
                        if (v1.First().StartsWith("下") && v2.First().StartsWith("下"))
                        {
                            if (v1.First().StartsWith("下左") && v2.First().StartsWith("下右"))
                            {
                                TurnRight = int.Parse(v1.Last());
                                TurnLeft = int.Parse(v2.Last());
                            }
                            else
                            if (v1.First().StartsWith("下右") && v2.First().StartsWith("下左"))
                            {
                                TurnLeft = int.Parse(v1.Last());
                                TurnRight = int.Parse(v2.Last());
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
                                TurnRight = int.Parse(vs1.Last());
                                v1 = vs2;
                                v2 = vs3;
                            }
                            else
                            if (vs1.First().StartsWith("左下") && vs2.First().StartsWith("左上") && vs3.First().StartsWith("左下"))
                            {
                                TurnRight = int.Parse(vs2.Last());
                                v1 = vs1;
                                v2 = vs3;
                            }
                            else
                            if (vs1.First().StartsWith("左下") && vs2.First().StartsWith("左下") && vs3.First().StartsWith("左上"))
                            {
                                TurnRight = int.Parse(vs3.Last());
                                v1 = vs1;
                                v2 = vs2;
                            }
                            else
                            if (vs1.First().StartsWith("左下") && vs2.First().StartsWith("左上") && vs3.First().StartsWith("左上"))
                            {
                                TurnLeft = int.Parse(vs1.Last());
                                v1 = vs2;
                                v2 = vs3;
                            }
                            else
                            if (vs1.First().StartsWith("左上") && vs2.First().StartsWith("左下") && vs3.First().StartsWith("左上"))
                            {
                                TurnLeft = int.Parse(vs2.Last());
                                v1 = vs1;
                                v2 = vs3;
                            }
                            else
                            if (vs1.First().StartsWith("左上") && vs2.First().StartsWith("左上") && vs3.First().StartsWith("左下"))
                            {
                                TurnLeft = int.Parse(vs3.Last());
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
                                TurnLeft = int.Parse(vs1.Last());
                                v1 = vs2;
                                v2 = vs3;
                            }
                            else
                            if (vs1.First().StartsWith("右下") && vs2.First().StartsWith("右上") && vs3.First().StartsWith("右下"))
                            {
                                TurnLeft = int.Parse(vs2.Last());
                                v1 = vs1;
                                v2 = vs3;
                            }
                            else
                            if (vs1.First().StartsWith("右下") && vs2.First().StartsWith("右下") && vs3.First().StartsWith("右上"))
                            {
                                TurnLeft = int.Parse(vs3.Last());
                                v1 = vs1;
                                v2 = vs2;
                            }
                            else
                            if (vs1.First().StartsWith("右下") && vs2.First().StartsWith("右上") && vs3.First().StartsWith("右上"))
                            {
                                TurnRight = int.Parse(vs1.Last());
                                v1 = vs2;
                                v2 = vs3;
                            }
                            else
                            if (vs1.First().StartsWith("右上") && vs2.First().StartsWith("右下") && vs3.First().StartsWith("右上"))
                            {
                                TurnRight = int.Parse(vs2.Last());
                                v1 = vs1;
                                v2 = vs3;
                            }
                            else
                            if (vs1.First().StartsWith("右上") && vs2.First().StartsWith("右上") && vs3.First().StartsWith("右下"))
                            {
                                TurnRight = int.Parse(vs3.Last());
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
                                TurnLeft = int.Parse(vs1.Last());
                                v1 = vs2;
                                v2 = vs3;
                            }
                            else
                            if (vs1.First().StartsWith("上右") && vs2.First().StartsWith("上左") && vs3.First().StartsWith("上右"))
                            {
                                TurnLeft = int.Parse(vs2.Last());
                                v1 = vs1;
                                v2 = vs3;
                            }
                            else
                            if (vs1.First().StartsWith("上右") && vs2.First().StartsWith("上右") && vs3.First().StartsWith("上左"))
                            {
                                TurnLeft = int.Parse(vs3.Last());
                                v1 = vs1;
                                v2 = vs2;
                            }
                            else
                            if (vs1.First().StartsWith("上右") && vs2.First().StartsWith("上左") && vs3.First().StartsWith("上左"))
                            {
                                TurnRight = int.Parse(vs1.Last());
                                v1 = vs2;
                                v2 = vs3;
                            }
                            else
                            if (vs1.First().StartsWith("上左") && vs2.First().StartsWith("上右") && vs3.First().StartsWith("上左"))
                            {
                                TurnRight = int.Parse(vs2.Last());
                                v1 = vs1;
                                v2 = vs3;
                            }
                            else
                            if (vs1.First().StartsWith("上左") && vs2.First().StartsWith("上左") && vs3.First().StartsWith("上右"))
                            {
                                TurnRight = int.Parse(vs3.Last());
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
                                TurnRight = int.Parse(vs1.Last());
                                v1 = vs2;
                                v2 = vs3;
                            }
                            else
                              if (vs1.First().StartsWith("下右") && vs2.First().StartsWith("下左") && vs3.First().StartsWith("下右"))
                            {
                                TurnRight = int.Parse(vs2.Last());
                                v1 = vs1;
                                v2 = vs3;
                            }
                            else
                              if (vs1.First().StartsWith("下右") && vs2.First().StartsWith("下右") && vs3.First().StartsWith("下左"))
                            {
                                TurnRight = int.Parse(vs3.Last());
                                v1 = vs1;
                                v2 = vs2;
                            }
                            else
                              if (vs1.First().StartsWith("下右") && vs2.First().StartsWith("下左") && vs3.First().StartsWith("下左"))
                            {
                                TurnLeft = int.Parse(vs1.Last());
                                v1 = vs2;
                                v2 = vs3;
                            }
                            else
                              if (vs1.First().StartsWith("下左") && vs2.First().StartsWith("下右") && vs3.First().StartsWith("下左"))
                            {
                                TurnLeft = int.Parse(vs2.Last());
                                v1 = vs1;
                                v2 = vs3;
                            }
                            else
                              if (vs1.First().StartsWith("下左") && vs2.First().StartsWith("下左") && vs3.First().StartsWith("下右"))
                            {
                                TurnLeft = int.Parse(vs3.Last());
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
                            //获取分叉索引
                            int ids1 = int.Parse(v1.First().Substring(2, v1.First().Length - 2));
                            int ids2 = int.Parse(v2.First().Substring(2, v2.First().Length - 2));
                            if (v1.First().StartsWith("左") && v2.First().StartsWith("左"))
                            {
                                Point pt1 = MapElement.MapObject.ForkLines[ids1].EndPoint;
                                Point pt2 = MapElement.MapObject.ForkLines[ids2].EndPoint;
                                //靠左的是直行
                                if (pt1.X < pt2.X)
                                {
                                    Straight = int.Parse(v1.Last());
                                    if (v2.First().StartsWith("左下") && TurnLeft != -1)
                                    {
                                        MapOperate.SystemMsg.WriteLine("错误：出现了两个上左！");
                                        return null;
                                    }
                                    else
                                    if (v2.First().StartsWith("左上") && TurnRight != -1)
                                    {
                                        MapOperate.SystemMsg.WriteLine("错误：出现了两个上右！");
                                        return null;
                                    }
                                    else
                                    {
                                        if (TurnLeft != -1)
                                            TurnRight = int.Parse(v2.Last());
                                        else
                                            TurnLeft = int.Parse(v2.Last());
                                    }
                                }
                                else
                                {
                                    if (v1.First().StartsWith("左下") && TurnLeft != -1)
                                    {
                                        MapOperate.SystemMsg.WriteLine("错误：出现了两个上左！");
                                        return null;
                                    }
                                    else
                                    if (v1.First().StartsWith("左上") && TurnRight != -1)
                                    {
                                        MapOperate.SystemMsg.WriteLine("错误：出现了两个上右！");
                                        return null;
                                    }
                                    else
                                    {
                                        if (TurnLeft != -1)
                                            TurnRight = int.Parse(v1.Last());
                                        else
                                            TurnLeft = int.Parse(v1.Last());
                                    }
                                    Straight = int.Parse(v2.Last());
                                }
                            }
                            else
                            if (v1.First().StartsWith("右") && v2.First().StartsWith("右"))
                            {
                                //获取分叉的坐标
                                Point pt1 = MapElement.MapObject.ForkLines[ids1].EndPoint;
                                Point pt2 = MapElement.MapObject.ForkLines[ids2].EndPoint;
                                //靠右的是直行
                                if (pt1.X > pt2.X)
                                {
                                    Straight = int.Parse(v1.Last());
                                    if (v2.First().StartsWith("右下") && TurnRight != -1)
                                    {
                                        MapOperate.SystemMsg.WriteLine("错误：出现了两个上右！");
                                        return null;
                                    }
                                    else
                                    if (v2.First().StartsWith("右上") && TurnLeft != -1)
                                    {
                                        MapOperate.SystemMsg.WriteLine("错误：出现了两个上左！");
                                        return null;
                                    }
                                    else
                                    {
                                        if (TurnLeft != -1)
                                            TurnRight = int.Parse(v2.Last());
                                        else
                                            TurnLeft = int.Parse(v2.Last());
                                    }
                                }
                                else
                                {
                                    if (v1.First().StartsWith("右下") && TurnRight != -1)
                                    {
                                        MapOperate.SystemMsg.WriteLine("错误：出现了两个上右！");
                                        return null;
                                    }
                                    else
                                    if (v1.First().StartsWith("右上") && TurnLeft != -1)
                                    {
                                        MapOperate.SystemMsg.WriteLine("错误：出现了两个上左！");
                                        return null;
                                    }
                                    else
                                    {
                                        if (TurnLeft != -1)
                                            TurnRight = int.Parse(v1.Last());
                                        else
                                            TurnLeft = int.Parse(v1.Last());
                                    }
                                    Straight = int.Parse(v2.Last());
                                }
                            }
                            else
                            if (v1.First().StartsWith("上") && v2.First().StartsWith("上"))
                            {
                                //获取分叉的坐标
                                Point pt1 = MapElement.MapObject.ForkLines[ids1].StartPoint;
                                Point pt2 = MapElement.MapObject.ForkLines[ids2].StartPoint;
                                //靠上的是直行
                                if (pt1.Y < pt2.Y)
                                {
                                    Straight = int.Parse(v1.Last());
                                    if (v2.First().StartsWith("上左") && TurnLeft != -1)
                                    {
                                        MapOperate.SystemMsg.WriteLine("错误：出现了两个上左！");
                                        return null;
                                    }
                                    else
                                    if (v2.First().StartsWith("上右") && TurnRight != -1)
                                    {
                                        MapOperate.SystemMsg.WriteLine("错误：出现了两个上右！");
                                        return null;
                                    }
                                    else
                                    {
                                        if (TurnLeft != -1)
                                            TurnRight = int.Parse(v2.Last());
                                        else
                                            TurnLeft = int.Parse(v2.Last());
                                    }
                                }
                                else
                                {
                                    if (v1.First().StartsWith("上左") && TurnLeft != -1)
                                    {
                                        MapOperate.SystemMsg.WriteLine("错误：出现了两个上左！");
                                        return null;
                                    }
                                    else
                                    if (v1.First().StartsWith("上右") && TurnRight != -1)
                                    {
                                        MapOperate.SystemMsg.WriteLine("错误：出现了两个上右！");
                                        return null;
                                    }
                                    else
                                    {
                                        if (TurnLeft != -1)
                                            TurnRight = int.Parse(v1.Last());
                                        else
                                            TurnLeft = int.Parse(v1.Last());
                                    }
                                    Straight = int.Parse(v2.Last());
                                }
                            }
                            else
                            if (v1.First().StartsWith("下") && v2.First().StartsWith("下"))
                            {
                                Point pt1 = MapElement.MapObject.ForkLines[ids1].StartPoint;
                                Point pt2 = MapElement.MapObject.ForkLines[ids2].StartPoint;
                                //靠下的是直行
                                if (pt1.Y > pt2.Y)
                                {
                                    Straight = int.Parse(v1.Last());
                                    if (v2.First().StartsWith("下左") && TurnRight != -1)
                                    {
                                        MapOperate.SystemMsg.WriteLine("错误：出现了两个上右！");
                                        return null;
                                    }
                                    else
                                    if (v2.First().StartsWith("下右") && TurnLeft != -1)
                                    {
                                        MapOperate.SystemMsg.WriteLine("错误：出现了两个上左！");
                                        return null;
                                    }
                                    else
                                    {
                                        if (TurnLeft != -1)
                                            TurnRight = int.Parse(v2.Last());
                                        else
                                            TurnLeft = int.Parse(v2.Last());
                                    }
                                }
                                else
                                {
                                    if (v1.First().StartsWith("下左") && TurnRight != -1)
                                    {
                                        MapOperate.SystemMsg.WriteLine("错误：出现了两个上右！");
                                        return null;
                                    }
                                    else
                                    if (v1.First().StartsWith("下右") && TurnLeft != -1)
                                    {
                                        MapOperate.SystemMsg.WriteLine("错误：出现了两个上左！");
                                        return null;
                                    }
                                    else
                                    {
                                        if (TurnLeft != -1)
                                            TurnRight = int.Parse(v1.Last());
                                        else
                                            TurnLeft = int.Parse(v1.Last());
                                    }
                                    Straight = int.Parse(v2.Last());
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
            if (debug)
                MapOperate.SystemMsg.WriteLine("向上：左分叉【{0}】直行【{1}】右分叉【{2}】",
               TurnLeft == -1 ? TurnLeft : MapElement.MapObject.RFIDS[TurnLeft].Num,
               Straight == -1 ? Straight : MapElement.MapObject.RFIDS[Straight].Num,
               TurnRight == -1 ? TurnRight : MapElement.MapObject.RFIDS[TurnRight].Num);
            List<int> rs = new List<int>();
            rs.Add(TurnLeft);
            rs.Add(Straight);
            rs.Add(TurnRight);
            return rs;
        }
        /// <summary>
        /// 将向下搜索结果解析成AGV可以识别的形式
        /// </summary>
        /// <param name="id">直行可以到达的标签索引</param>
        /// <param name="vs1">第一组搜索结果</param>
        /// <param name="vs2">第二组搜索结果</param>
        /// <param name="vs3">第三组搜索结果</param>
        /// <returns>0:向左分叉可到达的标签索引，1:直行可到达的标签索引，2:向右分叉可到达的标签索引</returns>
        public static List<int> AnalyDownResault(int id, List<string> va1, List<string> va2, List<string> va3)
        {
            List<string> vs1 = va1 == null ? null : new List<string>(va1.ToArray());
            List<string> vs2 = va2 == null ? null : new List<string>(va2.ToArray());
            List<string> vs3 = va3 == null ? null : new List<string>(va3.ToArray());
            bool debug = false;
            //直行
            int Straight = -1;
            //左分叉
            int TurnLeft = -1;
            //右分叉
            int TurnRight = -1;
            //有直接的直行标签
            if (id != -1)
            {
                Straight = id;
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
                    if (vs1.First().StartsWith("下左") && vs2.First().StartsWith("下左"))
                    {
                        MapOperate.SystemMsg.WriteLine("错误：下方出现了两个标签！");
                        return null;
                    }
                    else
                    if (vs1.First().StartsWith("下右") && vs2.First().StartsWith("下右"))
                    {
                        MapOperate.SystemMsg.WriteLine("错误：下方出现了两个标签！");
                        return null;
                    }
                    else
                    if (vs1.First().StartsWith("下左") && vs2.First().StartsWith("下右"))
                    {
                        TurnLeft = int.Parse(vs1.Last());
                        TurnRight = int.Parse(vs2.Last());
                    }
                    else
                    if (vs1.First().StartsWith("下右") && vs2.First().StartsWith("下左"))
                    {
                        TurnRight = int.Parse(vs1.Last());
                        TurnLeft = int.Parse(vs2.Last());
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
                    if (vs1.First().StartsWith("下左"))
                        TurnLeft = int.Parse(vs1.Last());
                    else
                        TurnRight = int.Parse(vs1.Last());
                }
            }
            else
            //没有直行的标签
            {
                //有一个标签
                if (vs1 != null && vs2 == null && vs3 == null)
                {
                    Straight = int.Parse(vs1.Last());
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
                            TurnLeft = int.Parse(vs1.Last());
                            TurnRight = int.Parse(vs2.Last());
                        }
                        else
                        if (vs1.First().StartsWith("左下") && vs2.First().StartsWith("左上"))
                        {
                            TurnRight = int.Parse(vs1.Last());
                            TurnLeft = int.Parse(vs2.Last());
                        }
                        else
                        {
                            //获取分叉的坐标
                            int ids1 = int.Parse(vs1.First().Substring(2, vs1.First().Length - 2));
                            int ids2 = int.Parse(vs2.First().Substring(2, vs2.First().Length - 2));
                            Point pt1 = MapElement.MapObject.ForkLines[ids1].EndPoint;
                            Point pt2 = MapElement.MapObject.ForkLines[ids2].EndPoint;
                            if (vs1.First().StartsWith("左上") && vs2.First().StartsWith("左上"))
                            {
                                //靠左的是直行，靠右的左分叉
                                if (pt1.X < pt2.X)
                                {
                                    Straight = int.Parse(vs1.Last());
                                    TurnLeft = int.Parse(vs2.Last());
                                }
                                else
                                {
                                    TurnLeft = int.Parse(vs1.Last());
                                    Straight = int.Parse(vs2.Last());
                                }
                            }
                            else
                            if (vs1.First().StartsWith("左下") && vs2.First().StartsWith("左下"))
                            {
                                //靠左的是直行，靠右的右分叉
                                if (pt1.X < pt2.X)
                                {
                                    Straight = int.Parse(vs1.Last());
                                    TurnRight = int.Parse(vs2.Last());
                                }
                                else
                                {
                                    TurnRight = int.Parse(vs1.Last());
                                    Straight = int.Parse(vs2.Last());
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
                            TurnRight = int.Parse(vs1.Last());
                            TurnLeft = int.Parse(vs2.Last());
                        }
                        else
                        if (vs1.First().StartsWith("右下") && vs2.First().StartsWith("右上"))
                        {
                            TurnLeft = int.Parse(vs1.Last());
                            TurnRight = int.Parse(vs2.Last());
                        }
                        else
                        {
                            //获取分叉的坐标
                            int ids1 = int.Parse(vs1.First().Substring(2, vs1.First().Length - 2));
                            int ids2 = int.Parse(vs2.First().Substring(2, vs2.First().Length - 2));
                            Point pt1 = MapElement.MapObject.ForkLines[ids1].EndPoint;
                            Point pt2 = MapElement.MapObject.ForkLines[ids2].EndPoint;
                            if (vs1.First().StartsWith("右上") && vs2.First().StartsWith("右上"))
                            {
                                //靠右的是直行，靠左的右分叉
                                if (pt1.X > pt2.X)
                                {
                                    Straight = int.Parse(vs1.Last());
                                    TurnRight = int.Parse(vs2.Last());
                                }
                                else
                                {
                                    TurnRight = int.Parse(vs1.Last());
                                    Straight = int.Parse(vs2.Last());
                                }
                            }
                            else
                            if (vs1.First().StartsWith("右下") && vs2.First().StartsWith("右下"))
                            {
                                //靠右的是直行，靠左的左分叉
                                if (pt1.X > pt2.X)
                                {
                                    Straight = int.Parse(vs1.Last());
                                    TurnLeft = int.Parse(vs2.Last());
                                }
                                else
                                {
                                    TurnLeft = int.Parse(vs1.Last());
                                    Straight = int.Parse(vs2.Last());
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
                            TurnRight = int.Parse(vs1.Last());
                            TurnLeft = int.Parse(vs2.Last());
                        }
                        else
                        if (vs1.First().StartsWith("上右") && vs2.First().StartsWith("上左"))
                        {
                            TurnLeft = int.Parse(vs1.Last());
                            TurnRight = int.Parse(vs2.Last());
                        }
                        else
                        {
                            //获取分叉的坐标
                            int ids1 = int.Parse(vs1.First().Substring(2, vs1.First().Length - 2));
                            int ids2 = int.Parse(vs2.First().Substring(2, vs2.First().Length - 2));
                            Point pt1 = MapElement.MapObject.ForkLines[ids1].StartPoint;
                            Point pt2 = MapElement.MapObject.ForkLines[ids2].StartPoint;
                            if (vs1.First().StartsWith("上左") && vs2.First().StartsWith("上左"))
                            {
                                //靠上的是直行，靠下的右分叉
                                if (pt1.Y < pt2.Y)
                                {
                                    Straight = int.Parse(vs1.Last());
                                    TurnRight = int.Parse(vs2.Last());
                                }
                                else
                                {
                                    TurnRight = int.Parse(vs1.Last());
                                    Straight = int.Parse(vs2.Last());
                                }
                            }
                            else
                            if (vs1.First().StartsWith("上右") && vs2.First().StartsWith("上右"))
                            {
                                //靠上的是直行，靠下的左分叉
                                if (pt1.Y < pt2.Y)
                                {
                                    Straight = int.Parse(vs1.Last());
                                    TurnLeft = int.Parse(vs2.Last());
                                }
                                else
                                {
                                    TurnLeft = int.Parse(vs1.Last());
                                    Straight = int.Parse(vs2.Last());
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
                            TurnLeft = int.Parse(vs1.Last());
                            TurnRight = int.Parse(vs2.Last());
                        }
                        else
                        if (vs1.First().StartsWith("下右") && vs2.First().StartsWith("下左"))
                        {
                            TurnRight = int.Parse(vs1.Last());
                            TurnLeft = int.Parse(vs2.Last());
                        }
                        else
                        {
                            //获取分叉的坐标
                            int ids1 = int.Parse(vs1.First().Substring(2, vs1.First().Length - 2));
                            int ids2 = int.Parse(vs2.First().Substring(2, vs2.First().Length - 2));
                            Point pt1 = MapElement.MapObject.ForkLines[ids1].StartPoint;
                            Point pt2 = MapElement.MapObject.ForkLines[ids2].StartPoint;
                            if (vs1.First().StartsWith("下左") && vs2.First().StartsWith("下左"))
                            {
                                //靠下的是直行，靠上的左分叉
                                if (pt1.Y > pt2.Y)
                                {
                                    Straight = int.Parse(vs1.Last());
                                    TurnLeft = int.Parse(vs2.Last());
                                }
                                else
                                {
                                    TurnLeft = int.Parse(vs1.Last());
                                    Straight = int.Parse(vs2.Last());
                                }
                            }
                            else
                            if (vs1.First().StartsWith("下右") && vs2.First().StartsWith("下右"))
                            {
                                //靠下的是直行，靠上的右分叉
                                if (pt1.Y > pt2.Y)
                                {
                                    Straight = int.Parse(vs1.Last());
                                    TurnRight = int.Parse(vs2.Last());
                                }
                                else
                                {
                                    TurnRight = int.Parse(vs1.Last());
                                    Straight = int.Parse(vs2.Last());
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
                            Straight = int.Parse(vs1.Last());
                            v1 = vs2;
                            v2 = vs3;
                        }
                        else
                        if (vs2.Count == 1)
                        {
                            //直行到达
                            Straight = int.Parse(vs2.Last());
                            v1 = vs1;
                            v2 = vs3;
                        }
                        else
                        if (vs3.Count == 1)
                        {
                            //直行到达
                            Straight = int.Parse(vs3.Last());
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
                                TurnLeft = int.Parse(v1.Last());
                                TurnRight = int.Parse(v2.Last());
                            }
                            else
                            if (v1.First().StartsWith("左下") && v2.First().StartsWith("左上"))
                            {
                                TurnRight = int.Parse(v1.Last());
                                TurnLeft = int.Parse(v2.Last());
                            }
                        }
                        else
                        if (v1.First().StartsWith("右") && v2.First().StartsWith("右"))
                        {
                            if (v1.First().StartsWith("右上") && v2.First().StartsWith("右下"))
                            {
                                TurnRight = int.Parse(v1.Last());
                                TurnLeft = int.Parse(v2.Last());
                            }
                            else
                            if (v1.First().StartsWith("右下") && v2.First().StartsWith("右上"))
                            {
                                TurnLeft = int.Parse(v1.Last());
                                TurnRight = int.Parse(v2.Last());
                            }
                        }
                        else
                        if (v1.First().StartsWith("上") && v2.First().StartsWith("上"))
                        {
                            if (v1.First().StartsWith("上左") && v2.First().StartsWith("上右"))
                            {
                                TurnRight = int.Parse(v1.Last());
                                TurnLeft = int.Parse(v2.Last());
                            }
                            else
                            if (v1.First().StartsWith("上右") && v2.First().StartsWith("上左"))
                            {
                                TurnLeft = int.Parse(v1.Last());
                                TurnRight = int.Parse(v2.Last());
                            }
                        }
                        else
                        if (v1.First().StartsWith("下") && v2.First().StartsWith("下"))
                        {
                            if (v1.First().StartsWith("下左") && v2.First().StartsWith("下右"))
                            {
                                TurnLeft = int.Parse(v1.Last());
                                TurnRight = int.Parse(v2.Last());
                            }
                            else
                            if (v1.First().StartsWith("下右") && v2.First().StartsWith("下左"))
                            {
                                TurnRight = int.Parse(v1.Last());
                                TurnLeft = int.Parse(v2.Last());
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
                                TurnLeft = int.Parse(vs1.Last());
                                v1 = vs2;
                                v2 = vs3;
                            }
                            else
                            if (vs1.First().StartsWith("左下") && vs2.First().StartsWith("左上") && vs3.First().StartsWith("左下"))
                            {
                                TurnLeft = int.Parse(vs2.Last());
                                v1 = vs1;
                                v2 = vs3;
                            }
                            else
                            if (vs1.First().StartsWith("左下") && vs2.First().StartsWith("左下") && vs3.First().StartsWith("左上"))
                            {
                                TurnLeft = int.Parse(vs3.Last());
                                v1 = vs1;
                                v2 = vs2;
                            }
                            else
                            if (vs1.First().StartsWith("左下") && vs2.First().StartsWith("左上") && vs3.First().StartsWith("左上"))
                            {
                                TurnRight = int.Parse(vs1.Last());
                                v1 = vs2;
                                v2 = vs3;
                            }
                            else
                            if (vs1.First().StartsWith("左上") && vs2.First().StartsWith("左下") && vs3.First().StartsWith("左上"))
                            {
                                TurnRight = int.Parse(vs2.Last());
                                v1 = vs1;
                                v2 = vs3;
                            }
                            else
                            if (vs1.First().StartsWith("左上") && vs2.First().StartsWith("左上") && vs3.First().StartsWith("左下"))
                            {
                                TurnRight = int.Parse(vs3.Last());
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
                                TurnRight = int.Parse(vs1.Last());
                                v1 = vs2;
                                v2 = vs3;
                            }
                            else
                            if (vs1.First().StartsWith("右下") && vs2.First().StartsWith("右上") && vs3.First().StartsWith("右下"))
                            {
                                TurnRight = int.Parse(vs2.Last());
                                v1 = vs1;
                                v2 = vs3;
                            }
                            else
                            if (vs1.First().StartsWith("右下") && vs2.First().StartsWith("右下") && vs3.First().StartsWith("右上"))
                            {
                                TurnRight = int.Parse(vs3.Last());
                                v1 = vs1;
                                v2 = vs2;
                            }
                            else
                            if (vs1.First().StartsWith("右下") && vs2.First().StartsWith("右上") && vs3.First().StartsWith("右上"))
                            {
                                TurnLeft = int.Parse(vs1.Last());
                                v1 = vs2;
                                v2 = vs3;
                            }
                            else
                            if (vs1.First().StartsWith("右上") && vs2.First().StartsWith("右下") && vs3.First().StartsWith("右上"))
                            {
                                TurnLeft = int.Parse(vs2.Last());
                                v1 = vs1;
                                v2 = vs3;
                            }
                            else
                            if (vs1.First().StartsWith("右上") && vs2.First().StartsWith("右上") && vs3.First().StartsWith("右下"))
                            {
                                TurnLeft = int.Parse(vs3.Last());
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
                                TurnRight = int.Parse(vs1.Last());
                                v1 = vs2;
                                v2 = vs3;
                            }
                            else
                            if (vs1.First().StartsWith("上右") && vs2.First().StartsWith("上左") && vs3.First().StartsWith("上右"))
                            {
                                TurnRight = int.Parse(vs2.Last());
                                v1 = vs1;
                                v2 = vs3;
                            }
                            else
                            if (vs1.First().StartsWith("上右") && vs2.First().StartsWith("上右") && vs3.First().StartsWith("上左"))
                            {
                                TurnRight = int.Parse(vs3.Last());
                                v1 = vs1;
                                v2 = vs2;
                            }
                            else
                            if (vs1.First().StartsWith("上右") && vs2.First().StartsWith("上左") && vs3.First().StartsWith("上左"))
                            {
                                TurnLeft = int.Parse(vs1.Last());
                                v1 = vs2;
                                v2 = vs3;
                            }
                            else
                            if (vs1.First().StartsWith("上左") && vs2.First().StartsWith("上右") && vs3.First().StartsWith("上左"))
                            {
                                TurnLeft = int.Parse(vs2.Last());
                                v1 = vs1;
                                v2 = vs3;
                            }
                            else
                            if (vs1.First().StartsWith("上左") && vs2.First().StartsWith("上左") && vs3.First().StartsWith("上右"))
                            {
                                TurnLeft = int.Parse(vs3.Last());
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
                                TurnLeft = int.Parse(vs1.Last());
                                v1 = vs2;
                                v2 = vs3;
                            }
                            else
                              if (vs1.First().StartsWith("下右") && vs2.First().StartsWith("下左") && vs3.First().StartsWith("下右"))
                            {
                                TurnLeft = int.Parse(vs2.Last());
                                v1 = vs1;
                                v2 = vs3;
                            }
                            else
                              if (vs1.First().StartsWith("下右") && vs2.First().StartsWith("下右") && vs3.First().StartsWith("下左"))
                            {
                                TurnLeft = int.Parse(vs3.Last());
                                v1 = vs1;
                                v2 = vs2;
                            }
                            else
                              if (vs1.First().StartsWith("下右") && vs2.First().StartsWith("下左") && vs3.First().StartsWith("下左"))
                            {
                                TurnRight = int.Parse(vs1.Last());
                                v1 = vs2;
                                v2 = vs3;
                            }
                            else
                              if (vs1.First().StartsWith("下左") && vs2.First().StartsWith("下右") && vs3.First().StartsWith("下左"))
                            {
                                TurnRight = int.Parse(vs2.Last());
                                v1 = vs1;
                                v2 = vs3;
                            }
                            else
                              if (vs1.First().StartsWith("下左") && vs2.First().StartsWith("下左") && vs3.First().StartsWith("下右"))
                            {
                                TurnRight = int.Parse(vs3.Last());
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
                            //获取分叉索引
                            int ids1 = int.Parse(v1.First().Substring(2, v1.First().Length - 2));
                            int ids2 = int.Parse(v2.First().Substring(2, v2.First().Length - 2));
                            if (v1.First().StartsWith("左") && v2.First().StartsWith("左"))
                            {
                                Point pt1 = MapElement.MapObject.ForkLines[ids1].EndPoint;
                                Point pt2 = MapElement.MapObject.ForkLines[ids2].EndPoint;
                                //靠左的是直行
                                if (pt1.X < pt2.X)
                                {
                                    Straight = int.Parse(v1.Last());
                                    if (v2.First().StartsWith("左下") && TurnRight != -1)
                                    {
                                        MapOperate.SystemMsg.WriteLine("错误：出现了两个下右 ！");
                                        return null;
                                    }
                                    else
                                    if (v2.First().StartsWith("左上") && TurnLeft != -1)
                                    {
                                        MapOperate.SystemMsg.WriteLine("错误：出现了两个下左！");
                                        return null;
                                    }
                                    else
                                    {
                                        if (TurnLeft != -1)
                                            TurnRight = int.Parse(v2.Last());
                                        else
                                            TurnLeft = int.Parse(v2.Last());
                                    }
                                }
                                else
                                {
                                    if (v1.First().StartsWith("左下") && TurnRight != -1)
                                    {
                                        MapOperate.SystemMsg.WriteLine("错误：出现了两个下右 ！");
                                        return null;
                                    }
                                    else
                                      if (v1.First().StartsWith("左上") && TurnLeft != -1)
                                    {
                                        MapOperate.SystemMsg.WriteLine("错误：出现了两个下左！");
                                        return null;
                                    }
                                    else
                                    {
                                        if (TurnLeft != -1)
                                            TurnRight = int.Parse(v1.Last());
                                        else
                                            TurnLeft = int.Parse(v1.Last());
                                    }
                                    Straight = int.Parse(v2.Last());
                                }
                            }
                            else
                            if (v1.First().StartsWith("右") && v2.First().StartsWith("右"))
                            {
                                //获取分叉的坐标
                                Point pt1 = MapElement.MapObject.ForkLines[ids1].EndPoint;
                                Point pt2 = MapElement.MapObject.ForkLines[ids2].EndPoint;
                                //靠右的是直行
                                if (pt1.X > pt2.X)
                                {
                                    Straight = int.Parse(v1.Last());
                                    if (v2.First().StartsWith("右下") && TurnLeft != -1)
                                    {
                                        MapOperate.SystemMsg.WriteLine("错误：出现了两个下左 ！");
                                        return null;
                                    }
                                    else
                                     if (v2.First().StartsWith("右上") && TurnRight != -1)
                                    {
                                        MapOperate.SystemMsg.WriteLine("错误：出现了两个下右！");
                                        return null;
                                    }
                                    else
                                    {
                                        if (TurnLeft != -1)
                                            TurnRight = int.Parse(v2.Last());
                                        else
                                            TurnLeft = int.Parse(v2.Last());
                                    }
                                }
                                else
                                {
                                    if (v1.First().StartsWith("右下") && TurnLeft != -1)
                                    {
                                        MapOperate.SystemMsg.WriteLine("错误：出现了两个下左 ！");
                                        return null;
                                    }
                                    else
                                      if (v1.First().StartsWith("右上") && TurnRight != -1)
                                    {
                                        MapOperate.SystemMsg.WriteLine("错误：出现了两个下右！");
                                        return null;
                                    }
                                    else
                                    {
                                        if (TurnLeft != -1)
                                            TurnRight = int.Parse(v1.Last());
                                        else
                                            TurnLeft = int.Parse(v1.Last());
                                    }
                                    Straight = int.Parse(v2.Last());
                                }
                            }
                            else
                            if (v1.First().StartsWith("上") && v2.First().StartsWith("上"))
                            {
                                //获取分叉的坐标
                                Point pt1 = MapElement.MapObject.ForkLines[ids1].StartPoint;
                                Point pt2 = MapElement.MapObject.ForkLines[ids2].StartPoint;
                                //靠上的是直行
                                if (pt1.Y < pt2.Y)
                                {
                                    Straight = int.Parse(v1.Last());
                                    if (v2.First().StartsWith("上左") && TurnRight != -1)
                                    {
                                        MapOperate.SystemMsg.WriteLine("错误：出现了两个下右！");
                                        return null;
                                    }
                                    else
                                    if (v2.First().StartsWith("上右") && TurnLeft != -1)
                                    {
                                        MapOperate.SystemMsg.WriteLine("错误：出现了两个下左 ！");
                                        return null;
                                    }
                                    else
                                    {
                                        if (TurnLeft != -1)
                                            TurnRight = int.Parse(v2.Last());
                                        else
                                            TurnLeft = int.Parse(v2.Last());
                                    }
                                }
                                else
                                {
                                    if (v1.First().StartsWith("上左") && TurnRight != -1)
                                    {
                                        MapOperate.SystemMsg.WriteLine("错误：出现了两个下右！");
                                        return null;
                                    }
                                    else
                                    if (v1.First().StartsWith("上右") && TurnLeft != -1)
                                    {
                                        MapOperate.SystemMsg.WriteLine("错误：出现了两个下左 ！");
                                        return null;
                                    }
                                    else
                                    {
                                        if (TurnLeft != -1)
                                            TurnRight = int.Parse(v1.Last());
                                        else
                                            TurnLeft = int.Parse(v1.Last());
                                    }
                                    Straight = int.Parse(v2.Last());
                                }
                            }
                            else
                            if (v1.First().StartsWith("下") && v2.First().StartsWith("下"))
                            {
                                Point pt1 = MapElement.MapObject.ForkLines[ids1].StartPoint;
                                Point pt2 = MapElement.MapObject.ForkLines[ids2].StartPoint;
                                //靠下的是直行
                                if (pt1.Y > pt2.Y)
                                {
                                    Straight = int.Parse(v1.Last());
                                    if (v2.First().StartsWith("下左") && TurnLeft != -1)
                                    {
                                        MapOperate.SystemMsg.WriteLine("错误：出现了两个下左！");
                                        return null;
                                    }
                                    else
                                    if (v2.First().StartsWith("下右") && TurnRight != -1)
                                    {
                                        MapOperate.SystemMsg.WriteLine("错误：出现了两个下右 ！");
                                        return null;
                                    }
                                    else
                                    {
                                        if (TurnLeft != -1)
                                            TurnRight = int.Parse(v2.Last());
                                        else
                                            TurnLeft = int.Parse(v2.Last());
                                    }
                                }
                                else
                                {
                                    if (v1.First().StartsWith("下左") && TurnLeft != -1)
                                    {
                                        MapOperate.SystemMsg.WriteLine("错误：出现了两个下左！");
                                        return null;
                                    }
                                    else
                                    if (v1.First().StartsWith("下右") && TurnRight != -1)
                                    {
                                        MapOperate.SystemMsg.WriteLine("错误：出现了两个下右 ！");
                                        return null;
                                    }
                                    else
                                    {
                                        if (TurnLeft != -1)
                                            TurnRight = int.Parse(v1.Last());
                                        else
                                            TurnLeft = int.Parse(v1.Last());
                                    }
                                    Straight = int.Parse(v2.Last());
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
            if (debug)
                MapOperate.SystemMsg.WriteLine("向下：左分叉【{0}】直行【{1}】右分叉【{2}】",
                   TurnLeft == -1 ? TurnLeft : MapElement.MapObject.RFIDS[TurnLeft].Num,
                   Straight == -1 ? Straight : MapElement.MapObject.RFIDS[Straight].Num,
                   TurnRight == -1 ? TurnRight : MapElement.MapObject.RFIDS[TurnRight].Num);
            List<int> rs = new List<int>();
            rs.Add(TurnLeft);
            rs.Add(Straight);
            rs.Add(TurnRight);
            return rs;
        }
        #endregion

        /// <summary>
        /// 车头方向集合
        /// </summary>
        public static List<HeadDirection> HeadDirectionList = new List<HeadDirection>();
        public static List<AGVNeighbour> AGVNeighbourList = new List<AGVNeighbour>();

        /// <summary>
        /// 生成地图标签的邻接关系
        /// </summary>
        /// <param name="index">标签号</param>
        public static MapNeighbour GetMapNeighbour(int index)
        {
            MapNeighbour neighbour = new MapNeighbour();
            neighbour.Index = index;
            MapElement.RFID rfid = MapElement.MapObject.RFIDS[index];
            #region 向左搜索
            /*-----------搜索【标签】----------------------*/
            int id = Base.FindRFID.Left(index, rfid.LeftPoint, new Base.Range());
            Base.Range range = new Base.Range();
            //搜到标签
            if (id != -1)
                //设置搜索的x终点坐标【X的最小值为找的的标签的右侧中心坐标】
                range.MinX = MapElement.MapObject.RFIDS[id].RightPoint.X;
            //向左找上分叉
            neighbour.Left.V1 = ProcessString(index, rfid.LeftPoint, ProcessState.LeftUp, range);
            //找到了继续向左找下分叉
            if (neighbour.Left.V1 != null)
            {
                //向左找下分叉
                neighbour.Left.V2 = ProcessString(index, rfid.LeftPoint, ProcessState.LeftDown, range);
                //没找到就进行第二次，找到了就直接进行第三次
                if (neighbour.Left.V2 == null || ListLastEquls(neighbour.Left.V1, neighbour.Left.V2))
                {
                    //第二次搜索
                    if (neighbour.Left.V1.Count > 1)
                        FindSecond(index, neighbour.Left.V1, ref neighbour.Left.V2, ref neighbour.Left.V3, range);
                }
                //第三次搜索
                if (!(neighbour.Left.V1 != null && neighbour.Left.V2 != null && id != -1))
                {
                    //不够三条路才进行第三次搜索
                    if (neighbour.Left.V2 != null && neighbour.Left.V3 == null)
                    {
                        List<string> vs = new List<string>();
                        bool p = false;
                        int num = 0;
                        for (int i = 0; i < neighbour.Left.V2.Count; i++)
                        {

                            if (p == false)
                            {
                                if (neighbour.Left.V2[i] == neighbour.Left.V1[i])
                                    continue;
                                else
                                { p = true; num = i; }
                            }
                            if (p)
                                vs.Add(neighbour.Left.V2[i]);
                        }
                        FindThird(index, neighbour.Left.V1, vs, ref neighbour.Left.V3);
                        //补齐前段
                        if (neighbour.Left.V3 != null)
                            neighbour.Left.V3.InsertRange(0, neighbour.Left.V2.GetRange(0, num));
                        else
                        //如果第三次还是没有找到，用vs1的公共部分后半段再搜一次
                        if (neighbour.Left.V3 == null)
                        {
                            vs = new List<string>();
                            p = false;
                            num = 0;
                            for (int i = 0; i < neighbour.Left.V1.Count; i++)
                            {

                                if (p == false)
                                {
                                    if (neighbour.Left.V1[i] == neighbour.Left.V2[i])
                                        continue;
                                    else
                                    { p = true; num = i; }
                                }
                                if (p)
                                    vs.Add(neighbour.Left.V1[i]);
                            }
                            FindThird(index, neighbour.Left.V2, vs, ref neighbour.Left.V3);
                            //补齐前段
                            if (neighbour.Left.V3 != null)
                                neighbour.Left.V3.InsertRange(0, neighbour.Left.V1.GetRange(0, num));
                        }
                    }
                }
            }
            //将搜索结果解析成AGV可以识别的形式
            List<int> rs = AnalyLeftResault(id, neighbour.Left.V1, neighbour.Left.V2, neighbour.Left.V3);
            if (rs == null)
            {
                return null;
            }
            else
            {
                //把结果整合到邻接对象中
                neighbour.Left.Up = rs[0];
                neighbour.Left.Straight = rs[1];
                neighbour.Left.Down = rs[2];
                //如果有直接可以到达的标签，把该标签加入到列表，方便后面的计算
                if (id != -1)
                {
                    if (neighbour.Left.V1 == null)
                    {
                        neighbour.Left.V1 = new List<string>();
                        neighbour.Left.V1.Add(id.ToString());
                    }
                    else
                    if (neighbour.Left.V2 == null)
                    {
                        neighbour.Left.V2 = new List<string>();
                        neighbour.Left.V2.Add(id.ToString());
                    }
                    else
                    if (neighbour.Left.V3 == null)
                    {
                        neighbour.Left.V3 = new List<string>();
                        neighbour.Left.V3.Add(id.ToString());
                    }
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
                //设置搜索的x终点坐标【X的最大值为找的的标签的左侧中心坐标】
                range.MaxX = MapElement.MapObject.RFIDS[id].LeftPoint.X;
            }
            //向右找上分叉
            neighbour.Right.V1 = ProcessString(index, rfid.RightPoint, ProcessState.RightUp, range);
            //找到了继续向右找下分叉
            if (neighbour.Right.V1 != null)
            {
                neighbour.Right.V2 = null;
                neighbour.Right.V3 = null;
                //向右找下分叉
                neighbour.Right.V2 = ProcessString(index, rfid.RightPoint, ProcessState.RightDown, range);
                //没找到就进行第二次，找到了就直接进行第三次
                if (neighbour.Right.V2 == null || ListLastEquls(neighbour.Right.V1, neighbour.Right.V2))
                {
                    //第二次搜索
                    if (neighbour.Right.V1.Count > 1)
                        FindSecond(index, neighbour.Right.V1, ref neighbour.Right.V2, ref neighbour.Right.V3, range);
                }
                //第三次搜索
                if (!(neighbour.Right.V1 != null && neighbour.Right.V2 != null && id != -1))
                {
                    //不够三条路才进行第三次搜索 
                    if (neighbour.Right.V2 != null && neighbour.Right.V3 == null)
                    {
                        List<string> vs = new List<string>();
                        bool p = false;
                        int num = 0;
                        for (int i = 0; i < neighbour.Right.V2.Count; i++)
                        {

                            if (p == false)
                            {
                                if (neighbour.Right.V2[i] == neighbour.Right.V1[i])
                                    continue;
                                else
                                { p = true; num = i; }
                            }
                            if (p)
                                vs.Add(neighbour.Right.V2[i]);
                        }
                        FindThird(index, neighbour.Right.V1, vs, ref neighbour.Right.V3);
                        //补齐前段
                        if (neighbour.Right.V3 != null)
                            neighbour.Right.V3.InsertRange(0, neighbour.Right.V2.GetRange(0, num));
                        else
                        //如果第三次还是没有找到，用vs1的公共部分后半段再搜一次
                        if (neighbour.Right.V3 == null)
                        {
                            vs = new List<string>();
                            p = false;
                            num = 0;
                            for (int i = 0; i < neighbour.Right.V1.Count; i++)
                            {

                                if (p == false)
                                {
                                    if (neighbour.Right.V1[i] == neighbour.Right.V2[i])
                                        continue;
                                    else
                                    { p = true; num = i; }
                                }
                                if (p)
                                    vs.Add(neighbour.Right.V1[i]);
                            }
                            FindThird(index, neighbour.Right.V2, vs, ref neighbour.Right.V3);
                            //补齐前段
                            if (neighbour.Right.V3 != null)
                                neighbour.Right.V3.InsertRange(0, neighbour.Right.V1.GetRange(0, num));
                        }
                    }
                }
            }
            //将搜索结果解析成AGV可以识别的形式
            rs = AnalyRightResault(id, neighbour.Right.V1, neighbour.Right.V2, neighbour.Right.V3);
            if (rs == null)
            {
                //解析出错,路径不合理
                return null;
            }
            else
            {
                //把结果整合到邻接对象中
                neighbour.Right.Up = rs[0];
                neighbour.Right.Straight = rs[1];
                neighbour.Right.Down = rs[2];
                //如果有直接可以到达的标签，把该标签加入到列表，方便后面的计算
                if (id != -1)
                {
                    if (neighbour.Right.V1 == null)
                    {
                        neighbour.Right.V1 = new List<string>();
                        neighbour.Right.V1.Add(id.ToString());
                    }
                    else
                    if (neighbour.Right.V2 == null)
                    {
                        neighbour.Right.V2 = new List<string>();
                        neighbour.Right.V2.Add(id.ToString());
                    }
                    else
                    if (neighbour.Right.V3 == null)
                    {
                        neighbour.Right.V3 = new List<string>();
                        neighbour.Right.V3.Add(id.ToString());
                    }
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
                //设置搜索的y终点坐标【Y的最小值为找的的标签的下侧中心坐标】
                range.MinY = MapElement.MapObject.RFIDS[id].DownPoint.Y;
            }
            //向上找左分叉
            neighbour.Up.V1 = ProcessString(index, rfid.UpPoint, ProcessState.UpLeft, range);
            //找到了继续向上找右分叉
            if (neighbour.Up.V1 != null)
            {
                //向上找右分叉
                neighbour.Up.V2 = ProcessString(index, rfid.UpPoint, ProcessState.UpRight, range);
                //没找到就进行第二次，找到了就直接进行第三次
                if (neighbour.Up.V2 == null || ListLastEquls(neighbour.Up.V1, neighbour.Up.V2))
                {
                    //第二次搜索
                    if (neighbour.Up.V1.Count > 1)
                        FindSecond(index, neighbour.Up.V1, ref neighbour.Up.V2, ref neighbour.Up.V3, range);
                }
                //第三次搜索
                if (!(neighbour.Up.V1 != null && neighbour.Up.V2 != null && id != -1))
                {
                    //不够三条路才进行第三次搜索 
                    if (neighbour.Up.V2 != null && neighbour.Up.V3 == null)
                    {
                        List<string> vs = new List<string>();
                        bool p = false;
                        int num = 0;
                        for (int i = 0; i < neighbour.Up.V2.Count; i++)
                        {

                            if (p == false)
                            {
                                if (neighbour.Up.V2[i] == neighbour.Up.V1[i])
                                    continue;
                                else
                                { p = true; num = i; }
                            }
                            if (p)
                                vs.Add(neighbour.Up.V2[i]);
                        }
                        FindThird(index, neighbour.Up.V1, vs, ref neighbour.Up.V3);
                        //补齐前段
                        if (neighbour.Up.V3 != null)
                            neighbour.Up.V3.InsertRange(0, neighbour.Up.V2.GetRange(0, num));
                        else
                        //如果第三次还是没有找到，用neighbour.Up.V1的公共部分后半段再搜一次
                        if (neighbour.Up.V3 == null)
                        {
                            vs = new List<string>();
                            p = false;
                            num = 0;
                            for (int i = 0; i < neighbour.Up.V1.Count; i++)
                            {

                                if (p == false)
                                {
                                    if (neighbour.Up.V1[i] == neighbour.Up.V2[i])
                                        continue;
                                    else
                                    { p = true; num = i; }
                                }
                                if (p)
                                    vs.Add(neighbour.Up.V1[i]);
                            }
                            FindThird(index, neighbour.Up.V2, vs, ref neighbour.Up.V3);
                            //补齐前段
                            if (neighbour.Up.V3 != null)
                                neighbour.Up.V3.InsertRange(0, neighbour.Up.V1.GetRange(0, num));
                        }
                    }
                }
            }
            //将搜索结果解析成AGV可以识别的形式
            rs = AnalyUpResault(id, neighbour.Up.V1, neighbour.Up.V2, neighbour.Up.V3);
            if (rs == null)
            {
                //解析出错,路径不合理
                return null;
            }
            else
            {
                //把结果整合到邻接对象中
                neighbour.Up.Left = rs[0];
                neighbour.Up.Straight = rs[1];
                neighbour.Up.Right = rs[2];
                //如果有直接可以到达的标签，把该标签加入到列表，方便后面的计算
                if (id != -1)
                {
                    if (neighbour.Up.V1 == null)
                    {
                        neighbour.Up.V1 = new List<string>();
                        neighbour.Up.V1.Add(id.ToString());
                    }
                    else
                    if (neighbour.Up.V2 == null)
                    {
                        neighbour.Up.V2 = new List<string>();
                        neighbour.Up.V2.Add(id.ToString());
                    }
                    else
                    if (neighbour.Up.V3 == null)
                    {
                        neighbour.Up.V3 = new List<string>();
                        neighbour.Up.V3.Add(id.ToString());
                    }
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
                //设置搜索的y终点坐标【Y的最大值为找的的标签的上侧中心坐标】
                range.MaxY = MapElement.MapObject.RFIDS[id].UpPoint.Y;
            }
            //向下找左分叉
            neighbour.Down.V1 = ProcessString(index, rfid.DownPoint, ProcessState.DownLeft, range);
            //找到了继续向下找右分叉
            if (neighbour.Down.V1 != null)
            {
                neighbour.Down.V2 = null;
                neighbour.Down.V3 = null;
                //向下找右分叉
                neighbour.Down.V2 = ProcessString(index, rfid.DownPoint, ProcessState.DownRight, range);
                //没找到就进行第二次，找到了就直接进行第三次
                if (neighbour.Down.V2 == null || ListLastEquls(neighbour.Down.V1, neighbour.Down.V2))
                {
                    //第二次搜索
                    if (neighbour.Down.V1.Count > 1)
                        FindSecond(index, neighbour.Down.V1, ref neighbour.Down.V2, ref neighbour.Down.V3, range);
                }
                //第三次搜索
                if (!(neighbour.Down.V1 != null && neighbour.Down.V2 != null && id != -1))
                {
                    //不够三条路才进行第三次搜索
                    if (neighbour.Down.V2 != null && neighbour.Down.V3 == null)
                    {
                        List<string> vs = new List<string>();
                        bool p = false;
                        int num = 0;
                        for (int i = 0; i < neighbour.Down.V2.Count; i++)
                        {

                            if (p == false)
                            {
                                if (neighbour.Down.V2[i] == neighbour.Down.V1[i])
                                    continue;
                                else
                                { p = true; num = i; }
                            }
                            if (p)
                                vs.Add(neighbour.Down.V2[i]);
                        }
                        FindThird(index, neighbour.Down.V1, vs, ref neighbour.Down.V3);
                        //补齐前段
                        if (neighbour.Down.V3 != null)
                            neighbour.Down.V3.InsertRange(0, neighbour.Down.V2.GetRange(0, num));
                        else
                        //如果第三次还是没有找到，用neighbour.Down.V1的公共部分后半段再搜一次
                        if (neighbour.Down.V3 == null)
                        {
                            vs = new List<string>();
                            p = false;
                            num = 0;
                            for (int i = 0; i < neighbour.Down.V1.Count; i++)
                            {

                                if (p == false)
                                {
                                    if (neighbour.Down.V1[i] == neighbour.Down.V2[i])
                                        continue;
                                    else
                                    { p = true; num = i; }
                                }
                                if (p)
                                    vs.Add(neighbour.Down.V1[i]);
                            }
                            FindThird(index, neighbour.Down.V2, vs, ref neighbour.Down.V3);
                            //补齐前段
                            if (neighbour.Down.V3 != null)
                                neighbour.Down.V3.InsertRange(0, neighbour.Down.V1.GetRange(0, num));
                        }
                    }
                }
            }
            //将搜索结果解析成AGV可以识别的形式
            rs = AnalyDownResault(id, neighbour.Down.V1, neighbour.Down.V2, neighbour.Down.V3);
            if (rs == null)
            {
                //解析出错,路径不合理
                return null;
            }
            else
            {
                //把结果整合到邻接对象中
                neighbour.Down.Left = rs[0];
                neighbour.Down.Straight = rs[1];
                neighbour.Down.Right = rs[2];
                //如果有直接可以到达的标签，把该标签加入到列表，方便后面的计算
                if (id != -1)
                {
                    if (neighbour.Down.V1 == null)
                    {
                        neighbour.Down.V1 = new List<string>();
                        neighbour.Down.V1.Add(id.ToString());
                    }
                    else
                    if (neighbour.Down.V2 == null)
                    {
                        neighbour.Down.V2 = new List<string>();
                        neighbour.Down.V2.Add(id.ToString());
                    }
                    else
                    if (neighbour.Down.V3 == null)
                    {
                        neighbour.Down.V3 = new List<string>();
                        neighbour.Down.V3.Add(id.ToString());
                    }
                }
            }
            #endregion
            return neighbour;
        }





        public static void DrawRFID(int index)
        {
            if (index == -1)
                return;
            //复制标签
            MapElement.RFID rfid = MapFunction.IgkClone.RFID(MapElement.MapObject.RFIDS[index]);

            //Ellipse ellipse = new Ellipse();
            //ellipse.Margin = MapElement.MapObject.RFIDS[index].ellipse.Margin;
            //ellipse.Width = MapElement.MapObject.RFIDS[index].ellipse.Width;
            //ellipse.Height = MapElement.MapObject.RFIDS[index].ellipse.Height;
            rfid.ellipse.Fill = Brushes.Pink;// CavnvasBase.GetSolid(255, Colors.Pink);
            DropShadowEffect drop = new DropShadowEffect();
            drop.ShadowDepth = 3;
            drop.Color = Colors.LightGray;
            rfid.ellipse.Effect = drop;
            rfid.ellipse.Stroke = Brushes.Pink;
            MapElement.CvRouteDisplay.Children.Add(rfid.ellipse);
            //显示编号
            CavnvasBase.DrawText(
                rfid.ellipse.Margin.Left + rfid.ellipse.Width / 2,
                rfid.ellipse.Margin.Top + rfid.ellipse.Width / 2,
                rfid.Num.ToString(),
                Colors.Black,
                MapElement.CvRouteDisplay,
                rfid.textBlock
                );
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="nb"></param>
        /// <param name="vs"></param>
        /// <param name="color"></param>
        /// <param name="Dir">搜索方向</param>
        public static void DrawLine(MapNeighbour nb, List<string> vs, Brush color, string Dir)
        {
            if (vs == null)
                return;
            Point p1 = new Point();
            Point p2 = new Point();
            for (int i = 0; i < vs.Count; i++)
            {
                //不转弯直接到达
                if (vs.Count == 1)
                {
                    if (Dir == "左")
                    {
                        //从当前标签的左中点画到目标标签的右终点
                        p1 = MapElement.MapObject.RFIDS[nb.Index].LeftPoint;
                        p2 = MapElement.MapObject.RFIDS[int.Parse(vs[0])].RightPoint;
                    }
                    else
                    if (Dir == "右")
                    {
                        p1 = MapElement.MapObject.RFIDS[nb.Index].RightPoint;
                        p2 = MapElement.MapObject.RFIDS[int.Parse(vs[0])].LeftPoint;
                    }
                    else
                    if (Dir == "上")
                    {
                        p1 = MapElement.MapObject.RFIDS[nb.Index].UpPoint;
                        p2 = MapElement.MapObject.RFIDS[int.Parse(vs[0])].DownPoint;
                    }
                    else
                    if (Dir == "下")
                    {
                        p1 = MapElement.MapObject.RFIDS[nb.Index].DownPoint;
                        p2 = MapElement.MapObject.RFIDS[int.Parse(vs[0])].UpPoint;
                    }
                }
                else
                {
                    //第一段直线
                    if (i == 0)
                    {
                        if (Dir == "左")
                        {
                            p1 = MapElement.MapObject.RFIDS[nb.Index].LeftPoint;
                            p2 = MapElement.MapObject.ForkLines[int.Parse(vs[i].Substring(2, vs[i].Length - 2))].EndPoint;
                        }
                        else
                        if (Dir == "右")
                        {
                            p1 = MapElement.MapObject.RFIDS[nb.Index].RightPoint;
                            p2 = MapElement.MapObject.ForkLines[int.Parse(vs[i].Substring(2, vs[i].Length - 2))].EndPoint;
                        }
                        else
                        if (Dir == "上")
                        {
                            p1 = MapElement.MapObject.RFIDS[nb.Index].UpPoint;
                            p2 = MapElement.MapObject.ForkLines[int.Parse(vs[i].Substring(2, vs[i].Length - 2))].StartPoint;
                        }
                        else
                        if (Dir == "下")
                        {
                            p1 = MapElement.MapObject.RFIDS[nb.Index].DownPoint;
                            p2 = MapElement.MapObject.ForkLines[int.Parse(vs[i].Substring(2, vs[i].Length - 2))].StartPoint;
                        }
                    }
                    else
                    //最后一段直线
                    if (i == vs.Count - 1)
                    {
                        if (vs[i - 1].StartsWith("左上") || vs[i - 1].StartsWith("右上"))
                        {
                            p1 = MapElement.MapObject.RFIDS[int.Parse(vs[i])].DownPoint;
                            p2 = MapElement.MapObject.ForkLines[int.Parse(vs[i - 1].Substring(2, vs[i - 1].Length - 2))].StartPoint;
                        }
                        else
                        if (vs[i - 1].StartsWith("左下") || vs[i - 1].StartsWith("右下"))
                        {
                            p1 = MapElement.MapObject.RFIDS[int.Parse(vs[i])].UpPoint;
                            p2 = MapElement.MapObject.ForkLines[int.Parse(vs[i - 1].Substring(2, vs[i - 1].Length - 2))].StartPoint;
                        }
                        else
                        if (vs[i - 1].StartsWith("上左") || vs[i - 1].StartsWith("下左"))
                        {
                            p1 = MapElement.MapObject.RFIDS[int.Parse(vs[i])].RightPoint;
                            p2 = MapElement.MapObject.ForkLines[int.Parse(vs[i - 1].Substring(2, vs[i - 1].Length - 2))].EndPoint;
                        }
                        else
                        if (vs[i - 1].StartsWith("上右") || vs[i - 1].StartsWith("下右"))
                        {
                            p1 = MapElement.MapObject.RFIDS[int.Parse(vs[i])].LeftPoint;
                            p2 = MapElement.MapObject.ForkLines[int.Parse(vs[i - 1].Substring(2, vs[i - 1].Length - 2))].EndPoint;
                        }
                    }
                    //中间
                    else
                    {
                        if (vs[i - 1].StartsWith("左上") || vs[i - 1].StartsWith("右上") ||
                           vs[i - 1].StartsWith("左下") || vs[i - 1].StartsWith("右下"))
                        {
                            p1 = MapElement.MapObject.ForkLines[int.Parse(vs[i].Substring(2, vs[i].Length - 2))].StartPoint;
                            p2 = MapElement.MapObject.ForkLines[int.Parse(vs[i - 1].Substring(2, vs[i - 1].Length - 2))].StartPoint;
                        }
                        else
                        if (vs[i - 1].StartsWith("上左") || vs[i - 1].StartsWith("下左") ||
                            vs[i - 1].StartsWith("上右") || vs[i - 1].StartsWith("下右"))
                        {
                            p1 = MapElement.MapObject.ForkLines[int.Parse(vs[i].Substring(2, vs[i].Length - 2))].EndPoint;
                            p2 = MapElement.MapObject.ForkLines[int.Parse(vs[i - 1].Substring(2, vs[i - 1].Length - 2))].EndPoint;
                        }
                    }
                }
                Line line = new Line();
                line.Stroke = color;
                line.X1 = p1.X;
                line.X2 = p2.X;
                line.Y1 = p1.Y;
                line.Y2 = p2.Y;
                //线的宽度
                line.StrokeThickness = 3.5;
                //添加发光效果
                DropShadowEffect drop = new DropShadowEffect();
                drop.ShadowDepth = 2;
                drop.Color = Colors.LightGray;
                line.Effect = drop;
                //添加到画布
                MapElement.CvRouteDisplay.Children.Add(line);
            }
        }
        public static void DrawForkLine(List<string> vs, Brush color)
        {
            if (vs == null)
                return;
            for (int i = 0; i < vs.Count - 1; i++)
            {
                //复制圆弧
                MapElement.RouteForkLine forkLine = MapFunction.IgkClone.ForkLine(MapElement.MapObject.ForkLines[int.Parse(vs[i].Substring(2, vs[i].Length - 2))]);
                //设置为虚线
                //  forkLine.SelectPath.StrokeDashArray = new DoubleCollection() { 3, 5 };
                forkLine.Path.Stroke = color;
                //更改颜色
                forkLine.SelectPath.Stroke = color;
                //设置线宽
                forkLine.SelectPath.StrokeThickness = 3.5;
                //添加发光效果
                DropShadowEffect drop = new DropShadowEffect();
                drop.ShadowDepth = 2;
                drop.Color = Colors.LightGray;
                forkLine.SelectPath.Effect = drop;
                //添加到画布
                MapElement.CvRouteDisplay.Children.Add(forkLine.SelectPath);
            }
        }
        /// <summary>
        /// 显示邻接关系
        /// </summary>
        /// <param name="nb"></param>
        public static void ShowMapNeighbour(MapNeighbour nb)
        {
            Brush color = Brushes.Orange;
            //清空画布
            MapElement.CvRouteDisplay.Children.Clear();
            #region 左
            //画的标签
            DrawRFID(nb.Left.Up);
            DrawRFID(nb.Left.Straight);
            DrawRFID(nb.Left.Down);
            //画所有的直线
            DrawLine(nb, nb.Left.V1, color, "左");
            DrawLine(nb, nb.Left.V2, color, "左");
            DrawLine(nb, nb.Left.V3, color, "左");
            //画所有的弧线
            DrawForkLine(nb.Left.V1, color);
            DrawForkLine(nb.Left.V2, color);
            DrawForkLine(nb.Left.V3, color);
            #endregion

            #region 右
            //画的标签
            DrawRFID(nb.Right.Up);
            DrawRFID(nb.Right.Straight);
            DrawRFID(nb.Right.Down);
            //画所有的直线
            DrawLine(nb, nb.Right.V1, color, "右");
            DrawLine(nb, nb.Right.V2, color, "右");
            DrawLine(nb, nb.Right.V3, color, "右");
            //画所有的弧线  
            DrawForkLine(nb.Right.V1, color);
            DrawForkLine(nb.Right.V2, color);
            DrawForkLine(nb.Right.V3, color);
            #endregion

            #region 上
            //画的标签
            DrawRFID(nb.Up.Left);
            DrawRFID(nb.Up.Straight);
            DrawRFID(nb.Up.Right);
            //画所有的直线
            DrawLine(nb, nb.Up.V1, color, "上");
            DrawLine(nb, nb.Up.V2, color, "上");
            DrawLine(nb, nb.Up.V3, color, "上");
            //画所有的弧线  
            DrawForkLine(nb.Up.V1, color);
            DrawForkLine(nb.Up.V2, color);
            DrawForkLine(nb.Up.V3, color);
            #endregion

            #region 下
            //画的标签
            DrawRFID(nb.Down.Left);
            DrawRFID(nb.Down.Straight);
            DrawRFID(nb.Down.Right);
            //画所有的直线
            DrawLine(nb, nb.Down.V1, color, "下");
            DrawLine(nb, nb.Down.V2, color, "下");
            DrawLine(nb, nb.Down.V3, color, "下");
            //画所有的弧线  
            DrawForkLine(nb.Down.V1, color);
            DrawForkLine(nb.Down.V2, color);
            DrawForkLine(nb.Down.V3, color);
            #endregion
        }

        /// <summary>
        /// 测试函数，计算并显示某一个标签的邻接关系
        /// </summary>
        public static void Test2_ShowNB(int index)
        {
            //获取邻接关系
            MapNeighbour map = GetMapNeighbour(index);
            //打印邻接关系
            MapOperate.SystemMsg.WriteLine(map.ToString());
            //显示
            ShowMapNeighbour(map);
        }

        /// <summary>
        /// 根据地图标签的邻接关系确定AGV的车头方向，最终结果保存在全局变量【HeadDirectionList】
        /// </summary>
        /// <param name="neighbour">某个标签的地图邻接关系</param>
        /// <param name="dir">AGV在这个标签的车头方向</param>
        public static void GetAgvHeadDirection(MapNeighbour neighbour, HeadDirections dir)
        {
            RotateAgvHeadDir(neighbour.Left.V1, "左", dir);
            RotateAgvHeadDir(neighbour.Left.V2, "左", dir);
            RotateAgvHeadDir(neighbour.Left.V3, "左", dir);

            RotateAgvHeadDir(neighbour.Right.V1, "右", dir);
            RotateAgvHeadDir(neighbour.Right.V2, "右", dir);
            RotateAgvHeadDir(neighbour.Right.V3, "右", dir);

            RotateAgvHeadDir(neighbour.Up.V1, "上", dir);
            RotateAgvHeadDir(neighbour.Up.V2, "上", dir);
            RotateAgvHeadDir(neighbour.Up.V3, "上", dir);

            RotateAgvHeadDir(neighbour.Down.V1, "下", dir);
            RotateAgvHeadDir(neighbour.Down.V2, "下", dir);
            RotateAgvHeadDir(neighbour.Down.V3, "下", dir);
        }

        /// <summary>
        /// 旋转车身方向
        /// </summary>
        /// <param name="headDirection"></param>
        /// <param name="Dir"></param>
        public static void RotateAgvHeadDir(List<string> vs, string findDir, HeadDirections dir)
        {
            if (vs != null)
            {
                //已经标记过的不再标记
                if (HeadDirectionList[int.Parse(vs.Last())].Dir != HeadDirections.None)
                    return;

                //设置方向为起点方向
                if (findDir == "左")
                {
                    if (dir == HeadDirections.Left || dir == HeadDirections.Right)
                        HeadDirectionList[int.Parse(vs.Last())].Dir = dir;
                    else
                        HeadDirectionList[int.Parse(vs.Last())].Dir = HeadDirections.Left;
                }
                else
                if (findDir == "右")
                {
                    if (dir == HeadDirections.Left || dir == HeadDirections.Right)
                        HeadDirectionList[int.Parse(vs.Last())].Dir = dir;
                    else
                        HeadDirectionList[int.Parse(vs.Last())].Dir = HeadDirections.Right;
                }
                else
                if (findDir == "上")
                {
                    if (dir == HeadDirections.Up || dir == HeadDirections.Down)
                        HeadDirectionList[int.Parse(vs.Last())].Dir = dir;
                    else
                        HeadDirectionList[int.Parse(vs.Last())].Dir = HeadDirections.Up;
                }
                else
                if (findDir == "下")
                {
                    if (dir == HeadDirections.Up || dir == HeadDirections.Down)
                        HeadDirectionList[int.Parse(vs.Last())].Dir = dir;
                    else
                        HeadDirectionList[int.Parse(vs.Last())].Dir = HeadDirections.Down;
                }

                if (vs.Count > 1)
                {
                    for (int i = 0; i < vs.Count - 1; i++)
                    {
                        //旋转车身方向
                        switch (vs[i].Substring(0, 2))
                        {
                            case "左上": HeadDirectionList[int.Parse(vs.Last())].Clockwise(); break;
                            case "左下": HeadDirectionList[int.Parse(vs.Last())].Counterclockwise(); break;
                            case "右上": HeadDirectionList[int.Parse(vs.Last())].Counterclockwise(); break;
                            case "右下": HeadDirectionList[int.Parse(vs.Last())].Clockwise(); break;
                            case "上左": HeadDirectionList[int.Parse(vs.Last())].Counterclockwise(); break;
                            case "上右": HeadDirectionList[int.Parse(vs.Last())].Clockwise(); break;
                            case "下左": HeadDirectionList[int.Parse(vs.Last())].Clockwise(); break;
                            case "下右": HeadDirectionList[int.Parse(vs.Last())].Counterclockwise(); break;
                            default:
                                break;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 生成AGV车载系统的邻接关系
        /// </summary>
        /// <param name="rfid">参考标签的索引</param>
        /// <param name="dir">参考标签的车头方向</param>
        public static void GerateAgvNeighbor(int rfid, HeadDirections dir)
        {
            bool debug = true;
            if (rfid == -1 || dir == HeadDirections.None)
                return;
            //重新实例化列表
            HeadDirectionList = new List<HeadDirection>();
            //1.添加所有标签,默认标签车头方向为None
            foreach (var item in MapElement.MapObject.RFIDS)
            {
                HeadDirection head = new HeadDirection();
                HeadDirectionList.Add(head);
            }
            //2.设置参考点方向
            HeadDirectionList[rfid].Dir = dir;
            //3.根据已知点推算未知点
            int num = 0;
            //定义邻接关系序列
            List<MapNeighbour> MapNeighbours = new List<MapNeighbour>();
            //生成每个标签的邻接关系
            for (int i = 0; i < MapElement.MapObject.RFIDS.Count; i++)
            {
                MapNeighbour neighbour = GetMapNeighbour(i);
                if (neighbour != null)
                    MapNeighbours.Add(GetMapNeighbour(i));
                else
                {
                    //有错误
                    MapOperate.SystemMsg.WriteLine("提示：【{0}】号标签出错，生成失败！", MapElement.MapObject.RFIDS[i].Num);
                    return;
                }
            }
            //根据邻接关系生成所有标签的车头方向
            for (int i = 0; i < HeadDirectionList.Count; i++)
            {
                //如果规定了当前标签的方向但是没有用该标签搜索过，则搜索一次
                if (HeadDirectionList[i].Dir != HeadDirections.None && HeadDirectionList[i].Searched == false)
                {
                    GetAgvHeadDirection(MapNeighbours[i], HeadDirectionList[i].Dir);
                    HeadDirectionList[i].Searched = true;
                    //从第一个重新遍历
                    i = 0; num++;
                }
            }
            //打印结果
            if (debug)
            {
                for (int i = 0; i < HeadDirectionList.Count; i++)
                {
                    if (HeadDirectionList[i].Dir == HeadDirections.None)
                        MapOperate.SystemMsg.WriteLine("【{0}】未标记", MapElement.MapObject.RFIDS[i].Num);
                    else
                        MapOperate.SystemMsg.WriteLine("【{0}】【{1}】：", MapElement.MapObject.RFIDS[i].Num, (HeadDirections)HeadDirectionList[i].Dir);
                }
                MapOperate.SystemMsg.WriteLine("共循环：【{0}】次", num);
            }
            //4.生成AGV邻接关系表【遍历所有点】
            AGVNeighbourList = new List<AGVNeighbour>();
            for (int i = 0; i < MapNeighbours.Count; i++)
            {
                AGVNeighbour nb = new AGVNeighbour();
                nb.NowNum = i;
                //根据车头方向解析邻接关系
                switch (HeadDirectionList[i].Dir)
                {
                    case HeadDirections.None:
                        break;
                    case HeadDirections.Up:
                        {
                            //左侧
                            if (MapNeighbours[i].Left.Straight != -1)
                            {
                                //如果左面的点的车头方向要求朝左
                                if (HeadDirectionList[MapNeighbours[i].Left.Straight].Dir == HeadDirections.Left)
                                {
                                    //左旋90度，前进
                                    nb.go.TurnLeft = MapNeighbours[i].Left.Straight;
                                    nb.go.AngleLeft = 90;
                                }
                                else
                                //如果左面的点的车头方向要求朝右
                                if (HeadDirectionList[MapNeighbours[i].Left.Straight].Dir == HeadDirections.Right)
                                {
                                    //右旋90度，后退
                                    nb.back.TurnRight = MapNeighbours[i].Left.Straight;
                                    nb.back.AngleRight = 90;
                                }
                            }
                            //右侧
                            if (MapNeighbours[i].Right.Straight != -1)
                            {
                                //如果右面的点的车头方向要求朝左
                                if (HeadDirectionList[MapNeighbours[i].Right.Straight].Dir == HeadDirections.Left)
                                {
                                    //左旋90度，后退
                                    nb.back.TurnLeft = MapNeighbours[i].Right.Straight;
                                    nb.back.AngleLeft = 90;
                                }
                                else
                                //如果右面的点的车头方向要求朝右
                                if (HeadDirectionList[MapNeighbours[i].Right.Straight].Dir == HeadDirections.Right)
                                {
                                    //右旋90度，前进
                                    nb.go.TurnRight = MapNeighbours[i].Right.Straight;
                                    nb.go.AngleRight = 90;
                                }
                            }
                            //上侧
                            nb.go.LeftFork = MapNeighbours[i].Up.Left;
                            nb.go.Straight = MapNeighbours[i].Up.Straight;
                            nb.go.RightFork = MapNeighbours[i].Up.Right;
                            //下侧
                            nb.back.LeftFork = MapNeighbours[i].Down.Left;
                            nb.back.Straight = MapNeighbours[i].Down.Straight;
                            nb.back.RightFork = MapNeighbours[i].Down.Right;
                            break;
                        }
                    case HeadDirections.Right:
                        {
                            //左侧
                            nb.back.LeftFork = MapNeighbours[i].Left.Up;
                            nb.back.Straight = MapNeighbours[i].Left.Straight;
                            nb.back.RightFork = MapNeighbours[i].Left.Down;
                            //右侧
                            nb.go.LeftFork = MapNeighbours[i].Right.Up;
                            nb.go.Straight = MapNeighbours[i].Right.Straight;
                            nb.go.RightFork = MapNeighbours[i].Right.Down;
                            //上侧
                            if (MapNeighbours[i].Up.Straight != -1)
                            {
                                //如果上面的点的车头方向要求朝上
                                if (HeadDirectionList[MapNeighbours[i].Up.Straight].Dir == HeadDirections.Up)
                                {
                                    //左旋90度，前进
                                    nb.go.TurnLeft = MapNeighbours[i].Up.Straight;
                                    nb.go.AngleLeft = 90;
                                }
                                else
                                //如果上面的点的车头方向要求朝下
                                if (HeadDirectionList[MapNeighbours[i].Up.Straight].Dir == HeadDirections.Down)
                                {
                                    //右旋90度，后退
                                    nb.back.TurnRight = MapNeighbours[i].Up.Straight;
                                    nb.back.AngleRight = 90;
                                }
                            }
                            //下侧
                            if (MapNeighbours[i].Down.Straight != -1)
                            {
                                //如果下面的点的车头方向要求朝上
                                if (HeadDirectionList[MapNeighbours[i].Down.Straight].Dir == HeadDirections.Up)
                                {
                                    //左旋90度，后退
                                    nb.back.TurnLeft = MapNeighbours[i].Down.Straight;
                                    nb.back.AngleLeft = 90;
                                }
                                else
                                //如果下面的点的车头方向要求朝下
                                if (HeadDirectionList[MapNeighbours[i].Down.Straight].Dir == HeadDirections.Down)
                                {
                                    //右旋90度，前进
                                    nb.go.TurnRight = MapNeighbours[i].Down.Straight;
                                    nb.go.AngleRight = 90;
                                }
                            }
                            break;
                        }
                    case HeadDirections.Down:
                        {
                            //左侧
                            if (MapNeighbours[i].Left.Straight != -1)
                            {
                                //如果左面的点的车头方向要求朝左
                                if (HeadDirectionList[MapNeighbours[i].Left.Straight].Dir == HeadDirections.Left)
                                {
                                    //右旋90度，前进
                                    nb.go.TurnRight = MapNeighbours[i].Left.Straight;
                                    nb.go.AngleRight = 90;
                                }
                                else
                                //如果左面的点的车头方向要求朝右
                                if (HeadDirectionList[MapNeighbours[i].Left.Straight].Dir == HeadDirections.Right)
                                {
                                    //左旋90度，后退
                                    nb.back.TurnLeft = MapNeighbours[i].Left.Straight;
                                    nb.back.AngleLeft = 90;
                                }
                            }
                            //右侧
                            if (MapNeighbours[i].Right.Straight != -1)
                            {
                                //如果右面的点的车头方向要求朝左
                                if (HeadDirectionList[MapNeighbours[i].Right.Straight].Dir == HeadDirections.Left)
                                {
                                    //右旋90度，后退
                                    nb.back.TurnRight = MapNeighbours[i].Right.Straight;
                                    nb.back.AngleRight = 90;
                                }
                                else
                                //如果右面的点的车头方向要求朝右
                                if (HeadDirectionList[MapNeighbours[i].Right.Straight].Dir == HeadDirections.Right)
                                {
                                    //左旋90度，前进
                                    nb.go.TurnLeft = MapNeighbours[i].Right.Straight;
                                    nb.go.AngleLeft = 90;
                                }
                            }
                            //上侧
                            nb.back.LeftFork = MapNeighbours[i].Up.Right;
                            nb.back.Straight = MapNeighbours[i].Up.Straight;
                            nb.back.RightFork = MapNeighbours[i].Up.Left;
                            //下侧
                            nb.go.LeftFork = MapNeighbours[i].Down.Right;
                            nb.go.Straight = MapNeighbours[i].Down.Straight;
                            nb.go.RightFork = MapNeighbours[i].Down.Left;
                            break;
                        }
                    case HeadDirections.Left:
                        {
                            //左侧
                            nb.go.RightFork = MapNeighbours[i].Left.Up;
                            nb.go.Straight = MapNeighbours[i].Left.Straight;
                            nb.go.LeftFork = MapNeighbours[i].Left.Down;
                            //右侧
                            nb.back.RightFork = MapNeighbours[i].Right.Up;
                            nb.back.Straight = MapNeighbours[i].Right.Straight;
                            nb.back.LeftFork = MapNeighbours[i].Right.Down;
                            //上侧
                            if (MapNeighbours[i].Up.Straight != -1)
                            {
                                //如果上面的点的车头方向要求朝上
                                if (HeadDirectionList[MapNeighbours[i].Up.Straight].Dir == HeadDirections.Up)
                                {
                                    //右旋90度，前进
                                    nb.go.TurnRight = MapNeighbours[i].Up.Straight;
                                    nb.go.AngleRight = 90;
                                }
                                else
                                //如果上面的点的车头方向要求朝下
                                if (HeadDirectionList[MapNeighbours[i].Up.Straight].Dir == HeadDirections.Down)
                                {
                                    //左旋90度，后退
                                    nb.back.TurnLeft = MapNeighbours[i].Up.Straight;
                                    nb.back.AngleLeft = 90;
                                }
                            }
                            //下侧
                            if (MapNeighbours[i].Down.Straight != -1)
                            {
                                //如果下面的点的车头方向要求朝上
                                if (HeadDirectionList[MapNeighbours[i].Down.Straight].Dir == HeadDirections.Up)
                                {
                                    //右旋90度，后退
                                    nb.back.TurnRight = MapNeighbours[i].Down.Straight;
                                    nb.back.AngleRight = 90;
                                }
                                else
                                //如果下面的点的车头方向要求朝下
                                if (HeadDirectionList[MapNeighbours[i].Down.Straight].Dir == HeadDirections.Down)
                                {
                                    //左旋90度，前进
                                    nb.go.TurnLeft = MapNeighbours[i].Down.Straight;
                                    nb.go.AngleLeft = 90;
                                }
                            }
                            break;
                        }
                    default:
                        break;
                }
                AGVNeighbourList.Add(nb);
            }
            if (debug)
            {
                //打印结果
                foreach (var item in AGVNeighbourList)
                {
                    //前进
                    if (item.go.LeftFork != -1)
                        MapOperate.SystemMsg.WriteLine("【{0}】：前进-左分叉->【{1}】", MapElement.MapObject.RFIDS[item.NowNum].Num, MapElement.MapObject.RFIDS[item.go.LeftFork].Num);
                    if (item.go.Straight != -1)
                        MapOperate.SystemMsg.WriteLine("【{0}】：前进-直行->【{1}】", MapElement.MapObject.RFIDS[item.NowNum].Num, MapElement.MapObject.RFIDS[item.go.Straight].Num);
                    if (item.go.RightFork != -1)
                        MapOperate.SystemMsg.WriteLine("【{0}】：前进-右分叉->【{1}】", MapElement.MapObject.RFIDS[item.NowNum].Num, MapElement.MapObject.RFIDS[item.go.RightFork].Num);

                    if (item.go.TurnLeft != -1)
                        MapOperate.SystemMsg.WriteLine("【{0}】：前进-左旋【{1}】度->【{2}】", MapElement.MapObject.RFIDS[item.NowNum].Num, item.go.AngleLeft, MapElement.MapObject.RFIDS[item.go.TurnLeft].Num);
                    if (item.go.TurnRight != -1)
                        MapOperate.SystemMsg.WriteLine("【{0}】：前进-右旋【{1}】度->【{2}】", MapElement.MapObject.RFIDS[item.NowNum].Num, item.go.AngleLeft, MapElement.MapObject.RFIDS[item.go.TurnRight].Num);

                    //后退
                    if (item.back.LeftFork != -1)
                        MapOperate.SystemMsg.WriteLine("【{0}】：后退-左分叉->【{1}】", MapElement.MapObject.RFIDS[item.NowNum].Num, MapElement.MapObject.RFIDS[item.back.LeftFork].Num);
                    if (item.back.Straight != -1)
                        MapOperate.SystemMsg.WriteLine("【{0}】：后退-直行->【{1}】", MapElement.MapObject.RFIDS[item.NowNum].Num, MapElement.MapObject.RFIDS[item.back.Straight].Num);
                    if (item.back.RightFork != -1)
                        MapOperate.SystemMsg.WriteLine("【{0}】：后退-右分叉->【{1}】", MapElement.MapObject.RFIDS[item.NowNum].Num, MapElement.MapObject.RFIDS[item.back.RightFork].Num);


                    if (item.back.TurnLeft != -1)
                        MapOperate.SystemMsg.WriteLine("【{0}】：后退-左旋【{1}】度->【{2}】", MapElement.MapObject.RFIDS[item.NowNum].Num, item.back.AngleLeft, MapElement.MapObject.RFIDS[item.back.TurnLeft].Num);
                    if (item.back.TurnRight != -1)
                        MapOperate.SystemMsg.WriteLine("【{0}】：后退-右旋【{1}】度->【{2}】", MapElement.MapObject.RFIDS[item.NowNum].Num, item.back.AngleLeft, MapElement.MapObject.RFIDS[item.back.TurnRight].Num);
                }
            }

            //显示车头方向
            MapElement.CvAGVHeadDir.Children.Clear();
            for (int i = 0; i < MapElement.MapObject.RFIDS.Count; i++)
            {
                if (HeadDirectionList[i].Dir != HeadDirections.None)
                {
                    Ellipse ellipse = new Ellipse();
                    double radius = 3;
                    ellipse.Width = radius * 2;
                    ellipse.Height = radius * 2;
                    ellipse.Fill = Brushes.Black;
                    Thickness thickness = new Thickness();

                    switch (HeadDirectionList[i].Dir)
                    {
                        case HeadDirections.None:
                            break;
                        case HeadDirections.Up:
                            thickness.Left = MapElement.MapObject.RFIDS[i].UpPoint.X - radius;
                            thickness.Top = MapElement.MapObject.RFIDS[i].UpPoint.Y - radius;
                            break;
                        case HeadDirections.Right:
                            thickness.Left = MapElement.MapObject.RFIDS[i].RightPoint.X - radius;
                            thickness.Top = MapElement.MapObject.RFIDS[i].RightPoint.Y - radius;
                            break;
                        case HeadDirections.Down:
                            thickness.Left = MapElement.MapObject.RFIDS[i].DownPoint.X - radius;
                            thickness.Top = MapElement.MapObject.RFIDS[i].DownPoint.Y - radius;
                            break;
                        case HeadDirections.Left:
                            thickness.Left = MapElement.MapObject.RFIDS[i].LeftPoint.X - radius;
                            thickness.Top = MapElement.MapObject.RFIDS[i].LeftPoint.Y - radius;
                            break;
                        default:
                            break;
                    }
                    ellipse.Margin = thickness;
                    MapElement.CvAGVHeadDir.Children.Add(ellipse);
                }
            }
        }

        /// <summary>
        /// 生成邻接关系
        /// </summary>
        public static void test()
        {
            //获取车头方向
            GerateAgvNeighbor(20, HeadDirections.Right);
        }
    }
}
