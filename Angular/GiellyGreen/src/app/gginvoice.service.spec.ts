import { TestBed } from '@angular/core/testing';

import { GGInvoiceService } from './gginvoice.service';

describe('GGInvoiceService', () => {
  let service: GGInvoiceService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(GGInvoiceService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
