import { Component, OnInit } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { RoleModel } from 'src/app/Models/roleModel';
import { UserModel } from 'src/app/Models/userModel';
import { InvitationService } from '../user/invitation/invitation.service';
import { UserService } from '../user/user.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  public username: string = "";
  public code: number = 0;
  public email: string = "";
  public password: string = "";
  public address: string = "";
  public invitation: any;
  public invitationObs: Observable<any> = new Observable<any>();
  public showRegister: BehaviorSubject<boolean> = new BehaviorSubject<boolean>(false);
  successfulResponse: boolean = false;
  successfulResponseMessage: string;
  errorResponse: boolean = false;
  errorResponseMessage: string;

  constructor(private invitationService: InvitationService, private userService: UserService) {
  }

  ngOnInit(): void {
  }

  public register() {

    let userModel = new UserModel();

    userModel.email = this.email;
    userModel.password = this.password;
    userModel.address = this.address;
    userModel.invitation = this.invitation;
    userModel.pharmacy = this.invitation.pharmacy;
    userModel.roles = new RoleModel();
    userModel.roles.permission = this.invitation.rol;

    this.userService.postUser(userModel).subscribe(
      data => {
        this.successfulResponse = true;
        this.successfulResponseMessage = `User created successfully`
        this.errorResponse = false;
      },
      error => {
        this.errorResponseMessage = error.error;
        if (this.errorResponseMessage != null) {
          this.errorResponse = true;
          this.successfulResponse = false;
        }
      }
    );
  }

  public isValidInvitation(): void {

    this.invitationService.getInvitation(this.username, this.code).subscribe(
      data => {
        this.invitation = data
        this.showRegister.next(true);
        this.successfulResponse = true;
        this.successfulResponseMessage = `Invitation authenticated successfully`
        this.errorResponse = false;
      },
      error => {
        this.errorResponseMessage = error.error;
        if (this.errorResponseMessage != null) {
          this.errorResponse = true;
          this.successfulResponse = false;
        }
        this.showRegister.next(false);
      }
    );
  }
}
