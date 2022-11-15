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
    this.sessionService.userLogged.subscribe(
      user => { this.user = user; }
    );
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
  }

}
