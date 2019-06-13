import { Component, OnInit } from '@angular/core';
import { ServerConnectionService } from '../server-connection.service';

@Component({
  selector: 'app-verification',
  templateUrl: './verification.component.html',
  styleUrls: ['./verification.component.css']
})
export class VerificationComponent implements OnInit {

  tableData: any[] = [];
  selectedRow: any;
  slikaBool: boolean = false;
  photoo:any;
  idd: string;
  
  constructor(private serverConnectionService: ServerConnectionService) { }

  ngOnInit() {
    this.getUsers();
  }

  getSlika(email:string)
  {
    this.serverConnectionService.getPhoto(email).subscribe(
      (res) => {
        this.photoo = 'data:image/png;base64,' + res;
        this.slikaBool = true;
      },
      (err) => {
        console.error(err);
      }
    );
  
  }

  getUsers()
  {
    this.serverConnectionService.getUsers().subscribe(
      (res) => {
        let i: string = res;
        this.tableData = res;
      }
    );
  }

  ApproveUser(u)
  {
    this.serverConnectionService.ApproveUser(u).subscribe(
      (res) => {
        this.getUsers();
      });
  }

  DenyUser(u)
  {
    this.serverConnectionService.DenyUser(u).subscribe(
      (res) => {
        this.getUsers();
      });
  }

  RowSelect(u:any)
  {
    this.selectedRow = u;
  }

}
