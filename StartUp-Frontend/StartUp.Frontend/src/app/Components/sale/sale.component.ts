import { Component, OnInit } from '@angular/core';
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

  successfulResponse: boolean = false;
  successfulResponseMessage: string;
  errorResponse: boolean = false;
  errorResponseMessage: string;

  constructor(private medicineService: MedicineService, private saleService: SaleService) {
    console.log(this.amount);
    this.getAllMedicines();
    console.log(this.medicines);
  }

  ngOnInit(): void {

  }

  addToCart() {
    var invoiceLine = new InvoiceLineModel();
    invoiceLine.medicine = this.selectedMedicine;
    console.log(this.amount);
    invoiceLine.amount = this.amount;
    this.medicines[0].find((m: { id: any; stock: number; }) => {
      if (m.id == this.selectedMedicine.id) {
        m.stock = m.stock - this.amount;
      }
    });
    this.amount = 0;
    console.log(this.medicines);
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
    console.log(this.selectedMedicine);

  }

  private getAllMedicines() {
    this.medicineService.getMedicine().forEach(
      data => {
        this.medicines.push(data);
      }
    );
  }

  public getSaleForCode() {
    console.log(this.trackingCode);
    this.saleService.getSale(this.trackingCode).subscribe(
      data => {
        console.log(data);
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
        console.log("form filter:", data);
        this.medicines.splice(0, this.medicines.length);
        console.log("clear", this.medicines);
        this.medicines.push(data);

        console.log("new med:", this.medicines);
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
    console.log("clear", this.medicines);
    this.getAllMedicines();
  }
}
