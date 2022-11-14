import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { RequestModel } from 'src/app/Models/requestModel';
import { environment } from 'src/environments/environment';
import { SessionService } from '../session/session.service';

@Injectable({
  providedIn: 'root'
})
export class RequestService {

  private URL: string = `${environment.API_URL}/request`;

  constructor(private http: HttpClient, private sessionService: SessionService) { }

  postRequest(requestModel: RequestModel): Observable<any> {
    const reqOp = {
      headers: new HttpHeaders({
        Authorization: `Bearer ${this.sessionService.token}`
      })
    }; return this.http.post<RequestModel>(this.URL, requestModel, reqOp);
  }

  getRequests(): Observable<any> {
    const reqOp = {
      headers: new HttpHeaders({
        Authorization: `Bearer ${this.sessionService.token}`
      })
    };
    return this.http.get(this.URL, reqOp);
  }

  putRequest(id: number, requestModel: RequestModel): Observable<any> {
    const reqOp = {
      headers: new HttpHeaders({
        Authorization: `Bearer ${this.sessionService.token}`
      })
    };

    return this.http.put(this.URL + `/${id}`, requestModel, reqOp);
  }

  deleteRequest(id: number): Observable<any> {
    const reqOp = {
      headers: new HttpHeaders({
        Authorization: `Bearer ${this.sessionService.token}`
      })
    };
    console.log(reqOp)

    return this.http.delete(this.URL + `/${id}`, reqOp);
  }
}
