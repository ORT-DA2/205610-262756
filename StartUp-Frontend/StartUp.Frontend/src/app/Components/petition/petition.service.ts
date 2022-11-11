import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { PetitionModel } from 'src/app/Models/petitionModel';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class PetitionService {

  public URL: string = `${environment.API_URL}/petition`;

  constructor(private http: HttpClient) { }

  postPetition(medicine: PetitionModel): Observable<any> {
    return this.http.post<PetitionModel>(this.URL, medicine);
  }
}
