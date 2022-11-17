import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { RoleModel } from 'src/app/Models/roleModel';
import { environment } from 'src/environments/environment';
import { SessionService } from '../session/session.service';

@Injectable({
  providedIn: 'root'
})
export class RoleService {

  public URL: string = `${environment.API_URL}/role`;

  constructor(private http: HttpClient, private sessionService: SessionService) { }

  getRoles(): Observable<any> {
    const reqOp = {
      headers: new HttpHeaders({
        Authorization: `Bearer ${localStorage.getItem('Token')}`
      })
    };
    return this.http.get<any>(this.URL, reqOp);
  };

  postRole(roleModel: RoleModel): Observable<any> {
    const reqOp = {
      headers: new HttpHeaders({
        Authorization: `Bearer ${localStorage.getItem('Token')}`
      })
    };

    return this.http.post<any>(this.URL, roleModel, reqOp);

  }
}
