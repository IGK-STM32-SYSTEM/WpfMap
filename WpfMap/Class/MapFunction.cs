using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace WpfMap
{
    public class MapFunction
    {
        public static SolidColorBrush GetSolid(byte alpha, Color color)
        {
            return new SolidColorBrush(Color.FromArgb(100, color.R, color.G, color.B));
        }
        /*-----------------RFID-------------------------------*/
        /// <summary>
        /// 判断坐标是否在某个标签上
        /// </summary>
        /// <param name="point">光标坐标</param>
        /// <returns>在：返回标签索引，不在：返回-1</returns>
        public static int IsOnRFID(Point point)
        {
            if (MapElement.MapObject.MapRFIDList.Count == 0)
                return -1;
            //遍历所有标签
            for (int i = 0; i < MapElement.MapObject.MapRFIDList.Count; i++)
            {
                Point pt = new Point(MapElement.MapObject.MapRFIDList[i].ellipse.Margin.Left + MapElement.RFID_Radius, MapElement.MapObject.MapRFIDList[i].ellipse.Margin.Top + MapElement.RFID_Radius);
                var distance = MathHelper.Distance(pt, point);
                //距离小于20,在当前站点上
                if (distance <= 20)
                {
                    return i;
                }
            }
            return -1;
        }
        /// <summary>
        /// 判断坐标是否在指定标签上
        /// </summary>
        /// <param name="point">光标坐标</param>
        /// <param name="index">RFID索引</param>
        /// <returns>在：返回标签索引，不在：返回-1</returns>
        public static bool IsOnOneRFID(Point point, int index)
        {
            if (MapElement.MapObject.MapRFIDList.Count == 0)
                return false;
            Point pt = new Point(MapElement.MapObject.MapRFIDList[index].ellipse.Margin.Left + MapElement.RFID_Radius, MapElement.MapObject.MapRFIDList[index].ellipse.Margin.Top + MapElement.RFID_Radius);
            var distance = MathHelper.Distance(pt, point);
            //距离小于20,在当前站点上
            if (distance <= 20)
            {
                return true;
            }
            else
                return false;
        }
        /// <summary>
        /// 移动RFID到指定位置
        /// </summary>
        /// <param name="index">RFID索引</param>
        /// <param name="point">目标位置</param>
        /// <param name="canvas">画布</param>
        public static void MoveRFIDTo(int index, Point point)
        {
            point.X -= MapElement.RFID_Radius-MapElement.GridSize/2;
            point.Y -= MapElement.RFID_Radius - MapElement.GridSize / 2;

            //计算xy方向偏差
            Thickness thickness = MapElement.MapObject.MapRFIDList[index].ellipse.Margin;
            //对MapElement.GridSize取余，实现移动时按照栅格移动效果
            double diff_x = thickness.Left - (point.X - point.X % MapElement.GridSize);
            double diff_y = thickness.Top - (point.Y - point.Y % MapElement.GridSize);
            //移动圆
            thickness.Left -= diff_x;
            thickness.Top -= diff_y;
            MapElement.MapObject.MapRFIDList[index].ellipse.Margin = thickness;

            //选择框跟随
            MapElement.MapObject.MapRFIDList[index].SelectRectangle.Margin = thickness;

            //文字跟随即可
            thickness.Left -= diff_x - 15;
            thickness.Top -= diff_y - 10;
            MapElement.MapObject.MapRFIDList[index].textBlock.Margin = thickness;

        }
        /// <summary>
        /// 设置标到选中状态
        /// </summary>
        /// <param name="index"></param>
        /// <param name="canvas"></param>
        public static void SetRFIDIsSelected(int index)
        {
            SetRFIDIsSelected(MapElement.MapObject.MapRFIDList[index]);
        }
        public static void SetRFIDIsSelected(MapElement.RFID rfid)
        {
            if (rfid == null)
                return;
            rfid.SelectRectangle.Fill = CavnvasBase.GetSolid(120, Colors.Green);
            rfid.SelectRectangle.StrokeThickness = 0.5;
            rfid.SelectRectangle.Stroke = CavnvasBase.GetSolid(180, Colors.Gray);
            rfid.SelectRectangle.Width = MapElement.RFID_Radius * 2;
            rfid.SelectRectangle.Height = MapElement.RFID_Radius * 2;
            rfid.SelectRectangle.Margin = rfid.ellipse.Margin;
            //显示虚线
            //rfid.SelectRectangle.StrokeDashArray = new DoubleCollection() { 5, 2 };
            //rfid.SelectRectangle.StrokeDashCap = PenLineCap.Flat;
            if (MapElement.CvRFID.Children.Contains(rfid.SelectRectangle))
                return;
            MapElement.CvRFID.Children.Add(rfid.SelectRectangle);
        }
        /// <summary>
        /// 设置标到正常状态
        /// </summary>
        /// <param name="index"></param>
        /// <param name="canvas"></param>
        public static void SetRFIDIsNormal(int index)
        {
            if (index == -1)
                return;
            MapElement.CvRFID.Children.Remove(MapElement.MapObject.MapRFIDList[index].SelectRectangle);
        }
        public static void SetRFIDIsNormal(MapElement.RFID rfid)
        {
            if (rfid.SelectRectangle == null)
                return;
            MapElement.CvRFID.Children.Remove(rfid.SelectRectangle);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="index"></param>
        public static void RemoveRFID(int index)
        {
            //从画布移除
            MapElement.CvRFID.Children.Remove(MapElement.MapObject.MapRFIDList[index].ellipse);
            MapElement.CvRFID.Children.Remove(MapElement.MapObject.MapRFIDList[index].textBlock);
            MapElement.CvRFID.Children.Remove(MapElement.MapObject.MapRFIDList[index].SelectRectangle);
            //从列表移除
            MapElement.MapObject.MapRFIDList.RemoveAt(index);
        }
        /*-----------------直线-------------------------------*/
        /// <summary>
        /// 判断坐标是否在某条直线上
        /// </summary>
        /// <param name="point">光标坐标</param>
        /// <returns>在：返回标签索引，不在：返回-1</returns>
        public static int IsOnRouteLine(Point point)
        {
            if (MapElement.MapObject.MapLineList.Count == 0)
                return -1;
            //遍历
            for (int i = 0; i < MapElement.MapObject.MapLineList.Count; i++)
            {
                //拿到直线
                Line line = MapElement.MapObject.MapLineList[i].line;
                Point pt = point;
                pt.X -= line.Margin.Left;
                pt.Y -= line.Margin.Top;
                double dis = MathHelper.DistancePointToLine(pt, line);
                if (dis < 5)
                    return i;
            }
            return -1;
        }
        /// <summary>
        /// 判断坐标是否在指定直线的【起点】编辑器上
        /// </summary>
        /// <param name="point">光标坐标</param>
        /// <returns>在：返回标签索引，不在：返回-1</returns>
        public static bool IsOnOneLineStart(int index, Point point)
        {
            if (index < 0)
                return false;
            //拿到编辑器的矩形
            Rectangle rectangle = MapElement.MapObject.MapLineList[index].StartRect;
            Point pt = point;
            //平移margin，让当前点和矩形在同一坐标系
            pt.X -= rectangle.Margin.Left;
            pt.Y -= rectangle.Margin.Top;
            bool rs = MathHelper.PointInRect(new Rect(0, 0, rectangle.Width, rectangle.Height), pt);
            return rs;
        }
        /// <summary>
        /// 判断坐标是否在指定直线的【终点】编辑器上
        /// </summary>
        /// <param name="point">光标坐标</param>
        /// <returns>在：返回标签索引，不在：返回-1</returns>
        public static bool IsOnOneLineEnd(int index, Point point)
        {
            if (index < 0)
                return false;
            //拿到编辑器的矩形
            Rectangle rectangle = MapElement.MapObject.MapLineList[index].EndRect;
            Point pt = point;
            //平移margin，让当前点和矩形在同一坐标系
            pt.X -= rectangle.Margin.Left;
            pt.Y -= rectangle.Margin.Top;
            bool rs = MathHelper.PointInRect(new Rect(0, 0, rectangle.Width, rectangle.Height), pt);
            return rs;
        }
        /// <summary>
        /// 更新直线到指定位置【添加状态】
        /// </summary>
        /// <param name="index">索引</param>
        /// <param name="point">目标位置</param>
        public static void MoveRouteLineStartForAdd(int index, Point point)
        {
            point.X -= MapElement.GridSize / 2;
            point.Y -= MapElement.GridSize / 2;

            //对MapElement.GridSize取余，实现移动时按照栅格移动效果
            Thickness thickness = MapElement.MapObject.MapLineList[index].line.Margin;
            thickness.Left = point.X - point.X % MapElement.GridSize;
            thickness.Top = point.Y - point.Y % MapElement.GridSize;

            //起点编辑器跟随
            thickness.Left += MapElement.GridSize / 2;
            thickness.Top += MapElement.GridSize / 2;
            MapElement.MapObject.MapLineList[index].StartRect.Margin = thickness;
        }
        /// <summary>
        /// 移动直线起点位置【编辑状态】
        /// </summary>
        /// <param name="index">索引</param>
        /// <param name="point">目标位置</param>
        public static void MoveRouteLineStart(int index, Point point)
        {
            point.X -= MapElement.GridSize / 2;
            point.Y -= MapElement.GridSize / 2;

            //对MapElement.GridSize取余，实现移动时按照栅格移动效果
            Thickness thickness = MapElement.MapObject.MapLineList[index].line.Margin;
            thickness.Left = point.X - point.X % MapElement.GridSize;
            thickness.Top = point.Y - point.Y % MapElement.GridSize;

            ////更新线起点坐标
            //MapElement.MapObject.MapLineList[index].line.X1 = thickness.Left + MapElement.GridSize - MapElement.MapObject.MapLineList[index].line.Margin.Left;
            //MapElement.MapObject.MapLineList[index].line.Y1 = thickness.Top + MapElement.GridSize - MapElement.MapObject.MapLineList[index].line.Margin.Top;

            ////选择线跟随
            //MapElement.MapObject.MapLineList[index].SelectLine.X1 = MapElement.MapObject.MapLineList[index].line.X1;
            //MapElement.MapObject.MapLineList[index].SelectLine.Y1 = MapElement.MapObject.MapLineList[index].line.Y1;

            //更新终点坐标
            MapElement.MapObject.MapLineList[index].line.X2 -= thickness.Left + MapElement.GridSize - MapElement.MapObject.MapLineList[index].line.Margin.Left;
            MapElement.MapObject.MapLineList[index].line.Y2 -= thickness.Top + MapElement.GridSize - MapElement.MapObject.MapLineList[index].line.Margin.Top;
            //选择线跟随
            MapElement.MapObject.MapLineList[index].SelectLine.X2 = MapElement.MapObject.MapLineList[index].line.X2;
            MapElement.MapObject.MapLineList[index].SelectLine.Y2 = MapElement.MapObject.MapLineList[index].line.Y2;

            //修正直线margin
            Thickness tk = MapElement.MapObject.MapLineList[index].line.Margin;
            tk.Left += thickness.Left + MapElement.GridSize - MapElement.MapObject.MapLineList[index].line.Margin.Left;
            tk.Top += thickness.Top + MapElement.GridSize - MapElement.MapObject.MapLineList[index].line.Margin.Top;
            MapElement.MapObject.MapLineList[index].line.Margin = tk;
            //选择线margin跟随
            MapElement.MapObject.MapLineList[index].SelectLine.Margin = tk;

            //起点编辑器跟随
            thickness.Left += MapElement.GridSize / 2;
            thickness.Top += MapElement.GridSize / 2;
            MapElement.MapObject.MapLineList[index].StartRect.Margin = thickness;
        }
        /// <summary>
        /// 移动直线终点位置【编辑状态】
        /// </summary>
        /// <param name="index">索引</param>
        /// <param name="point">目标位置</param>
        public static void MoveRouteLineEnd(int index, Point point)
        {
            point.X -= MapElement.GridSize / 2;
            point.Y -= MapElement.GridSize / 2;

            //对MapElement.GridSize取余，实现移动时按照栅格移动效果
            Thickness thickness = MapElement.MapObject.MapLineList[index].line.Margin;
            thickness.Left = point.X - point.X % MapElement.GridSize;
            thickness.Top = point.Y - point.Y % MapElement.GridSize;

            //更新线终点坐标
            MapElement.MapObject.MapLineList[index].line.X2 = thickness.Left + MapElement.GridSize - MapElement.MapObject.MapLineList[index].line.Margin.Left;
            MapElement.MapObject.MapLineList[index].line.Y2 = thickness.Top + MapElement.GridSize - MapElement.MapObject.MapLineList[index].line.Margin.Top;

            //选择线跟随
            MapElement.MapObject.MapLineList[index].SelectLine.X2 = MapElement.MapObject.MapLineList[index].line.X2;
            MapElement.MapObject.MapLineList[index].SelectLine.Y2 = MapElement.MapObject.MapLineList[index].line.Y2;

            //终点编辑器跟随
            thickness.Left += MapElement.GridSize / 2;
            thickness.Top += MapElement.GridSize / 2;
            MapElement.MapObject.MapLineList[index].EndRect.Margin = thickness;
        }
        /// <summary>
        /// 更新直线到指定位置【编辑状态】
        /// </summary>
        /// <param name="index">索引</param>
        /// <param name="point">目标位置</param>
        public static void MoveRouteLineAll(int index, Point point)
        {
            //获取移动偏差
            double difx = MapOperate.mouseLeftBtnDownMoveDiff.X;
            double dify = MapOperate.mouseLeftBtnDownMoveDiff.Y;

            //提高变化阈值【当光标超过半个栅格就发生跳格】
            if (difx > 0)
                difx += MapElement.GridSize* 0.6;
            else
                difx -= MapElement.GridSize * 0.6;
            if (dify > 0)
                dify += MapElement.GridSize * 0.6;
            else
                dify -= MapElement.GridSize * 0.6;

            //对齐栅格
            difx -= difx % MapElement.GridSize;
            dify -= dify % MapElement.GridSize;
            Console.WriteLine("x:{0},y:{1}",difx,dify);

            //计算起点编辑器和终点编辑器margin的偏差
            double margin_Diff_Left = MapElement.MapObject.MapLineList[index].EndRect.Margin.Left - MapElement.MapObject.MapLineList[index].StartRect.Margin.Left;
            double margin_Diff_Top = MapElement.MapObject.MapLineList[index].EndRect.Margin.Top - MapElement.MapObject.MapLineList[index].StartRect.Margin.Top;

            //移动线
            Thickness tk = new Thickness();
            tk.Left = MapOperate.ElementMarginLast.Left;
            tk.Top = MapOperate.ElementMarginLast.Top;

            tk.Left += difx;
            tk.Top += dify;

            MapElement.MapObject.MapLineList[index].line.Margin = tk;
            //选择线跟随
            MapElement.MapObject.MapLineList[index].SelectLine.Margin = tk;
            //起点编辑器跟随【减X1和Y1是因为在移动起点时修改了X1和Y1，而不是整体移动Margin】
            tk.Left -= MapElement.GridSize / 2;
            tk.Top -= MapElement.GridSize / 2;
            //tk.Left = tk.Left - MapElement.GridSize / 2 - MapElement.MapObject.MapLineList[index].line.X1;
            //tk.Top = tk.Top - MapElement.GridSize / 2 - MapElement.MapObject.MapLineList[index].line.Y1;
            MapElement.MapObject.MapLineList[index].StartRect.Margin = tk;
            //终点编辑器跟随
            tk.Left += margin_Diff_Left;
            tk.Top += margin_Diff_Top;
            MapElement.MapObject.MapLineList[index].EndRect.Margin = tk;
        }
        /// <summary>
        /// 设置标到选中状态
        /// </summary>
        /// <param name="index"></param>
        /// <param name="canvas"></param>
        public static void SetRouteLineIsSelected(int index)
        {
            if (index == -1)
                return;
            //显示选择虚线
            if (MapElement.CvRouteLine.Children.Contains(MapElement.MapObject.MapLineList[index].SelectLine) == false)
            {
                MapElement.RouteLineShowSelectLine(index);
            }
            //显示起点编辑器
            if (MapElement.CvRouteLine.Children.Contains(MapElement.MapObject.MapLineList[index].StartRect) == false)
            {
                MapElement.RouteLineShowStart(index);
            }
            //显示终点编辑器
            if (MapElement.CvRouteLine.Children.Contains(MapElement.MapObject.MapLineList[index].EndRect) == false)
            {
                MapElement.RouteLineShowEnd(index);
            }
        }
        public static void SetRouteLineIsSelected(MapElement.RouteLine routeLine)
        {
            if (routeLine == null)
                return;
            //显示选择虚线
            if (MapElement.CvRouteLine.Children.Contains(routeLine.SelectLine) == false)
            {
                MapElement.RouteLineShowSelectLine(routeLine);
            }
            //显示起点编辑器
            if (MapElement.CvRouteLine.Children.Contains(routeLine.StartRect) == false)
            {
                MapElement.RouteLineShowStart(routeLine);
            }
            //显示终点编辑器
            if (MapElement.CvRouteLine.Children.Contains(routeLine.EndRect) == false)
            {
                MapElement.RouteLineShowEnd(routeLine);
            }
        }
        /// <summary>
        /// 设置标到正常状态
        /// </summary>
        /// <param name="index"></param>
        /// <param name="canvas"></param>
        public static void SetRouteLineIsNormal(int index)
        {
            if (index == -1)
                return;
            MapElement.CvRouteLine.Children.Remove(MapElement.MapObject.MapLineList[index].SelectLine);
            MapElement.CvRouteLine.Children.Remove(MapElement.MapObject.MapLineList[index].StartRect);
            MapElement.CvRouteLine.Children.Remove(MapElement.MapObject.MapLineList[index].EndRect);
            MapElement.CvRouteLine.Children.Remove(MapElement.MapObject.MapLineList[index].textBlock);
        }
        public static void SetRouteLineIsNormal(MapElement.RouteLine routeLine)
        {
            if (routeLine == null)
                return;
            MapElement.CvRouteLine.Children.Remove(routeLine.SelectLine);
            MapElement.CvRouteLine.Children.Remove(routeLine.StartRect);
            MapElement.CvRouteLine.Children.Remove(routeLine.EndRect);
            MapElement.CvRouteLine.Children.Remove(routeLine.textBlock);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="index"></param>
        public static void RemoveRouteLine(int index)
        {
            //从画布移除
            MapElement.CvRouteLine.Children.Remove(MapElement.MapObject.MapLineList[index].line);
            MapElement.CvRouteLine.Children.Remove(MapElement.MapObject.MapLineList[index].textBlock);
            MapElement.CvRouteLine.Children.Remove(MapElement.MapObject.MapLineList[index].SelectLine);
            MapElement.CvRouteLine.Children.Remove(MapElement.MapObject.MapLineList[index].StartRect);
            MapElement.CvRouteLine.Children.Remove(MapElement.MapObject.MapLineList[index].EndRect);
            //从列表移除
            MapElement.MapObject.MapLineList.RemoveAt(index);
        }
        /*-----------------分叉线-----------------------------*/
        /// <summary>
        /// 判断坐标是否在圆弧上
        /// </summary>
        /// <param name="point">光标坐标</param>
        /// <returns>在：返回标签索引，不在：返回-1</returns>
        public static int IsOnForkLine(Point point)
        {
            if (MapElement.MapObject.MapForkLineList.Count == 0)
                return -1;

            //遍历
            for (int i = 0; i < MapElement.MapObject.MapForkLineList.Count; i++)
            {
                //取当前坐标【需要margin对齐】
                Point nowPoint = point;
                nowPoint.X -= MapElement.MapObject.MapForkLineList[i].Path.Margin.Left;
                nowPoint.Y -= MapElement.MapObject.MapForkLineList[i].Path.Margin.Top;
                //找到圆弧起点和终点坐标
                PathGeometry pathGeometry = MapElement.MapObject.MapForkLineList[i].Path.Data as PathGeometry;
                PathFigure figure = pathGeometry.Figures.First();
                ArcSegment arc = figure.Segments.First() as ArcSegment;
                //圆弧起点
                Point start = figure.StartPoint;
                //圆弧终点
                Point end = arc.Point;
                end.Y = -end.Y;

                //圆弧半径
                double radius = arc.Size.Width;
                //找到圆心坐标
                Point center = new Point();
                //逆时针
                if (arc.SweepDirection == SweepDirection.Counterclockwise)
                {
                    if (end.X > start.X && end.Y > start.Y)
                    {
                        center.X = start.X;
                        center.Y = end.Y;
                    }
                    else
                    if (end.X < start.X && end.Y > start.Y)
                    {
                        center.X = end.X;
                        center.Y = start.Y;
                    }
                    else
                    if (end.X < start.X && end.Y < start.Y)
                    {
                        center.X = start.X;
                        center.Y = end.Y;
                    }
                    else
                    if (end.X > start.X && end.Y < start.Y)
                    {
                        center.X = end.X;
                        center.Y = start.Y;
                    }
                }
                //顺时针
                if (arc.SweepDirection == SweepDirection.Clockwise)
                {
                    if (end.X > start.X && end.Y > start.Y)
                    {
                        center.X = end.X;
                        center.Y = start.Y;
                    }
                    else
                    if (end.X < start.X && end.Y > start.Y)
                    {
                        center.X = start.X;
                        center.Y = end.Y;
                    }
                    else
                    if (end.X < start.X && end.Y < start.Y)
                    {
                        center.X = end.X;
                        center.Y = start.Y;
                    }
                    else
                    if (end.X > start.X && end.Y < start.Y)
                    {
                        center.X = start.X;
                        center.Y = end.Y;
                    }
                }
                //计算到圆心距离
                double dis = MathHelper.Distance(center, nowPoint);
                //判断是否在圆上【允许偏差 5】
                if ((dis < (radius + 5)) && (dis > (radius - 5)))
                {
                    //恢复Y轴方向
                    end.Y = -end.Y;
                    //判断是否在圆弧范围内【X、Y都在起点和终点的XY之间】
                    double xMax = start.X > end.X ? start.X : end.X;
                    double xMin = start.X < end.X ? start.X : end.X;

                    double yMax = start.Y > end.Y ? start.Y : end.Y;
                    double yMin = start.Y < end.Y ? start.Y : end.Y;

                    if (nowPoint.X > xMin && nowPoint.X < xMax && nowPoint.Y > yMin && nowPoint.Y < yMax)
                        return i;
                }
            }
            return -1;
        }
        /// <summary>
        /// 判断坐标是否在指定【起点】编辑器上
        /// </summary>
        /// <param name="point">光标坐标</param>
        /// <returns>在：返回标签索引，不在：返回-1</returns>
        public static bool IsOnOneForkLineStart(int index, Point point)
        {
            if (index < 0)
                return false;
            //拿到编辑器的矩形
            Rectangle rectangle = MapElement.MapObject.MapForkLineList[index].StartRect;
            Point pt = point;
            //平移margin，让当前点和矩形在同一坐标系
            pt.X -= rectangle.Margin.Left;
            pt.Y -= rectangle.Margin.Top;
            bool rs = MathHelper.PointInRect(new Rect(0, 0, rectangle.Width, rectangle.Height), pt);
            return rs;
        }
        /// <summary>
        /// 判断坐标是否在指定【终点】编辑器上
        /// </summary>
        /// <param name="point">光标坐标</param>
        /// <returns>在：返回标签索引，不在：返回-1</returns>
        public static bool IsOnOneForkLineEnd(int index, Point point)
        {
            if (index < 0)
                return false;
            //拿到编辑器的矩形
            Rectangle rectangle = MapElement.MapObject.MapForkLineList[index].EndRect;
            Point pt = point;
            //平移margin，让当前点和矩形在同一坐标系
            pt.X -= rectangle.Margin.Left;
            pt.Y -= rectangle.Margin.Top;
            bool rs = MathHelper.PointInRect(new Rect(0, 0, rectangle.Width, rectangle.Height), pt);
            return rs;
        }
        /// <summary>
        /// 设置标到正常状态
        /// </summary>
        /// <param name="index"></param>
        /// <param name="canvas"></param>
        public static void SetRouteForkLineIsNormal(int index)
        {
            if (index == -1)
                return;
            MapElement.CvForkLine.Children.Remove(MapElement.MapObject.MapForkLineList[index].StartRect);
            MapElement.CvForkLine.Children.Remove(MapElement.MapObject.MapForkLineList[index].EndRect);
            MapElement.CvForkLine.Children.Remove(MapElement.MapObject.MapForkLineList[index].textBlock);
            MapElement.CvForkLine.Children.Remove(MapElement.MapObject.MapForkLineList[index].SelectPath);
        }
        public static void SetRouteForkLineIsNormal(MapElement.RouteForkLine routeForkLine)
        {
            if (routeForkLine == null)
                return;
            MapElement.CvForkLine.Children.Remove(routeForkLine.StartRect);
            MapElement.CvForkLine.Children.Remove(routeForkLine.EndRect);
            MapElement.CvForkLine.Children.Remove(routeForkLine.textBlock);
            MapElement.CvForkLine.Children.Remove(routeForkLine.SelectPath);
        }
        /// <summary>
        /// 移动起点编辑器到指定位置【添加模式】
        /// </summary>
        /// <param name="index">索引</param>
        /// <param name="point">目标位置</param>
        public static void MoveForkLineStartForAdd(int index, Point point)
        {
            point.X -= MapElement.GridSize / 2;
            point.Y -= MapElement.GridSize / 2;

            //对MapElement.GridSize取余，实现移动时按照栅格移动效果
            Thickness thickness = MapElement.MapObject.MapForkLineList[index].Path.Margin;
            thickness.Left = point.X - point.X % MapElement.GridSize;
            thickness.Top = point.Y - point.Y % MapElement.GridSize;

            //起点编辑器跟随
            thickness.Left += MapElement.GridSize / 2;
            thickness.Top += MapElement.GridSize / 2;
            MapElement.MapObject.MapForkLineList[index].StartRect.Margin = thickness;
        }
        /// <summary>
        /// 移动终点位置
        /// </summary>
        /// <param name="index">索引</param>
        /// <param name="point">目标位置</param>
        public static void MoveForkLineEnd(int index, Point point)
        {
            point.X -= MapElement.GridSize / 2;
            point.Y -= MapElement.GridSize / 2;

            //对MapElement.GridSize取余，实现移动时按照栅格移动效果
            Thickness thickness = MapElement.MapObject.MapForkLineList[index].Path.Margin;
            thickness.Left = point.X - point.X % MapElement.GridSize;
            thickness.Top = point.Y - point.Y % MapElement.GridSize;

            //终点编辑器跟随
            thickness.Left += MapElement.GridSize / 2;
            thickness.Top += MapElement.GridSize / 2;
            MapElement.MapObject.MapForkLineList[index].EndRect.Margin = thickness;

            //计算当前点【终点编辑器】和起点的偏差
            double diffx = thickness.Left - MapElement.MapObject.MapForkLineList[index].Path.Margin.Left;
            double diffy = thickness.Top - MapElement.MapObject.MapForkLineList[index].Path.Margin.Top;

            //定义圆弧半径
            int radius = MapElement.ForkLineArc_Radius;

            PathGeometry pathGeometry = new PathGeometry();
            PathFigure figure = new PathFigure();
            //路径图起点坐标
            figure.StartPoint = new Point();
            ArcSegment arc = new ArcSegment();
            //半径
            arc.Size = new Size(radius, radius);
            //旋转角度
            arc.RotationAngle = 0;
            //false
            arc.IsLargeArc = false;
            //isStroked
            arc.IsStroked = true;
            //给Y轴反向，画面坐标和XY坐标系不一致
            diffy = -diffy;
            //圆弧终点坐标
            if (diffx >= 10 && diffy <= -10)
            {
                //扫描方向
                arc.SweepDirection = SweepDirection.Counterclockwise;
                arc.Point = new Point(radius, radius);
            }
            else
            if (diffx >= 10 && diffy >= 10)
            {
                //扫描方向
                arc.SweepDirection = SweepDirection.Clockwise;
                arc.Point = new Point(radius, -radius);
            }
            else
            if (diffx <= -10 && diffy <= -10)
            {
                //扫描方向
                arc.SweepDirection = SweepDirection.Clockwise;
                arc.Point = new Point(-radius, radius);
            }
            else
            if (diffx <= -10 && diffy >= 10)
            {
                //扫描方向
                arc.SweepDirection = SweepDirection.Counterclockwise;
                arc.Point = new Point(-radius, -radius);
            }
            else
                return;
            figure.Segments.Add(arc);
            pathGeometry.Figures.Add(figure);
            MapElement.MapObject.MapForkLineList[index].Path.Data = pathGeometry;
            //同步选择曲线
            MapElement.MapObject.MapForkLineList[index].SelectPath.Data = MapElement.MapObject.MapForkLineList[index].Path.Data.Clone();
            //终点跟随到圆弧末端
            Point end = arc.Point;
            end.X += MapElement.MapObject.MapForkLineList[index].Path.Margin.Left - MapElement.GridSize / 2;
            end.Y += MapElement.MapObject.MapForkLineList[index].Path.Margin.Top - MapElement.GridSize / 2;
            MapElement.MapObject.MapForkLineList[index].EndRect.Margin = new Thickness(end.X, end.Y, 0, 0);
        }
        /// <summary>
        /// 移动分叉【圆弧】到指定位置【编辑状态】
        /// </summary>
        /// <param name="index">索引</param>
        /// <param name="point">目标位置</param>
        public static void MoveForkLineAll(int index, Point point)
        {
            //获取移动偏差
            double difx = MapOperate.mouseLeftBtnDownMoveDiff.X;
            double dify = MapOperate.mouseLeftBtnDownMoveDiff.Y;

            //提高变化阈值【当光标超过半个栅格就发生跳格】
            if (difx > 0)
                difx += MapElement.GridSize / 2;
            else
                difx -= MapElement.GridSize / 2;
            if (dify > 0)
                dify += MapElement.GridSize / 2;
            else
                dify -= MapElement.GridSize / 2;

            //对齐栅格
            difx -= difx % MapElement.GridSize;
            dify -= dify % MapElement.GridSize;

            //移动线
            Thickness tk = new Thickness();
            tk.Left = MapOperate.ElementMarginLast.Left;
            tk.Top = MapOperate.ElementMarginLast.Top;

            tk.Left += difx;
            tk.Top += dify;

            MapElement.MapObject.MapForkLineList[index].Path.Margin = tk;
            //选择线跟随
            MapElement.MapObject.MapForkLineList[index].SelectPath.Margin = tk;
            //起点编辑器跟随【减X1和Y1是因为在移动起点时修改了X1和Y1，而不是整体移动Margin】
            tk.Left -= MapElement.GridSize / 2;
            tk.Top -= MapElement.GridSize / 2;
            MapElement.MapObject.MapForkLineList[index].StartRect.Margin = tk;
            //终点编辑器跟随
            //找到圆弧终点坐标
            PathGeometry pathGeometry = MapElement.MapObject.MapForkLineList[index].Path.Data as PathGeometry;
            PathFigure figure = pathGeometry.Figures.First();
            ArcSegment arc = figure.Segments.First() as ArcSegment;
            //圆弧终点
            Point end = arc.Point;
            end.X += MapElement.MapObject.MapForkLineList[index].Path.Margin.Left - MapElement.GridSize / 2;
            end.Y += MapElement.MapObject.MapForkLineList[index].Path.Margin.Top - MapElement.GridSize / 2;
            //终点编辑器的位置和圆弧终点坐标同步
            MapElement.MapObject.MapForkLineList[index].EndRect.Margin = new Thickness(end.X, end.Y, 0, 0);
        }
        /// <summary>
        /// 设置标到选中状态
        /// </summary>
        /// <param name="index"></param>
        /// <param name="canvas"></param>
        public static void SetForkLineIsSelected(int index)
        {
            if (index == -1)
                return;
            //显示选择虚线
            if (MapElement.CvForkLine.Children.Contains(MapElement.MapObject.MapForkLineList[index].SelectPath) == false)
            {
                MapElement.ForkLineShowSelect(index);
            }
            ////显示起点编辑器
            //if (MapElement.CvForkLine.Children.Contains( MapElement.MapObject.MapForkLineList[index].StartRect) == false)
            //{
            //    MapElement.ForkLineShowStart(index);
            //}
            //显示终点编辑器
            if (MapElement.CvForkLine.Children.Contains(MapElement.MapObject.MapForkLineList[index].EndRect) == false)
            {
                MapElement.ForkLineShowEnd(index);
            }
        }
        public static void SetForkLineIsSelected(MapElement.RouteForkLine routeForkLine)
        {
            if (routeForkLine == null)
                return;
            //显示选择虚线
            if (MapElement.CvForkLine.Children.Contains(routeForkLine.SelectPath) == false)
            {
                MapElement.ForkLineShowSelect(routeForkLine);
            }
            ////显示起点编辑器
            //if (MapElement.CvForkLine.Children.Contains( MapElement.MapObject.MapForkLineList[index].StartRect) == false)
            //{
            //    MapElement.ForkLineShowStart(index);
            //}
            //显示终点编辑器
            if (MapElement.CvForkLine.Children.Contains(routeForkLine.EndRect) == false)
            {
                MapElement.ForkLineShowEnd(routeForkLine);
            }
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="index"></param>
        public static void RemoveForkLine(int index)
        {
            //从画布移除
            MapElement.CvForkLine.Children.Remove(MapElement.MapObject.MapForkLineList[index].Path);
            MapElement.CvForkLine.Children.Remove(MapElement.MapObject.MapForkLineList[index].textBlock);
            MapElement.CvForkLine.Children.Remove(MapElement.MapObject.MapForkLineList[index].SelectPath);
            MapElement.CvForkLine.Children.Remove(MapElement.MapObject.MapForkLineList[index].StartRect);
            MapElement.CvForkLine.Children.Remove(MapElement.MapObject.MapForkLineList[index].EndRect);
            //从列表移除
            MapElement.MapObject.MapForkLineList.RemoveAt(index);
        }
        /*-----------------综合-----------------------------*/
        /// <summary>
        /// 清除当前选中【单个】
        /// </summary>
        public static void ClearSelect()
        {
            switch (MapOperate.NowType)
            {
                case MapOperate.EnumElementType.RFID:
                    if (MapOperate.NowSelectIndex != -1)
                    {
                        MapFunction.SetRFIDIsNormal(MapOperate.NowSelectIndex);
                    }
                    break;
                case MapOperate.EnumElementType.RouteLine:
                    if (MapOperate.NowSelectIndex != -1)
                    {
                        MapFunction.SetRouteLineIsNormal(MapOperate.NowSelectIndex);
                    }
                    break;
                case MapOperate.EnumElementType.RouteForkLine:
                    if (MapOperate.NowSelectIndex != -1)
                    {
                        MapFunction.SetRouteForkLineIsNormal(MapOperate.NowSelectIndex);
                    }
                    break;
                default: break;
            }
        }
        public static void ClearAllSelect()
        {
            //清除RFID
            foreach (var item in MapOperate.MultiSelected.MapRFIDList)
            {
                SetRFIDIsNormal(item);
            }
            MapOperate.MultiSelected.MapRFIDList.Clear();
            //清除Line
            foreach (var item in MapOperate.MultiSelected.MapLineList)
            {
                SetRouteLineIsNormal(item);
            }
            MapOperate.MultiSelected.MapLineList.Clear();
            //清除ForkLine
            foreach (var item in MapOperate.MultiSelected.MapForkLineList)
            {
                SetRouteForkLineIsNormal(item);
            }
            MapOperate.MultiSelected.MapForkLineList.Clear();
        }
        //计算选中对象列表【多选框模式】
        public static void GetMultiSelectedObject()
        {
            //选择框的Rectangle
            Rectangle selectRectangle = MapOperate.SelectRectangle;
            //RFID
            foreach (var item in MapElement.MapObject.MapRFIDList)
            {
                //计算最小坐标
                Point min = new Point(item.ellipse.Margin.Left, item.ellipse.Margin.Top);
                //计算最大坐标
                Point max = new Point(min.X + item.ellipse.Width, min.Y + item.ellipse.Height);
                //判断是否在选框矩形内
                bool rs1 = MathHelper.PointInRect(selectRectangle, min);
                bool rs2 = MathHelper.PointInRect(selectRectangle, max);
                //如果都在范围内，判定为选中
                if (rs1 && rs2)
                {
                    //设置为选中状态
                    MapFunction.SetRFIDIsSelected(item);
                    //添加到选中列表
                    MapOperate.MultiSelected.MapRFIDList.Add(item);
                }
            }
            //Line
            foreach (var item in MapElement.MapObject.MapLineList)
            {
                //计算最小坐标
                Point min = new Point(item.line.X1 + item.line.Margin.Left, item.line.Y1 + item.line.Margin.Top);
                //计算最大坐标
                Point max = new Point(item.line.X2 + item.line.Margin.Left, item.line.Y2 + item.line.Margin.Top);
                //判断是否在选框矩形内
                bool rs1 = MathHelper.PointInRect(selectRectangle, min);
                bool rs2 = MathHelper.PointInRect(selectRectangle, max);
                //如果都在范围内，判定为选中
                if (rs1 && rs2)
                {
                    //设置为选中状态
                    MapFunction.SetRouteLineIsSelected(item);
                    //添加到选中列表
                    MapOperate.MultiSelected.MapLineList.Add(item);
                }
            }
            //ForkLine
            foreach (var item in MapElement.MapObject.MapForkLineList)
            {

                //找到圆弧起点和终点坐标
                PathGeometry pathGeometry = item.Path.Data as PathGeometry;
                PathFigure figure = pathGeometry.Figures.First();
                ArcSegment arc = figure.Segments.First() as ArcSegment;
                //圆弧起点
                Point start = figure.StartPoint;
                //做margin平移
                start.X += item.Path.Margin.Left;
                start.Y += item.Path.Margin.Top;
                //圆弧终点
                Point end = arc.Point;
                //做margin平移
                end.X += item.Path.Margin.Left;
                end.Y += item.Path.Margin.Top;

                //判断是否在选框矩形内
                bool rs1 = MathHelper.PointInRect(selectRectangle, start);
                bool rs2 = MathHelper.PointInRect(selectRectangle, end);
                //如果都在范围内，判定为选中
                if (rs1 && rs2)
                {
                    //设置为选中状态
                    MapFunction.SetForkLineIsSelected(item);
                    //添加到选中列表
                    MapOperate.MultiSelected.MapForkLineList.Add(item);
                }
            }
        }

    }
}
