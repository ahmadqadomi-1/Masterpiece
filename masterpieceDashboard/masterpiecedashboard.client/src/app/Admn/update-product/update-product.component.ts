import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ServiceService } from '../../URL/service.service';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-update-product',
  templateUrl: './update-product.component.html',
  styleUrls: ['./update-product.component.css']
})
export class UpdateProductComponent implements OnInit {
  param: string | null = null;
  ProductData: any = {
    productName: '',
    productImage: '',
    productDescription: '',
    productDescriptionList1: '',
    productDescriptionList2: '',
    productDescriptionList3: '',
    price: 0,
    stock: 0
  };

  constructor(
    private _ser: ServiceService,
    private _active: ActivatedRoute
  ) { }

  ngOnInit() {
    // Retrieve the product ID from the route
    this.param = this._active.snapshot.paramMap.get('id');
    console.log('Product ID:', this.param);

    // Fetch the product data if the ID is available
    if (this.param) {
      this._ser.getProductById(this.param).subscribe(
        (data) => {
          console.log('Received Data from API:', data);
          this.ProductData = { ...this.ProductData, ...data }; // Merge received data with default values
        },
        (error) => {
          console.error('Error fetching product data:', error);
        }
      );
    }
  }

  UpdateProduct() {
    console.log('Product Data:', this.ProductData);

    if (this.param) {
      this._ser.EditProduct(this.param, this.ProductData).subscribe(
        () => {
          Swal.fire({
            icon: 'success',
            title: 'Success',
            text: 'The product has been updated successfully!',
            confirmButtonColor: '#3085d6'
          });
        },
        (error) => {
          console.error('Error:', error);
          Swal.fire({
            icon: 'error',
            title: 'Error',
            text: 'An error occurred while updating the product.',
            confirmButtonColor: '#d33'
          });
        }
      );
    } else {
      Swal.fire({
        icon: 'warning',
        title: 'Warning',
        text: 'Product data is missing. Please try again.',
        confirmButtonColor: '#d33'
      });
    }
  }


}
