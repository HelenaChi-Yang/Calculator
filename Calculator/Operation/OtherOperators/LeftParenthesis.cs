using Calculator.Operation.Operators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.Operation.OtherOperators
{
    /// <summary>
    /// 左括號
    /// </summary>
    public class LeftParenthesis : Operations
    {
        /// <summary>
        /// 建構子
        /// </summary>
        public LeftParenthesis()
        {
            Sign = ButtonSign.LEFTPARENTHESIS_SIGN;
        }
    }
}
