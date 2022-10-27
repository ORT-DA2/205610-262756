import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { UserModel } from 'src/app/Models/userModel';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  private URL: string = `${environment.API_URL}/user`;

  constructor(private http: HttpClient) { }

  postUser(user: UserModel) {

    console.log("user", user);

    const body = {
      email: user.email,
      password: user.password,
      roles: user.roles,
      address: user.address,
      invitation: user.invitation,
      pharmacy: user.pharmacy,
    };

    console.log("body", body);

    return this.http.post<UserModel>(this.URL, body)
  }
}
