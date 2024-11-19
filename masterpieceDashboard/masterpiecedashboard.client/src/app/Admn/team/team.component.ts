import { Component } from '@angular/core';
import Swal from 'sweetalert2';
import { ServiceService } from '../../URL/service.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-team',
  templateUrl: './team.component.html',
  styleUrl: './team.component.css'
})
export class TeamComponent {
  ngOnInit() {
    this.getAllTeam()
  }

  constructor(private _ser: ServiceService, private router: Router) { }

  TeamArray: any
  getAllTeam() {
    this._ser.getTeam().subscribe((data) => {
      this.TeamArray = data
      console.log(this.TeamArray, "this.TeamArray")
    })
  }

  deleteTeamById(id: any) {
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
        this._ser.deleteTeam(id).subscribe(() => {
          Swal.fire(
            'Deleted!',
            'This message has been deleted successfully.',
            'success'
          );
          this.getAllTeam();
        });
      }
    });
  }

  navigateToAddMember() {
    this.router.navigate(['/dashboard/AddMember']);
  }
}
