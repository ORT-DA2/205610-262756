import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})

export class HeaderService {

  private showMenu = new BehaviorSubject<boolean>(false);
  showMenu$ = this.showMenu.asObservable();

  constructor() { }

  changeShowMenuValue(show: boolean){
    this.showMenu.next(show);
    console.log("server", this.showMenu.value);
  }
}
