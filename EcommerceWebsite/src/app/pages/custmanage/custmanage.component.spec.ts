import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CustmanageComponent } from './custmanage.component';

describe('CustmanageComponent', () => {
  let component: CustmanageComponent;
  let fixture: ComponentFixture<CustmanageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [CustmanageComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CustmanageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
