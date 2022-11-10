import { Component, OnInit } from '@angular/core';
import { FormGroup, Validators, FormControl } from '@angular/forms';
import { PharmacyModel } from 'src/app/Models/pharmacyModel';
import { PharmacyService } from './pharmacy.service';

@Component({
  selector: 'app-pharmacy',
  templateUrl: './pharmacy.component.html',
  styleUrls: ['./pharmacy.component.css']
})
export class PharmacyComponent implements OnInit {

  formCreatePharmacy: FormGroup;
  name: string = "";
  address: string = "";
  successfulResponse: boolean = false;
  successfulResponseMessage: string;
  errorResponse: boolean = false;
  errorResponseMessage: string;

  constructor(private pharmacyService: PharmacyService) { }

  ngOnInit(): void {
    this.createFormPharmacy();
  }

  createFormPharmacy() {
    this.formCreatePharmacy = new FormGroup(
      {
        name: new FormControl('', [
          Validators.maxLength(50),
          Validators.required
        ]),
        address: new FormControl('', [
          Validators.required,
        ]),
      }
    );

    this.formCreatePharmacy.get('name')?.valueChanges.subscribe((change) => {
      this.name = change;
    });
    this.formCreatePharmacy.get('address')?.valueChanges.subscribe((change) => {
      this.address = change;
    });
  }

  createPharmacy() {
    var newPharmacy = new PharmacyModel();
    newPharmacy.address = this.address;
    newPharmacy.name = this.name;
    newPharmacy.requests = [];
    newPharmacy.stock = [];

    this.pharmacyService.postPharmacy(newPharmacy).subscribe(
      data => {
        this.successfulResponse = true;
        this.successfulResponseMessage = `${newPharmacy.name} was created successfully`
        this.errorResponse = false;
        this.formCreatePharmacy.reset();
      },
      error => {
        this.errorResponseMessage = error.error;
        if (this.errorResponseMessage != null) {
          this.errorResponse = true;
        }
      }
    );
  }
}
