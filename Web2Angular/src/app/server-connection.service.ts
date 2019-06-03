import { Injectable } from '@angular/core';
import { Observable, pipe, of  } from 'rxjs';
import {HttpClient, HttpHeaders} from '@angular/common/http'
import { catchError, map } from 'rxjs/operators';
import { Ticket } from './Models/ticket';

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

  getCoefficient(): Observable<any>
  {
    return this.http.get<any>('http://localhost:52295/api/Data/getCoefficient');
  }

  buyTicket(typeOfUser: any, typeOfTicket: any, totalPrice: any) : Observable<any>
  {
    let ticket: Ticket = new Ticket();
    ticket.typeOfUser = typeOfUser;
    ticket.typeOfTicket = typeOfTicket;
    ticket.totalPrice = totalPrice;
    return this.http.post<any>('http://localhost:52295/api/Data/buyTicket',ticket,httpOptions);
  }

}
