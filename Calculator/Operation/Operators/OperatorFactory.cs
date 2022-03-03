using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.Operation.Operators
{
    /// <summary>
    /// 運算子工廠
    /// </summary>
    internal class OperatorFactory
    {
        /// <summary>
        /// 查找運算子
        /// </summary>
        private Dictionary<string, Operator> OperatorSign;

        /// <summary>
        /// 建構子
        /// </summary>
        public OperatorFactory()
        {
            OperatorSign = new Dictionary<string, Operator>()
            {
                {ButtonSign.ADDICTION_SIGN, new Addition()},
                {ButtonSign.SUBTRACTION_SIGN, new Subtraction()},
                {ButtonSign.MULTIPLICATION_SIGN, new Multiplication()},
                {ButtonSign.DIVISION_SIGN, new Division()}
            };
        }

        /// <summary>
        /// 產生運算子
        /// </summary>
        /// <param name="sign">符號</param>
        /// <returns></returns>
        internal Operator CreateOperator(string sign)
        {
            return OperatorSign[sign];
        }
    }
}
