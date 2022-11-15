import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { InvitationComponent } from './Components/user/invitation/invitation.component';
import { LoginComponent } from './Components/session/login/login.component';
import { MedicineComponent } from './Components/medicine/medicine.component';
import { PetitionComponent } from './Components/request/petition/petition.component';
import { PharmacyComponent } from './Components/pharmacy/pharmacy.component';
import { RegisterComponent } from './Components/register/register.component';
import { RequestComponent } from './Components/request/request.component';
import { RequestOwnerComponent } from './Components/request/request-owner/request-owner.component';

const routes: Routes = [
  { path: 'login', component: LoginComponent },
  { path: 'register', component: RegisterComponent },
  { path: 'invitation', component: InvitationComponent },
  { path: 'pharmacy', component: PharmacyComponent },
  { path: 'createMedicine', component: MedicineComponent },
  { path: 'createPetition', component: PetitionComponent },
  { path: 'createRequest', component: RequestComponent },
  { path: 'ViewRequest', component: RequestOwnerComponent }

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
