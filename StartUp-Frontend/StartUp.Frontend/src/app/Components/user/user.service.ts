import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { UserModel } from 'src/app/Models/userModel';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  public URL: string = `${environment.API_URL}/user`;

  constructor(private http: HttpClient) { }

  postUser(user: UserModel) {

    console.log("body:", user);

    let req = this.http.post<UserModel>(this.URL, user);

    req.forEach(usr => console.log(usr));

    return req;
  }

  getUser(username: string): any {
    let req = this.http.get(this.URL + `/${username}`);

    return req;
  }
}
