import { Component, OnInit } from '@angular/core';
import { SessionService } from '../session/session.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css'],
})

export class HeaderComponent implements OnInit {
  public userName: any;
  public userRole: any;
  errorResponse: boolean = false;
  errorResponseMessage: string;

  constructor(private sessionService: SessionService) {
    if (this.sessionService.userLogged != null || this.sessionService.userLogged != undefined) {
      this.sessionService.userLogged.subscribe(value => {
        value = localStorage.getItem('userLogged');
        if (value != null) {
          this.userName = localStorage.getItem('ActualUserName');
          this.userRole = localStorage.getItem('Rol');
        }
      });
    }
  }

  public toggle() {
    document.body.classList.toggle('toggle-sidebar');
  }

  logOut() {
    this.sessionService.logOut().subscribe(
      data => {

      },
      error => {
        this.errorResponseMessage = error.error;
        if (this.errorResponseMessage != null) {
          console.error(this.errorResponseMessage);
        }
      }
    );
    window.location.reload();
  }

  ngOnInit(): void {
  }

}
