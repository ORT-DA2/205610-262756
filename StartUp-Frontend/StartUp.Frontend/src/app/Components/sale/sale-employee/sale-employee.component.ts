import { Component, OnInit } from '@angular/core';
import { InvoiceLineModel } from 'src/app/Models/invoiceLineModel';
import { InvoiceLineService } from '../invoice-line.service';
import { SaleService } from '../sale.service';

@Component({
  selector: 'app-sale-employee',
  templateUrl: './sale-employee.component.html',
  styleUrls: ['./sale-employee.component.css']
})
export class SaleEmployeeComponent implements OnInit {

  sales: any[] = [];
  successfulResponse: boolean = false;
  successfulResponseMessage: string;
  errorResponse: boolean = false;
  errorResponseMessage: string;

  constructor(private saleService: SaleService, private invoiceLineService: InvoiceLineService) {
    this.loadSales()
  }

  ngOnInit(): void {
  }

  loadSales() {
    this.saleService.getSales().forEach(item => this.sales.push(item));
  }

  approveLine(sale: any, line: any) {

    sale.invoiceLines.forEach((Iline: { state: string; }) => {
      if (Iline == line) {
        Iline.state = 'approved';
      }
    })

    this.saleService.updateSale(sale.code, sale).subscribe(
      data => {
        this.successfulResponse = true;
        this.successfulResponseMessage = `request was approved successfully`
        this.errorResponse = false;
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
