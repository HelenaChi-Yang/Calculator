using Calculator.Operation.Operand;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Calculator.Operation.Operators
{
    /// <summary>
    /// 運算子
    /// </summary>
    /// 
    public abstract class Operator : Operations
    {
        /// <summary>
        /// 運算
        /// </summary>
        /// <param name="firstOperand">第一運算元</param>
        /// <param name="secondOperand">第二運算元</param>
        /// <returns></returns>
        public abstract decimal Calculate(decimal firstOperand, decimal secondOperand);
    }
}
