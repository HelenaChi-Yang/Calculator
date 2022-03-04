using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Calculator.Operation.Operand
{
    /// <summary>
    /// 數字
    /// </summary>
    public class Number : Operations
    {
        /// <summary>
        /// 建構子
        /// </summary>
        /// <param name="sign">符號</param>
        public Number(string sign)
        {
            Sign = sign;
            Priority = int.MaxValue;
        }

        /// <summary>
        /// 回傳符號
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return Sign;
        }
    }
}
