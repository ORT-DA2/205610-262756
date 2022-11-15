import { Component, OnInit } from '@angular/core';
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
    this.requestService.getRequests().forEach(req => {
      console.log(req);
      this.Requests.push(req)
      console.log(this.Requests);
    });
  }

  ngOnInit(): void {
  }

  approveRequest(request: any) {
    request.state = 'approved';
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
    this.requestService.deleteRequest(request.id).subscribe(
      data => {
        this.successfulResponse = true;
        this.successfulResponseMessage = `request was rejected and deleted successfully`
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
