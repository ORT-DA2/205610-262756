import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ExportMedicineComponent } from './export-medicine.component';

describe('ExportMedicineComponent', () => {
  let component: ExportMedicineComponent;
  let fixture: ComponentFixture<ExportMedicineComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ExportMedicineComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ExportMedicineComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
