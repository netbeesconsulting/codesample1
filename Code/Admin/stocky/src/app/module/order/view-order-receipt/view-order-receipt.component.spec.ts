import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ViewOrderReceiptComponent } from './view-order-receipt.component';

describe('ViewOrderReceiptComponent', () => {
  let component: ViewOrderReceiptComponent;
  let fixture: ComponentFixture<ViewOrderReceiptComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ViewOrderReceiptComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ViewOrderReceiptComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
