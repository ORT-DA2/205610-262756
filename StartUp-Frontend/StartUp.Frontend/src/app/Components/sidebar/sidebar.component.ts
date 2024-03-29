import { Component, OnInit } from '@angular/core';
import { SessionService } from '../session/session.service';

@Component({
  selector: 'app-sidebar',
  templateUrl: './sidebar.component.html',
  styleUrls: ['./sidebar.component.css']
})
export class SidebarComponent implements OnInit {
  user: any
  role: any;

  constructor(private sessionService: SessionService) {
    this.sessionService.userLogged.subscribe(
      user => {
        this.user = localStorage.getItem('userLogged');
        this.role = localStorage.getItem('Rol');
      }
    );
  }

  ngOnInit(): void {
  }

}
