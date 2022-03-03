using System;

namespace API.Models
{
    /// <summary>
    /// 錯誤回傳
    /// </summary>
    public class ErrorViewModel
    {
        /// <summary>
        /// 請求ID
        /// </summary>
        public string RequestId { get; set; }

        /// <summary>
        /// 顯示請求ID
        /// </summary>
        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}