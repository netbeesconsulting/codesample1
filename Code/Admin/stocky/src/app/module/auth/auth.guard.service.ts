import { Injectable } from '@angular/core';
import { Router, ActivatedRouteSnapshot, CanActivate } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class AuthGuardService implements CanActivate {
  constructor(private router: Router) {
  }
  canActivate(route: ActivatedRouteSnapshot) {
    if ((localStorage.getItem('Token') === null || localStorage.getItem('Token') === undefined)) {
      this.router.navigate(['login']);
    }
    else
      return true;
  }
}
