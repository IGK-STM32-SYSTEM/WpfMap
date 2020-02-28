using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfMap.Class
{
    public class SysParameterClass
    {
        /// <summary>
        /// 路径相关的参数
        /// </summary>
        public class Route
        {
            /// <summary>
            /// 参考点标签索引
            /// </summary>
            public static int RefRFIDIndex = 1;
            /// <summary>
            /// 参考点车头方向
            /// </summary>
            public static WpfMap.Route.Helper.HeadDirections RefDir = WpfMap.Route.Helper.HeadDirections.Up;

        }
    }
}
