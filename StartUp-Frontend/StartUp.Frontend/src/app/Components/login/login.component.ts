import { Component, OnInit } from '@angular/core';
import { SessionModel } from 'src/app/Models/sessionModel';
import { SessionService } from '../session/session.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  sessions: SessionModel[] = [];
  public username: string = "";
  public password: string = "";

  constructor(private sessionService: SessionService) { }

  ngOnInit(): void {
  }

  login(userName: string, password: string): void {
    console.log("here");
    console.log(this.sessionService.login({ userName, password } as SessionModel)
      .subscribe(session => this.sessions.push(session)));
  }
}
