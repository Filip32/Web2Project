import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import {HTTP_INTERCEPTORS} from '@angular/common/http';
import { JwtInterceptor } from './jwt-interceptor';
//import { ValuesHttpService } from './services/http/values-http.service';
//import { AuthHttpService } from './services/http/auth-http.service';

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
import { AgmCoreModule } from '@agm/core';
import { MapComponent } from './map/map.component';
import { UploadPhotoComponent } from './upload-photo/upload-photo.component';
import { ControllerAddComponent } from './controller-add/controller-add.component';

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
    MapComponent,
    UploadPhotoComponent,
    ControllerAddComponent,
  ],
  imports: [
    BrowserModule,
    FormsModule,
    ReactiveFormsModule,
    AppRoutingModule,
    HttpClientModule,
    AgmCoreModule.forRoot({apiKey: 'AIzaSyDnihJyw_34z5S1KZXp90pfTGAqhFszNJk'})
  ],
  providers: [{provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true},
    
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
