import { Component } from '@angular/core';
import Swal from 'sweetalert2';
import { ServiceService } from '../../URL/service.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-categry',
  templateUrl: './categry.component.html',
  styleUrl: './categry.component.css'
})
export class CategryComponent {
  ngOnInit() {
    this.getAllCategory()
  }

  constructor(private _ser: ServiceService, private router: Router) {

  }

  CategoryArray: any
  getAllCategory() {
    this._ser.getCategory().subscribe((data) => {
      this.CategoryArray = data
      console.log(this.CategoryArray, "this.CategoryArray")
    })
  }

  deleteCategoryById(id: any) {
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
        this._ser.deleteCategory(id).subscribe(
          () => {
            Swal.fire(
              'Deleted!',
              'This Category has been deleted successfully.',
              'success'
            );
            this.getAllCategory();
          },
          (error) => {
            if (error.status === 400) {
              Swal.fire(
                'Error',
                'Cannot delete this category because it contains products.',
                'error'
              );
            } else {
              Swal.fire(
                'Error',
                'An error occurred while deleting the category. Please try again.',
                'error'
              );
            }
          }
        );
      }
    });
  }



  navigateToAddCategory() {
    this.router.navigate(['/dashboard/AddCategory']);
  }
}
