import { TestBed } from '@angular/core/testing';

import { ExportersService } from './exporters.service';

describe('ExportersService', () => {
  let service: ExportersService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ExportersService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
