import { Component, OnInit } from '@angular/core';
import { ServerConnectionService } from '../server-connection.service';
import { Router } from '@angular/router';
import { FormBuilder, Validators } from '@angular/forms';

@Component({
  selector: 'app-timetable-admin',
  templateUrl: './timetable-admin.component.html',
  styleUrls: ['./timetable-admin.component.css']
})
export class TimetableAdminComponent implements OnInit {

  routes: any;
  routeNumber: any;
  departures: any;

  view: boolean = false;
  izmeni: boolean = false;
  obrisi: boolean = false;
  novaLinija: boolean = true;

  message: any;
  idlinije: number;

  tipDana: any;
  selectedtipDana = null;
  tipRedVoznje: any;
  selectedTipRedVoznje = null;

  polasciForm = this.fb.group({
    svipolasci: [""]
  });

  naslovForm = this.fb.group({
    naslov: [""]
  });

  danForm = this.fb.group({
    dan: [""]
  });

  novaLinijaForm = this.fb.group({
    naslovNew: ['', Validators.required],
    danNew: [""],
    redvoznje: ["", Validators.required],
    svipolasciNew: ["", Validators.required]
  });

  constructor(private fb: FormBuilder, private serverConnectionService: ServerConnectionService, private router: Router) { }

  ngOnInit() {
    this.getRoutesAdmin();
  }

  View() {
    return this.view;
  }

  getRoutesAdmin() {
    this.serverConnectionService.getRoutesAdmin().subscribe(
      (res) => {
        this.routes = res;
      },
      (err) => {
        console.error(err);
      }
    );
  }

  openRote(Id: number, RouteNumber: string): void {
    this.serverConnectionService.getRouteListAdmin(Id).subscribe(
      (res) => {
        this.message = "";
        this.departures = res;
        this.routeNumber = RouteNumber;
        this.view = true;
        this.izmeni = false;
        this.obrisi = false;
      });
  }

  changeRoute(Id: number, RouteNumber: string): void {
    this.serverConnectionService.getRouteAdmin(Id).subscribe(
      (res) => {
        this.message = "";
        this.departures = res;
        this.routeNumber = RouteNumber
        this.view = false;
        this.izmeni = true;
        this.obrisi = false;
        this.polasciForm['svipolasci'] = res;
        this.naslovForm['naslov'] = RouteNumber;
        this.idlinije = Id;
        this.getTypeOfDay();
      });
  }

  getTypeOfDay() {
    this.serverConnectionService.getTypeOfDay().subscribe(
      (res) => {
        this.tipDana = res;
        this.selectedtipDana = this.tipDana[0];
        console.log(res);
      },
      (err) => {
        console.error(err);
      }
    );
  }

  onSubmitRouteNumber() {
    this.serverConnectionService.changeRouteNumberAdmin(this.routeNumber, this.idlinije).subscribe(
      (res) => {
        this.message = res;
        this.getRoutesAdmin();
      },
      (err) => {
        console.error(err);
      }
    );
  }

  onSubmitDay() {
    this.serverConnectionService.chageDayRouteAdmin(this.selectedtipDana, this.idlinije).subscribe(
      (res) => {
        this.message = res;
        this.getRoutesAdmin();
      },
      (err) => {
        console.error(err);
      }
    );
  }

  onSubmit() {
    this.serverConnectionService.changeDepAdmin(this.polasciForm.value, this.idlinije).subscribe(
      (res) => {
        this.message = res;
      },
      (err) => {
        console.error(err);
      }
    );
  }

  deleteRouteAdmin(Id: number): void {
    this.message = "";
    this.serverConnectionService.deleteRouteAdmin(Id).subscribe(
      (res) => {
        this.message = res;
        this.view = false;
        this.izmeni = false;
        this.obrisi = true;
        this.getRoutesAdmin();
      });
    }

    addNewRoute() {
      this.message = "";
      this.novaLinija = false;
      this.getTypeOfTimetable();
      this.getTypeOfDay();
      this.novaLinijaForm['naslovNew'] = '';
      this.novaLinijaForm['svipolasciNew'] = "";
      this.view = false;
      this.izmeni = false;
      this.obrisi = false;
    }

    getTypeOfTimetable() {
      this.serverConnectionService.getTypeOfTimetable().subscribe(
        (res) => {
          this.tipRedVoznje = res;
          this.selectedTipRedVoznje = this.tipRedVoznje[0];
        },
        (err) => {
          console.error(err);
        }
      );
    }

    addRoute() {
      if (this.novaLinijaForm.valid) {
        this.serverConnectionService.addNewRouteAdmin(this.novaLinijaForm.value).subscribe(
          (res) => {
            this.obrisi = true;
            this.message = res;
            this.novaLinija = true;
            this.getRoutesAdmin();
            if (res === "New route added.") {
  
              setTimeout(() => {
                this.router.navigate(['/red-voznje-admin']);
              },
                2000);
            }
          }
        );
      }
      else {
        Object.keys(this.novaLinijaForm.controls).forEach(field => {
          const control = this.novaLinijaForm.get(field);
          control.markAsTouched({ onlySelf: true });
        });
      }
  
    }

}
