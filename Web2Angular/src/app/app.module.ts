import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NavBarComponent } from './nav-bar/nav-bar.component';
import { HomeComponent } from './home/home.component';
import { RegisterComponent } from './register/register.component';
import { LoginComponent } from './login/login.component';
import { FormBuilder, ReactiveFormsModule, FormsModule } from '@angular/forms';
import { TimetableComponent } from './timetable/timetable.component';
import { PricelistComponent } from './pricelist/pricelist.component';
import { HttpClientModule} from '@angular/common/http';
import { ProfilComponent } from './profil/profil.component';
import { TicketsChackComponent } from './tickets-chack/tickets-chack.component';
import { VerificationComponent } from './verification/verification.component';
import { TimetableAdminComponent } from './timetable-admin/timetable-admin.component';
import { RoutesStationsAdminComponent } from './routes-stations-admin/routes-stations-admin.component';
import { PricelistAdminComponent } from './pricelist-admin/pricelist-admin.component';

@NgModule({
  declarations: [
    AppComponent,
    NavBarComponent,
    HomeComponent,
    RegisterComponent,
    LoginComponent,
    TimetableComponent,
    PricelistComponent,
    ProfilComponent,
    TicketsChackComponent,
    VerificationComponent,
    TimetableAdminComponent,
    RoutesStationsAdminComponent,
    PricelistAdminComponent,
  ],
  imports: [
    BrowserModule,
    FormsModule,
    ReactiveFormsModule,
    AppRoutingModule,
    HttpClientModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
