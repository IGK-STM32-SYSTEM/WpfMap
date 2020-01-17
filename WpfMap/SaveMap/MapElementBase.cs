using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfMap.SaveMap
{
    public class MapElementBase
    {
        public class BaseRFID
        {
            /// <summary>
            /// 编号
            /// </summary>
            public int Num { get; set; }
            /// <summary>
            /// 绘图对象
            /// </summary>
            public SaveMap.ShapesBase.BaseEllipse BaseEllipse { get; set; } = new ShapesBase.BaseEllipse(); 
            /// <summary>
            /// 绘文字对象
            /// </summary>
            public SaveMap.ShapesBase.BaseTextBlock BaseTextBlock { get; set; } = new  ShapesBase.BaseTextBlock();
            /// <summary>
            /// 选中框
            /// </summary>
            public SaveMap.ShapesBase.BaseRectangle BaseSelectRectangle { get; set; } = new  ShapesBase.BaseRectangle();
        }
    }
}
