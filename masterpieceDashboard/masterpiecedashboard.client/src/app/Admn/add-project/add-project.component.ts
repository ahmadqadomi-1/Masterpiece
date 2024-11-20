import { Component } from '@angular/core';
import { ServiceService } from '../../URL/service.service';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-add-project',
  templateUrl: './add-project.component.html',
  styleUrls: ['./add-project.component.css']
})
export class AddProjectComponent {
  image: any;
  governorates: string[] = [
    'الأردن-إربد',
    'الأردن-عمان',
    'الأردن-الزرقاء',
    'الأردن-السلط',
    'الأردن-العقبة',
    'الأردن-معان',
    'الأردن-الطفيلة',
    'الأردن-الكرك',
    'الأردن-مادبا',
    'الأردن-جرش',
    'الأردن-عجلون',
    'الأردن-المفرق',
    'الأردن-البحر الميت'
  ];

  constructor(private _ser: ServiceService) { }

  ngOnInit() { }

  imageChange(e: any) {
    this.image = e.target.files[0];
  }

  AddNewProject(form: any) {
    if (!form.valid || !this.image) {
      Swal.fire({
        icon: 'error',
        title: 'Error',
        text: 'Please fill all required fields and select an image before submitting.',
        confirmButtonColor: '#d33'
      });
      return;
    }

    const formData = new FormData();
    for (let key in form.value) {
      formData.append(key, form.value[key]);
    }
    formData.append("ProjectImage", this.image);

    this._ser.addProject(formData).subscribe(
      () => {
        Swal.fire({
          icon: 'success',
          title: 'Success',
          text: 'The Project Added Successfully',
          confirmButtonColor: '#3085d6'
        });
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
