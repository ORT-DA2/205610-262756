import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { PetitionModel } from 'src/app/Models/petitionModel';
import { environment } from 'src/environments/environment';
import { SessionService } from '../../session/session.service';

@Injectable({
  providedIn: 'root'
})
export class PetitionService {

  public URL: string = `${environment.API_URL}/petition`;

  constructor(private http: HttpClient, private sessionService: SessionService) { }

  postPetition(medicine: PetitionModel): Observable<any> {
    const reqOp = {
      headers: new HttpHeaders({
        Authorization: `Bearer ${this.sessionService.token}`
      })
    };
    return this.http.post<PetitionModel>(this.URL, medicine, reqOp);
  }

  getPetitions() {
    const reqOp = {
      headers: new HttpHeaders({
        Authorization: `Bearer ${this.sessionService.token}`
      })
    };
    return this.http.get(this.URL + "/fromPharmacy", reqOp);
  }
}
