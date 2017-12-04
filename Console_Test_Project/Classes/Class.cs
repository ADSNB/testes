using Console_Application.Interfaces;
using System;

namespace Console_Application.Classes
{
    public class Class : IObject
    {
        /*
        public Class()
        { }
        */

        public decimal StockId { get; set; }

        public decimal Quantity { get; set; }

        public DateTime Data { get; set; }

        public decimal Post() { return 2; }
        public void Load(decimal stockId) { }
        public void Decrease(decimal materialId) { }
        public void Decrease(decimal materialId, decimal quantity) { }
    }
}
