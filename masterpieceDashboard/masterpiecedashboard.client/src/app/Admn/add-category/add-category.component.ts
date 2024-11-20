import { Component } from '@angular/core';
import { ServiceService } from '../../URL/service.service';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-add-category',
  templateUrl: './add-category.component.html',
  styleUrls: ['./add-category.component.css']
})
export class AddCategoryComponent {
  image: any;
  isSubmitted = false; 

  constructor(private _ser: ServiceService) { }

  ngOnInit() { }

  imageChange(e: any) {
    this.image = e.target.files[0];
  }

  AddNewCategory(form: any) {
    this.isSubmitted = true;

    if (!form.valid || !this.image) {
      Swal.fire({
        icon: 'error',
        title: 'Form Error',
        text: 'Please fill in all required fields and upload an image.',
        confirmButtonColor: '#d33'
      });
      return;
    }

    const formData = new FormData();
    for (let key in form.value) {
      formData.append(key, form.value[key]);
    }
    formData.append("CategoryImage", this.image);

    this._ser.addCategory(formData).subscribe(
      () => {
        Swal.fire({
          icon: 'success',
          title: 'Success',
          text: 'The Category Added Successfully',
          confirmButtonColor: '#3085d6'
        });
        this.isSubmitted = false; 
        form.reset(); 
        this.image = null; 
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
          confirmButtonColor: '#d33'
        });
      }
    );
  }
}
