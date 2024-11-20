import { Component } from '@angular/core';
import { ServiceService } from '../../URL/service.service';
import Swal from 'sweetalert2';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-add-product',
  templateUrl: './add-product.component.html',
  styleUrls: ['./add-product.component.css'],
})
export class AddProductComponent {
  image1: any;
  image2: any;
  image3: any;
  categoryId: string = '';
  isSubmitted = false; 

  constructor(private _ser: ServiceService, private route: ActivatedRoute) { }

  ngOnInit() {
    this.route.params.subscribe((params) => {
      this.categoryId = params['categoryId'];
    });
  }

  imageChange1(e: any) {
    this.image1 = e.target.files[0];
  }

  imageChange2(e: any) {
    this.image2 = e.target.files[0];
  }

  imageChange3(e: any) {
    this.image3 = e.target.files[0];
  }

  AddNewProduct(form: any) {
    this.isSubmitted = true;

    if (!form.valid || !this.image1 || !this.image2 || !this.image3) {
      Swal.fire({
        icon: 'error',
        title: 'Form Error',
        text: 'Please fill in all required fields and upload all images.',
        confirmButtonColor: '#d33',
      });
      return;
    }

    const formData = new FormData();
    for (let key in form.value) {
      formData.append(key, form.value[key]);
    }

    formData.append('ProductImage', this.image1);
    formData.append('ProductImage2', this.image2);
    formData.append('ProductImage3', this.image3);

    this._ser.addProductToCategory(this.categoryId, formData).subscribe(
      () => {
        Swal.fire({
          icon: 'success',
          title: 'Success',
          text: 'The Product Added Successfully',
          confirmButtonColor: '#3085d6',
        });
        this.isSubmitted = false;
        form.reset();
        this.image1 = this.image2 = this.image3 = null;
      },
      (error) => {
        let errorMessage = 'An unexpected error occurred. Please try again later.';
        if (error.status === 400) {
          errorMessage = 'There was an error in the data you submitted. Please check your inputs.';
        } else if (error.status === 500) {
          errorMessage = 'An internal server error occurred. Please try again later.';
        }
        Swal.fire({
          icon: 'error',
          title: 'Error',
          text: errorMessage,
          confirmButtonColor: '#d33',
        });
      }
    );
  }
}
