namespace Console_Application.Interfaces
{
    public interface IObject
    {
        decimal StockId { get; set; }
        decimal Quantity { get; set; }

        decimal Post();
        void Load(decimal stockId);
        void Decrease(decimal materialId);
        void Decrease(decimal materialId, decimal quantity);
    }
}