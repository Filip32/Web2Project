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
import { AuthGuard } from './auth.guard';

const routes: Routes = [

{path: '', component: HomeComponent},
{path: 'login', component: LoginComponent},
{path: 'register', component: RegisterComponent},
{path: 'timetable', component: TimetableComponent},
{path: 'pricelist', component: PricelistComponent},
{path: 'profil', component: ProfilComponent, 
                 canActivate: [AuthGuard]},
{path: 'tickets-c', component: TicketsChackComponent, 
                    canActivate: [AuthGuard]},
{path: 'verification-c', component: VerificationComponent, 
                         canActivate: [AuthGuard]},
{path: 'timetable-a', component: TimetableAdminComponent, 
                      canActivate: [AuthGuard]},
{path: 'routes-stations-a', component: RoutesStationsAdminComponent, 
                            canActivate: [AuthGuard]},
{path: 'priceist-a', component: PricelistAdminComponent, 
                     canActivate: [AuthGuard]},
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
