import { Component } from '@angular/core';
import { ServiceService } from '../../URL/service.service';
import { ActivatedRoute } from '@angular/router';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-update-order',
  templateUrl: './update-order.component.html',
  styleUrls: ['./update-order.component.css']
})
export class UpdateOrderComponent {
  param: string | null = null;
  StatusData = {
    orderStatus: ''
  };

  statusOptions = ['Pending', 'Approved', 'InPacking', 'Shipping', 'Delivered'];

  constructor(
    private _ser: ServiceService,
    private _active: ActivatedRoute
  ) { }

  ngOnInit() {
    this.param = this._active.snapshot.paramMap.get('id');
    console.log('Order ID:', this.param);

    if (this.param) {
      this._ser.getStatusById(this.param).subscribe(
        (data) => {
          console.log('Received Data from API:', data);
          if (data && data.orderStatus) {
            this.StatusData.orderStatus = data.orderStatus;
          }
        },
        (error) => {
          console.error('Error fetching order data:', error);
        }
      );
    }
  }

  UpdateStatus(data: any) {
    const statusName = data.orderStatus;
    console.log('Status Name:', statusName);

    if (!statusName) {
      console.error('Status Name is undefined or empty.');
      Swal.fire({
        icon: 'error',
        title: 'Error',
        text: 'Order status is undefined. Please select a valid status.',
        confirmButtonColor: '#d33'
      });
      return;
    }

    this._ser.EditStatus(this.param, statusName).subscribe(
      () => {
        Swal.fire({
          icon: 'success',
          title: 'Success',
          text: 'The order status has been updated successfully!',
          confirmButtonColor: '#3085d6'
        }).then(() => {
          // إضافة رسالة تحذير للمستخدم بأن البريد الإلكتروني قد تم إرساله
          Swal.fire({
            icon: 'info',
            title: 'Email Sent',
            text: 'An email notification has been sent to the user with the updated order status.',
            confirmButtonColor: '#3085d6'
          });
        });
      },
      (error) => {
        console.error('Error:', error);

        let errorMessage = 'An error occurred while updating the status.';
        if (error && error.error && typeof error.error === 'string') {
          errorMessage = error.error;
        }

        Swal.fire({
          icon: 'error',
          title: 'Error',
          text: errorMessage,
          confirmButtonColor: '#d33'
        });
      }
    );
  }

}
