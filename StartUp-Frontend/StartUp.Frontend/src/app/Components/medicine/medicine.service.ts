import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { MedicineModel } from 'src/app/Models/medicineModel';
import { environment } from 'src/environments/environment';
import { SessionService } from '../session/session.service';

@Injectable({
  providedIn: 'root'
})
export class MedicineService {

  public URL: string = `${environment.API_URL}/medicine`;

  constructor(private http: HttpClient, private sessionService: SessionService) { }

  postMedicine(medicine: MedicineModel): Observable<any> {

    const reqOp = {
      headers: new HttpHeaders({
        Authorization: `Bearer ${this.sessionService.token}`
      })
    };

    console.log("header", reqOp);
    return this.http.post<MedicineModel>(this.URL, medicine, reqOp);
  }

  getMedicine(): Observable<any> {
    const reqOp = {
      headers: new HttpHeaders({
        Authorization: `Bearer ${this.sessionService.token}`
      })
    };
    return this.http.get<MedicineModel>(this.URL, reqOp);
  }
}
