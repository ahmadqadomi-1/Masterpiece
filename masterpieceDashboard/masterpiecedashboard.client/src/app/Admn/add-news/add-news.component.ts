import { Component } from '@angular/core';
import { ServiceService } from '../../URL/service.service';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-add-news',
  templateUrl: './add-news.component.html',
  styleUrls: ['./add-news.component.css'] // Corrected the styleUrls property
})
export class AddNewsComponent {
  isSubmitted = false;

  constructor(private _ser: ServiceService) { }

  ngOnInit() { }

  AddNewNews(form: any) {
    this.isSubmitted = true;

    if (!form.valid) {
      Swal.fire({
        icon: 'error',
        title: 'Form Error',
        text: 'Please fill in all required fields.',
        confirmButtonColor: '#d33'
      });
      return;
    }

    // Convert form values into FormData
    const formData = new FormData();
    formData.append('NewsName', form.value.newsName);
    formData.append('YoutubeUrl', form.value.youtubeUrl);
    formData.append('NewsDescription', form.value.newsDescription);

    console.log('Submitting FormData:', formData);

    this._ser.addNews(formData).subscribe(
      () => {
        Swal.fire({
          icon: 'success',
          title: 'Success',
          text: 'The News has been added successfully.',
          confirmButtonColor: '#3085d6'
        });
        this.isSubmitted = false;
        form.reset();
      },
      (error) => {
        console.error('Error:', error);
        Swal.fire({
          icon: 'error',
          title: 'Error',
          text: 'An error occurred while adding the news. Please try again later.',
          confirmButtonColor: '#d33'
        });
      }
    );
  }


}
