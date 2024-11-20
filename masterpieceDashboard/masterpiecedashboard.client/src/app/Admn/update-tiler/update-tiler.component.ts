import { Component, OnInit } from '@angular/core';
import { ServiceService } from '../../URL/service.service';
import { ActivatedRoute } from '@angular/router';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-update-tiler',
  templateUrl: './update-tiler.component.html',
  styleUrls: ['./update-tiler.component.css']
})
export class UpdateTilerComponent implements OnInit {
  param: string | null = null;
  tilerData: any = {
    tilerName: '',
    profession: '',
    tilerPhoneNum: ''
  };
  selectedFile: File | null = null;

  constructor(private _ser: ServiceService, private _active: ActivatedRoute) { }

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

  onFileSelected(event: any) {
    const file = event.target.files[0];
    if (file) {
      this.selectedFile = file;
    }
  }

  UpdateTiler() {
    if (!this.param || !this.tilerData) {
      Swal.fire({
        icon: 'warning',
        title: 'Warning',
        text: 'Tiler data is missing. Please try again.',
        confirmButtonColor: '#d33'
      });
      return;
    }

    const formData = new FormData();
    formData.append('tilerName', this.tilerData.tilerName);
    formData.append('profession', this.tilerData.profession);
    formData.append('tilerPhoneNum', this.tilerData.tilerPhoneNum);

    if (this.selectedFile) {
      formData.append('tilerImg', this.selectedFile);
    }

    this._ser.EditTiler(this.param, formData).subscribe(
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
  }
}
