﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace WpfMap
{
    //绘图基础类，主要实现基本元素的绘制
  public  class CavnvasBase
    {
        /// <summary>
        /// 画文字
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="text"></param>
        /// <param name="color"></param>
        /// <param name="canvasObj"></param>
        /// <param name="textBlock"></param>
        public static void DrawText(double x, double y, string text, Color color, Canvas canvasObj, TextBlock textBlock)
        {
            textBlock.Text = text;
            textBlock.Foreground = new SolidColorBrush(color);
            textBlock.Margin = new Thickness(x, y, 0, 0);
            canvasObj.Children.Add(textBlock);
        }
    }
}