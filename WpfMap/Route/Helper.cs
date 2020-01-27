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
                /// <summary>
                /// 最左边
                /// </summary>
                public static double MinX = 0;
                /// <summary>
                /// 最上边
                /// </summary>
                public static double MinY = 0;
                /// <summary>
                /// 最右边
                /// </summary>
                public static double MaxX = double.MaxValue;
                /// <summary>
                /// 最下边
                /// </summary>
                public static double MaxY = double.MaxValue;
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
                public static int Left(int index, Point point)
                {
                    //定义临时坐标
                    Point pt = new Point(point.X, point.Y);
                    //标签索引
                    int id = -1;
                    for (double i = pt.X; i > Range.MinX; i -= 1)
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
                public static int Right(int index, Point point)
                {
                    //定义临时坐标
                    Point pt = new Point(point.X, point.Y);
                    //标签索引
                    int id = -1;
                    for (double i = pt.X; i < Range.MaxX; i += 1)
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
                public static int Up(int index, Point point)
                {
                    //定义临时坐标
                    Point pt = new Point(point.X, point.Y);
                    //标签索引
                    int id = -1;
                    for (double i = pt.Y; i > Range.MinY; i -= 1)
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
                public static int Down(int index, Point point)
                {
                    //定义临时坐标
                    Point pt = new Point(point.X, point.Y);
                    //标签索引
                    int id = -1;
                    for (double i = pt.Y; i < Range.MaxY; i += 1)
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
                /// <returns></returns>
                public static int LeftUp(Point point)
                {
                    if (MapElement.MapObject.ForkLines.Count == 0)
                        return -1;
                    //定义临时坐标
                    Point pt = new Point(point.X, point.Y);
                    //分叉索引
                    for (double i = pt.X; i > Range.MinX; i -= 1)
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
                /// <returns></returns>
                public static int LeftDown(Point point)
                {
                    if (MapElement.MapObject.ForkLines.Count == 0)
                        return -1;
                    //定义临时坐标
                    Point pt = new Point(point.X, point.Y);
                    //分叉索引
                    for (double i = pt.X; i > Range.MinX; i -= 1)
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
                /// <returns></returns>
                public static int RightUp(Point point)
                {
                    if (MapElement.MapObject.ForkLines.Count == 0)
                        return -1;
                    //定义临时坐标
                    Point pt = new Point(point.X, point.Y);
                    //分叉索引
                    for (double i = pt.X; i < Base.Range.MaxX; i += 1)
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
                /// <returns></returns>
                public static int RightDown(Point point)
                {
                    if (MapElement.MapObject.ForkLines.Count == 0)
                        return -1;
                    //定义临时坐标
                    Point pt = new Point(point.X, point.Y);
                    //分叉索引
                    for (double i = pt.X; i  < Base.Range.MaxX; i += 1)
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
                /// <returns></returns>
                public static int UpLeft(Point point)
                {
                    if (MapElement.MapObject.ForkLines.Count == 0)
                        return -1;
                    //定义临时坐标
                    Point pt = new Point(point.X, point.Y);
                    //分叉索引
                    for (double i = pt.Y; i > Range.MinY; i -= 1)
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
                /// <returns></returns>
                public static int UpRight(Point point)
                {
                    if (MapElement.MapObject.ForkLines.Count == 0)
                        return -1;
                    //定义临时坐标
                    Point pt = new Point(point.X, point.Y);
                    //分叉索引
                    for (double i = pt.Y; i > Range.MinY; i -= 1)
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
                /// <returns></returns>
                public static int DownLeft(Point point)
                {
                    if (MapElement.MapObject.ForkLines.Count == 0)
                        return -1;
                    //定义临时坐标
                    Point pt = new Point(point.X, point.Y);
                    //分叉索引
                    for (double i = pt.Y; i < Range.MaxY; i += 1)
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
                /// <returns></returns>
                public static int DownRight(Point point)
                {
                    if (MapElement.MapObject.ForkLines.Count == 0)
                        return -1;
                    //定义临时坐标
                    Point pt = new Point(point.X, point.Y);
                    //分叉索引
                    for (double i = pt.Y; i < Range.MaxY; i += 1)
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
        /// 生成一个点的邻接关系
        /// </summary>
        /// <param name="index">标签号</param>
        public static void GenerateNeighbour(int index)
        {
            //清除选中
            for (int i = 0; i < MapElement.MapObject.RFIDS.Count; i++)
            {
                MapFunction.SetRFIDIsNormal(i);
            }
            for (int i = 0; i < MapElement.MapObject.ForkLines.Count; i++)
            {
                MapFunction.SetRouteForkLineIsNormal(i);
            }
            //计算标签上下左右四个点坐标
            double x = MapElement.MapObject.RFIDS[index].ellipse.Margin.Left;
            double y = MapElement.MapObject.RFIDS[index].ellipse.Margin.Top;
            double w = MapElement.MapObject.RFIDS[index].SelectRectangle.Width;
            double h = MapElement.MapObject.RFIDS[index].SelectRectangle.Height;
            Point pointLeft = new Point(x, y + h / 2);
            Point pointRight = new Point(x + w, y + h / 2);
            Point pointUp = new Point(x + w / 2, y);
            Point pointDown = new Point(x + w / 2, y + h);
                            
            /*-----------向左搜索【标签】----------------------*/
            int id = Base.FindRFID.Left(index, pointLeft);
            if (id != -1)
            {
                //设置选中
                MapFunction.SetRFIDIsSelected(id);
                MapOperate.SystemMsg.WriteLine("向左找到【{0}】号标签!", MapElement.MapObject.RFIDS[id].Num);
            }
            /*-----------向右搜索【标签】----------------------*/
            id = Base.FindRFID.Right(index, pointRight);
            if (id != -1)
            {
                //设置选中
                MapFunction.SetRFIDIsSelected(id);
                MapOperate.SystemMsg.WriteLine("向右找到【{0}】号标签!", MapElement.MapObject.RFIDS[id].Num);
            }
            /*-----------向上搜索【标签】----------------------*/
            id = Base.FindRFID.Up(index, pointUp);
            if (id != -1)
            {
                //设置选中
                MapFunction.SetRFIDIsSelected(id);
                MapOperate.SystemMsg.WriteLine("向上找到【{0}】号标签!", MapElement.MapObject.RFIDS[id].Num);
            }
            /*-----------向下搜索【标签】----------------------*/
            id = Base.FindRFID.Down(index, pointDown);
            if (id != -1)
            {
                //设置选中
                MapFunction.SetRFIDIsSelected(id);
                MapOperate.SystemMsg.WriteLine("向下找到【{0}】号标签!", MapElement.MapObject.RFIDS[id].Num);
            }



            /*-----------向【左】搜索【上分叉】----------------------*/
            id = Base.FindForkLine.LeftUp(pointLeft);
            //找到分叉，继续找标签
            if (id != -1)
            {
                //设置选中
                MapFunction.SetForkLineIsSelected(id);
                MapOperate.SystemMsg.WriteLine("向左找到【{0}】号【上分叉】!", MapElement.MapObject.ForkLines[id].Num);
                //沿分叉再找标签
            }
            /*-----------向【左】搜索【下分叉】----------------------*/
            id = Base.FindForkLine.LeftDown(pointLeft);
            //找到分叉，继续找标签
            if (id != -1)
            {
                //设置选中
                MapFunction.SetForkLineIsSelected(id);
                MapOperate.SystemMsg.WriteLine("向左找到【{0}】号【下分叉】!", MapElement.MapObject.ForkLines[id].Num);
                //沿分叉再找标签
            }
            /*-----------向【右】搜索【上分叉】----------------------*/
            id = Base.FindForkLine.RightUp(pointRight);
            //找到分叉，继续找标签
            if (id != -1)
            {
                //设置选中
                MapFunction.SetForkLineIsSelected(id);
                MapOperate.SystemMsg.WriteLine("向右找到【{0}】号【上分叉】!", MapElement.MapObject.ForkLines[id].Num);
                //沿分叉再找标签
            }
            /*-----------向【右】搜索【下分叉】----------------------*/
            id = Base.FindForkLine.RightDown(pointRight);
            //找到分叉，继续找标签
            if (id != -1)
            {
                //设置选中
                MapFunction.SetForkLineIsSelected(id);
                MapOperate.SystemMsg.WriteLine("向右找到【{0}】号【下分叉】!", MapElement.MapObject.ForkLines[id].Num);
                //沿分叉再找标签
            }


            /*-----------向【上】搜索【左分叉】----------------------*/
            id = Base.FindForkLine.UpLeft(pointUp);
            //找到分叉，继续找标签
            if (id != -1)
            {
                //设置选中
                MapFunction.SetForkLineIsSelected(id);
                MapOperate.SystemMsg.WriteLine("向上找到【{0}】号【左分叉】!", MapElement.MapObject.ForkLines[id].Num);
                //沿分叉再找标签
            }
            /*-----------向【上】搜索【右分叉】----------------------*/
            id = Base.FindForkLine.UpRight(pointUp);
            //找到分叉，继续找标签
            if (id != -1)
            {
                //设置选中
                MapFunction.SetForkLineIsSelected(id);
                MapOperate.SystemMsg.WriteLine("向上找到【{0}】号【右分叉】!", MapElement.MapObject.ForkLines[id].Num);
                //沿分叉再找标签
            }
            /*-----------向【下】搜索【左分叉】----------------------*/
            id = Base.FindForkLine.DownLeft(pointDown);
            //找到分叉，继续找标签
            if (id != -1)
            {
                //设置选中
                MapFunction.SetForkLineIsSelected(id);
                MapOperate.SystemMsg.WriteLine("向下找到【{0}】号【左分叉】!", MapElement.MapObject.ForkLines[id].Num);
                //沿分叉再找标签
            }
            /*-----------向【下】搜索【右分叉】----------------------*/
            id = Base.FindForkLine.DownRight(pointDown);
            //找到分叉，继续找标签
            if (id != -1)
            {
                //设置选中
                MapFunction.SetForkLineIsSelected(id);
                MapOperate.SystemMsg.WriteLine("向下找到【{0}】号【右分叉】!", MapElement.MapObject.ForkLines[id].Num);
                //沿分叉再找标签
            }


            MapOperate.SystemMsg.WriteLine("------------------------");
        }
    }
}
