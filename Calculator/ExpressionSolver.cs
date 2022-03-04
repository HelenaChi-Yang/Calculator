using Calculator.BinaryExpressionTree;
using Calculator.Operation.Operand;
using Calculator.Operation.Operators;
using Calculator.Operation.OtherOperators;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace Calculator
{
    /// <summary>
    /// 運算式運算器
    /// </summary>
    public class ExpressionSolver
    {
        /// <summary>
        /// 目前運算式權重
        /// </summary>
        public int ExpressionWeight { get; set; }

        /// <summary>
        /// 根樹
        /// </summary>
        public Tree.Node RootTree { get; set; }

        /// <summary>
        /// 樹
        /// </summary>
        private Tree Tree;

        /// <summary>
        /// 建構子
        /// </summary>
        public ExpressionSolver()
        {
            ExpressionWeight = 0;
            Tree = new Tree();
        }

        /// <summary>
        /// 建樹
        /// </summary>
        /// <param name="calculator">計算機</param>
        /// <param name="currentNumber">目前數字</param>
        /// <param name="currentOperator">目前運算元</param>
        /// <param name="isPreviousRootTree">是否回前一棵樹以更改運算元</param>
        public void BuildTree(TheCalculator calculator, Number currentNumber, Operator currentOperator, bool isPreviousRootTree)
        {
            Tree.Node currentNumberNode = Tree.NewNode(currentNumber, currentNumber.Priority);
            Tree.Node currentOperatorNode = Tree.NewNode(currentOperator, currentOperator.Priority + ExpressionWeight);
            if (!isPreviousRootTree)
            {
                RootTree = Tree.InsertNumberIntoTree(RootTree, currentNumberNode);
                RootTree = Tree.InsertOperatorIntoTree(RootTree, currentOperatorNode, RootTree);
                return;
            }
            Tree.BackToPreviousTree(RootTree, this);
            RootTree = Tree.InsertNumberIntoTree(RootTree, currentNumberNode);
            RootTree = Tree.InsertOperatorIntoTree(RootTree, currentOperatorNode, RootTree);
        }

        /// <summary>
        /// 建構最終樹
        /// </summary>
        /// <param name="currentNumber">目前數字</param>
        /// <param name="calculator">計算機</param>
        public void BuildFinalTree(Number currentNumber, TheCalculator calculator)
        {
            if (!(CheckDenominatorIsZero(calculator)))
            {
                Tree.Node currentNumberNode = Tree.NewNode(currentNumber, currentNumber.Priority);
                RootTree = Tree.InsertNumberIntoTree(RootTree, currentNumberNode);

                List<Operations> postorderOperation = Tree.PostorderTraversal(RootTree, new List<Operations>());
                decimal result = Tree.PostfixResult(postorderOperation);
                calculator.UpdateCalculator();
                calculator.ScreenNumber = result.ToString();
            }
        }

        /// <summary>
        /// 檢查分母為0
        /// </summary>
        /// <param name="calculator">計算機</param>
        /// <returns></returns>
        public bool CheckDenominatorIsZero(TheCalculator calculator)
        {
            foreach (Operations AOperator in calculator.OperationsList)
            {
                if (AOperator.Sign.Equals(ButtonSign.DIVISION_SIGN))
                {
                    int index = calculator.OperationsList.IndexOf(AOperator);
                    if (calculator.OperationsList[index + 1].Sign.Equals(ButtonSign.ZERO_SIGN))
                    {
                        calculator.Number.Remove(0, calculator.Number.Length);
                        calculator.OperationsList.RemoveRange(0, calculator.OperationsList.Count);
                        calculator.ErrorMessage = "分母不可為0";
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
