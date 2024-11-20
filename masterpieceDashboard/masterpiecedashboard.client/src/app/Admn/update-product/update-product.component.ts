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

  image: any
  imageChange(e: any) {
    this.image = e.target.files[0];
  }

  ngOnInit() {
    this.param = this._active.snapshot.paramMap.get('id');
    console.log('Product ID:', this.param);

    if (this.param) {
      this._ser.getProductById(this.param).subscribe(
        (data) => {
          console.log('Received Data from API:', data);
          this.ProductData = { ...this.ProductData, ...data }; 
        },
        (error) => {
          console.error('Error fetching product data:', error);
        }
      );
    }
  }

  UpdateProduct(data: any) {
    const formData = new FormData();
    for (let key in data) {
      formData.append(key, data[key]);
    }
    console.log(data, "data r ")
    console.log(this.image , " this.image r ")
    formData.append("ProductImage", this.image)
    console.log('Product Data:', this.ProductData);

    if (this.param) {
      this._ser.EditProduct(this.param, formData).subscribe(
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
