import { Injectable } from '@angular/core';
import { Observable, pipe, of } from 'rxjs';
import { HttpClient, HttpHeaders } from '@angular/common/http'
import { catchError, map } from 'rxjs/operators';
import { Ticket } from './Models/ticket';
import { ArrayType } from '@angular/compiler';
import { environment } from 'src/environments/environment';


const httpOptions = {
  headers: new HttpHeaders({ 'Content-type': 'x-www-form-urlencoded' })
};

@Injectable({
  providedIn: 'root'
})
export class ServerConnectionService {

  constructor(private http: HttpClient) { }

  postFile(fileToUpload: File,name:string): Observable<any> {
    const endpoint = 'http://localhost:52295/api/Data/UploadDishImage';
    const formData: FormData = new FormData();
    formData.append(name, fileToUpload, fileToUpload.name);
    return this.http.post(endpoint, formData);
}



  getPricelist(): Observable<any> {
    return this.http.get<any>('http://localhost:52295/api/Data/getPricelist');
  }

  getProfileData(): Observable<any> {
    return this.http.get<any>('http://localhost:52295/api/Data/getProfileData');
  }

  getRoutes(): Observable<any>
  {
    return this.http.get<any>('http://localhost:52295/api/Data/getRoutes');
  }

  getUsers(): Observable<any>
  {
    return this.http.get<any>('http://localhost:52295/api/Data/getUsers');
  }


  getRoute(id: number): Observable<any>
  {
    return this.http.get<any>('http://localhost:52295/api/Data/getRoute' + `/?id=${id}`);
  }

  getCoefficient(): Observable<any> {
    return this.http.get<any>('http://localhost:52295/api/Data/getCoefficient');
  }

  getTypeOfLoginUser(): Observable<any> {
    return this.http.get<any>('http://localhost:52295/api/Data/getTypeOfLoginUser');
  }

  notify(): Observable<any> {  
    return this.http.post("http://localhost:52295/api/Notify", "", {headers:{"Accept": "text/plain"}});
  }

  buyTicket(typeOfUser: string, typeOfTicket: string, totalPrice: number): Observable<any> {
    let ticket: Ticket = new Ticket();
    ticket.TypeOfUser = typeOfUser;
    ticket.TypeOfTicket = typeOfTicket;
    ticket.TotalPrice = totalPrice;
    return this.http.post<any>('http://localhost:52295/api/Data/buyTicket', ticket);
  }
  GetTickets(): Observable<any>{
    return this.http.get<any>('http://localhost:52295/api/Data/getTickes');
  }

  getTicket(arg: any):Observable<any>{
    let id = arg.ticket_id;
    return this.http.get<any>('http://localhost:52295/api/Data/getTicket' + `/?id=${id}`);
  }

  Register(arg: any): Observable<any> {
    let headers = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json'
      })
    }

    let par = {
      username: arg.Email,
      password: arg.password,
      name: arg.Name,
      lastName: arg.Lastname,
      birthday: arg.Birthday,
      streetName: arg.Address_StreetName,
      streetNumber: arg.Address_StreetNumber,
      city: arg.Address_City,
      UserType: arg.UserType
    };

    return this.http.post<any>('http://localhost:52295/api/Register/registerUser', par, headers);
  }

  DeleteTicket(u: any): Observable<any>{
    return this.http.post<any>('http://localhost:52295/api/Data/deleteTicket',u);
  }

  ApproveUser(u: any): Observable<any>{
    return this.http.post<any>('http://localhost:52295/api/Data/approveUser',u);
  }

  DenyUser(u: any): Observable<any>{
    return this.http.post<any>('http://localhost:52295/api/Data/denyUser',u);
  }

  uploadPhotoToBackend(data: any, name: string, options?: any) : Observable<any>{

    let par = {
      Id: name,
      Picture: data
    }
    let headers = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json'
      })
    }

    return this.http.post('http://localhost:52295/api/Data/UploadPhoto', par);
}

  UpdateProfile(arg: any): Observable<any> {
    let headers = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json'
      })
    }

    let par = {
      username: arg.Email,
      password: arg.password,
      name: arg.Name,
      lastName: arg.Lastname,
      birthday: arg.Birthday,
      streetName: arg.Address_StreetName,
      streetNumber: arg.Address_StreetNumber,
      city: arg.Address_City,
      UserType: arg.UserType,
      originalPassword:arg.CurrentPassword,
    };

    return this.http.post<any>('http://localhost:52295/api/Data/updateProfile', par, headers);
  }

  private handleError<T>(operation = 'operation', result?: T) {
    return (error: any): Observable<T> => {
      return of(result as T);
    };
  }

  getPhoto(id: string): Observable<any> {
    return this.http.get<any>('http://localhost:52295/api/Data/getPhoto' + `/?id=${id}`);
  }

  

}
