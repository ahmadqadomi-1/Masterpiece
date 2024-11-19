import { Component } from '@angular/core';
import Swal from 'sweetalert2';
import { ServiceService } from '../../URL/service.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-project',
  templateUrl: './project.component.html',
  styleUrls: ['./project.component.css'] 
})
export class ProjectComponent {
  ProjectArray: any;

  constructor(private _ser: ServiceService, private router: Router) { }

  ngOnInit() {
    this.getAllProject();
  }

  getAllProject() {
    this._ser.getProject().subscribe((data) => {
      
      this.ProjectArray = data.map((project: any) => {
        const date = new Date(project.projectDate);
        project.projectDate = `${date.getMonth() + 1}/${date.getDate()}/${date.getFullYear()}, ${date.getHours().toString().padStart(2, '0')}:${date.getMinutes().toString().padStart(2, '0')}`;
        return project;
      });
      console.log(this.ProjectArray, "this.ProjectArray");
    });
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

  navigateToAddProject() {
    this.router.navigate(['/dashboard/AddProject']);
  }
}
