import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-pricelist',
  templateUrl: './pricelist.component.html',
  styleUrls: ['./pricelist.component.css']
})
export class PricelistComponent implements OnInit {

  selectedRow: any;
  typeOfTicket: [{Ticket: "Time Ticket", Price: "50"}, {Ticket: "Daily Ticket", Price: "50"}];
  constructor() { }

  ngOnInit() {
  }

  RowSelect(u:any)
  {
    this.selectedRow = u;
  }
}
