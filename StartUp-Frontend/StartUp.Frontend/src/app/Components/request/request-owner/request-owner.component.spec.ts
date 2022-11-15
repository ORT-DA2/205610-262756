import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RequestOwnerComponent } from './request-owner.component';

describe('RequestOwnerComponent', () => {
  let component: RequestOwnerComponent;
  let fixture: ComponentFixture<RequestOwnerComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ RequestOwnerComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(RequestOwnerComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
