import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { PetitionModel } from 'src/app/Models/petitionModel';
import { PetitionService } from './petition.service';

@Component({
  selector: 'app-petition',
  templateUrl: './petition.component.html',
  styleUrls: ['./petition.component.css']
})
export class PetitionComponent implements OnInit {

  formCreatePetition: FormGroup;
  code: string;
  amount: number;
  successfulResponse: boolean = false;
  successfulResponseMessage: string;
  errorResponse: boolean = false;
  errorResponseMessage: string;

  constructor(private petitionService: PetitionService) { }

  ngOnInit(): void {
  }

  createFormPetition() {
    this.formCreatePetition = new FormGroup(
      {
        name: new FormControl('', [
          Validators.required,
        ]),
        code: new FormControl('', [
          Validators.required,
        ]),
      }
    );

    this.setFormValues();
  }

  setFormValues() {
    this.formCreatePetition.get('amount')?.valueChanges.subscribe((change) => {
      this.amount = change;
    });
    this.formCreatePetition.get('code')?.valueChanges.subscribe((change) => {
      this.code = change;
    });
  }

  createPetition() {
    var petition = new PetitionModel();
    petition.amount = this.amount;
    petition.code = this.code;

    this.petitionService.postPetition(petition).subscribe(
      data => {
        this.successfulResponse = true;
        this.successfulResponseMessage = `${petition.code} was created successfully`
        this.errorResponse = false;
        this.formCreatePetition.reset();
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
