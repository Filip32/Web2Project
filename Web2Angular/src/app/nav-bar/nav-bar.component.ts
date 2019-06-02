import { Component, OnInit } from '@angular/core';
import { Router,NavigationEnd } from '@angular/router';
import { ServerConnectionService } from '../server-connection.service';
@Component({
  selector: 'app-nav-bar',
  templateUrl: './nav-bar.component.html',
  styleUrls: ['./nav-bar.component.css']
})
export class NavBarComponent implements OnInit {

  constructor(private router: Router, private serverConnectionService: ServerConnectionService) { 

  }

  ngOnInit() {
  }	

getPricelist()
{
  this.serverConnectionService.getPricelist().subscribe(
    (res) => {
     console.log(res);
    }
  );

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
    if(localStorage.role == "Controller")
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
