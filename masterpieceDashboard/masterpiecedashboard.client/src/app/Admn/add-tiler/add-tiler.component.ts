import { Component } from '@angular/core';
import { ServiceService } from '../../URL/service.service';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-add-tiler',
  templateUrl: './add-tiler.component.html',
  styleUrls: ['./add-tiler.component.css'],
})
export class AddTilerComponent {
  image: any;
  isSubmitted = false;

  constructor(private _ser: ServiceService) { }

  ngOnInit() { }

  imageChange(e: any) {
    this.image = e.target.files[0];
  }

  AddNewTiler(form: any) {
    this.isSubmitted = true;

    // Validate form and image
    if (!form.valid || !this.image) {
      Swal.fire({
        icon: 'error',
        title: 'Form Error',
        text: 'Please fill in all required fields and upload an image.',
        confirmButtonColor: '#d33',
      });
      return;
    }

    const formData = new FormData();
    for (let key in form.value) {
      formData.append(key, form.value[key]);
    }
    formData.append('tilerImg', this.image);

    this._ser.addTiler(formData).subscribe(
      () => {
        Swal.fire({
          icon: 'success',
          title: 'Success',
          text: 'The Tiler Added Successfully',
          confirmButtonColor: '#3085d6',
        });
        this.isSubmitted = false;
        form.reset();
        this.image = null;
      },
      (error) => {
        Swal.fire({
          icon: 'error',
          title: 'Error',
          text: 'An error occurred while adding the tiler.',
          confirmButtonColor: '#d33',
        });
      }
    );
  }
}
