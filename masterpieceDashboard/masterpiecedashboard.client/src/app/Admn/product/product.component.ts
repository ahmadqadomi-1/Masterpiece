import { Component } from '@angular/core';
import Swal from 'sweetalert2';
import { ServiceService } from '../../URL/service.service';

@Component({
  selector: 'app-product',
  templateUrl: './product.component.html',
  styleUrl: './product.component.css'
})
export class ProductComponent {
  ngOnInit() {
    this.getAllProduct()
  }

  constructor(private _ser: ServiceService) {

  }

  ProductArray: any
  getAllProduct() {
    this._ser.getProduct().subscribe((data) => {
      this.ProductArray = data
      console.log(this.ProductArray, "this.ProductArray")
    })
  }

  deleteProductById(id: any) {
    Swal.fire({
      title: 'Are you sure?',
      text: "You won't be able to revert this!",
      icon: 'warning',
      showCancelButton: true,
      confirmButtonColor: '#3085d6',
      cancelButtonColor: '#d33',
      confirmButtonText: 'Yes, delete it!'
    }).then((result) => {
      if (result.isConfirmed) {
        this._ser.deleteProduct(id).subscribe(() => {
          Swal.fire(
            'Deleted!',
            'This Product has been deleted successfully.',
            'success'
          );
          this.getAllProduct();
        });
      }
    });
  }
}
