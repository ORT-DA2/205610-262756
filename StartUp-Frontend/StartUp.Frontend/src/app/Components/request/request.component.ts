import { Component, OnInit } from '@angular/core';
import { FormArray, FormControl, FormGroup, Validators } from '@angular/forms';
import { PetitionModel } from 'src/app/Models/petitionModel';
import { RequestModel } from 'src/app/Models/requestModel';
import { PetitionService } from './petition/petition.service';
import { RequestService } from './request.service';

@Component({
  selector: 'app-request',
  templateUrl: './request.component.html',
  styleUrls: ['./request.component.css']
})
export class RequestComponent implements OnInit {

  petitions: any[] = [];
  petitionsSelected: any[] = [];
  requestForm: FormGroup;

  successfulResponse: boolean = false;
  successfulResponseMessage: string;
  errorResponse: boolean = false;
  errorResponseMessage: string;

  constructor(private petitionService: PetitionService, private requestService: RequestService) {
    this.petitionService.getPetitions().forEach(
      pet => {
        this.petitions.push(pet);
        console.log(this.petitions);
      })
  }

  createFormRequest() {
    this.requestForm = new FormGroup(
      {
        petitions: new FormArray([], [
          Validators.required
        ])
      }
    );

    this.requestForm.get('petitions')?.valueChanges.subscribe((change) => {
      this.petitions = change;
    });
  }

  onChange(petition: any, event: any) {
    console.log("event:", event.target.checked);
    if (event.target.checked) {
      this.petitionsSelected.push(petition);
      console.log("list", this.petitionsSelected);
    } else {
      const index = this.petitionsSelected.findIndex(x => x.value === petition);
      this.petitionsSelected.splice(index, 1);
      console.log(this.petitionsSelected);
    }
  }

  ngOnInit(): void {
    this.createFormRequest()
  }

  createRequest() {
    var request = new RequestModel();

    request.petitions = new Array<PetitionModel>();

    this.petitionsSelected.forEach((pet: PetitionModel) => {
      var petition = new PetitionModel();
      petition = pet;
      request.petitions.push(petition);
    })

    this.requestService.postRequest(request).subscribe(
      data => {
        this.successfulResponse = true;
        this.successfulResponseMessage = `An request was created for your petitions`;
        this.errorResponse = false;
      },
      error => {
        this.errorResponseMessage = error.error;
        if (this.errorResponseMessage != null) {
          this.errorResponse = true;
        }
      }
    )
  }

}
