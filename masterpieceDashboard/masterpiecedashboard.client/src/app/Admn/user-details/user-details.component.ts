import { Component, OnInit } from '@angular/core';
import { ServiceService } from '../../URL/service.service';
import { ActivatedRoute } from '@angular/router';
import Swal from 'sweetalert2'; // استيراد SweetAlert2

@Component({
  selector: 'app-user-details',
  templateUrl: './user-details.component.html',
  styleUrls: ['./user-details.component.css']
})
export class UserDetailsComponent implements OnInit {
  UserOrderArray: any;
  userId: string = '';

  constructor(private _ser: ServiceService, private route: ActivatedRoute) { }

  ngOnInit() {
    this.route.params.subscribe(params => {
      this.userId = params['id'];
      this.getAllUserOrder(this.userId);
    });
  }

  getAllUserOrder(userId: string) {
    this._ser.getOredebyUserId(userId).subscribe((data) => {
      this.UserOrderArray = data;
      console.log(this.UserOrderArray, "this.UserOrderArray");

      if (!this.UserOrderArray || this.UserOrderArray.length === 0) {
        Swal.fire({
          title: 'No Orders Found',
          text: 'There are no orders for this user.',
          icon: 'info',
          timer: 3000,
          timerProgressBar: true,
        });
      }
    }, (error) => {
      console.error('Error fetching orders:', error);

      Swal.fire({
        title: 'Notice',
        text: 'There are no orders for this user.',
        icon: 'warning',
        timer: 3000,
        timerProgressBar: true,
      });
    });
  }
}
