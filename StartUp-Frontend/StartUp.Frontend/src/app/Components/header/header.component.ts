import { Component, OnInit } from '@angular/core';
import { HeaderService } from './header.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css'],
})

export class HeaderComponent implements OnInit {

  constructor(private service: HeaderService) {
  }

  public toggle() {
    document.body.classList.toggle('toggle-sidebar');
  }

  ngOnInit(): void {
  }

}
