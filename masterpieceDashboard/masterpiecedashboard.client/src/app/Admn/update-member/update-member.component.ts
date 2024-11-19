import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ServiceService } from '../../URL/service.service';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-update-member',
  templateUrl: './update-member.component.html',
  styleUrls: ['./update-member.component.css'] 
})
export class UpdateMemberComponent implements OnInit {
  param: string | null = null;
  memberData: any = {
    teamName: '',
    teamImg: '',
    profession: '',
    teamPhoneNum: ''
  };

  constructor(
    private _ser: ServiceService,
    private _active: ActivatedRoute
  ) { }

  ngOnInit() {
    this.param = this._active.snapshot.paramMap.get('id');
    console.log('Member ID:', this.param);

    if (this.param) {
      this._ser.getTeamById(this.param).subscribe(
        (data) => {
          console.log('Received Data from API:', data);
          this.memberData = data; 
        },
        (error) => {
          console.error('Error fetching member data:', error);
        }
      );
    }
  }

  UpdateMember() {
    console.log('Form Data:', this.memberData);

    // Ensure that the member data and ID are valid
    if (this.param && this.memberData) {
      this._ser.EditTeam(this.param, this.memberData).subscribe(
        () => {
          Swal.fire({
            icon: 'success',
            title: 'Success',
            text: 'The member has been updated successfully!',
            confirmButtonColor: '#3085d6'
          });
        },
        (error) => {
          console.error('Error:', error);
          Swal.fire({
            icon: 'error',
            title: 'Error',
            text: 'An error occurred while updating the member.',
            confirmButtonColor: '#d33'
          });
        }
      );
    } else {
      Swal.fire({
        icon: 'warning',
        title: 'Warning',
        text: 'Member data is missing. Please try again.',
        confirmButtonColor: '#d33'
      });
    }
  }

}
