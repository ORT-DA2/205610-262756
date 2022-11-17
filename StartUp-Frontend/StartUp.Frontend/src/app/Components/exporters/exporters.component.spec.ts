import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ExportersComponent } from './exporters.component';

describe('ExportersComponent', () => {
  let component: ExportersComponent;
  let fixture: ComponentFixture<ExportersComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ExportersComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ExportersComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
