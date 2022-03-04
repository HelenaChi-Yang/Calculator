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
    /// 減法
    /// </summary>
    public class Subtraction : Operator
    {
        /// <summary>
        /// 建構子
        /// </summary>
        public Subtraction()
        {
            Sign = ButtonSign.SUBTRACTION_SIGN;
            Priority = 1;
        }

        /// <summary>
        /// 計算
        /// </summary>
        /// <param name="firstOperand">第一運算子</param>
        /// <param name="secondOperand">第二運算子</param>
        /// <returns></returns>
        public override decimal Calculate(decimal firstOperand, decimal secondOperand)
        {
            return firstOperand - secondOperand;
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
