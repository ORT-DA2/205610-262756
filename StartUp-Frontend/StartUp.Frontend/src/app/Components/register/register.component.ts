import { Component, OnInit } from '@angular/core';
import { ChildActivationStart } from '@angular/router';
import { BehaviorSubject, firstValueFrom, Observable } from 'rxjs';
import { InvitationModel } from 'src/app/Models/invitationModel';
import { PharmacyModel } from 'src/app/Models/pharmacyModel';
import { RoleModel } from 'src/app/Models/roleModel';
import { InvitationSearchCriteria } from 'src/app/Models/SearchCriteria/invitationSearchCriteria';
import { UserModel } from 'src/app/Models/userModel';
import { InvitationService } from '../invitation/invitation.service';
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

    this.userService.postUser(userModel);
  }

  public isValidInvitation(): void {

    let invitation = this.invitationService.getInvitation(this.username, this.code);

    if (invitation != null) {
      this.showRegister.next(true);
      this.invitationObs = invitation;
      this.invitationObs.subscribe(inv => this.invitation = inv);

    } else {
      this.showRegister.next(false);
    }
  }
}
