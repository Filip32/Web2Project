import { Injectable } from '@angular/core';
import { Observable, pipe, of  } from 'rxjs';
import {HttpClient, HttpHeaders} from '@angular/common/http'
import { catchError, map } from 'rxjs/operators';

const httpOptions = {
headers: new HttpHeaders({'Content-type': 'x-www-form-urlencoded'})
};

@Injectable({
  providedIn: 'root'
})
export class ServerConnectionService {

  constructor(private http: HttpClient) { }

  getPricelist(): Observable<any>
  {
    return this.http.get<any>('http://localhost:52295/api/Data/getPricelist').pipe(
      map(res => {
        console.log(res);
      }),

      catchError(this.handleError<any>('getPricelist'))
    );
  }

  private handleError<T>(operation = 'operation', result?: T) {
    return (error: any): Observable<T> => {
      return of(result as T);
    };
  }
}