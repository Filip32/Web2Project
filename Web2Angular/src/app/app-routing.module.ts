import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { TimetableComponent } from './timetable/timetable.component';
import { PricelistComponent } from './pricelist/pricelist.component';
import { ProfilComponent } from './profil/profil.component';
import { TicketsChackComponent } from './tickets-chack/tickets-chack.component';
import { VerificationComponent } from './verification/verification.component';
import { TimetableAdminComponent } from './timetable-admin/timetable-admin.component';
import { RoutesStationsAdminComponent } from './routes-stations-admin/routes-stations-admin.component';
import { PricelistAdminComponent } from './pricelist-admin/pricelist-admin.component';

const routes: Routes = [

{path: '', component: HomeComponent},
{path: 'login', component: LoginComponent},
{path: 'register', component: RegisterComponent},
{path: 'timetable', component: TimetableComponent},
{path: 'pricelist', component: PricelistComponent},
{path: 'profil', component: ProfilComponent},
{path: 'tickets-c', component: TicketsChackComponent},
{path: 'verification-c', component: VerificationComponent},
{path: 'timetable-a', component: TimetableAdminComponent},
{path: 'routes-stations-a', component: RoutesStationsAdminComponent},
{path: 'priceist-a', component: PricelistAdminComponent},
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
