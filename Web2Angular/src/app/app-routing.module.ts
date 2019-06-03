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
import { AdminGuard } from './admin.guard';
import { ControllerGuard } from './controller.guard';
import { AppUserGuard } from './app-user.guard';
import { NegAuthGuard } from './neg-auth.guard';

const routes: Routes = [

{path: '', component: HomeComponent},

{path: 'timetable', component: TimetableComponent},
{path: 'pricelist', component: PricelistComponent},

{path: 'login', component: LoginComponent,
                canActivate: [NegAuthGuard]},
{path: 'register', component: RegisterComponent,
                   canActivate: [NegAuthGuard]},

{path: 'profil', component: ProfilComponent, 
                 canActivate: [AuthGuard, AppUserGuard]},

{path: 'tickets-c', component: TicketsChackComponent, 
                    canActivate: [AuthGuard, ControllerGuard]},
{path: 'verification-c', component: VerificationComponent, 
                         canActivate: [AuthGuard, ControllerGuard]},

{path: 'timetable-a', component: TimetableAdminComponent, 
                      canActivate: [AuthGuard, AdminGuard]},
{path: 'routes-stations-a', component: RoutesStationsAdminComponent, 
                            canActivate: [AuthGuard, AdminGuard]},
{path: 'priceist-a', component: PricelistAdminComponent, 
                     canActivate: [AuthGuard, AdminGuard]},
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
