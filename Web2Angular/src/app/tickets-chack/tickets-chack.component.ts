import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { Validators } from '@angular/forms';
import { ServerConnectionService } from '../server-connection.service';
import {HttpClient, HttpHeaders} from '@angular/common/http'
import { Router } from '@angular/router';

@Component({
  selector: 'app-tickets-chack',
  templateUrl: './tickets-chack.component.html',
  styleUrls: ['./tickets-chack.component.css']
})
export class TicketsChackComponent implements OnInit {
  serverSuccessMessage = "";
  ticketsCheckForm:FormGroup;
  constructor(private fb: FormBuilder,private ServerConnectionService: ServerConnectionService, private http: HttpClient, private router: Router) { }

  ngOnInit() {
    this.ticketsCheckForm = this.fb.group({
      ticket_id: ['',Validators.required]
    });
  }
  get f() { return this.ticketsCheckForm.controls; }

  onSubmit() {
    let poruka = this.ServerConnectionService.getTicket(this.ticketsCheckForm.value).subscribe(
      (res) => {
        this.serverSuccessMessage = res;
      }
    );
    }

  

}

