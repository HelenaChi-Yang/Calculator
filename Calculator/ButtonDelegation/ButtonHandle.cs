using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.ButtonDelegation
{
    /// <summary>
    /// 產生類別路徑工廠
    /// </summary>
    public class ButtonHandle
    {
        /// <summary>
        /// 紀錄所有類別名稱
        /// </summary>
        private Dictionary<string, Action<TheCalculator, string>> ButtonBehavior;

        /// <summary>
        /// 建構子
        /// </summary>
        /// <param name="calculator">計算機</param>
        public ButtonHandle(TheCalculator calculator)
        {
            ButtonBehavior = new Dictionary<string, Action<TheCalculator, string>>
            {
                {"+", calculator.DelegationService.PressOperators},
                {"-", calculator.DelegationService.PressOperators},
                {"*", calculator.DelegationService.PressOperators},
                {"/", calculator.DelegationService.PressOperators},
                {"C", calculator.DelegationService.PressC},
                {"CE", calculator.DelegationService.PressCE},
                {"back", calculator.DelegationService.PressBack},
                {"+/-", calculator.DelegationService.PressPositiveAndNegative},
                {"(", calculator.DelegationService.PressLeftParenthesis},
                {")", calculator.DelegationService.PressRightParenthesis},
                {"√", calculator.DelegationService.PressSquare},
                {".", calculator.DelegationService.PressDot},
                {"=", calculator.DelegationService.PressEqual},
                {"0", calculator.DelegationService.PressNumber},
                {"1", calculator.DelegationService.PressNumber},
                {"2", calculator.DelegationService.PressNumber},
                {"3", calculator.DelegationService.PressNumber},
                {"4", calculator.DelegationService.PressNumber},
                {"5", calculator.DelegationService.PressNumber},
                {"6", calculator.DelegationService.PressNumber},
                {"7", calculator.DelegationService.PressNumber},
                {"8", calculator.DelegationService.PressNumber},
                {"9", calculator.DelegationService.PressNumber}
            };
        }

        /// <summary>
        /// 取得類別名稱
        /// </summary>
        /// <param name="calculator">計算機</param>
        /// <param name="sign">符號</param>
        public void DelegateButtonAction(TheCalculator calculator, string sign)
        {
            if (ButtonBehavior.TryGetValue(sign, out Action<TheCalculator, string> result))
            {
                result(calculator, sign);
            }
        }
    }
}
