import { Component, Input, OnInit, Output } from '@angular/core';
import { HeaderService } from './header.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})

export class HeaderComponent implements OnInit {

  public _menuVisible:boolean = false;
  private _headerService: HeaderService;

  constructor(private service: HeaderService) { 
    this._headerService = service;
    
  }

  public ShowMenu(){
    this._menuVisible = !this._menuVisible;
    this._headerService.changeShowMenuValue(this._menuVisible);
    console.log("menu", this._menuVisible);
  }

  ngOnInit(): void {
    this._headerService.showMenu$.subscribe();
  }

}
