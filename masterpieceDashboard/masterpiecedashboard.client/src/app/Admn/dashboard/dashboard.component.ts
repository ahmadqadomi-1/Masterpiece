import { Component } from '@angular/core';
import { BehaviorSubjectService } from '../BehaviorSubject/behavior-subject.service';
import Swal from 'sweetalert2';
import { Router } from '@angular/router';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrl: './dashboard.component.css'
})
export class DashboardComponent {
  //private openedDropdowns: Set<string> = new Set();
  isLoggedIn: boolean = false;

  constructor(private behaviorSubjectService: BehaviorSubjectService, private _router: Router) { }

  //toggleDropdown(menuId: string) {
  //  if (this.openedDropdowns.has(menuId)) {
  //    this.openedDropdowns.delete(menuId);
  //  } else {
  //    this.openedDropdowns.clear();
  //    this.openedDropdowns.add(menuId);
  //  }
  //}

  //isDropdownOpen(menuId: string): boolean {
  //  return this.openedDropdowns.has(menuId);
  //}
  ngOnInit() {
    this.behaviorSubjectService.userId$.subscribe(userId => {
      this.isLoggedIn = !!userId;
      if (!this.isLoggedIn) {
        this._router.navigate(['/LogIn']);
      }
    });
  }

  logout() {
    this.behaviorSubjectService.setUserId('');
    Swal.fire({
      icon: 'success',
      title: 'Logged Out',
      text: 'Logged out successfully.',
      confirmButtonText: 'OK'
    });
    this._router.navigate(['/LogIn']);
  }

}
