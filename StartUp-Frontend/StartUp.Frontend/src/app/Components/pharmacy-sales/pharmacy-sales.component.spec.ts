import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PharmacySalesComponent } from './pharmacy-sales.component';

describe('PharmacySalesComponent', () => {
  let component: PharmacySalesComponent;
  let fixture: ComponentFixture<PharmacySalesComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PharmacySalesComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(PharmacySalesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
