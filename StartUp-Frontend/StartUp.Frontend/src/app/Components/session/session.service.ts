import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { BehaviorSubject, Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { SessionModel } from 'src/app/Models/sessionModel';

@Injectable({
  providedIn: 'root'
})

export class SessionService {

  public URL: string = `${environment.API_URL}/sessions`;
  public userLogged: BehaviorSubject<any>;
  public authHeader: HttpHeaders = new HttpHeaders;
  public token: string;
  public options: any;

  constructor(private http: HttpClient) {
    this.userLogged = new BehaviorSubject<any>(null);
  }

  login(session: SessionModel): Observable<any> {
    var request = this.http.post<SessionModel>(this.URL, session)
      .pipe(
        catchError(error => {
          console.error('HTTP error: ', error)
          window.alert(error.error.message);
          return throwError(() => error)
        })
      );

    return request;
  };

  logOut() {
    var req = this.http.delete(this.URL + `${localStorage.getItem('ActualUserName')}`);

    this.userLogged.next(null);
    localStorage.clear();
    console.log(localStorage);

    return req;
  }
}
