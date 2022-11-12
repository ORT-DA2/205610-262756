import { PetitionModel } from "./petitionModel";

export class RequestModel {
    petitions: Array<PetitionModel>[];
    state: string;
}