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
import { FormsModule } from '@angular/forms';
import { UserComponent } from './Components/user/user.component';


@NgModule({
  declarations: [
    AppComponent,
    HeaderComponent,
    SidebarComponent,
    LoginComponent,
    SessionComponent,
    RegisterComponent,
    InvitationComponent,
    UserComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
