import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router'; // استبدال Router بـ ActivatedRoute
import { ServiceService } from '../../URL/service.service';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-product-to-one-category',
  templateUrl: './product-to-one-category.component.html',
  styleUrl: './product-to-one-category.component.css'
})
export class ProductToOneCategoryComponent implements OnInit {
  ProductArray: any;
  categoryId: string = '';

  constructor(private _ser: ServiceService, private route: ActivatedRoute, private router: Router) { } 

  ngOnInit() {
    this.route.params.subscribe((params) => {
      this.categoryId = params['id'];
      this.getAllToOneCategoryProduct(this.categoryId);
    });
  }

  getAllToOneCategoryProduct(userId: string) {
    this._ser.getproductbycategoryid(userId).subscribe((data) => {
      this.ProductArray = data;
      console.log(this.ProductArray, "this.ProductArray");

      if (!this.ProductArray || this.ProductArray.length === 0) {
        Swal.fire({
          title: 'No product Found',
          text: 'There are no product for this Category.',
          icon: 'info',
          timer: 3000,
          timerProgressBar: true,
        });
      }
    }, (error) => {
      console.error('Error fetching product:', error);

      Swal.fire({
        title: 'Notice',
        text: 'There are no product for this category.',
        icon: 'warning',
        timer: 3000,
        timerProgressBar: true,
      });
    });
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
          this.getAllToOneCategoryProduct(this.categoryId); 
        });
      }
    });
  }

  navigateToAddProduct() {
    this.router.navigate(['/dashboard/AddProduct', this.categoryId]);
  }

}
