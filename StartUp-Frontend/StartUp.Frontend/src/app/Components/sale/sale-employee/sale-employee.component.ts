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

  constructor(private saleService: SaleService, private invoiceLine: InvoiceLineService) {
    this.saleService.getSales().forEach(item => this.sales.push(item));
  }

  ngOnInit(): void {
  }

  approveLine(line: any) {
    console.log("line", line);
    var invoiceLine = new InvoiceLineModel();
    invoiceLine.state = 'approved';
    invoiceLine.medicine = line.medicine;
    invoiceLine.amount = line.amount;

    this.invoiceLine.updateInvoideLine(line.id, invoiceLine).subscribe(
      data => {
        this.sales.find(l => {
          if (l.id == line.id) {
            l.state = invoiceLine.state;
          }
        });
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
