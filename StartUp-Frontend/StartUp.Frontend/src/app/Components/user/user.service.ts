import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { UserModel } from 'src/app/Models/userModel';
import { environment } from 'src/environments/environment';
import { SessionService } from '../session/session.service';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  public URL: string = `${environment.API_URL}/user`;

  constructor(private http: HttpClient, private sessionService: SessionService) { }

  postUser(user: UserModel) {
    let req = this.http.post<UserModel>(this.URL, user);

    return req;
  }

  getUser(username: string): any {
    const reqOp = {
      headers: new HttpHeaders({
        Authorization: `Bearer ${localStorage.getItem('Token')}`
      })
    };
    let req = this.http.get(this.URL + `/${username}`, reqOp);

    return req;
  }
}
