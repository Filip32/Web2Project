import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { ServerConnectionService } from '../server-connection.service';
import { PricelistHelp } from '../Models/pricelist-help';

@Component({
  selector: 'app-pricelist-admin',
  templateUrl: './pricelist-admin.component.html',
  styleUrls: ['./pricelist-admin.component.css']
})
export class PricelistAdminComponent implements OnInit {

  pricelists: any;
  pricelist: PricelistHelp;
  message: string = "";
  pricelistId: number;

  view: boolean = false;
  change: boolean = false;
  newPricelist: boolean = true;
  changeDate: boolean = false;

  newPricelistForm = this.fb.group({
    from: ['', Validators.required],
    to: ['', Validators.required],
    timely: ['', Validators.required],
    daily: ['', Validators.required],
    monthly: ['', Validators.required],
    yearly: ['', Validators.required]
  });
  
  constructor(private fb: FormBuilder, private serverConnectionService: ServerConnectionService) { }

  get f() { return this.newPricelistForm.controls; }

  ngOnInit() {
    this.getPriceist();
  }
  
  getPriceist() {
    this.serverConnectionService.getPricelistsAdmin().subscribe(
      (res) => {
        this.pricelists = res;
      },
      (err) => {
        console.error(err);
      }
    );
  }

  openPricelist(Id: number): void {
    this.serverConnectionService.getPricelistAdmin(Id).subscribe(
      (res) => {
        this.pricelist = res;
        this.newPricelistForm.controls['from'].setValue(res["FromDate"]);
        this.newPricelistForm.controls['to'].setValue(res["ToDate"]);
        this.newPricelistForm.controls['timely'].setValue(+res["TimePrice"]);
        this.newPricelistForm.controls['daily'].setValue(+res["DailyPrice"]);
        this.newPricelistForm.controls['monthly'].setValue(+res["MonthlyPrice"]);
        this.newPricelistForm.controls['yearly'].setValue(+res["YearlyPrice"]);
        this.message = "";
        this.view = true;
        this.newPricelist = true;
        this.change = false;
        this.changeDate = false;
      });
  }

  changePricelist(Id: number): void {
    this.serverConnectionService.getPricelistAdminChange(Id).subscribe(
      (res) => {
        if (res.Change == true) {
          this.pricelistId = Id;
          this.changeDate = true;
          this.newPricelist = true;
          this.change = true;
          this.view = true;
          this.newPricelistForm.controls['from'].setValue(res["FromDate"]);
          this.newPricelistForm.controls['to'].setValue(res["ToDate"]);
          this.newPricelistForm.controls['timely'].setValue(+res["TimePrice"]);
          this.newPricelistForm.controls['daily'].setValue(+res["DailyPrice"]);
          this.newPricelistForm.controls['monthly'].setValue(+res["MonthlyPrice"]);
          this.newPricelistForm.controls['yearly'].setValue(+res["YearlyPrice"]);
        }
        else
        {
            this.message = "This pricelist has expired, you can not change it.";
            this.change = false;
          this.changeDate = false;
          this.view = false;
          this.newPricelist = true;
        }
      });
  }

  addNewPricelist() {
    this.message = "";
    this.newPricelist = false;
    this.view = false;
    this.change = false;
    this.changeDate = true;

    this.newPricelistForm = this.fb.group({
      from: ['', Validators.required],
      to: ['', Validators.required],
      timely: ['', [Validators.required, Validators.pattern("[0-9]*")]],
      daily: ['', [Validators.required, Validators.pattern("[0-9]*")]],
      monthly: ['', [Validators.required, Validators.pattern("[0-9]*")]],
      yearly: ['', [Validators.required, Validators.pattern("[0-9]*")]]
    });
  }

  onSubmit() {
    if (this.newPricelistForm.valid) {
      if (!this.change) {
        this.serverConnectionService.addPricelist(this.newPricelistForm.value).subscribe(
          (res) => {
            this.message = res;
            this.getPriceist();

            setTimeout(() => {
              this.message = "";
              this.view = false;
              this.newPricelist = true;
              this.change = false;
              this.changeDate = false;
            }, 2000);
          }
        )
      }
      else {
        console.log("sss");
        this.serverConnectionService.changePricelist(this.newPricelistForm.value, this.pricelistId).subscribe(
          (res) => {
            this.message = res;
            this.getPriceist();

            setTimeout(() => {
              this.message = "";
              this.view = false;
              this.newPricelist = true;
              this.change = false;
              this.changeDate = false;
            }, 2000);
          }
        )
      }
    }
    else {
      Object.keys(this.newPricelistForm.controls).forEach(field => {
        const control = this.newPricelistForm.get(field);
        control.markAsTouched({ onlySelf: true });
      });
    }
  }
}
