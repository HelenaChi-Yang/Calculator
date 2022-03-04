using Calculator.Operation.Operand;
using Calculator.Operation.Operators;
using Calculator.Operation.OtherOperators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.BinaryExpressionTree
{
    /// <summary>
    /// Expression Tree
    /// </summary>
    public class Tree
    {
        /// <summary>
        /// 上一個數字node
        /// </summary>
        private Node PreviousNumber;

        /// <summary>
        /// 上一個運算子node
        /// </summary>
        private Node PreviousOperator;

        /// <summary>
        /// 斷開上一個運算子node的前面運算子
        /// </summary>
        private Node BeforePreviousOperator;

        /// <summary>
        /// 建構子
        /// </summary>
        public Tree()
        {
            PreviousNumber = new Node();

            PreviousOperator = new Node();

            BeforePreviousOperator = new Node();
        }

        /// <summary>
        /// 內部類別 Node
        /// </summary>
        public class Node
        {
            /// <summary>
            /// 運算符號
            /// </summary>
            internal Operations Operation { get; set; }

            /// <summary>
            /// 左節點
            /// </summary>
            internal Node LeftNode { get; set; }

            /// <summary>
            /// 右節點
            /// </summary>
            internal Node RightNode { get; set; }

            /// <summary>
            /// node的權重
            /// </summary>
            internal int Weight { get; set; }
        }

        /// <summary>
        /// 新增node節點
        /// </summary>
        /// <param name="operation">運算式</param>
        /// <param name="weight">權重</param>
        /// <returns>Node</returns>
        public Node NewNode(Operations operation, int weight)
        {
            Node tempNode = new Node();
            tempNode.Operation = operation;
            tempNode.LeftNode = null;
            tempNode.RightNode = null;
            tempNode.Weight = weight;
            return tempNode;
        }

        /// <summary>
        /// 恢復上一次根樹
        /// </summary>
        /// <param name="root">根樹</param>
        /// <param name="expressionSolver">算式處理器</param>
        public void BackToPreviousTree(Node root, ExpressionSolver expressionSolver)
        {
            //刪除上一個數字node
            DeletePreviousNumber(PreviousNumber, root);
            //找到beforeAddress，rightNode = PreviousOperator.leftNode
            BackToPrevious(BeforePreviousOperator, root, expressionSolver);
        }

        /// <summary>
        /// 插入運算元
        /// </summary>
        /// <param name="root">根樹</param>
        /// <param name="currentNumber">目前要插入的運算元</param>
        /// <returns></returns>
        public Node InsertNumberIntoTree(Node root, Node currentNumber)
        {
            if (root == null)
            {
                PreviousNumber = currentNumber;
                return currentNumber;
            }
            else
            {
                root.RightNode = InsertNumberIntoTree(root.RightNode, currentNumber);
            }
            return root;
        }

        /// <summary>
        /// 插入運算子
        /// </summary>
        /// <param name="root">根樹</param>
        /// <param name="currentOperator">目前要插入的運算子</param>
        /// <param name="beforeNode">斷開前的運算子</param>
        /// <returns></returns>
        public Node InsertOperatorIntoTree(Node root, Node currentOperator, Node beforeNode)
        {
            if (root.Weight >= currentOperator.Weight)
            {
                BeforePreviousOperator = beforeNode;
                //預防第一個運算子就按下換運算符，這時beforeNode，會是特例(上一個數字node)
                if (beforeNode.Operation.GetType() == typeof(Number))
                {
                    BeforePreviousOperator = currentOperator;
                }
                PreviousOperator = currentOperator;
                currentOperator.LeftNode = root;
                return currentOperator;
            }
            else
            {
                root.RightNode = InsertOperatorIntoTree(root.RightNode, currentOperator, root);
            }
            return root;
        }

        /// <summary>
        /// 刪掉上一個node number
        /// </summary>
        /// <param name="inputNode">輸入地址</param>
        /// <param name="root">根樹</param>
        private void DeletePreviousNumber(Node inputNode, Node root)
        {
            if (root.LeftNode != null)
            {
                if (Object.ReferenceEquals(inputNode, root.LeftNode))
                {
                    root.LeftNode = null;
                    return;
                }
                DeletePreviousNumber(inputNode, root.LeftNode);
            }
            if (root.RightNode != null)
            {
                if (Object.ReferenceEquals(inputNode, root.RightNode))
                {
                    root.RightNode = null;
                    return;
                }
                DeletePreviousNumber(inputNode, root.RightNode);
            }
        }

        /// <summary>
        /// 把原本運算子刪掉後，beforeNode右腳接上原本運算子的左腳
        /// </summary>
        /// <param name="inputNode">樹入地址</param>
        /// <param name="root">根樹</param>
        /// <param name="expressionSolver">算式處理器</param>
        private void BackToPrevious(Node inputNode, Node root, ExpressionSolver expressionSolver)
        {
            if (Object.ReferenceEquals(inputNode, root))
            {
                //預防第一個運算子就按下換運算符，這時beforeNode，會是特例(上一個數字node)
                if (root.RightNode == null)
                {
                    expressionSolver.RootTree = null;
                    return;
                }
                root.RightNode = null;
                return;
            }
            if (Object.ReferenceEquals(inputNode, root.LeftNode))
            {
                root = root.LeftNode;
            }
            if (root.RightNode != null)
            {
                if (Object.ReferenceEquals(inputNode, root.RightNode))
                {
                    root.RightNode = PreviousOperator.LeftNode;
                    return;
                }
                BackToPrevious(inputNode, root.RightNode, expressionSolver);
            }
        }

        /// <summary>
        /// 後序式
        /// </summary>
        /// <param name="root">根節點</param>
        /// <param name="operations">運算式</param>
        /// <returns></returns>
        public List<Operations> PostorderTraversal(Node root, List<Operations> operations)
        {
            if (root == null)
            {
                return operations;
            }
            if (root.LeftNode != null)
            {
                PostorderTraversal(root.LeftNode, operations);
            }
            if (root.RightNode != null)
            {
                PostorderTraversal(root.RightNode, operations);
            }
            operations.Add(root.Operation);
            return operations;
        }

        /// <summary>
        /// 前序式
        /// </summary>
        /// <param name="root">根節點</param>
        /// <param name="operations">運算式</param>
        /// <returns></returns>
        public List<Operations> PreorderTraversal(Node root, List<Operations> operations)
        {
            operations.Add(root.Operation);
            if (root == null)
            {
                return operations;
            }
            if (root.LeftNode != null)
            {
                PreorderTraversal(root.LeftNode, operations);
            }
            if (root.RightNode != null)
            {
                PreorderTraversal(root.RightNode, operations);
            }
            return operations;
        }

        /// <summary>
        /// 中序式
        /// </summary>
        /// <param name="root">根節點</param>
        /// <param name="operations">運算式</param>
        /// <returns></returns>
        public List<Operations> InorderTraversal(Node root, List<Operations> operations)
        {
            if (root == null)
            {
                return operations;
            }
            if (root.LeftNode != null)
            {
                InorderTraversal(root.LeftNode, operations);
            }
            operations.Add(root.Operation);
            if (root.RightNode != null)
            {
                InorderTraversal(root.RightNode, operations);
            }
            return operations;
        }

        /// <summary>
        /// 後序式演算法輸出結果
        /// </summary>
        /// <param name="result">運算式</param>
        /// <returns></returns>
        public decimal PostfixResult(List<Operations> result)
        {
            //pop出來需要被計算的數字
            List<int> computeNumber = new List<int>();
            //postfix的stack紀錄
            Stack<decimal> PostfixStack = new Stack<decimal>();

            foreach (Operations operatorOrOperand in result)
            {
                if (operatorOrOperand is Operator)
                {
                    //目前運算子
                    Operator currentOperator = (Operator)operatorOrOperand;

                    //pop出最上層兩個運算元
                    decimal firstNumber = PostfixStack.Pop();
                    decimal secondNumber = PostfixStack.Pop();

                    //執行運算並回到stack
                    decimal tempResult = currentOperator.Calculate(secondNumber, firstNumber);
                    PostfixStack.Push(tempResult);
                }
                else
                {
                    //數字
                    PostfixStack.Push(Convert.ToDecimal(operatorOrOperand.Sign));
                }
            }
            decimal finalResult = PostfixStack.Pop();
            return finalResult;
        }
    }
}
