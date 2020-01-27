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
        //public void 

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
            //找到的标签号
            int id = -1;
            Point point = new Point(pointLeft.X, pointLeft.Y);
            /*-----------向左沿直线搜索标签----------------------*/
            for (double i = point.X; i > 0; i -= 1)
            {
                //更新当前位置
                point.X = i;
                //当前点在直线上
                if (MapFunction.IsOnRouteLine(point) != -1)
                {
                    //当前点在标签上【非起点标签】
                    id = MapFunction.IsOnRFID(point);
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
                //设置选中
                MapFunction.SetRFIDIsSelected(id);
                MapOperate.SystemMsg.WriteLine("向左找到【{0}】号标签!", MapElement.MapObject.RFIDS[id].Num);
            }
            else
            {
                MapOperate.SystemMsg.WriteLine("向左未找到标签!");
            }
            /*-----------向左沿直线搜索分叉----------------------*/
            id = -1;
            point = new Point(pointLeft.X, pointLeft.Y);
            for (double i = point.X; i > 0; i -= 1)
            {
                //更新当前位置
                point.X = i;
                //当前点在直线上
                if (MapFunction.IsOnRouteLine(point) != -1)
                {
                    //当前点是某一个分叉的终点
                    id = MapFunction.IsOnForkLineEnd(point);
                    //判断是否找到，且判断终点在起点的左边还是右边
                    if (id != -1 && MapElement.MapObject.ForkLines[id].StartPoint.X < MapElement.MapObject.ForkLines[id].EndPoint.X)
                    {
                        //起点在终点左边【有效】，停止搜索
                        break;
                    }
                    else
                    {
                        //起点在终点右边【无效】，继续找
                        continue;
                    }
                }
                else
                    break;

            }
            //找分叉，继续找标签
            if (id != -1 && MapElement.MapObject.ForkLines[id].StartPoint.X < MapElement.MapObject.ForkLines[id].EndPoint.X)
            {
                //设置选中
                MapFunction.SetForkLineIsSelected(id);
                MapOperate.SystemMsg.WriteLine("向左找到【{0}】号分叉!", MapElement.MapObject.ForkLines[id].Num);
                //沿分叉再找标签
            }
            //未找到，结束
            else
            {
                MapOperate.SystemMsg.WriteLine("向左未找到分叉!");
            }
            /*-----------向右搜索----------------------*/
            id = -1;
            point = new Point(pointRight.X, pointRight.Y);
            for (double i = point.X; i < 2000; i += 1)
            {
                //更新当前位置
                point.X = i;
                //当前点在直线上
                if (MapFunction.IsOnRouteLine(point) != -1)
                {
                    //当前点在标签上【非起点标签】
                    id = MapFunction.IsOnRFID(point);
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
                //设置选中
                MapFunction.SetRFIDIsSelected(id);
                MapOperate.SystemMsg.WriteLine("向右找到【{0}】号标签!", MapElement.MapObject.RFIDS[id].Num);
            }
            else
                MapOperate.SystemMsg.WriteLine("向右未找到标签!");
            /*-----------向上搜索----------------------*/
            id = -1;
            point = new Point(pointUp.X, pointUp.Y);
            for (double i = point.Y; i > 0; i -= 1)
            {
                //更新当前位置
                point.Y = i;
                //当前点在直线上
                if (MapFunction.IsOnRouteLine(point) != -1)
                {
                    //当前点在标签上【非起点标签】
                    id = MapFunction.IsOnRFID(point);
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
                //设置选中
                MapFunction.SetRFIDIsSelected(id);
                MapOperate.SystemMsg.WriteLine("向上找到【{0}】号标签!", MapElement.MapObject.RFIDS[id].Num);
            }
            else
                MapOperate.SystemMsg.WriteLine("向上未找到标签!");
            /*-----------向下搜索----------------------*/
            id = -1;
            point = new Point(pointDown.X, pointDown.Y);
            for (double i = point.Y; i < 2000; i += 1)
            {
                //更新当前位置
                point.Y = i;
                //当前点在直线上
                if (MapFunction.IsOnRouteLine(point) != -1)
                {
                    //当前点在标签上【非起点标签】
                    id = MapFunction.IsOnRFID(point);
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
                //设置选中
                MapFunction.SetRFIDIsSelected(id);
                MapOperate.SystemMsg.WriteLine("向下找到【{0}】号标签!", MapElement.MapObject.RFIDS[id].Num);
            }
            else
                MapOperate.SystemMsg.WriteLine("向下未找到标签!");


        }
    }
}
