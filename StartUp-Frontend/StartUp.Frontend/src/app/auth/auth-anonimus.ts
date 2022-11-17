import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot } from '@angular/router';
import { Observable } from 'rxjs';
import { SessionService } from '../Components/session/session.service';
import { AuthService } from './auth.service';

@Injectable({
    providedIn: 'root'
})
export class AuthAnonimusUser implements CanActivate {
    constructor(private authService: AuthService, private sessionService: SessionService) { };

    canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<boolean> | Promise<boolean> | boolean {

        if (localStorage.getItem('Rol') == null) {
            return true;
        }
        return false;
    }
}