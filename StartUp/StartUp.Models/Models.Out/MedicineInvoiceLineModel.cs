

using StartUp.Domain;

namespace StartUp.Models.Models.Out
{
    public class MedicineInvoiceLineModel
    {
        public string Code { get; set; }
        public string Name { get; set; }
        

        public MedicineInvoiceLineModel(Medicine medicine)
        {
            this.Name = medicine.Name;
            this.Code = medicine.Code;
        }
    }
}
