import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { InvitationModel } from 'src/app/Models/invitationModel';

@Injectable({
  providedIn: 'root'
})
export class InvitationService {

  public URL: string = `${environment.API_URL}/invitation`;

  constructor(private http: HttpClient) { }

  postInvitation(invitation: InvitationModel): Observable<any> {
    var req = this.http.post<InvitationModel>(this.URL, invitation)
      .pipe(
        catchError(error => {
          console.error('HTTP error: ', error)
          window.alert(error.error.message);
          return throwError(() => error)
        })
      );

    console.log("req", req);
    return req;
  }

  getInvitation(userName: string, code: number): Observable<any> {
    return this.http.get<any>(this.URL + `/${userName}` + `/${code}`);
  };

  getAllInvitations(): Observable<any> {
    return this.http.get<any>(this.URL);
  };
}
