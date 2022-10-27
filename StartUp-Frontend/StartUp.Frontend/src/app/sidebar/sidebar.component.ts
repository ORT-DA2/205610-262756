import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { HeaderService } from '../header/header.service';

@Component({
  selector: 'app-sidebar',
  templateUrl: './sidebar.component.html',
  styleUrls: ['./sidebar.component.css']
})
export class SidebarComponent implements OnInit {
  
  public showMenue: boolean = false;
  private _headerService: HeaderService;

  constructor(private headerService: HeaderService) { 
    this._headerService = headerService;
    this._headerService.showMenu$.subscribe( value => 
      { 
        console.log(value)
        this.showMenue = value;
        console.log("sidebar:", this.showMenue);
      });
    
  }

  ngOnInit(): void {
    this._headerService.showMenu$.subscribe();
  }

}
