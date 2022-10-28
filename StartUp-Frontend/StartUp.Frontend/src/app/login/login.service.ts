import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError, retry } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class LoginService {

  constructor(private http: HttpClient) { }

  public ActionResult<GUID> login(session: Session): Promise<ActionResult<GUID>>{
    return this.http.post<Session>("api/sessions", session).pipe(
      catchError
    )
  }
}
