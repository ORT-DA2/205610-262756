import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError, map, tap } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { InvitationModel } from 'src/app/Models/invitationModel';
import { SessionService } from '../../session/session.service';

@Injectable({
  providedIn: 'root'
})
export class InvitationService {

  public errorMessage: string;
  public URL: string = `${environment.API_URL}/invitation`;

  constructor(private http: HttpClient, private sessionService: SessionService) { }

  generateNewCode(): Observable<any> {
    const reqOp = {
      headers: new HttpHeaders({
        Authorization: `Bearer ${localStorage.getItem('Token')}`
      })
    };
    return this.http.get<any>(this.URL + '/generateCode', reqOp)
      .pipe(
        tap(
          {
            next: (data: any) => { console.log(data) },
            error: (error: any) => { return error.message }
          }
        )
      );
  }

  updateInvitation(id: number, invitation: InvitationModel): Observable<any> {
    const reqOp = {
      headers: new HttpHeaders({
        Authorization: `Bearer ${localStorage.getItem('Token')}`
      })
    };
    var req = this.http.put<any>(this.URL + `/${id}`, invitation, reqOp);

    return req;
  }

  postInvitation(invitation: InvitationModel): Observable<any> {
    const reqOp = {
      headers: new HttpHeaders({
        Authorization: `Bearer ${localStorage.getItem('Token')}`
      })
    };
    var req = this.http.post<InvitationModel>(this.URL, invitation, reqOp);
    return req;
  }

  getInvitation(userName: string, code: number): Observable<any> {
    const reqOp = {
      headers: new HttpHeaders({
        Authorization: `Bearer ${localStorage.getItem('Token')}`
      })
    };
    return this.http.get<any>(this.URL + `/${userName}` + `/${code}`, reqOp);
  };

  getAllInvitations(): Observable<any> {
    const reqOp = {
      headers: new HttpHeaders({
        Authorization: `Bearer ${localStorage.getItem('Token')}`
      })
    };
    return this.http.get<any>(this.URL, reqOp);
  };
}
