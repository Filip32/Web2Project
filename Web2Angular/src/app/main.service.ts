import { Injectable } from '@angular/core';
import { Observable, pipe, of  } from 'rxjs';
import {HttpClient, HttpHeaders} from '@angular/common/http'
import { catchError, map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class MainService {

  constructor(private http: HttpClient) { }

  login(arg: any): Observable<any>
  {
    console.warn(arg);
    let par = "username=" + arg.email + "&" + "password=" + arg.password + "&grant_type=password";
    return this.http.post<any>('http://localhost:52295/oauth/token',par,{ 'headers': { 'Content-type': 'x-www-form-urlencoded' } }).pipe(
      map(res => {
        let jwt = res.access_token;
        console.log(jwt);
        let jwtData = jwt.split('.')[1]
        let decodedJwtJsonData = window.atob(jwtData)
        let decodedJwtData = JSON.parse(decodedJwtJsonData)

        let role = decodedJwtData.role;
        console.log(role);
        localStorage.setItem('jwt', jwt)
        localStorage.setItem('role', role);
      }),

      catchError(this.handleError<any>('login'))
    );
  }

  register(arg: any): any
  {
    localStorage.removeItem('jwt');
    localStorage.removeItem('role');
  }

  private handleError<T>(operation = 'operation', result?: T) {
    return (error: any): Observable<T> => {
      return of(result as T);
    };
  }
}
