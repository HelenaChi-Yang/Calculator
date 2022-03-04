using Calculator.BinaryExpressionTree;
using Calculator.Operation.Operand;
using Calculator.Operation.Operators;
using Calculator.Operation.OtherOperators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.ButtonDelegation
{
    /// <summary>
    /// 按鈕輸入
    /// </summary>
    public class Process
    {
        /// <summary>
        /// Singleton實體
        /// </summary>
        private static Process Instance;

        /// <summary>
        /// 私有建構子(Singleton)
        /// </summary>
        private Process()
        {
        }

        /// <summary>
        /// 取得Singleton實體
        /// </summary>
        /// <returns></returns>
        public static Process GetInstance()
        {
            if (Instance == null)
            {
                Instance = new Process();
            }
            return Instance;
        }

        /// <summary>
        /// 按 =
        /// </summary>
        /// <param name="calculator">計算機</param>
        /// <param name="buttonSign">按鈕符號</param>
        public void PressEqual(TheCalculator calculator, string buttonSign)
        {
            if (calculator.OperationsList.Count != 0)
            {
                Type lastType = calculator.OperationsList[calculator.OperationsList.Count - 1].GetType();
                if (lastType.BaseType == typeof(Operator))
                {
                    calculator.OperationsList.Add(new Number(calculator.ScreenNumber));
                    calculator.ExpressionSolver.BuildFinalTree(new Number(calculator.ScreenNumber), calculator);
                }
            }
        }

        /// <summary>
        /// 按.
        /// </summary>
        /// <param name="calculator">計算機</param>
        /// <param name="buttonSign">按鈕符號</param>
        public void PressDot(TheCalculator calculator, string buttonSign)
        {
            if (!(calculator.ScreenNumber.Contains(ButtonSign.DOT_SIGN)))
            {
                calculator.ScreenNumber = $"{calculator.ScreenNumber}{buttonSign}";
                calculator.Number = new StringBuilder(calculator.ScreenNumber);
            }
        }

        /// <summary>
        /// 按數字鍵
        /// </summary>
        /// <param name="calculator">計算機</param>
        /// <param name="buttonSign">按鈕符號</param>
        public void PressNumber(TheCalculator calculator, string buttonSign)
        {
            if (calculator.OperationsList.Count == 0)
            {
                calculator.Number.Append(buttonSign);
                calculator.ScreenNumber = calculator.Number.ToString();
                return;
            }
            Type lastType = calculator.OperationsList[calculator.OperationsList.Count - 1].GetType();
            if (lastType != typeof(RightParenthesis))
            {
                calculator.Number.Append(buttonSign);
                calculator.ScreenNumber = calculator.Number.ToString();
            }
        }

        /// <summary>
        /// 按運算子
        /// </summary>
        /// <param name="calculator">計算機</param>
        /// <param name="buttonSign">按鈕符號</param>
        public void PressOperators(TheCalculator calculator, string buttonSign)
        {
            OperatorFactory operatorFactory = new OperatorFactory();
            Operator theOperator = operatorFactory.CreateOperator(buttonSign);

            if (calculator.OperationsList.Count == 0)
            {
                calculator.OperationsList.Add(new Number(calculator.ScreenNumber));
                calculator.OperationsList.Add(theOperator);
                calculator.ExpressionSolver.BuildTree(calculator, new Number(calculator.ScreenNumber), theOperator, false);
                calculator.Number.Clear();
                return;
            }

            if (!(string.IsNullOrEmpty(calculator.Number.ToString())))
            {
                calculator.OperationsList.Add(new Number(calculator.ScreenNumber));
                calculator.Number.Clear();
            }

            Type lastType = calculator.OperationsList[calculator.OperationsList.Count - 1].GetType();
            if (lastType != typeof(LeftParenthesis))
            {
                //更換運算子
                if (lastType.BaseType == typeof(Operator))
                {
                    calculator.OperationsList.RemoveAt(calculator.OperationsList.Count - 1);
                    calculator.OperationsList.Add(theOperator);
                    calculator.ExpressionSolver.BuildTree(calculator, new Number(calculator.ScreenNumber), theOperator, true);
                    return;
                }
                calculator.OperationsList.Add(theOperator);
                calculator.ExpressionSolver.BuildTree(calculator, new Number(calculator.ScreenNumber), theOperator, false);
            }
        }

        /// <summary>
        /// 按 +/-
        /// </summary>
        /// <param name="calculator">計算機</param>
        /// <param name="buttonSign">按鈕符號</param>
        public void PressPositiveAndNegative(TheCalculator calculator, string buttonSign)
        {
            if (!(calculator.ScreenNumber.Contains(ButtonSign.SUBTRACTION_SIGN)))
            {
                calculator.ScreenNumber = $"{ButtonSign.SUBTRACTION_SIGN}{calculator.ScreenNumber}";
                calculator.Number = new StringBuilder(calculator.ScreenNumber);
                return;
            }
            calculator.ScreenNumber = calculator.ScreenNumber.Remove(0, 1);
            calculator.Number = new StringBuilder(calculator.ScreenNumber);
        }

        /// <summary>
        /// 按根號
        /// </summary>
        /// <param name="calculator">計算機</param>
        /// <param name="buttonSign">按鈕符號</param>
        public void PressSquare(TheCalculator calculator, string buttonSign)
        {
            try
            {
                //開根號不能為負數
                if (Convert.ToDouble(calculator.ScreenNumber) >= 0)
                {
                    double result = Math.Sqrt(Convert.ToDouble(calculator.ScreenNumber));
                    calculator.ScreenNumber = Convert.ToString(result);
                    calculator.Number = new StringBuilder(calculator.ScreenNumber);
                }
                else
                {
                    calculator.Restart();
                    calculator.ErrorMessage = "無效輸入";
                }
            }
            catch (OverflowException)
            {
                calculator.Restart();
                calculator.ErrorMessage = "數值超出範圍";
                return;
            }
        }

        /// <summary>
        /// 按 C
        /// </summary>
        /// <param name="calculator">計算機</param>
        /// <param name="buttonSign">按鈕符號</param>
        public void PressC(TheCalculator calculator, string buttonSign)
        {
            calculator.Restart();
        }

        /// <summary>
        /// 按 CE
        /// </summary>
        /// <param name="calculator">計算機</param>
        /// <param name="buttonSign">按鈕符號</param>
        public void PressCE(TheCalculator calculator, string buttonSign)
        {
            calculator.Number = new StringBuilder(ButtonSign.ZERO_SIGN);
            calculator.ScreenNumber = calculator.Number.ToString();
        }

        /// <summary>
        /// 按 Back
        /// </summary>
        /// <param name="calculator">計算機</param>
        /// <param name="buttonSign">按鈕符號</param>
        public void PressBack(TheCalculator calculator, string buttonSign)
        {
            if (!(calculator.Number.ToString().Equals(string.Empty)))
            {
                calculator.Number.Remove(calculator.Number.Length - 1, 1);
                calculator.ScreenNumber = calculator.Number.ToString();
            }

            if (string.IsNullOrEmpty(calculator.Number.ToString()))
            {
                calculator.ScreenNumber = ButtonSign.ZERO_SIGN;
            }
        }

        /// <summary>
        /// 按 RightParenthesis
        /// </summary>
        /// <param name="calculator">計算機</param>
        /// <param name="buttonSign">按鈕符號</param>
        public void PressRightParenthesis(TheCalculator calculator, string buttonSign)
        {
            if (calculator.OperationsList.Count != 0)
            {
                if (!(string.IsNullOrEmpty(calculator.Number.ToString())))
                {
                    calculator.OperationsList.Add(new Number(calculator.Number.ToString()));
                    calculator.Number.Clear();
                }

                Type lastOperation = calculator.OperationsList[calculator.OperationsList.Count - 1].GetType();
                if (lastOperation == typeof(Number) || lastOperation == typeof(RightParenthesis))
                {
                    int numberOfRightParenthese = CountRightParenthesis(calculator);
                    if (calculator.CountParenthesis > numberOfRightParenthese)
                    {
                        calculator.OperationsList.Add(new RightParenthesis());
                        calculator.ExpressionSolver.ExpressionWeight -= 10;
                    }
                }
            }
        }

        /// <summary>
        /// 按 LeftParenthesis
        /// </summary>
        /// <param name="calculator">計算機</param>
        /// <param name="buttonSign">按鈕符號</param>
        public void PressLeftParenthesis(TheCalculator calculator, string buttonSign)
        {
            if (calculator.OperationsList.Count == 0)
            {
                calculator.OperationsList.Add(new LeftParenthesis());
                calculator.ExpressionSolver.ExpressionWeight += 10;
                calculator.CountParenthesis += 1;
                return;
            }
            //若前面為運算子 或 ( 可加 (
            Type lastType = calculator.OperationsList[calculator.OperationsList.Count - 1].GetType();
            if (lastType.BaseType == typeof(Operator) || lastType == typeof(LeftParenthesis))
            {
                calculator.OperationsList.Add(new LeftParenthesis());
                calculator.ExpressionSolver.ExpressionWeight += 10;
                calculator.CountParenthesis += 1;
            }
        }

        /// <summary>
        /// 計算有幾個右括號
        /// </summary>
        /// <param name="calculator">計算機</param>
        /// <returns></returns>
        public int CountRightParenthesis(TheCalculator calculator)
        {
            //判斷 )數量有無超過 (
            int numberOfRightParenthese = 0;
            foreach (Operations operation in calculator.OperationsList)
            {
                if (operation.GetType() == typeof(RightParenthesis))
                {
                    numberOfRightParenthese += 1;
                }
            }
            return numberOfRightParenthese;
        }
    }
}
