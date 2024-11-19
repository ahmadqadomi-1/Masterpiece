import { Component } from '@angular/core';
import { ServiceService } from '../../URL/service.service';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-add-product',
  templateUrl: './add-product.component.html',
  styleUrl: './add-product.component.css'
})
export class AddProductComponent {
  ngOnInit() {
  }

  constructor(private _ser: ServiceService) {

  }


  AddNewProduct(data: any) {
    const formData = new FormData();
    for (let key in data) {
      formData.append(key, data[key]);
    }

    this._ser.addProduct(formData).subscribe(
      () => {
        Swal.fire({
          icon: 'success',
          title: 'Success',
          text: 'The Product Added Successfully',
          confirmButtonColor: '#3085d6'
        });
      },
      (error) => {
        if (error.status === 400) {
          Swal.fire({
            icon: 'error',
            title: 'Error',
            text: 'There was an error in the data you submitted. Please check your inputs.',
            confirmButtonColor: '#d33'
          });
        } else if (error.status === 500) {
          Swal.fire({
            icon: 'error',
            title: 'Server Error',
            text: 'An internal server error occurred. Please try again later.',
            confirmButtonColor: '#d33'
          });
        } else {
          Swal.fire({
            icon: 'error',
            title: 'Unexpected Error',
            text: 'An unexpected error occurred: ' + error.message,
            confirmButtonColor: '#d33'
          });
        }
      }
    );
  }

}
