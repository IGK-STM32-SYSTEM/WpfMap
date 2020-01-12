using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace WpfMap
{
    public class MathHelper
    {
        /// <summary>
        /// 计算两点距离
        /// </summary>
        /// <param name="point1"></param>
        /// <param name="point2"></param>
        /// <returns></returns>
        public static double Distance(Point point1, Point point2)
        {
            double dx, dy;
            dx = point2.X - point1.X;
            dy = point2.Y - point1.Y;
            return Math.Sqrt(dx * dx + dy * dy);
        }
        /// <summary>
        /// 求方向角
        /// </summary>
        /// <returns></returns>
        public static double Slope(Point point1, Point point2)
        {
            return Math.Atan2((point2.Y - point1.Y), (point2.X - point1.X)) * 180 / Math.PI;
        }

        /// <summary>
        /// 已知:A、B两点的坐标, C到A的距离D 。求C点的坐标
        /// </summary>
        /// <param name="pointA">A点的坐标</param>
        /// <param name="pointB">B点的坐标</param>
        /// <param name="Distance">C到A的距离</param>
        /// <returns></returns>
        public static Point GetPointFromTwoPointWithDistance(Point pointA, Point pointB, double Distance)
        {
            Point point = new Point();
            //求a到b的距离
            double r = Math.Sqrt((pointA.X - pointB.X) * (pointA.X - pointB.X) + (pointA.Y - pointB.Y) * (pointA.Y - pointB.Y));
            //求D点的坐标
            point.X = (Distance * (pointB.X - pointA.X)) / r + pointA.X;
            point.Y = (Distance * (pointB.Y - pointA.Y)) / r + pointA.Y;
            return point;
        }

        /// <summary>
        /// 已知:A、B两点的坐标,求AB中点坐标
        /// </summary>
        /// <param name="pointA">A点的坐标</param>
        /// <param name="pointB">B点的坐标</param>
        /// <returns></returns>
        public static Point GetPointFromTwoPointCenter(Point pointA, Point pointB)
        {
            //两点之间的距离
            double distance = Distance(pointA, pointB);
            //中心坐标
            Point point = GetPointFromTwoPointWithDistance(pointA, pointB, distance * 0.5);
            return point;
        }

        /// <summary>
        /// 已知一个点的坐标，到另一个点的距离和角度求另外一个坐标
        /// </summary>
        /// <param name="pointA"></param>
        /// <param name="angle"></param>
        /// <param name="distance"></param>
        /// <returns></returns>
        public static Point GetPointFromOnePointWhithAngleAndDistance(Point pointA, double angle, double distance)
        {
            Point point = new Point();
            point.X = pointA.X + Math.Cos(Math.PI * (angle / 180.0)) * distance;
            point.Y = pointA.Y - Math.Sin(Math.PI * (angle / 180.0)) * distance;
            return point;
        }

        ///// <summary>
        ///// 三角形，根据一个参考起点坐标，三角形高度，三角形顶角角度，计算出三角形的三个顶点坐标
        ///// </summary>
        ///// <param name="SatrtPoint">考起点坐标</param>
        ///// <param name="Hight">三角形高度</param>
        ///// <param name="Angle">三角形顶角角度</param>
        ///// <returns></returns>
        //public static Point[] GetTriangle(Point SatrtPoint, double Hight, float Angle,float RoatAngle)
        //{
        //    Point[] points = new Point[3];

        //    Point pointHead = GetPointFromOnePointWhithAngleAndDistance(SatrtPoint, Angle, Hight * 1.8);
        //    double angleLeft = Angle + Angle * 0.5;
        //    angleLeft = angleLeft > 360 ? angleLeft - 360 : angleLeft;
        //    Point pointLeft = GetPointFromOnePointWhithAngleAndDistance(SatrtPoint, angleLeft, Hight * 1.2);
        //    double angleRight = Angle - Angle * 0.5;
        //    angleRight = angleRight < 0 ? angleRight + 360 : angleRight;
        //    Point pointRight = GetPointFromOnePointWhithAngleAndDistance(SatrtPoint, angleRight, Hight * 1.2);

        //    return points;
        //}
    }
}
