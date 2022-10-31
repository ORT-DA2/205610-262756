import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, of, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { SessionModel } from 'src/app/Models/sessionModel';

@Injectable({
  providedIn: 'root'
})

export class SessionService {

  private URL: string = `${environment.API_URL}/sessions`;

  constructor(private http: HttpClient) { }

  login(session: SessionModel): Observable<any> {
    return this.http.post<SessionModel>(this.URL, session)
    .pipe(
      catchError(error => {
        console.error('HTTP error: ', error)
        return throwError(()=> error)
      })
    );
  };
}
