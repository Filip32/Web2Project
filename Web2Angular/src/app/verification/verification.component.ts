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
  
  constructor(private serverConnectionService: ServerConnectionService) { }

  ngOnInit() {
    this.getUsers();
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
