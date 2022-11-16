import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { InvoiceLineModel } from 'src/app/Models/invoiceLineModel';
import { environment } from 'src/environments/environment';
import { SessionService } from '../session/session.service';

@Injectable({
  providedIn: 'root'
})
export class InvoiceLineService {

  public URL: string = `${environment.API_URL}/invoiceLine`;

  constructor(private http: HttpClient, private sessionService: SessionService) { }

  updateInvoideLine(id: number, invoiceLine: InvoiceLineModel): Observable<any> {
    const reqOp = {
      headers: new HttpHeaders({
        Authorization: `Bearer ${this.sessionService.token}`
      }),
    };

    return this.http.put<InvoiceLineModel>(this.URL + `/${id}`, invoiceLine, reqOp);
  }


}
