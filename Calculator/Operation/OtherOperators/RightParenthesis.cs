using Calculator.Operation.Operand;
using Calculator.Operation.Operators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.Operation.OtherOperators
{
    /// <summary>
    /// 右括號
    /// </summary>
    public class RightParenthesis : Operations
    {
        public RightParenthesis()
        {
            Sign = ButtonSign.RIGHTPARENTHESIS_SIGN;
            Priority = 0;
        }
    }
}
