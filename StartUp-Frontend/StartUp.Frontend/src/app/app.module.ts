import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HeaderComponent } from './Components/header/header.component';
import { SidebarComponent } from './Components/sidebar/sidebar.component';
import { LoginComponent } from './Components/login/login.component';
import { SessionComponent } from './Components/session/session.component';
import { RegisterComponent } from './Components/register/register.component';
import { InvitationComponent } from './Components/invitation/invitation.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { UserComponent } from './Components/user/user.component';
import { AuthGuard } from './auth/auth.guard';
import { AuthService } from './auth/auth.service';
import { RoleComponent } from './Components/role/role.component';
import { PharmacyComponent } from './Components/pharmacy/pharmacy.component';
import { MedicineComponent } from './Components/medicine/medicine.component';


@NgModule({
  declarations: [
    AppComponent,
    HeaderComponent,
    SidebarComponent,
    LoginComponent,
    SessionComponent,
    RegisterComponent,
    InvitationComponent,
    UserComponent,
    RoleComponent,
    PharmacyComponent,
    MedicineComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule
  ],
  providers: [AuthService, AuthGuard],
  bootstrap: [AppComponent]
})
export class AppModule { }
