import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { TicketsChackComponent } from './tickets-chack.component';

describe('TicketsChackComponent', () => {
  let component: TicketsChackComponent;
  let fixture: ComponentFixture<TicketsChackComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ TicketsChackComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(TicketsChackComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
