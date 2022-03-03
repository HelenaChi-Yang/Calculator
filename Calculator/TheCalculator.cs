using Calculator.Operation.Operators;
using Calculator.Operation.OtherOperators;
using Calculator.ButtonDelegation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Calculator
{
    /// <summary>
    /// 計算機運算邏輯
    /// </summary>
    public class TheCalculator
    {
        /// <summary>
        /// 所有數字
        /// </summary>
        public StringBuilder Number { get; set; }

        /// <summary>
        /// 儲存運算式
        /// </summary>
        public List<Operations> OperationsList { get; set; }

        /// <summary>
        /// 螢幕目前數字
        /// </summary>
        public string ScreenNumber { get; set; }

        /// <summary>
        /// 錯誤訊息
        /// </summary>
        public string ErrorMessage { get; set; }

        /// <summary>
        /// 畫面結果狀態
        /// </summary>
        public Process DelegationService { get; set; }

        /// <summary>
        /// 計算括號數量
        /// </summary>
        public int CountParenthesis { get; set; }

        /// <summary>
        /// 運算式處理器
        /// </summary>
        public ExpressionSolver ExpressionSolver { get; set; }

        /// <summary>
        /// 建構子
        /// </summary>
        public TheCalculator()
        {
            OperationsList = new List<Operations>();
            Number = new StringBuilder();
            DelegationService = Process.GetInstance();
            ScreenNumber = ButtonSign.ZERO_SIGN;
            ExpressionSolver = new ExpressionSolver();
            ErrorMessage = string.Empty;
            CountParenthesis = 0;
        }

        /// <summary>
        /// 委派按鈕方法
        /// </summary>
        /// <param name="sign">符號</param>
        public void DelegateButton(string sign)
        {
            ButtonHandle buttonHandle = new ButtonHandle(this);
            buttonHandle.DelegateButtonAction(this, sign);
        }

        /// <summary>
        /// 結果生成後，重置計算機
        /// </summary>
        public void UpdateCalculator()
        {
            OperationsList = new List<Operations>();
            Number.Clear();
            ErrorMessage = string.Empty;
            CountParenthesis = 0;
            ExpressionSolver = new ExpressionSolver();
        }

        /// <summary>
        /// 計算機全部重置
        /// </summary>
        public void Restart()
        {
            OperationsList = new List<Operations>();
            Number.Clear();
            ErrorMessage = string.Empty;
            CountParenthesis = 0;
            ScreenNumber = ButtonSign.ZERO_SIGN;
            ExpressionSolver = new ExpressionSolver();
        }

        /// <summary>
        /// 目前數字
        /// </summary>
        /// <param name="theCalculator">計算機</param>
        public string CreateOperand(TheCalculator theCalculator)
        {
            if (string.IsNullOrEmpty(theCalculator.ErrorMessage))
            {
                return theCalculator.ScreenNumber;
            }
            string tempErrorMessage = theCalculator.ErrorMessage;
            theCalculator.Restart();
            return tempErrorMessage;
        }

        /// <summary>
        /// 目前算式
        /// </summary>
        /// <param name="operationsList">運算式</param>
        /// /// <returns></returns>
        public string CreateExpression(List<Operations> operationsList)
        {
            StringBuilder stringBuilder = new StringBuilder();
            foreach(var operation in operationsList)
            {
                stringBuilder.Append(operation.Sign);
            }
            return stringBuilder.ToString();
        }
    }
}
