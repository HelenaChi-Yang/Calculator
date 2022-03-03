using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Calculator;
using API.Models;
using System.Collections.Concurrent;
using LocalDB;
using System.Threading;

namespace API.Controllers
{
    /// <summary>
    /// 計算機控制器
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class CalculatorController : ControllerBase
    {
        /// <summary>
        /// 當前使用者的計算機
        /// </summary>
        private readonly Calculators _calculators;

        /// <summary>
        /// 建構子
        /// </summary>
        /// <param name="calculators">計算機</param>

        public CalculatorController(Calculators calculators)
        {
            _calculators = calculators;
        }

        /// <summary>
        /// 取得計算機當前運算式，和運算元
        /// </summary>
        /// <param name="sign">輸入符號</param>
        /// <param name="id">使用者id</param>
        /// <returns></returns>       
        [HttpGet("{id}")]
        public TwoResultOnScreen Get(string sign, string id)
        {
            TwoResultOnScreen twoResultOnScreen = new TwoResultOnScreen();
            TheCalculator calculator = _calculators.GetCalculator(id);
            calculator.DelegateButton(sign);

            twoResultOnScreen.OperandResult = calculator.CreateOperand(calculator);
            twoResultOnScreen.ExpressionResult = calculator.CreateExpression(calculator.OperationsList);
            return twoResultOnScreen;
        }

        /// <summary>
        /// 取得id
        /// </summary>
        /// <param name="sign">輸入符號</param>
        /// <param name="id">使用者id</param>
        /// <returns></returns>       
        [HttpGet()]
        public string GetID()
        {
            return _calculators.GetCalculatorID();
        }
    }
}
