import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SaleEmployeeComponent } from './sale-employee.component';

describe('SaleEmployeeComponent', () => {
  let component: SaleEmployeeComponent;
  let fixture: ComponentFixture<SaleEmployeeComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [SaleEmployeeComponent]
    })
      .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SaleEmployeeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
