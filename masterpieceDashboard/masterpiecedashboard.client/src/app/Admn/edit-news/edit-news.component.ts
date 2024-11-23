import { Component } from '@angular/core';
import { ServiceService } from '../../URL/service.service';
import { ActivatedRoute } from '@angular/router';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-edit-news',
  templateUrl: './edit-news.component.html',
  styleUrl: './edit-news.component.css'
})
export class EditNewsComponent {
  param: string | null = null;
  newsData: any = {};

  constructor(
    private _ser: ServiceService,
    private _active: ActivatedRoute
  ) { }

  ngOnInit() {
    this.param = this._active.snapshot.paramMap.get('id');
    console.log('News ID:', this.param);

    if (this.param) {
      this._ser.getNewsbyid(this.param).subscribe(
        (data) => {
          console.log('Received Data from API:', data);
          this.newsData = data;
        },
        (error) => {
          console.error('Error fetching news data:', error);
        }
      );
    }
  }


  UpdateNews(data: any) {
    const formData = new FormData();
    for (let key in data) {
      formData.append(key, data[key]);
    }
    this._ser.EditNews(this.param, formData).subscribe(
      () => {
        Swal.fire({
          icon: 'success',
          title: 'Success',
          text: 'The news has been updated successfully!',
          confirmButtonColor: '#3085d6'
        });
      },
      (error) => {
        console.error('Error:', error);
        Swal.fire({
          icon: 'error',
          title: 'Error',
          text: 'An error occurred while updating the news.',
          confirmButtonColor: '#d33'
        });
      }
    );
  }

}
