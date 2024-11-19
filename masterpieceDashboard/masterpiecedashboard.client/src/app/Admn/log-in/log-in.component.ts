import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { ServiceService } from '../../URL/service.service';
import { BehaviorSubjectService } from '../BehaviorSubject/behavior-subject.service';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-log-in',
  templateUrl: './log-in.component.html',
  styleUrls: ['./log-in.component.css']
})
export class LogInComponent {
  email: string = ''; // خاصية لحفظ البريد الإلكتروني
  password: string = ''; // خاصية لحفظ كلمة المرور

  constructor(
    private _ser: ServiceService,
    private _router: Router,
    private behaviorSubjectService: BehaviorSubjectService
  ) { }

  ngOnInit() {
    // استرجاع البريد الإلكتروني وكلمة المرور من التخزين
    const savedEmail = localStorage.getItem('email');
    const savedPassword = sessionStorage.getItem('password');

    if (savedEmail) {
      this.email = savedEmail;
    }

    if (savedPassword) {
      this.password = savedPassword;
    }
  }

  saveCredentials() {
    // حفظ البريد الإلكتروني وكلمة المرور
    if (this.email) {
      localStorage.setItem('email', this.email);
    }
    if (this.password) {
      sessionStorage.setItem('password', this.password);
    }
  }

  Login(data: any) {
    // حفظ البريد الإلكتروني وكلمة المرور قبل تسجيل الدخول
    if (data.email) {
      this.email = data.email;
    }
    if (data.password) {
      this.password = data.password;
    }
    this.saveCredentials();

    const form = new FormData();
    for (let key in data) {
      form.append(key, data[key]);
    }

    console.log("Login Data:", data);

    this._ser.LoginAdmin(form).subscribe(
      (response) => {
        console.log("Response from LoginUser:", response);
        if (response.userId) {
          this.behaviorSubjectService.setUserId(response.userId);
          Swal.fire({
            icon: 'success',
            title: 'Success',
            text: 'Admin Logged In Successfully',
            timer: 1500,
            showConfirmButton: false
          }).then(() => {
            this._router.navigate(['/dashboard/catgory']);
          });
        } else {
          console.warn("User ID not found in response.");
          Swal.fire({
            icon: 'warning',
            title: 'Warning',
            text: 'User ID not found in response.'
          });
        }
      },
      (error) => {
        console.error("Login Error:", error);
        Swal.fire({
          icon: 'error',
          title: 'Login Failed',
          text: error.error?.message || 'An error occurred.'
        });
      }
    );
  }
}
