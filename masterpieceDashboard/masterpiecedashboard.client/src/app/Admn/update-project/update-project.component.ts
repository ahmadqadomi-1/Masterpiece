import { Component } from '@angular/core';
import { ServiceService } from '../../URL/service.service';
import { ActivatedRoute } from '@angular/router';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-update-project',
  templateUrl: './update-project.component.html',
  styleUrl: './update-project.component.css'
})
export class UpdateProjectComponent {

  param: string | null = null;
  ProjectData: any = {};

  constructor(
    private _ser: ServiceService,
    private _active: ActivatedRoute
  ) { }

  ngOnInit() {
    this.param = this._active.snapshot.paramMap.get('id');
    console.log('Project ID:', this.param);


    if (this.param) {
      this._ser.getProjectById(this.param).subscribe(
        (data) => {
          console.log('Received Data from API:', data);
          this.ProjectData = data;
        },
        (error) => {
          console.error('Error fetching project data:', error);
        }
      );
    }
  }

  UpdateProject(data: any) {
    console.log('Form Data:', data);

    this._ser.EditProject(this.param, data).subscribe(
      () => {
        Swal.fire({
          icon: 'success',
          title: 'Success',
          text: 'The project has been updated successfully!',
          confirmButtonColor: '#3085d6'
        });
      },
      (error) => {
        console.error('Error:', error);
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
