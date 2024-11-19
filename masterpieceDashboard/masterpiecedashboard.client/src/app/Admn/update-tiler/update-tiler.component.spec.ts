import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UpdateTilerComponent } from './update-tiler.component';

describe('UpdateTilerComponent', () => {
  let component: UpdateTilerComponent;
  let fixture: ComponentFixture<UpdateTilerComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [UpdateTilerComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(UpdateTilerComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
