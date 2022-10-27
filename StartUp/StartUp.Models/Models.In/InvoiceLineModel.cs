using StartUp.Domain;

namespace StartUp.Models.Models.In
{
    public class InvoiceLineModel
    {
        public Medicine Medicine { get; set; }
        public int Amount { get; set; }

        public InvoiceLine ToEntity()
        {
            return new InvoiceLine()
            {
                Amount = this.Amount,
                Medicine = this.Medicine
            };
        }
    }
}
