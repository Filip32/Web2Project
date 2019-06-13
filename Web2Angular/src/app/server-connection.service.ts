import { Injectable } from '@angular/core';
import { Observable, pipe, of } from 'rxjs';
import { HttpClient, HttpHeaders } from '@angular/common/http'
import { catchError, map } from 'rxjs/operators';
import { Ticket } from './Models/ticket';
import { ArrayType } from '@angular/compiler';
import { PricelistHelp } from './Models/pricelist-help';
import { environment } from 'src/environments/environment';


const httpOptions = {
  headers: new HttpHeaders({ 'Content-type': 'x-www-form-urlencoded' })
};

@Injectable({
  providedIn: 'root'
})
export class ServerConnectionService {

  constructor(private http: HttpClient) { }

  addPricelist(arg: any): Observable<any> {
    let headers = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json'
      })
    }
    let par = {
      FromDate: arg.from,
      ToDate: arg.to,
      TimePrice: arg.timely,
      DailyPrice: arg.daily,
      MonthlyPrice: arg.monthly,
      YearlyPrice: arg.yearly
    };
    return this.http.post<any>('http://localhost:52295/api/Pricelist/addPricelist', par, headers);
  }

  changePricelist(arg: any, id: number): Observable<any> {
    let headers = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json'
      })
    }
    let c: PricelistHelp = new PricelistHelp();
          c.Id = id;
          c.ToDate = arg.to;
    return this.http.post<any>('http://localhost:52295/api/Pricelist/changePricelist', c, headers);
  }

  getPricelistsAdmin(): Observable<any> {
    return this.http.get<any>('http://localhost:52295/api/Pricelist/getPricelists');
  }

  getPricelistAdminChange(id: number): Observable<any> {
    return this.http.get<any>('http://localhost:52295/api/Pricelist/getPricelistChange' + `/?id=${id}`);
  }

  getPricelistAdmin(id: number): Observable<any>
  {
    return this.http.get<any>('http://localhost:52295/api/Pricelist/getPricelist' + `/?id=${id}`);
  }

  getRoutesCityWorkday(): Observable<any> {
    return this.http.get<any>('http://localhost:52295/api/Data/getRoutesCityWorkday');
  }
  getRoutesCitySaturday(): Observable<any> {
    return this.http.get<any>('http://localhost:52295/api/Data/getRoutesCitySaturday');
  }
  getRoutesCitySunday(): Observable<any> {
    return this.http.get<any>('http://localhost:52295/api/Data/getRoutesCitySunday');
  }
  getRoutesSuburbanWorkday(): Observable<any> {
    return this.http.get<any>('http://localhost:52295/api/Data/getRoutesSuburbanWorkday');
  }
  getRoutesSuburbanSaturday(): Observable<any> {
    return this.http.get<any>('http://localhost:52295/api/Data/getRoutesSuburbanSaturday');
  }
  getRoutesSuburbanSunday(): Observable<any> {
    return this.http.get<any>('http://localhost:52295/api/Data/getRoutesSuburbanSunday');
  }
  Hi(): Observable<any> {
    return this.http.get<any>('http://localhost:52295/api/Account/hi');
  }
  getTimetable(par: any): Observable<any> {
    let headers = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json'
      })
    }
    return this.http.post<any>('http://localhost:52295/api/Data/timetable', par, headers);
  }

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

  getRouteListAdmin(id: number): Observable<any> {
    return this.http.get<any>('http://localhost:52295/api/Timetable/getRouteListAdmin' + `/?id=${id}`);
  }

  getRoutesAdmin(): Observable<any>
  {
    return this.http.get<any>('http://localhost:52295/api/Timetable/getRoutesAdmin');
  }

  getRouteAdmin(id: number): Observable<any> {
    return this.http.get<any>('http://localhost:52295/api/Timetable/getRouteAdmin' + `/?id=${id}`);
  }

  getTypeOfDay(): Observable<any> {
    return this.http.get<any>('http://localhost:52295/api/Timetable/getTypeOfDay');
  }

  changeRouteNumberAdmin(line: string, idlinije: number): Observable<any> {
    let headers = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json'
      })
    }

    let par = {
      RouteNumber: line,
      Id: idlinije
    };
    return this.http.post<any>('http://localhost:52295/api/Timetable/changeRouteNumberAdmin', par, headers);
  }

  chageDayRouteAdmin(selectedtipDana: string, idlinije: number): Observable<any> {
    let headers = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json'
      })
    }
    let par = {
      Day: selectedtipDana,
      Id: idlinije
    };
    return this.http.post<any>('http://localhost:52295/api/Timetable/chageDayRouteAdmin', par, headers);
  }

  changeDepAdmin(arg: any, idlinije: number): Observable<any> {
    let headers = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json'
      })
    }

    let par = {
      Departures: arg.svipolasci,
      Id: idlinije
    };
    return this.http.post<any>('http://localhost:52295/api/Timetable/changeDepAdmin', par, headers);
  }

  deleteRouteAdmin(id: number): Observable<any> {
    return this.http.get<any>('http://localhost:52295/api/Timetable/deleteRouteAdmin' + `/?id=${id}`);
  }

  getTypeOfTimetable(): Observable<any> {
    return this.http.get<any>('http://localhost:52295/api/Timetable/getTypeOfTimetable');
  }

  getPhoto(id: string): Observable<any> {
    return this.http.get<any>('http://localhost:52295/api/Data/getPhoto' + `/?id=${id}`);
  }

  addNewRouteAdmin(arg: any): Observable<any> {
    let headers = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json'
      })
    }
    let par = {
      Day: arg.danNew,
      Departures: arg.svipolasciNew,
      RouteNumber: arg.naslovNew,
      TyoeOfRoute: arg.redvoznje
    };
    return this.http.post<any>('http://localhost:52295/api/Timetable/addNewRouteAdmin', par, headers);
  }

}
