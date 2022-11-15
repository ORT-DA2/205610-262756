import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MedicineModel } from 'src/app/Models/medicineModel';
import { SymptomModel } from 'src/app/Models/symptomModel';
import { MedicineService } from './medicine.service';

@Component({
  selector: 'app-medicine',
  templateUrl: './medicine.component.html',
  styleUrls: ['./medicine.component.css']
})
export class MedicineComponent implements OnInit {

  formCreateMedicine: FormGroup;
  name: string = "";
  code: string = "";
  symptom: string = "";
  presentation: string = "";
  prescription: boolean = false;
  amount: number = 0;
  measure: string = "";
  price: number = 0;
  symptoms: SymptomModel[] = [];

  successfulResponse: boolean = false;
  successfulResponseMessage: string;
  errorResponse: boolean = false;
  errorResponseMessage: string;

  constructor(private medicineService: MedicineService) { }

  ngOnInit(): void {
    this.createFormMedicine();
  }

  createFormMedicine() {
    this.formCreateMedicine = new FormGroup(
      {
        name: new FormControl('', [
          Validators.required,
        ]),
        code: new FormControl('', [
          Validators.required,
        ]),
        presentation: new FormControl('', [
          Validators.required,
        ]),
        amount: new FormControl('', [
          Validators.required,
        ]),
        measure: new FormControl('', [
          Validators.required,
        ]),
        price: new FormControl('', [
          Validators.required,
        ]),
        symptom: new FormControl('', []),
        prescription: new FormControl('', []),
      }
    );

    this.setFormValues();
  }

  setFormValues() {
    this.formCreateMedicine.get('name')?.valueChanges.subscribe((change) => {
      this.name = change;
    });
    this.formCreateMedicine.get('code')?.valueChanges.subscribe((change) => {
      this.code = change;
    });
    this.formCreateMedicine.get('presentation')?.valueChanges.subscribe((change) => {
      this.presentation = change;
    });
    this.formCreateMedicine.get('amount')?.valueChanges.subscribe((change) => {
      this.amount = change;
    });
    this.formCreateMedicine.get('measure')?.valueChanges.subscribe((change) => {
      this.measure = change;
    });
    this.formCreateMedicine.get('price')?.valueChanges.subscribe((change) => {
      this.price = change;
    });
    this.formCreateMedicine.get('symptom')?.valueChanges.subscribe((change) => {
      this.symptom = change;
    });
    this.formCreateMedicine.get('prescription')?.valueChanges.subscribe((change) => {
      this.prescription = change;
    });
  }

  addSymptom() {
    var newSymptom = new SymptomModel();
    newSymptom.symptomDescription = this.symptom;
    this.symptoms.push(newSymptom);
    console.log(this.symptoms);
  }

  createMedicine() {
    var medicine = new MedicineModel();
    medicine.amount = this.amount;
    medicine.code = this.code;
    medicine.measure = this.measure;
    medicine.name = this.name;
    medicine.prescription = this.prescription;
    medicine.presentation = this.presentation;
    medicine.price = this.price;
    medicine.symptoms = this.symptoms;
    this.medicineService.postMedicine(medicine).subscribe(
      data => {
        this.successfulResponse = true;
        this.successfulResponseMessage = `${medicine.name} was created successfully`
        this.errorResponse = false;
        this.formCreateMedicine.reset();
        this.symptoms = [];
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
