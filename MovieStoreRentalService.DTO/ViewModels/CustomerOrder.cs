namespace MovieStoreRentalService.DTO.ViewModels
{
    /// <summary>
    /// Поръчка
    /// </summary>
    public class CustomerOrder
    {
        /// <summary>
        /// Клиентски номер
        /// </summary>
        public string CustomerNumber { get; set; }

        /// <summary>
        /// Списък с поръчки
        /// </summary>
        public List<ItemOrder> Items { get; set; } = new List<ItemOrder>();
    }
}
