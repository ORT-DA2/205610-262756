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

    console.log("body:", user);

    let req = this.http.post<UserModel>(this.URL, user);

    req.forEach(usr => console.log(usr));

    return req;
  }

  getUser(username: string): any {
    const reqOp = {
      headers: new HttpHeaders({
        Authorization: `Bearer ${this.sessionService.token}`
      })
    };
    let req = this.http.get(this.URL + `/${username}`, reqOp);

    return req;
  }
}
