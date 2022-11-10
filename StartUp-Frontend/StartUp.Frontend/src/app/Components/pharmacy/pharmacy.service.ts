import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { PharmacyModel } from 'src/app/Models/pharmacyModel';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class PharmacyService {

  public URL: string = `${environment.API_URL}/pharmacy`;

  constructor(private http: HttpClient) { }

  getPharmacies(): Observable<any> {
    return this.http.get<any>(this.URL);
  };

  postPharmacy(pharmacy: PharmacyModel): Observable<any> {
    return this.http.post<PharmacyModel>(this.URL, pharmacy);
  };
}
