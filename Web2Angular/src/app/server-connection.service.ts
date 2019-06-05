import { Injectable } from '@angular/core';
import { Observable, pipe, of  } from 'rxjs';
import {HttpClient, HttpHeaders} from '@angular/common/http'
import { catchError, map } from 'rxjs/operators';
import { Ticket } from './Models/ticket';
import { ArrayType } from '@angular/compiler';


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
    return this.http.get<any>('http://localhost:52295/api/Data/getPricelist');
  }

  getProfileData(): Observable<any>
  {
    return this.http.get<any>('http://localhost:52295/api/Data/getProfileData');
  }

  getRoutes(): Observable<any>
  {
    return this.http.get<any>('http://localhost:52295/api/Data/getRoutes');
  }

  getCoefficient(): Observable<any>
  {
    return this.http.get<any>('http://localhost:52295/api/Data/getCoefficient');
  }

  buyTicket(typeOfUser: string, typeOfTicket: string, totalPrice: number) : Observable<any>
  {
    let ticket: Ticket = new Ticket();
    ticket.TypeOfUser = typeOfUser;
    ticket.TypeOfTicket = typeOfTicket;
    ticket.TotalPrice = totalPrice;
    return this.http.post<any>('http://localhost:52295/api/Data/buyTicket',ticket);
  }
  
  Register(arg: any): Observable<any>
  {
    let headers={
      headers: new HttpHeaders({
          'Content-Type': 'application/json'
      })
  }

     let par= {
      username:arg.Email,
      password: arg.password,
      name: arg.Name,
      lastName: arg.Lastname,
      birthday : arg.Birthday,
      streetName:arg.Address_StreetName,
      streetNumber: arg.Address_StreetNumber,
      city: arg.Address_City,
      UserType: arg.UserType
    };

    return this.http.post<any>('http://localhost:52295/api/Register/registerUser',par,headers);
  }

  UpdateProfile(arg: any): Observable<any>
  {
    let headers={
      headers: new HttpHeaders({
          'Content-Type': 'application/json'
      })
  }

     let par= {
      username:arg.Email,
      password: arg.password,
      name: arg.Name,
      lastName: arg.Lastname,
      birthday : arg.Birthday,
      streetName:arg.Address_StreetName,
      streetNumber: arg.Address_StreetNumber,
      city: arg.Address_City,
      UserType: arg.UserType
    };

    return this.http.post<any>('http://localhost:52295/api/Data/updateProfile',par,headers);
  }

  private handleError<T>(operation = 'operation', result?: T) {
    return (error: any): Observable<T> => {
      return of(result as T);
    };
  }

}
