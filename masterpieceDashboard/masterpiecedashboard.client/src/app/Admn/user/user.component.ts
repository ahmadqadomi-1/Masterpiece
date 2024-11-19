import { Component } from '@angular/core';
import { ServiceService } from '../../URL/service.service';
import { Router } from '@angular/router';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-user',
  templateUrl: './user.component.html',
  styleUrls: ['./user.component.css'] // Fixed 'styleUrl' to 'styleUrls'
})
export class UserComponent {
  UserArray: any;

  constructor(private _ser: ServiceService) { }

  ngOnInit() {
    this.getAllUsers();
  }

  getAllUsers() {
    this._ser.getUser().subscribe((data) => {
      this.UserArray = data;
      console.log(this.UserArray, "this.UserArray");
    });
  }

  deleteUserById(id: any) {
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
        this._ser.deletUser(id).subscribe(
          (response: string) => { 
            Swal.fire(
              'Blocked!',
              response, 
              'success'
            );
            this.getAllUsers();
          },
          (error) => {
            Swal.fire(
              'Error',
              error.error || 'An error occurred while deleting the user.',
              'error'
            );
            console.error("Delete error details:", error);
          }
        );
      }
    });
  }






}
