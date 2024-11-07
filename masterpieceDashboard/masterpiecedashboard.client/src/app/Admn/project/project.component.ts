import { Component } from '@angular/core';
import Swal from 'sweetalert2';
import { ServiceService } from '../../URL/service.service';

@Component({
  selector: 'app-project',
  templateUrl: './project.component.html',
  styleUrl: './project.component.css'
})
export class ProjectComponent {
  ngOnInit() {
    this.getAllProject()
  }
  constructor(private _ser: ServiceService) {

  }

  ProjectArray: any
  getAllProject() {
    this._ser.getProject().subscribe((data) => {
      this.ProjectArray = data
      console.log(this.ProjectArray, "this.ProjectArray")
    })
  }

  deleteProjectById(id: any) {
    Swal.fire({
      title: 'Are you sure?',
      text: "You won't be able to revert this!",
      icon: 'warning',
      showCancelButton: true,
      confirmButtonColor: '#3085d6',
      cancelButtonColor: '#d33',
      confirmButtonText: 'Yes, delete it!'
    }).then((result) => {
      if (result.isConfirmed) {
        this._ser.deleteProject(id).subscribe(() => {
          Swal.fire(
            'Deleted!',
            'This Project has been deleted successfully.',
            'success'
          );
          this.getAllProject();
        });
      }
    });
  }

}
