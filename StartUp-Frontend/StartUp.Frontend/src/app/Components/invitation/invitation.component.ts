import { Component, OnInit } from '@angular/core';
import { InvitationModel } from 'src/app/Models/invitationModel';
import { PharmacyModel } from 'src/app/Models/pharmacyModel';
import { PharmacyService } from '../pharmacy/pharmacy.service';
import { RoleService } from '../role/role.service';
import { InvitationService } from './invitation.service';

@Component({
  selector: 'app-invitation',
  templateUrl: './invitation.component.html',
  styleUrls: ['./invitation.component.css']
})
export class InvitationComponent implements OnInit {

  Roles: Array<any> = [];
  Pharmacies: Array<any> = [];
  Invitations: Array<any> = [];
  username: string = "";
  selectedRole: any;
  selectedPharmacy: any = null;
  constructor(private roleService: RoleService, private pharmacyService: PharmacyService, private invitationService: InvitationService) {
    this.getAllRoles();
    this.getAllPharmacies();
    this.getAllInvitations();
  }

  ngOnInit(): void {
  }

  onRoleChecked(value: Event) {
    this.selectedRole = (value.target as HTMLInputElement).value;
    console.log(this.selectedRole);
  }

  onPharmacieChecked(value: Event) {
    this.Pharmacies[0].forEach((ph: any) => {
      if (ph.id == (value.target as HTMLInputElement).value) {
        this.selectedPharmacy = ph;
      }
    });
  }

  createInvitation() {
    let invModel = new InvitationModel();

    invModel.username = this.username;
    invModel.rol = this.selectedRole;
    invModel.pharmacy = null;
    if (this.selectedPharmacy != null && this.selectedRole != 'administrator') {
      invModel.pharmacy = new PharmacyModel();
      invModel.pharmacy.name = this.selectedPharmacy.name;
    }

    var invitationCreated = this.invitationService.postInvitation(invModel);

    invitationCreated.subscribe(i => this.Invitations.push(i));
    console.log(this.Invitations);
  }

  private getAllRoles() {
    this.roleService.getRoles().forEach(r => {
      this.Roles.push(r);
    });
  }

  private getAllPharmacies() {
    this.pharmacyService.getPharmacies().forEach(ph => {
      this.Pharmacies.push(ph);
    });
  }

  private getAllInvitations() {
    this.invitationService.getAllInvitations().forEach(inv => {
      this.Pharmacies.push(inv);
    });
  }
}
