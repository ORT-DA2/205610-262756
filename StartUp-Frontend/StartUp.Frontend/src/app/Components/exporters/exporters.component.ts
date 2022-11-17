import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ExportModel } from 'src/app/Models/exporterModel';
import { ExportersService } from './exporters.service';

@Component({
  selector: 'app-exporters',
  templateUrl: './exporters.component.html',
  styleUrls: ['./exporters.component.css']
})
export class ExportersComponent implements OnInit {

  formats: any[] = [];
  formExporter: FormGroup;
  routeName: string = '';
  format: string = '';
  selectedFormat: string;
  successfulResponse: boolean = false;
  successfulResponseMessage: string;
  errorResponse: boolean = false;
  errorResponseMessage: string;
  formCreateInvitation: FormGroup;
  formEditInvitation: FormGroup;

  constructor(private exporterService: ExportersService) {
    this.loadFormats();
  }

  ngOnInit(): void {
    this.createFormExport();
  }

  loadFormats() {
    const repited: any[] = [];
    this.exporterService.getFormats().forEach(
      (format) => {
        repited.push(format);
        console.log(repited);
        repited[0].forEach((element: string) => {
          if (!this.formats.includes(element)) {
            this.formats.push(element)
            console.log("formats:", this.formats);
          }
        });
      });
  }

  createFormExport() {
    this.formExporter = new FormGroup({
      routeName: new FormControl('', []),
      format: new FormControl('', [Validators.required]),
    });

    this.formExporter.controls['routeName'].valueChanges.subscribe((change) => {
      this.routeName = change;
    });

    this.formExporter.controls['format'].valueChanges.subscribe((change) => {
      this.format = change;
    });
  }

  exportMedicines() {
    var model = new ExportModel();
    model.Format = this.format;
    console.log(this.format);
    model.RouteName = this.routeName;

    this.exporterService.postExportMedicines(model).subscribe(
      data => {
        this.successfulResponse = true;
        this.successfulResponseMessage = `All the medicines for this farmacy where exported successfully`
        this.errorResponse = false;
        this.formCreateInvitation.reset();
      },
      error => {
        this.errorResponseMessage = error.error.text;
        console.log(this.errorResponseMessage);
        if (this.errorResponseMessage != null) {
          this.errorResponse = true;
        }
      }
    );
  }
}
