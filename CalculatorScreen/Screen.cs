using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Calculator;
using Newtonsoft.Json;

namespace ScreenUI
{
    /// <summary>
    /// UI介面
    /// </summary>
    public partial class Screen : Form
    {
        /// <summary>
        /// Screen 的 ID
        /// </summary>
        private string Id;

        /// <summary>
        /// 畫面表格
        /// </summary>
        public Screen()
        {
            InitializeComponent();
            textBox1.Text = ButtonSign.ZERO_SIGN;
            Id = GetID();
        }

        /// <summary>
        /// 所有按鈕點擊事件
        /// </summary>
        /// <param name="sender">物件傳遞</param>
        /// <param name="e">事件</param>
        public async void AllButtomClick(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            string buttonSign = WebUtility.UrlEncode(btn.Text);

            string requestURL = $"https://localhost:44347/api/calculator/{Id}?sign={buttonSign}";
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(requestURL);
                if (response.IsSuccessStatusCode)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    TwoResultOnScreen result = JsonConvert.DeserializeObject<TwoResultOnScreen>(responseBody);

                    resultBox.Text = result.ExpressionResult;
                    textBox1.Text = result.OperandResult;
                }
                else
                {
                    resultBox.Text = "網頁出現錯誤請稍後";
                }
            }       
        }

        /// <summary>
        /// 取得ID
        /// </summary>
        /// <returns></returns>
        public string GetID()
        {
            string responseBody = null;
            string requestURL = $"https://localhost:44347/api/calculator";
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = client.GetAsync(requestURL).Result;
                if (response.IsSuccessStatusCode)
                {
                    responseBody = response.Content.ReadAsStringAsync().Result;
                }
                else
                {
                    resultBox.Text = "網頁出現錯誤請稍後";
                }
            }
            return responseBody;
        }
    }
}
