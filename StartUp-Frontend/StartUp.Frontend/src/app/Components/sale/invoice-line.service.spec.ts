import { TestBed } from '@angular/core/testing';

import { InvoiceLineService } from './invoice-line.service';

describe('InvoiceLineService', () => {
  let service: InvoiceLineService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(InvoiceLineService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
