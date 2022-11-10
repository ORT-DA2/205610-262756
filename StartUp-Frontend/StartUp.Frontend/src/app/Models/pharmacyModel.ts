import { MedicineModel } from "./medicineModel";
import { RequestModel } from "./requestModel";

export class PharmacyModel {
    name: string;
    address: string;
    stock: Array<MedicineModel> | [];
    requests: Array<RequestModel> | [];
}