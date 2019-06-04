import { Component, OnInit } from '@angular/core';
import { ServerConnectionService } from '../server-connection.service';

@Component({
  selector: 'app-pricelist',
  templateUrl: './pricelist.component.html',
  styleUrls: ['./pricelist.component.css']
})
export class PricelistComponent implements OnInit {

  selectedRow: any;
  tableData: any[] = [];
  coefficients: any;
  selectedPrice: any;
  totalPrice: any;
  typeOfUser: any;
  canBuy: boolean = false;
  isSelectedTicket: boolean = false;

  constructor(private serverConnectionService: ServerConnectionService) { }

  ngOnInit() {
    this.getPricelist();
    this.getCoefficeints();
  }

  selectChangeHandler (event: any) {
    //update the ui
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

  getCoefficeints()
  {
    this.serverConnectionService.getCoefficient().subscribe(
      (res) => {
        this.coefficients = res;
      }
    );
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
        this.canBuy = false;
        this.isSelectedTicket = false;

        this.serverConnectionService.buyTicket(this.typeOfUser, this.selectedRow.type, this.totalPrice).subscribe(
          (res) => {
            console.log(res);
          }
        );
      }
      else
      {
        this.isSelectedTicket = true;
      }
    }
    else
    {
      this.canBuy = true;
    }
  }
}
