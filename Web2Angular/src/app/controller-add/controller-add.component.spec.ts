import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ControllerAddComponent } from './controller-add.component';

describe('ControllerAddComponent', () => {
  let component: ControllerAddComponent;
  let fixture: ComponentFixture<ControllerAddComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ControllerAddComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ControllerAddComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
