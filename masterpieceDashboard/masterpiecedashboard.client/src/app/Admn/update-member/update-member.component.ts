import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ServiceService } from '../../URL/service.service';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-update-member',
  templateUrl: './update-member.component.html',
  styleUrls: ['./update-member.component.css'],
})
export class UpdateMemberComponent implements OnInit {
  param: string | null = null;
  memberData: any = {
    teamName: '',
    teamImg: '',
    profession: '',
    teamPhoneNum: '',
  };
  image: any;

  constructor(private _ser: ServiceService, private _active: ActivatedRoute) { }

  ngOnInit() {
    this.param = this._active.snapshot.paramMap.get('id');
    if (this.param) {
      this._ser.getTeamById(this.param).subscribe(
        (data) => {
          this.memberData = data;
        },
        (error) => {
          Swal.fire({
            icon: 'error',
            title: 'Error',
            text: 'Error fetching member details.',
            confirmButtonColor: '#d33',
          });
        }
      );
    }
  }

  imageChange(event: any) {
    this.image = event.target.files[0];
  }

  UpdateMember() {
    if (!this.param) {
      Swal.fire({
        icon: 'error',
        title: 'Error',
        text: 'Invalid member ID.',
        confirmButtonColor: '#d33',
      });
      return;
    }

    const formData = new FormData();
    for (let key in this.memberData) {
      formData.append(key, this.memberData[key]);
    }

    if (this.image) {
      formData.append('teamImg', this.image);
    }

    this._ser.EditTeam(this.param, formData).subscribe(
      () => {
        Swal.fire({
          icon: 'success',
          title: 'Success',
          text: 'The member has been updated successfully!',
          confirmButtonColor: '#3085d6',
        });
      },
      (error) => {
        Swal.fire({
          icon: 'error',
          title: 'Error',
          text: 'An error occurred while updating the member.',
          confirmButtonColor: '#d33',
        });
      }
    );
  }
}
