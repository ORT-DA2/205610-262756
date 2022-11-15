import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { PharmacyModel } from 'src/app/Models/pharmacyModel';
import { environment } from 'src/environments/environment';
import { SessionService } from '../session/session.service';

@Injectable({
  providedIn: 'root'
})
export class PharmacyService {

  public URL: string = `${environment.API_URL}/pharmacy`;

  constructor(private http: HttpClient, private sessionService: SessionService) { }

  getPharmacies(): Observable<any> {
    const reqOp = {
      headers: new HttpHeaders({
        Authorization: `Bearer ${this.sessionService.token}`
      })
    };
    return this.http.get<any>(this.URL, reqOp);
  };

  postPharmacy(pharmacy: PharmacyModel): Observable<any> {
    const reqOp = {
      headers: new HttpHeaders({
        Authorization: `Bearer ${this.sessionService.token}`
      })
    };
    return this.http.post<PharmacyModel>(this.URL, pharmacy, reqOp);
  };
}
