import { PharmacyModel } from "./pharmacyModel";

export class InvitationModel {
    username: string;
    rol: string;
    pharmacy: PharmacyModel | null;
}