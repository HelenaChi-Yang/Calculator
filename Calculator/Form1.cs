using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Calculator
{
    /// <summary>
    /// UI介面
    /// </summary>
    public partial class Form1 : Form
    {
        /// <summary>
        /// 計算機
        /// </summary>
        private TheCalculator TheCalculator;

        /// <summary>
        /// 畫面表格
        /// </summary>
        public Form1()
        {
            InitializeComponent();
            TheCalculator = new TheCalculator();
            textBox1.Text = ButtonSign.ZERO_SIGN;
        }

        /// <summary>
        /// 所有按鈕點擊事件
        /// </summary>
        /// <param name="sender">物件傳遞</param>
        /// <param name="e">事件</param>
        private void AllButtomClick(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            string buttonSign = btn.Text;

            TheCalculator.DelegateButton(buttonSign);

            resultBox.Text = TheCalculator.CreateExpression(TheCalculator.OperationsList);
            textBox1.Text = TheCalculator.CreateOperand(TheCalculator);
        }
    }
}
