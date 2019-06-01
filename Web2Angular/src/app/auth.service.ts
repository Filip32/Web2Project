import { Injectable } from '@angular/core';
import { Observable, pipe, of  } from 'rxjs';
import {HttpClient, HttpHeaders} from '@angular/common/http'
import { catchError, map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  constructor(private http: HttpClient) { }

  login(arg: any): Observable<any>
  {
    let par = "username=" + arg.email + "&" + "password=" + arg.password + "&grant_type=password";
    return this.http.post<any>('http://localhost:52295/oauth/token',par,{ 'headers': { 'Content-type': 'x-www-form-urlencoded' } }).pipe(
      map(res => {
        let jwt = res.access_token;
        let jwtData = jwt.split('.')[1]
        let decodedJwtJsonData = window.atob(jwtData)
        let decodedJwtData = JSON.parse(decodedJwtJsonData)

        let role = decodedJwtData.role;
        localStorage.setItem('jwt', jwt)
        localStorage.setItem('role', role);
        localStorage.setItem('login', "true");
      }),

      catchError(this.handleError<any>('login'))
    );
  }

  private handleError<T>(operation = 'operation', result?: T) {
    return (error: any): Observable<T> => {
      return of(result as T);
    };
  }
}
