import { Component, NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { InvitationComponent } from './Components/user/invitation/invitation.component';
import { LoginComponent } from './Components/session/login/login.component';
import { MedicineComponent } from './Components/medicine/medicine.component';
import { PetitionComponent } from './Components/request/petition/petition.component';
import { PharmacyComponent } from './Components/pharmacy/pharmacy.component';
import { RegisterComponent } from './Components/register/register.component';
import { RequestComponent } from './Components/request/request.component';
import { RequestOwnerComponent } from './Components/request/request-owner/request-owner.component';
import { SaleComponent } from './Components/sale/sale.component';
import { SaleEmployeeComponent } from './Components/sale/sale-employee/sale-employee.component';
import { ExportersComponent } from './Components/exporters/exporters.component';
import { AuthGuard } from './auth/auth.guard';
import { AuthGuardEmployee } from './auth/auth-employee';
import { AuthGuardOwner } from './auth/auth-owner';

const routes: Routes = [
  { path: '', },
  { path: 'login', component: LoginComponent },
  { path: 'register', component: RegisterComponent },
  { path: 'Shop', component: SaleComponent },
  { path: 'invitation', component: InvitationComponent, canActivate: [AuthGuard] },
  { path: 'pharmacy', component: PharmacyComponent, canActivate: [AuthGuard] },
  { path: 'createMedicine', component: MedicineComponent, canActivate: [AuthGuardEmployee] },
  { path: 'createPetition', component: PetitionComponent, canActivate: [AuthGuardEmployee] },
  { path: 'createRequest', component: RequestComponent, canActivate: [AuthGuardEmployee] },
  { path: 'ViewRequest', component: RequestOwnerComponent, canActivate: [AuthGuardOwner] },
  { path: 'sales', component: SaleEmployeeComponent, canActivate: [AuthGuardEmployee] },
  { path: 'exportMedicines', component: ExportersComponent, canActivate: [AuthGuardEmployee] }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
