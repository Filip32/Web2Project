import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { RoutesStationsAdminComponent } from './routes-stations-admin.component';

describe('RoutesStationsAdminComponent', () => {
  let component: RoutesStationsAdminComponent;
  let fixture: ComponentFixture<RoutesStationsAdminComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ RoutesStationsAdminComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(RoutesStationsAdminComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
