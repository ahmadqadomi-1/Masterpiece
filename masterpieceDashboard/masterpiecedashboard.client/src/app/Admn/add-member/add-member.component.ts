import { Component } from '@angular/core';
import { ServiceService } from '../../URL/service.service';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-add-member',
  templateUrl: './add-member.component.html',
  styleUrls: ['./add-member.component.css'],
})
export class AddMemberComponent {
  image: any;
  isSubmitted = false; 

  constructor(private _ser: ServiceService) { }

  ngOnInit() { }

  imageChange(e: any) {
    this.image = e.target.files[0];
  }

  AddNewMember(form: any) {
    this.isSubmitted = true; 

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
    formData.append('teamImg', this.image);

    this._ser.addTeam(formData).subscribe(
      () => {
        Swal.fire({
          icon: 'success',
          title: 'Success',
          text: 'The Member Added Successfully',
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
          text: 'An error occurred while adding the member.',
          confirmButtonColor: '#d33',
        });
      }
    );
  }
}
