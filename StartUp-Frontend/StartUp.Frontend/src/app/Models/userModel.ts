import { InvitationModel } from "./invitationModel"
import { PharmacyModel } from "./pharmacyModel"
import { RoleModel } from "./roleModel"

export class UserModel {
    email: string;
    password: string;
    address: string;
    roles: RoleModel;
    invitation: InvitationModel;
    pharmacy: PharmacyModel;
}