import { Injectable } from '@angular/core';
import { CanActivate, Router, ActivatedRouteSnapshot, RouterStateSnapshot, CanActivateChild } from '@angular/router';
import { AuthService } from './auth.service';

@Injectable({
  providedIn: 'root',
})
export class AuthGuard implements CanActivate, CanActivateChild {
  constructor(private router: Router) { }

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean {    
    if (localStorage.login) {
        console.log(state.url);
        console.log(localStorage.role);
      if(localStorage.role == "Admin" && (state.url == "/routes-stations-a" ||
                                          state.url == "/timetable-a" ||
                                          state.url == "/priceist-a"
                                          )){
                return true;
      }
      else if(localStorage.role == "AppUser" && (state.url == "/" || 
                                               state.url == "/timetable" ||
                                               state.url == "/pricelist" ||
                                               state.url == "/profil")){
                return true;
      }
      else if(localStorage.role == "Controller" && (state.url == "/verification-c" || 
                                               state.url == "/tickets-c")){
                return true;
      }
      else{
        console.error("Access denied");
        if(localStorage.role == "Admin")
        {
            this.router.navigate(['/routes-stations-a']);
        }
        else if(localStorage.role == "AppUser")
        {
            this.router.navigate(['/']);
        }
        else if(localStorage.role == "Controller")
        {
            this.router.navigate(['/verification-c']);
        }
                return false;
      }
    }
    else {
      console.error("Access denied");
      this.router.navigate(['/']);
      return false;
    }
  }

  canActivateChild(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean {
    return this.canActivate(route, state);
  }

}