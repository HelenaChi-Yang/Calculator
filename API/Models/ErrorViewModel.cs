using System;

namespace API.Models
{
    /// <summary>
    /// ���~�^��
    /// </summary>
    public class ErrorViewModel
    {
        /// <summary>
        /// �ШDID
        /// </summary>
        public string RequestId { get; set; }

        /// <summary>
        /// ��ܽШDID
        /// </summary>
        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}