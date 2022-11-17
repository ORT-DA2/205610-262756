import { MedicineModel } from "./medicineModel";

export class InvoiceLineModel {
    medicine: MedicineModel;
    amount: number;
    state: string;
}