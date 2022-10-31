import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { stringify } from 'querystring';
import { catchError, Observable, throwError } from 'rxjs';
import { InvitationModel } from 'src/app/Models/invitationModel';
import { InvitationSearchCriteria } from 'src/app/Models/SearchCriteria/invitationSearchCriteria';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class InvitationService {

  private URL: string = `${environment.API_URL}/invitation`;

  constructor(private http: HttpClient) { }

  postInvitation(invitation: InvitationModel) {
    let params = new HttpParams;
    params.append("username", invitation.username);
    params.append("code", invitation.rol);
    if (invitation.pharmacy) {
      params.append("pharmacyName", invitation.pharmacy.name);
    }

    return this.http.post<InvitationModel>(this.URL, { observe: 'body', params })
  }

  getInvitation(userName: string, code: number): Observable<any> {

    return this.http.get<any>(this.URL + `/${userName}` + `/${code}`);
  };
}
