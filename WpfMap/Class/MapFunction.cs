﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace WpfMap
{
    public class MapFunction
    {
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
        /// 更新RFID到指定位置
        /// </summary>
        /// <param name="index">RFID索引</param>
        /// <param name="point">目标位置</param>
        /// <param name="canvas">画布</param>
        public static void MoveRFIDTo(int index,Point  point,Canvas canvas)
        {
            point.X -= MapElement.GridSize/2;
            point.Y -= MapElement.GridSize/2;

            //计算xy方向偏差
            Thickness thickness = MapElement.MapRFIDList[index].ellipse.Margin;
            //对MapElement.GridSize取余，实现移动时按照栅格移动效果
            double diff_x = thickness.Left - (point.X - point.X % MapElement.GridSize);
            double diff_y = thickness.Top - (point.Y - point.Y % MapElement.GridSize);
            //移动圆
            thickness.Left -= diff_x;
            thickness.Top -= diff_y;
            MapElement.MapRFIDList[index].ellipse.Margin = thickness;

            //文字跟随即可
            thickness.Left -= diff_x-15;
            thickness.Top -= diff_y-10;
            MapElement.MapRFIDList[index].textBlock.Margin = thickness;
        }
    }
}