import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators, FormControl } from '@angular/forms';
import { catchError, pipe } from 'rxjs';
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
  selectedRole: any = null;
  selectedPharmacy: any = null;
  selectedToEdit: any;
  invitationEdited: any;
  code: any;
  successfulResponse: boolean = false;
  successfulResponseMessage: string;
  errorResponse: boolean = false;
  errorResponseMessage: string;
  formCreateInvitation: FormGroup;
  formEditInvitation: FormGroup;


  constructor(private fb: FormBuilder, private roleService: RoleService,
    private pharmacyService: PharmacyService, private invitationService: InvitationService) {
    this.getAllRoles();
    this.getAllPharmacies();
    this.getAllInvitations();
  }

  ngOnInit(): void {
    this.createFormInvitation();
    this.editFormInvitation();
  }

  createFormInvitation() {
    this.formCreateInvitation = new FormGroup(
      {
        userName: new FormControl(null, [
          Validators.maxLength(50),
          Validators.required
        ]),
        role: new FormControl(null, [
          Validators.required,
        ]),
        pharmacy: new FormControl(null, [])
      }
    );

    this.formCreateInvitation.get('userName')?.valueChanges.subscribe((change) => {
      this.username = change;
      console.log(change);
    });
    this.formCreateInvitation.get('role')?.valueChanges.subscribe((change) => {
      this.selectedRole = change;
      this.validateAdminWithNoPharma(change);
      console.log(change);
    });
    this.formCreateInvitation.get('pharmacy')?.valueChanges.subscribe((change) => {
      this.selectedPharmacy = change;
      console.log(change);
    });
  }

  editFormInvitation() {
    this.formEditInvitation = new FormGroup(
      {
        userName: new FormControl('', [
          Validators.maxLength(50),
          Validators.required
        ]),
        role: new FormControl('', [
          Validators.required,
        ]),
        pharmacy: new FormControl('', []),
        code: new FormControl('', [
          Validators.required, Validators.min(100000), Validators.max(999999)
        ])
      }
    );

    this.formEditInvitation.get('userName')?.valueChanges.subscribe((change) => {
      this.username = change;
      console.log(change);
    });
    this.formEditInvitation.get('role')?.valueChanges.subscribe((change) => {
      this.selectedRole = change;
      this.validateAdminWithNoPharma(change);
      console.log(change);
    });
    this.formEditInvitation.get('pharmacy')?.valueChanges.subscribe((change) => {
      this.selectedPharmacy = change;
      console.log(change);
    });
  }

  validateAdminWithNoPharma(role: string) {
    console.log("hi");
    if (role != 'administrator') {
      console.log("here");
      this.formCreateInvitation.controls['pharmacy'].addValidators(Validators.required);
    } else {
      console.log("oopps");
      this.formCreateInvitation.controls['pharmacy'].removeValidators(Validators.required);
    }
  }

  onRoleChecked(value: Event) {
    console.log("hi");
    this.Roles[0].forEach((role: any) => {
      if (role.permission == (value.target as HTMLInputElement).value) {
        this.selectedRole = (value.target as HTMLInputElement).value;
        console.log("hi");
      }
    });
  }

  onPharmacieChecked(value: Event) {
    this.Pharmacies[0].forEach((ph: any) => {
      if (ph.name == (value.target as HTMLInputElement).value) {
        this.selectedPharmacy = (value.target as HTMLInputElement).value;
        console.log(this.selectedPharmacy);
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
      let pharmacy = this.Pharmacies[0].find((ph: any) => ph.name == this.selectedPharmacy);
      invModel.pharmacy = pharmacy;
    }

    this.invitationService.postInvitation(invModel)
      .subscribe(
        data => {
          console.log(data);
          this.Invitations.push(data);
          this.successfulResponse = true;
          this.successfulResponseMessage = `An invitation for ${invModel.username} was created with the code ${data.code}`
          this.errorResponse = false;
        },
        error => {
          this.errorResponseMessage = error.error;
          if (this.errorResponseMessage != null) {
            this.errorResponse = true;
          }
        }
      );

    this.getAllInvitations();
  }

  generateNewCode() {
    this.invitationService.generateNewCode().subscribe(
      (newCode: any) => {
        this.code = newCode;
      }
    );
  }

  changeSelectedInvitation(inv: any) {
    this.Invitations[0].forEach((invit: any) => {
      if (invit.code == inv.code) {
        this.selectedToEdit = invit;
      }
    });

    this.formEditInvitation.controls['userName'].setValue(inv.userName);
    this.formEditInvitation.controls['role'].setValue(inv.rol);
    this.selectedRole = inv.rol;
    this.formEditInvitation.controls['code'].setValue(inv.code);
    this.code = inv.code;
    if (inv.rol != 'administrator') {
      this.formEditInvitation.controls['pharmacy'].setValue(inv.pharmacy.name);
      this.selectedPharmacy = inv.pharmacy.name;
    }
  }

  editInvitation() {
    this.selectedToEdit.userName = this.username;
    this.selectedToEdit.rol = this.selectedRole;
    this.selectedToEdit.code = this.code;
    if (this.selectedRole != 'administrator') {
      this.selectedToEdit.pharmacy.id = 0;
      this.selectedToEdit.pharmacy.name = this.selectedPharmacy;
    }
    this.invitationService.updateInvitation(this.selectedToEdit.id, this.selectedToEdit).subscribe(invit => {
      console.log(invit);
    });
  }

  requiredPharmacy(formGroup: FormGroup) {
    if (formGroup.controls['role'].value == 'owner' || formGroup.controls['role'].value == 'employee') {
      formGroup.controls['pharmacy'].addValidators([Validators.required]);
    }
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
      this.Invitations.push(inv);
    });
  }
}
