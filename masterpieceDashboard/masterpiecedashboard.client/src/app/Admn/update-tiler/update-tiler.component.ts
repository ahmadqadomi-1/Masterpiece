import { Component, OnInit } from '@angular/core';
import { ServiceService } from '../../URL/service.service';
import { ActivatedRoute } from '@angular/router';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-update-tiler',
  templateUrl: './update-tiler.component.html',
  styleUrl: './update-tiler.component.css'
})
export class UpdateTilerComponent implements OnInit {
  param: string | null = null;
  tilerData: any = {
    tilerName: '',
    tilerImg: '',
    profession: '',
    tilerPhoneNum: ''
  };

  constructor(
    private _ser: ServiceService,
    private _active: ActivatedRoute
  ) { }

  ngOnInit() {
    this.param = this._active.snapshot.paramMap.get('id');
    console.log('Tiler ID:', this.param);

    if (this.param) {
      this._ser.getTilerById(this.param).subscribe(
        (data) => {
          console.log('Received Data from API:', data);
          this.tilerData = data;
        },
        (error) => {
          console.error('Error fetching Tiler data:', error);
        }
      );
    }
  }


  UpdateTiler() {
    console.log('Form Data:', this.tilerData);
    if (this.param && this.tilerData) {
      this._ser.EditTiler(this.param, this.tilerData).subscribe(
        () => {
          Swal.fire({
            icon: 'success',
            title: 'Success',
            text: 'The tiler has been updated successfully!',
            confirmButtonColor: '#3085d6'
          });
        },
        (error) => {
          console.error('Error:', error);
          Swal.fire({
            icon: 'error',
            title: 'Error',
            text: 'An error occurred while updating the tiler.',
            confirmButtonColor: '#d33'
          });
        }
      );
    } else {
      Swal.fire({
        icon: 'warning',
        title: 'Warning',
        text: 'Tiler data is missing. Please try again.',
        confirmButtonColor: '#d33'
      });
    }
  }


}
