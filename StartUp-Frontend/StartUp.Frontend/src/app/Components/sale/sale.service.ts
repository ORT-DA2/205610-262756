import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { SaleModel } from 'src/app/Models/saleModel';
import { environment } from 'src/environments/environment';
import { SessionService } from '../session/session.service';

@Injectable({
  providedIn: 'root'
})
export class SaleService {

  public URL: string = `${environment.API_URL}/sale`;

  constructor(private http: HttpClient, private sessionService: SessionService) { }

  getSale(code: number): Observable<any> {
    return this.http.delete(this.URL + `/${code}`);
  }

  getSales(): Observable<any> {
    const reqOp = {
      headers: new HttpHeaders({
        Authorization: `Bearer ${this.sessionService.token}`
      })
    };

    return this.http.get(this.URL, reqOp);
  }

  postSale(sale: SaleModel): Observable<any> {
    return this.http.post<SaleModel>(this.URL, sale);
  }
}
