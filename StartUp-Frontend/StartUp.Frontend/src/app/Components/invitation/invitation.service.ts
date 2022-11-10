import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError, map, tap } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { InvitationModel } from 'src/app/Models/invitationModel';

@Injectable({
  providedIn: 'root'
})
export class InvitationService {

  public errorMessage: string;
  public URL: string = `${environment.API_URL}/invitation`;

  constructor(private http: HttpClient) { }

  generateNewCode(): Observable<any> {
    return this.http.get<any>(this.URL + '/generateCode')
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
    var req = this.http.put<any>(this.URL + `/${id}`, invitation);

    return req;
  }

  postInvitation(invitation: InvitationModel): Observable<any> {
    var req = this.http.post<InvitationModel>(this.URL, invitation);
    return req;
  }

  getInvitation(userName: string, code: number): Observable<any> {
    return this.http.get<any>(this.URL + `/${userName}` + `/${code}`);
  };

  getAllInvitations(): Observable<any> {
    return this.http.get<any>(this.URL);
  };
}
