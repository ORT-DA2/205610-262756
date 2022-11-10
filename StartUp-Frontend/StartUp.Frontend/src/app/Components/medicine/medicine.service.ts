import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { MedicineModel } from 'src/app/Models/medicineModel';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class MedicineService {

  public URL: string = `${environment.API_URL}/medicine`;

  constructor(private http: HttpClient) { }

  postMedicine(medicine: MedicineModel): Observable<any> {
    return this.http.post<MedicineModel>(this.URL, medicine);
  }

  getMedicine(): Observable<any> {
    return this.http.get<MedicineModel>(this.URL);
  }
}
