import { Component } from '@angular/core';
import { ServiceService } from '../../URL/service.service';
import { ActivatedRoute } from '@angular/router';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-update-category',
  templateUrl: './update-category.component.html',
  styleUrls: ['./update-category.component.css']
})
export class UpdateCategoryComponent {
  param: string | null = null;
  categoryData: any = {}; 

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
    console.log('Category ID:', this.param);


    if (this.param) {
      this._ser.getCategoryById(this.param).subscribe(
        (data) => {
          console.log('Received Data from API:', data);
          this.categoryData = data;
        },
        (error) => {
          console.error('Error fetching category data:', error);
        }
      );
    }
  }

  UpdateCategory(data: any) {
    const formData = new FormData();
    for (let key in data) {
      formData.append(key, data[key]);
    }
    console.log('Form Data:', data);
    console.log(this.image, " this.image r ")
    formData.append("categoryImage", this.image)
    console.log('category Data:', this.categoryData);
    this._ser.EditCategory(this.param, formData).subscribe(
      () => {
        Swal.fire({
          icon: 'success',
          title: 'Success',
          text: 'The category has been updated successfully!',
          confirmButtonColor: '#3085d6'
        });
      },
      (error) => {
        console.error('Error:', error);
        Swal.fire({
          icon: 'error',
          title: 'Error',
          text: 'An error occurred while updating the category.',
          confirmButtonColor: '#d33'
        });
      }
    );
  }


}
