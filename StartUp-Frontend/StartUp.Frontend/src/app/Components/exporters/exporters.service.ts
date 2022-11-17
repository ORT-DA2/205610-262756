import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ExportModel } from 'src/app/Models/exporterModel';
import { environment } from 'src/environments/environment';
import { SessionService } from '../session/session.service';

@Injectable({
  providedIn: 'root'
})
export class ExportersService {

  public URL: string = `${environment.API_URL}/exporter`

  constructor(private http: HttpClient, private sessionService: SessionService) { }

  postExportMedicines(model: ExportModel): Observable<any> {
    const reqOp = {
      headers: new HttpHeaders({
        Authorization: `Bearer ${localStorage.getItem('Token')}`
      })
    };

    return this.http.post<ExportModel>(this.URL, model, reqOp);
  }

  getFormats(): Observable<any> {
    const reqOp = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
        Authorization: `Bearer ${localStorage.getItem('Token')}`,
      })
    };

    return this.http.get<ExportModel>(this.URL, reqOp);
  }
}
