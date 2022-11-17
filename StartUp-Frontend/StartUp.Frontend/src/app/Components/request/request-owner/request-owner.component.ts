import { Component, OnInit } from '@angular/core';
import { RequestModel } from 'src/app/Models/requestModel';
import { runInThisContext } from 'vm';
import { RequestService } from '../request.service';

@Component({
  selector: 'app-request-owner',
  templateUrl: './request-owner.component.html',
  styleUrls: ['./request-owner.component.css']
})
export class RequestOwnerComponent implements OnInit {

  Requests: any[] = [];
  showEmptyMessage: boolean = false;
  successfulResponse: boolean = false;
  successfulResponseMessage: string;
  errorResponse: boolean = false;
  errorResponseMessage: string;

  constructor(private requestService: RequestService) {
    this.loadRequest();
  }

  loadRequest() {
    this.requestService.getRequests().forEach(req => {
      this.Requests.push(req)
    });
  }

  ngOnInit(): void {
  }

  approveRequest(request: any) {
    request.state = 'Approved';
    this.requestService.putRequest(request.id, request).subscribe(
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

  rejectRequest(request: any) {
    var model = new RequestModel();
    model.state = 'Rejected',
      model.petitions = request.petitions;

    this.requestService.rejectRequest(request.id, model).subscribe(
      data => {
        this.successfulResponse = true;
        this.successfulResponseMessage = `request was rejected successfully`
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
