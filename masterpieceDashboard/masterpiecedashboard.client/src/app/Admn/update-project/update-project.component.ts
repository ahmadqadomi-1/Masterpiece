import { Component } from '@angular/core';
import { ServiceService } from '../../URL/service.service';
import { ActivatedRoute } from '@angular/router';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-update-project',
  templateUrl: './update-project.component.html',
  styleUrls: ['./update-project.component.css']
})
export class UpdateProjectComponent {

  param: string | null = null;
  ProjectData: any = {};
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

  constructor(
    private _ser: ServiceService,
    private _active: ActivatedRoute
  ) { }

  ngOnInit() {
    this.param = this._active.snapshot.paramMap.get('id');
    if (this.param) {
      this._ser.getProjectById(this.param).subscribe(
        (data) => {
          this.ProjectData = data;
        },
        (error) => {
          console.error('Error fetching project data:', error);
        }
      );
    }
  }

  imageChange(e: any) {
    this.image = e.target.files[0];
  }

  UpdateProject(data: any) {
    const formData = new FormData();
    for (let key in data) {
      formData.append(key, data[key]);
    }

    if (this.image) {
      formData.append("projectImage", this.image);
    }

    if (this.param) {
      this._ser.EditProject(this.param, formData).subscribe(
        () => {
          Swal.fire({
            icon: 'success',
            title: 'Success',
            text: 'The project has been updated successfully!',
            confirmButtonColor: '#3085d6'
          });
        },
        (error) => {
          Swal.fire({
            icon: 'error',
            title: 'Error',
            text: 'An error occurred while updating the project.',
            confirmButtonColor: '#d33'
          });
        }
      );
    }
  }
}
