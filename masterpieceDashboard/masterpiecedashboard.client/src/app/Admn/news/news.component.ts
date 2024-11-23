import { Component } from '@angular/core';
import { ServiceService } from '../../URL/service.service';
import { Router } from '@angular/router';
import Swal from 'sweetalert2';
import { DomSanitizer, SafeResourceUrl } from '@angular/platform-browser';

@Component({
  selector: 'app-news',
  templateUrl: './news.component.html',
  styleUrls: ['./news.component.css']
})
export class NewsComponent {
  NewsArray: any;

  constructor(
    private _ser: ServiceService,
    private router: Router,
    private sanitizer: DomSanitizer
  ) { }

  ngOnInit() {
    this.getAllNews();
  }

  getAllNews() {
    this._ser.getNews().subscribe((data) => {
      this.NewsArray = data;
      console.log(this.NewsArray, 'this.NewsArray');
    });
  }

  deleteNewsById(id: any) {
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
        this._ser.deleteNews(id).subscribe(
          () => {
            Swal.fire(
              'Deleted!',
              'This news has been deleted successfully.',
              'success'
            );
            this.getAllNews();
          },
          (error) => {
            if (error.status === 400) {
              Swal.fire(
                'Error',
                'Cannot delete this news because it contains related data.',
                'error'
              );
            } else {
              Swal.fire(
                'Error',
                'An error occurred while deleting the news. Please try again.',
                'error'
              );
            }
          }
        );
      }
    });
  }

  sanitizeUrl(youtubeUrl: string): SafeResourceUrl {
    const embedUrl = this.convertToEmbedUrl(youtubeUrl);
    return this.sanitizer.bypassSecurityTrustResourceUrl(embedUrl);
  }

  convertToEmbedUrl(url: string): string {
    return url.replace('watch?v=', 'embed/');
  }

  navigateToAddNews() {
    this.router.navigate(['/dashboard/AddNews']); 
  }
}
