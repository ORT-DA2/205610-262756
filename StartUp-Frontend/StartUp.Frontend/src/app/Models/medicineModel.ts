import { SymptomModel } from "./symptomModel";

export class MedicineModel {
    code: string;
    name: string;
    presentation: string;
    amount: number;
    measure: string;
    price: number;
    prescription: boolean;
    symptoms: Array<SymptomModel>;
}