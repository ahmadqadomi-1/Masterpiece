import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddTilerComponent } from './add-tiler.component';

describe('AddTilerComponent', () => {
  let component: AddTilerComponent;
  let fixture: ComponentFixture<AddTilerComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [AddTilerComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AddTilerComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
