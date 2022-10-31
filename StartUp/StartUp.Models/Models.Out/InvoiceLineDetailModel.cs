using StartUp.Domain;

namespace StartUp.Models.Models.Out
{
    public class InvoiceLineDetailModel
    {
        public int Id { get; set; }
        public MedicineInvoiceLineModel Medicine { get; set; }
        public string State { get; set; }

        public InvoiceLineDetailModel(InvoiceLine line)
        {
            Medicine = new MedicineInvoiceLineModel(line.Medicine);
            this.State = line.State;
        }

        public override bool Equals(object? obj)
        {
            if (obj is InvoiceLineDetailModel)
            {
                var otherInvoiceLine = obj as InvoiceLineDetailModel;

                return Id == otherInvoiceLine.Id;
            }
            else
            {
                return false;
            }
        }
    }
}
