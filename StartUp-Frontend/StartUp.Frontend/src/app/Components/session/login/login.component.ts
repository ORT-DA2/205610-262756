import { HttpHeaders } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { SessionModel } from 'src/app/Models/sessionModel';
import { SessionService } from '../session.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  sessions: SessionModel[] = [];
  public username: string = "";
  public password: string = "";
  errorResponse: boolean = false;
  errorResponseMessage: string;

  constructor(private sessionService: SessionService) { }

  ngOnInit(): void {
  }

  login(userName: string, password: string): void {
    this.sessionService.login({ userName, password } as SessionModel)
      .subscribe(
        session => {
          this.sessions.push(session)
          sessionStorage.setItem('Rol', session.user.invitation.rol);
          sessionStorage.setItem('Token', session.token);
          sessionStorage.setItem('ActualUserName', session.user.invitation.userName);
          sessionStorage.setItem('userLogged', session.user);
          this.sessionService.userLogged.next(session.user);
          this.sessionService.token = session.token;
        },
        error => {
          this.errorResponseMessage = error.error;
          if (this.errorResponseMessage != null) {
            this.errorResponse = true;
          }
        }
      )
  }
}
