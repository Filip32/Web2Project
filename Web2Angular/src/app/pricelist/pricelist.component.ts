import { Component, OnInit } from '@angular/core';
import { ServerConnectionService } from '../server-connection.service';

@Component({
  selector: 'app-pricelist',
  templateUrl: './pricelist.component.html',
  styleUrls: ['./pricelist.component.css']
})
export class PricelistComponent implements OnInit {

  selectedRow: any;
  purchasedTickets: any;
  textMessage: string = "";
  tableData: any[] = [];
  coefficients: any;
  selectedPrice: any;
  totalPrice: any;
  typeOfUser: any = "Regular";
  typeOfLoginUser: any;

  constructor(private serverConnectionService: ServerConnectionService) { }

  ngOnInit() {
    this.getPricelist();
    this.getCoefficeints();
    if(localStorage.login){
        this.getTypeOfLoginUser();
        this.getTickets();
    }
  }

  isLogin(): boolean
  {
    if(localStorage.login)
    {
      return true;
    }
    return false;
  }

  selectChangeHandler (event: any) {
    this.typeOfUser = event.target.value;
    this.totalPrice = this.selectedPrice;
    this.calculatePrice();
  }

  RowSelect(u:any)
  {
    this.selectedRow = u;
    this.selectedPrice = u.price;
    this.totalPrice = u.price;
    this.calculatePrice();
  }

  getPricelist()
  {
    this.serverConnectionService.getPricelist().subscribe(
      (res) => {
        let i: string = res;
        let j: any = JSON.parse(i);
        this.tableData = j;
      }
    );
  }

getTickets()
{
  this.serverConnectionService.GetTickets().subscribe(
    (res) => {
      this.purchasedTickets = res;
      console.log(res);
    }
  );
}

  getTypeOfLoginUser()
  {
    this.serverConnectionService.getTypeOfLoginUser().subscribe(
      (res) => {
        let i: string = res;
        let j: any = JSON.parse(i);
        this.typeOfLoginUser = j;
      }
    );
  }

  getCoefficeints()
  {
    this.serverConnectionService.getCoefficient().subscribe(
      (res) => {
        this.coefficients = res;
      }
    );
  }
  deleteTicket(u)
  {
    this.serverConnectionService.DeleteTicket(u).subscribe(
      (res) => {
        this.getTickets();
      });
  }

  calculatePrice()
  {
    if(this.totalPrice > 0){
        if(this.typeOfUser == "Pensioner")
        {
          this.totalPrice = this.totalPrice*this.coefficients.CoefficientPensioner;
        }
        else if(this.typeOfUser == "Student")
        {
          this.totalPrice = this.totalPrice*this.coefficients.CoefficientStudent;
        }
    }
  }

  onSubmit() {
    if(localStorage.login)
    {
      if(this.totalPrice > 0){
        if(this.typeOfUser.toLowerCase() == this.typeOfLoginUser.TypeOfUser.toLowerCase()){
            if(this.typeOfLoginUser.IsValid == "ACCEPTED"){
                this.textMessage = "";

                this.serverConnectionService.buyTicket(this.typeOfUser, this.selectedRow.type, this.totalPrice).subscribe(
                  (res) => {
                    console.log(res);
                  }
                );

                this.getTickets();
            }
            else
            {
              this.textMessage = "You are not verified";
            }
        }else{
          this.textMessage = "You can't take this discount";
        }
      }
      else
      {
        this.textMessage = "First select a ticket.";
      }
    }
    else
    {
      this.textMessage = "To purchase a ticket, first login or register.";
    }
  }
}
