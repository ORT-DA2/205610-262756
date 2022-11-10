import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { InvitationComponent } from './Components/invitation/invitation.component';
import { LoginComponent } from './Components/login/login.component';
import { PharmacyComponent } from './Components/pharmacy/pharmacy.component';
import { RegisterComponent } from './Components/register/register.component';

const routes: Routes = [
  { path: 'login', component: LoginComponent },
  { path: 'register', component: RegisterComponent },
  { path: 'invitation', component: InvitationComponent },
  { path: 'pharmacy', component: PharmacyComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
