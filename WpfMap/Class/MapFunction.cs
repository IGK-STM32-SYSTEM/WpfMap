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
            if (MapElement.MapRFIDList.Count == 0)
                return -1;
            //遍历所有标签
            for (int i = 0; i < MapElement.MapRFIDList.Count; i++)
            {
                Point pt = new Point(MapElement.MapRFIDList[i].ellipse.Margin.Left + MapElement.RFID_Radius, MapElement.MapRFIDList[i].ellipse.Margin.Top + MapElement.RFID_Radius);
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
            if (MapElement.MapRFIDList.Count == 0)
                return false;
            Point pt = new Point(MapElement.MapRFIDList[index].ellipse.Margin.Left + MapElement.RFID_Radius, MapElement.MapRFIDList[index].ellipse.Margin.Top + MapElement.RFID_Radius);
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
            point.X -= MapElement.GridSize / 2;
            point.Y -= MapElement.GridSize / 2;

            //计算xy方向偏差
            Thickness thickness = MapElement.MapRFIDList[index].ellipse.Margin;
            //对MapElement.GridSize取余，实现移动时按照栅格移动效果
            double diff_x = thickness.Left - (point.X - point.X % MapElement.GridSize);
            double diff_y = thickness.Top - (point.Y - point.Y % MapElement.GridSize);
            //移动圆
            thickness.Left -= diff_x;
            thickness.Top -= diff_y;
            MapElement.MapRFIDList[index].ellipse.Margin = thickness;

            //选择框跟随
            MapElement.MapRFIDList[index].selectRectangle.Margin = thickness;

            //文字跟随即可
            thickness.Left -= diff_x - 15;
            thickness.Top -= diff_y - 10;
            MapElement.MapRFIDList[index].textBlock.Margin = thickness;

        }
        /// <summary>
        /// 设置标到选中状态
        /// </summary>
        /// <param name="index"></param>
        /// <param name="canvas"></param>
        public static void SetRFIDIsSelected(int index)
        {
            if (index == -1)
                return;
            if (MapElement.CvRFID.Children.Contains(MapElement.MapRFIDList[index].selectRectangle))
                return;
            MapElement.MapRFIDList[index].selectRectangle.Fill = null;
            MapElement.MapRFIDList[index].selectRectangle.StrokeThickness = 0.8;
            MapElement.MapRFIDList[index].selectRectangle.Stroke = Brushes.Black;
            MapElement.MapRFIDList[index].selectRectangle.Width = MapElement.RFID_Radius * 2;
            MapElement.MapRFIDList[index].selectRectangle.Height = MapElement.RFID_Radius * 2;
            MapElement.MapRFIDList[index].selectRectangle.Margin = MapElement.MapRFIDList[index].ellipse.Margin;
            //显示虚线
            MapElement.MapRFIDList[index].selectRectangle.StrokeDashArray = new DoubleCollection() { 3, 5 };
            MapElement.MapRFIDList[index].selectRectangle.StrokeDashCap = PenLineCap.Triangle;
            MapElement.CvRFID.Children.Add(MapElement.MapRFIDList[index].selectRectangle);
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
            MapElement.CvRFID.Children.Remove(MapElement.MapRFIDList[index].selectRectangle);
        }
        /*-----------------直线-------------------------------*/
        /// <summary>
        /// 判断坐标是否在某条直线上
        /// </summary>
        /// <param name="point">光标坐标</param>
        /// <returns>在：返回标签索引，不在：返回-1</returns>
        public static int IsOnRouteLine(Point point)
        {
            if (MapElement.MapLineList.Count == 0)
                return -1;
            //遍历
            for (int i = 0; i < MapElement.MapLineList.Count; i++)
            {
                //拿到直线
                Line line = MapElement.MapLineList[i].line;
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
            Rectangle rectangle = MapElement.MapLineList[index].StartRect;
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
            Rectangle rectangle = MapElement.MapLineList[index].EndRect;
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
            Thickness thickness = MapElement.MapLineList[index].line.Margin;
            thickness.Left = point.X - point.X % MapElement.GridSize;
            thickness.Top = point.Y - point.Y % MapElement.GridSize;

            //起点编辑器跟随
            thickness.Left += MapElement.GridSize / 2;
            thickness.Top += MapElement.GridSize / 2;
            MapElement.MapLineList[index].StartRect.Margin = thickness;
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
            Thickness thickness = MapElement.MapLineList[index].line.Margin;
            thickness.Left = point.X - point.X % MapElement.GridSize;
            thickness.Top = point.Y - point.Y % MapElement.GridSize;

            ////更新线起点坐标
            //MapElement.MapLineList[index].line.X1 = thickness.Left + MapElement.GridSize - MapElement.MapLineList[index].line.Margin.Left;
            //MapElement.MapLineList[index].line.Y1 = thickness.Top + MapElement.GridSize - MapElement.MapLineList[index].line.Margin.Top;

            ////选择线跟随
            //MapElement.MapLineList[index].SelectLine.X1 = MapElement.MapLineList[index].line.X1;
            //MapElement.MapLineList[index].SelectLine.Y1 = MapElement.MapLineList[index].line.Y1;

            //更新终点坐标
            MapElement.MapLineList[index].line.X2 -= thickness.Left + MapElement.GridSize - MapElement.MapLineList[index].line.Margin.Left;
            MapElement.MapLineList[index].line.Y2 -= thickness.Top + MapElement.GridSize - MapElement.MapLineList[index].line.Margin.Top;
            //选择线跟随
            MapElement.MapLineList[index].SelectLine.X2 = MapElement.MapLineList[index].line.X2;
            MapElement.MapLineList[index].SelectLine.Y2 = MapElement.MapLineList[index].line.Y2;

            //修正直线margin
            Thickness tk = MapElement.MapLineList[index].line.Margin;
            tk.Left += thickness.Left + MapElement.GridSize - MapElement.MapLineList[index].line.Margin.Left;
            tk.Top += thickness.Top + MapElement.GridSize - MapElement.MapLineList[index].line.Margin.Top;
            MapElement.MapLineList[index].line.Margin = tk;
            //选择线margin跟随
            MapElement.MapLineList[index].SelectLine.Margin = tk;

            //起点编辑器跟随
            thickness.Left += MapElement.GridSize / 2;
            thickness.Top += MapElement.GridSize / 2;
            MapElement.MapLineList[index].StartRect.Margin = thickness;
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
            Thickness thickness = MapElement.MapLineList[index].line.Margin;
            thickness.Left = point.X - point.X % MapElement.GridSize;
            thickness.Top = point.Y - point.Y % MapElement.GridSize;

            //更新线终点坐标
            MapElement.MapLineList[index].line.X2 = thickness.Left + MapElement.GridSize - MapElement.MapLineList[index].line.Margin.Left;
            MapElement.MapLineList[index].line.Y2 = thickness.Top + MapElement.GridSize - MapElement.MapLineList[index].line.Margin.Top;

            //选择线跟随
            MapElement.MapLineList[index].SelectLine.X2 = MapElement.MapLineList[index].line.X2;
            MapElement.MapLineList[index].SelectLine.Y2 = MapElement.MapLineList[index].line.Y2;

            //终点编辑器跟随
            thickness.Left += MapElement.GridSize / 2;
            thickness.Top += MapElement.GridSize / 2;
            MapElement.MapLineList[index].EndRect.Margin = thickness;
        }
        /// <summary>
        /// 更新直线到指定位置【编辑状态】
        /// </summary>
        /// <param name="index">索引</param>
        /// <param name="point">目标位置</param>
        /// <param name="canvas">画布</param>
        public static void MoveRouteLineAll(int index, Point point)
        {
            //获取移动偏差
            double difx = GlobalVar.mouseLeftBtnDownMoveDiff.X;
            double dify = GlobalVar.mouseLeftBtnDownMoveDiff.Y;

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
            //未达到移动标准，退出，提高效率
            if (Math.Abs(difx) == 0 && Math.Abs(dify) == 0)
                return;

            //计算起点编辑器和终点编辑器margin的偏差
            double margin_Diff_Left = MapElement.MapLineList[index].EndRect.Margin.Left - MapElement.MapLineList[index].StartRect.Margin.Left;
            double margin_Diff_Top = MapElement.MapLineList[index].EndRect.Margin.Top - MapElement.MapLineList[index].StartRect.Margin.Top;

            //移动线
            Thickness tk = new Thickness();
            tk.Left = GlobalVar.ElementMarginLast.Left;
            tk.Top = GlobalVar.ElementMarginLast.Top;

            tk.Left += difx;
            tk.Top += dify;

            MapElement.MapLineList[index].line.Margin = tk;
            //选择线跟随
            MapElement.MapLineList[index].SelectLine.Margin = tk;
            //起点编辑器跟随【减X1和Y1是因为在移动起点时修改了X1和Y1，而不是整体移动Margin】
            tk.Left -= MapElement.GridSize / 2;
            tk.Top -= MapElement.GridSize / 2;
            //tk.Left = tk.Left - MapElement.GridSize / 2 - MapElement.MapLineList[index].line.X1;
            //tk.Top = tk.Top - MapElement.GridSize / 2 - MapElement.MapLineList[index].line.Y1;
            MapElement.MapLineList[index].StartRect.Margin = tk;
            //终点编辑器跟随
            tk.Left += margin_Diff_Left;
            tk.Top += margin_Diff_Top;
            MapElement.MapLineList[index].EndRect.Margin = tk;
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
            if (MapElement.CvRouteLine.Children.Contains(MapElement.MapLineList[index].SelectLine) == false)
            {
                MapElement.RouteLineShowSelectLine(index);
            }
            //显示起点编辑器
            if (MapElement.CvRouteLine.Children.Contains(MapElement.MapLineList[index].StartRect) == false)
            {
                MapElement.RouteLineShowStart(index);
            }
            //显示终点编辑器
            if (MapElement.CvRouteLine.Children.Contains(MapElement.MapLineList[index].EndRect) == false)
            {
                MapElement.RouteLineShowEnd(index);
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
            MapElement.CvRouteLine.Children.Remove(MapElement.MapLineList[index].SelectLine);
            MapElement.CvRouteLine.Children.Remove(MapElement.MapLineList[index].StartRect);
            MapElement.CvRouteLine.Children.Remove(MapElement.MapLineList[index].EndRect);
            MapElement.CvRouteLine.Children.Remove(MapElement.MapLineList[index].textBlock);
        }

        /*-----------------分叉线-----------------------------*/
        /// <summary>
        /// 判断坐标是否在圆弧上
        /// </summary>
        /// <param name="point">光标坐标</param>
        /// <returns>在：返回标签索引，不在：返回-1</returns>
        public static int IsOnForkLine(Point point)
        {
            if (MapElement.MapForkLineList.Count == 0)
                return -1;

            //遍历
            for (int i = 0; i < MapElement.MapForkLineList.Count; i++)
            {
                //取当前坐标【需要margin对齐】
                point.X -= MapElement.MapForkLineList[i].Path.Margin.Left;
                point.Y -= MapElement.MapForkLineList[i].Path.Margin.Top;
                //找到圆弧起点和终点坐标
                PathGeometry pathGeometry = MapElement.MapForkLineList[i].Path.Data as PathGeometry;
                PathFigure figure = pathGeometry.Figures.First();
                ArcSegment arc = figure.Segments.First() as ArcSegment;
                //圆弧起点
                Point start = figure.StartPoint;
                //圆弧终点
                Point end = arc.Point;
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
                double dis = MathHelper.Distance(center,point);
                //判断是否在圆上【允许偏差 5】
                if (dis < 5)
                {
                    //判断是否在圆弧范围内【X、Y都在起点和终点的XY之间】【允许偏差 5】
                    return i;
                }
            }
            return -1;
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
            MapElement.CvForkLine.Children.Remove(MapElement.MapForkLineList[index].StartRect);
            MapElement.CvForkLine.Children.Remove(MapElement.MapForkLineList[index].EndRect);
            MapElement.CvForkLine.Children.Remove(MapElement.MapForkLineList[index].textBlock);
            MapElement.CvForkLine.Children.Remove(MapElement.MapForkLineList[index].SelectPath);
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
            Thickness thickness = MapElement.MapForkLineList[index].Path.Margin;
            thickness.Left = point.X - point.X % MapElement.GridSize;
            thickness.Top = point.Y - point.Y % MapElement.GridSize;

            //起点编辑器跟随
            thickness.Left += MapElement.GridSize / 2;
            thickness.Top += MapElement.GridSize / 2;
            MapElement.MapForkLineList[index].StartRect.Margin = thickness;
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
            Thickness thickness = MapElement.MapForkLineList[index].Path.Margin;
            thickness.Left = point.X - point.X % MapElement.GridSize;
            thickness.Top = point.Y - point.Y % MapElement.GridSize;

            //终点编辑器跟随
            thickness.Left += MapElement.GridSize / 2;
            thickness.Top += MapElement.GridSize / 2;
            MapElement.MapForkLineList[index].EndRect.Margin = thickness;

            //计算当前点【终点编辑器】和起点的偏差
            double diffx = thickness.Left - MapElement.MapForkLineList[index].Path.Margin.Left;
            double diffy = thickness.Top - MapElement.MapForkLineList[index].Path.Margin.Top;

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
            MapElement.MapForkLineList[index].Path.Data = pathGeometry;
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
            if (MapElement.CvForkLine.Children.Contains(MapElement.MapForkLineList[index].SelectPath) == false)
            {
                MapElement.ForkLineShowSelect(index);
            }
            //显示起点编辑器
            if (MapElement.CvForkLine.Children.Contains(MapElement.MapForkLineList[index].StartRect) == false)
            {
                MapElement.ForkLineShowStart(index);
            }
            //显示终点编辑器
            if (MapElement.CvForkLine.Children.Contains(MapElement.MapForkLineList[index].EndRect) == false)
            {
                MapElement.ForkLineShowEnd(index);
            }
        }

        /*-----------------综合-----------------------------*/
        public static void ClearAllSelect()
        {
            switch (GlobalVar.NowType)
            {
                case GlobalVar.EnumElementType.RFID:
                    if (GlobalVar.NowSelectIndex != -1)
                    {
                        MapFunction.SetRFIDIsNormal(GlobalVar.NowSelectIndex);
                    }
                    break;
                case GlobalVar.EnumElementType.RouteLine:
                    if (GlobalVar.NowSelectIndex != -1)
                    {
                        MapFunction.SetRouteLineIsNormal(GlobalVar.NowSelectIndex);
                    }
                    break;
                case GlobalVar.EnumElementType.RouteForkLine:
                    if (GlobalVar.NowSelectIndex != -1)
                    {
                        MapFunction.SetRouteForkLineIsNormal(GlobalVar.NowSelectIndex);
                    }
                    break;
                default: break;
            }
        }


    }
}
