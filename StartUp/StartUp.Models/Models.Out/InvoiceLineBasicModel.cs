using StartUp.Domain;

namespace StartUp.Models.Models.Out
{
    public class InvoiceLineBasicModel
    {
        public int Id { get; set; }
        public int Amount { get; set; }
        public string State { get; set; }
        public Pharmacy Pharmacy { get; set; }

        public InvoiceLineBasicModel(InvoiceLine line)
        {
            Id = line.Id;
            this.Amount = line.Amount;
            this.State = line.State;
            this.Pharmacy = line.Pharmacy;
        }



        public override bool Equals(object? obj)
        {
            if (obj is InvoiceLineBasicModel)
            {
                var otherInvoiceLine = obj as InvoiceLineBasicModel;

                return Id == otherInvoiceLine.Id;
            }
            else
            {
                return false;
            }
        }
    }
}
