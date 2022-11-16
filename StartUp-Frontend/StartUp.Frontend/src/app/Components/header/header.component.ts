import { Component, OnInit } from '@angular/core';
import { SessionService } from '../session/session.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css'],
})

export class HeaderComponent implements OnInit {
  public user: any;

  constructor(private sessionService: SessionService) {
  }

  public toggle() {
    document.body.classList.toggle('toggle-sidebar');
  }

  loadUserLogged() {
  }

  logOut() {
    this.sessionService.logOut();
  }

  ngOnInit(): void {
    if (localStorage.getItem('userLogged') != null && localStorage.getItem('userLogged') != undefined) {
      this.sessionService.userLogged.subscribe(value => {
        this.user = value
        console.log(value);
      });
    }
  }

}
