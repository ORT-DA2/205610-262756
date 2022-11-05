import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { SessionModel } from 'src/app/Models/sessionModel';
import { UserService } from '../user/user.service';

@Injectable({
  providedIn: 'root'
})

export class SessionService {

  public URL: string = `${environment.API_URL}/sessions`;
  public userLogged: any;

  constructor(private http: HttpClient, private userService: UserService) { }

  login(session: SessionModel): Observable<any> {
    var request = this.http.post<SessionModel>(this.URL, session)
      .pipe(
        catchError(error => {
          console.error('HTTP error: ', error)
          window.alert(error.error.message);
          return throwError(() => error)
        })
      );

    this.userLogged = this.userService.getUser(session.userName);
    console.log("USERLOGGED: ", this.userLogged);

    return request;
  };
}
