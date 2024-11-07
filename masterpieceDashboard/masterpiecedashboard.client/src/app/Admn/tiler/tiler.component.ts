import { Component } from '@angular/core';
import Swal from 'sweetalert2';
import { ServiceService } from '../../URL/service.service';

@Component({
  selector: 'app-tiler',
  templateUrl: './tiler.component.html',
  styleUrl: './tiler.component.css'
})
export class TilerComponent {
  ngOnInit() {
    this.getAllTiler()
  }

  constructor(private _ser: ServiceService) {

  }

  TilerArray: any
  getAllTiler() {
    this._ser.getTiler().subscribe((data) => {
      this.TilerArray = data
      console.log(this.TilerArray, "this.TilerArray")
    })
  }

  deleteTilerById(id: any) {
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
        this._ser.deleteTiler(id).subscribe(() => {
          Swal.fire(
            'Deleted!',
            'This message has been deleted successfully.',
            'success'
          );
          this.getAllTiler();
        });
      }
    });
  }



}
