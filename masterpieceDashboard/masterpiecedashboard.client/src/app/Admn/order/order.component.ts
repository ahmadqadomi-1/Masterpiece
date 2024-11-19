import { Component } from '@angular/core';
import { ServiceService } from '../../URL/service.service';
import { ActivatedRoute } from '@angular/router';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-order',
  templateUrl: './order.component.html',
  styleUrl: './order.component.css'
})
export class OrderComponent {
  OrderArray: any;
  

  constructor(private _ser: ServiceService, private route: ActivatedRoute) { }

  ngOnInit() {
      this.getAllOrder();
  }

  getAllOrder() {
    this._ser.getAllOrders().subscribe((data) => {
      this.OrderArray = data;
      console.log(this.OrderArray, "this.OrderArray");
    });
  }

  deleteOrderById(id: any) {
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
        this._ser.deletOrder(id).subscribe(
          () => {
            Swal.fire(
              'Deleted!',
              'This order has been deleted successfully.',
              'success'
            );
            this.getAllOrder();
          },
          (error) => {
            console.error("Error:", error);
            Swal.fire(
              'Error',
              error.error || 'An error occurred while deleting the order.',
              'error'
            );
          }
        );
      }
    });
  }

}
