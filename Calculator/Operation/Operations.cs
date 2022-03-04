using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator
{
    /// <summary>
    /// 運算符號介面
    /// </summary>
    public abstract class Operations
    {
        /// <summary>
        /// 符號
        /// </summary>
        public string Sign { get; protected set; }

        /// <summary>
        /// 優先級
        /// </summary>
        public int Priority { get; protected set; }
    }
}
