import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-nav-bar',
  templateUrl: './nav-bar.component.html',
  styleUrls: ['./nav-bar.component.css']
})
export class NavBarComponent implements OnInit {

  constructor() { }
  loginvar:boolean = false;

  ngOnInit() {
  }

login():void{
this.loginvar= true;

}

logout(): any
  {
    localStorage.removeItem('jwt');
    localStorage.removeItem('role');
    localStorage.removeItem('login');
  }

isLogin(): boolean
{
  if(localStorage.login)
  {
    return true;
  }
  else
  {
    return false;
  }
}

isAdmin()
{
  if(localStorage.role){
      if(localStorage.role == "Admin")
      {
        return true;
      }
      else
      {
        return false;
      }
  }
  return false;
}

isControlor()
{
  if(localStorage.role){
    if(localStorage.role == "Controlor")
    {
      return true;
    }
    else
    {
      return false;
    }
  }
  return false;
}

isUser()
{
  if(localStorage.role)
  {
    if(localStorage.role == "AppUser")
    {
      return true;
    }
    else
    {
      return false;
    }
  }
  return true;
}

isUserProfile()
{
  if(localStorage.role)
  {
    if(localStorage.role == "AppUser")
    {
      return true;
    }
    else
    {
      return false;
    }
  }
  return false;
}

}
