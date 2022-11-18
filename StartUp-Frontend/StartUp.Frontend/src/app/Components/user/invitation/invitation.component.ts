import { Component, OnInit } from '@angular/core';
import { FormGroup, Validators, FormControl } from '@angular/forms';
import { InvitationModel } from 'src/app/Models/invitationModel';
import { PharmacyModel } from 'src/app/Models/pharmacyModel';
import { PharmacyService } from '../../pharmacy/pharmacy.service';
import { RoleService } from '../../role/role.service';
import { InvitationService } from './invitation.service';

@Component({
  selector: 'app-invitation',
  templateUrl: './invitation.component.html',
  styleUrls: ['./invitation.component.css']
})
export class InvitationComponent implements OnInit {

  Roles: Array<any> = [];
  Pharmacies: Array<any> = [];
  Invitations: any[] = [];
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

  constructor(private roleService: RoleService,
    private pharmacyService: PharmacyService, private invitationService: InvitationService) {
    this.getAllRoles();
    this.getAllPharmacies();
    this.invitationService.getAllInvitations().forEach(inv => {
      this.Invitations.push(inv)
    });
  }

  ngOnInit(): void {
    this.createFormInvitation();
    this.editFormInvitation();
  }

  createFormInvitation() {
    this.formCreateInvitation = new FormGroup(
      {
        userName: new FormControl(null, [
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
    });
    this.formCreateInvitation.get('role')?.valueChanges.subscribe((change) => {
      this.selectedRole = change;
      this.validateAdminWithNoPharma(change);
    });
    this.formCreateInvitation.get('pharmacy')?.valueChanges.subscribe((change) => {
      this.selectedPharmacy = change;
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
        pharmacy: new FormControl('', [])
      }
    );

    this.formEditInvitation.get('userName')?.valueChanges.subscribe((change) => {
      this.username = change;
    });
    this.formEditInvitation.get('role')?.valueChanges.subscribe((change) => {
      this.selectedRole = change;
      this.validateAdminWithNoPharma(change);
    });
    this.formEditInvitation.get('pharmacy')?.valueChanges.subscribe((change) => {
      this.selectedPharmacy = change;
    });
  }

  validateAdminWithNoPharma(role: string) {
    if (role != 'administrator') {
      this.formCreateInvitation.controls['pharmacy'].addValidators(Validators.required);
    } else {
      this.formCreateInvitation.controls['pharmacy'].removeValidators(Validators.required);
    }
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
          this.Invitations[0].push(data);
          this.successfulResponse = true;
          this.successfulResponseMessage = `An invitation for ${invModel.username} was created with the code ${data.code}`
          this.errorResponse = false;
          this.formCreateInvitation.reset();
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

  generateNewCode() {
    this.invitationService.generateNewCode().subscribe(
      (newCode: any) => {
        this.code = newCode;
      }
    );
  }

  changeSelectedInvitation(inv: any) {
    this.selectedToEdit = inv

    this.formEditInvitation.controls['userName'].setValue(inv.userName);
    this.formEditInvitation.controls['role'].setValue(inv.rol);
    this.selectedRole = inv.rol;
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
    if (this.selectedToEdit.rol != 'administrator') {
      this.selectedToEdit.pharmacy.id = 0;
      this.selectedToEdit.pharmacy.name = this.selectedPharmacy;
    } else {
      this.selectedToEdit.pharmacy = null;
    }
    this.invitationService.updateInvitation(this.selectedToEdit.id, this.selectedToEdit).subscribe(
      data => {
        this.successfulResponse = true;
        this.successfulResponseMessage = `the invitation for ${this.selectedToEdit.userName} was edited successfully`
        this.errorResponse = false;
        this.formCreateInvitation.reset();
      },
      error => {
        this.errorResponseMessage = error.error;
        if (this.errorResponseMessage != null) {
          this.errorResponse = true;
        }
      }
    );
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
}
