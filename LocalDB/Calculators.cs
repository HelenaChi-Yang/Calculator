using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Calculator;

namespace LocalDB
{
    /// <summary>
    /// 計算機資料庫
    /// </summary>
    public class Calculators
    {
        /// <summary>
        /// 多台計算機
        /// </summary>
        private ConcurrentDictionary<string, TheCalculator> AllCalculator;

        /// <summary>
        /// 建構子
        /// </summary>
        public Calculators()
        {
            AllCalculator = new ConcurrentDictionary<string, TheCalculator>();
        }

        /// <summary>
        /// 取得計算機
        /// </summary>
        /// <param name="input">輸入計算機 key</param>
        /// <returns></returns>
        public TheCalculator GetCalculator(string input)
        {
            TheCalculator theCalculator;
            if (AllCalculator.TryGetValue(input, out theCalculator))
            {
                return theCalculator;
            }
            theCalculator = new TheCalculator();
            AllCalculator.TryAdd(input, theCalculator);
            return theCalculator;
        }

        /// <summary>
        /// 取得計算機ID
        /// </summary>
        /// <returns></returns>
        public string GetCalculatorID()
        {
            TheCalculator theCalculator;
            string id = Guid.NewGuid().ToString();
            while(AllCalculator.TryGetValue(id, out theCalculator))
            {
                id = Guid.NewGuid().ToString();
            }
            return id;
        }
    }
}
