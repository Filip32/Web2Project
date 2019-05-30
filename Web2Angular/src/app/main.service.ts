import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import {HttpClient, HttpHeaders} from '@angular/common/http'
@Injectable({
  providedIn: 'root'
})
export class MainService {

  constructor(private http: HttpClient) { }

  login(arg: any): any
  {
    console.warn(arg);
    let poruka = this.http.get<string>('http://localhost:52295/api/Account');
  }

  register(arg: any): any
  {
  }
}
