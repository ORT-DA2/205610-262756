import { Component, OnInit } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
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
  public invitationObs: Observable<any> = new Observable<any>();
  public invitation: any;
  public showRegister: BehaviorSubject<boolean> = new BehaviorSubject<boolean>(false);
  private role: {} = {};
  private pharmacy: PharmacyModel = new PharmacyModel;


  constructor(private invitationService: InvitationService, private userService: UserService) {
  }

  ngOnInit(): void {
  }

  public register() {
    let userModel = new UserModel();

    userModel.email = this.email;
    userModel.password = this.password;
    userModel.address = this.address;
    this.invitationObs.forEach(inv => userModel.invitation = inv);
    this.invitationObs.forEach(inv => userModel.roles = inv.rol);
    this.invitationObs.forEach(inv => userModel.pharmacy = inv.pharmacy);

    this.userService.postUser(userModel);
  }

  public isValidInvitation(): void {

    let invitation = this.invitationService.getInvitation(this.username, this.code);

    if (invitation != null) {
      this.showRegister.next(true);
      this.invitationObs = invitation;

    } else {
      this.showRegister.next(false);
    }
  }
}
