using Calculator.Operation.Operand;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Calculator.Operation.Operators
{
    /// <summary>
    /// 加法
    /// </summary>
    public class Addition : Operator
    {
        /// <summary>
        /// 加法建構子
        /// </summary>
        public Addition()
        {
            Sign = ButtonSign.ADDICTION_SIGN;
            Priority = 1;
        }

        /// <summary>
        /// 計算加法
        /// </summary>
        /// <param name="firstOperand">第一個運算元</param>
        /// <param name="secondOperand">第二個運算元</param>
        /// <returns></returns>
        public override decimal Calculate(decimal firstOperand, decimal secondOperand)
        {
            return firstOperand + secondOperand;
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
