import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Observable } from 'rxjs';
import { InvoiceLineModel } from 'src/app/Models/invoiceLineModel';
import { SaleModel } from 'src/app/Models/saleModel';
import { SearchCriteriaMedicine } from 'src/app/Models/SearchCriteria/searchCriteriaMedicine';
import { MedicineService } from '../medicine/medicine.service';
import { SaleService } from './sale.service';

@Component({
  selector: 'app-sale',
  templateUrl: './sale.component.html',
  styleUrls: ['./sale.component.css']
})
export class SaleComponent implements OnInit {
  medicines: any[] = [];
  cart: InvoiceLineModel[] = [];
  amount: number = 0;
  selectedMedicine: any;
  sales: any[] = [];
  trackingCode: number = 0;
  saleTracked: any;
  medicineName: string = "";
  formSale: FormGroup;

  successfulResponse: boolean = false;
  successfulResponseMessage: string;
  errorResponse: boolean = false;
  errorResponseMessage: string;

  constructor(private medicineService: MedicineService, private saleService: SaleService) {
    this.getAllMedicines();
  }

  ngOnInit(): void {
    this.createFormSale();

  }

  createFormSale() {
    this.formSale = new FormGroup(
      {
        amount: new FormControl('', [
          Validators.min(1)
        ]),
      }
    );

    this.formSale.get('amount')?.valueChanges.subscribe((change) => {
      this.amount = change;
    });
  }


  addToCart() {
    var invoiceLine = new InvoiceLineModel();
    invoiceLine.medicine = this.selectedMedicine;
    invoiceLine.amount = this.amount;
    this.medicines[0].find((m: { id: any; stock: number; }) => {
      if (m.id == this.selectedMedicine.id) {
        m.stock = m.stock - this.amount;
      }
    });
    this.amount = 0;
    this.cart.push(invoiceLine);
  }

  buy() {
    var sale = new SaleModel();
    sale.invoiceLines = [];
    this.cart.forEach(item => {
      sale.invoiceLines.push(item)
    });

    this.saleService.postSale(sale).subscribe(
      data => {
        this.successfulResponse = true;
        this.successfulResponseMessage = `The sale was created successfully with the code ${data.code}`
        this.errorResponse = false;
        this.amount = 0;
      },
      error => {
        this.errorResponseMessage = error.error;
        if (this.errorResponseMessage != null) {
          this.errorResponse = true;
        }
      }
    );
  }

  changeSelectedMedicine(med: any) {
    this.selectedMedicine = med;

  }

  private getAllMedicines() {
    this.medicineService.getMedicine().forEach(
      data => {
        this.medicines.push(data);
      }
    );
  }

  public getSaleForCode() {
    this.saleService.getSale(this.trackingCode).subscribe(
      data => {
        this.errorResponse = false;
        this.saleTracked = data;
      },
      error => {
        this.errorResponseMessage = error.error;
        if (this.errorResponseMessage != null) {
          this.errorResponse = true;
        }
      }
    );
  }

  filter() {
    var search = new SearchCriteriaMedicine();
    search.name = this.medicineName;
    this.medicineService.getMedicine(search).subscribe(
      data => {
        this.medicines.splice(0, this.medicines.length);
        this.medicines.push(data);

        this.errorResponse = false;
        this.saleTracked = data;
      },
      error => {
        this.errorResponseMessage = error.error;
        if (this.errorResponseMessage != null) {
          this.errorResponse = true;
        }
      }
    );
  }

  clear() {
    this.medicines.splice(0, this.medicines.length);
    this.getAllMedicines();
  }
}
