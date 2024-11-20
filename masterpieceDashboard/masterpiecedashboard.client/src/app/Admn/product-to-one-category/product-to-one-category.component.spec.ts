import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ProductToOneCategoryComponent } from './product-to-one-category.component';

describe('ProductToOneCategoryComponent', () => {
  let component: ProductToOneCategoryComponent;
  let fixture: ComponentFixture<ProductToOneCategoryComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ProductToOneCategoryComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ProductToOneCategoryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
