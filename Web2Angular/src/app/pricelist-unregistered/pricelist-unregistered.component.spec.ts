import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PricelistUnregisteredComponent } from './pricelist-unregistered.component';

describe('PricelistUnregisteredComponent', () => {
  let component: PricelistUnregisteredComponent;
  let fixture: ComponentFixture<PricelistUnregisteredComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PricelistUnregisteredComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PricelistUnregisteredComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
