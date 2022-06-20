using System.ComponentModel;

namespace AtomicAssetsClient.Data
{
    public enum SaleState
    {
        /// <summary>
        /// Sale created but offer was not send yet.
        /// </summary>
        [Description("Sale created but offer was not send yet")]
        Waiting = 0,

        /// <summary>
        /// Assets for sale.
        /// </summary>
        [Description("Assets for sale")]
        Listed = 1,

        /// <summary>
        /// Sale was canceled.
        /// </summary>
        [Description("Sale was canceled")]
        Cancelled = 2,

        /// <summary>
        /// Sale was bought.
        /// </summary>
        [Description("Sale was bought")]
        Sold = 3,

        /// <summary>
        /// Sale is still listed but offer is currently invalid (can become valid again if the user owns all assets again).
        /// </summary>
        [Description("Sale is still listed but offer is currently invalid (can become valid again if the user owns all assets again)")]
        Invalid = 4,
    }
}
