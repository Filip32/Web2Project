import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-pricelist-admin',
  templateUrl: './pricelist-admin.component.html',
  styleUrls: ['./pricelist-admin.component.css']
})
export class PricelistAdminComponent implements OnInit {

  selectedRow: any;
  constructor() { }

  ngOnInit() {
  }
  
  RowSelect(u:any)
  {
    this.selectedRow = u;
  }

}
