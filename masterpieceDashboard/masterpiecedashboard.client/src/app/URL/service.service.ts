import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ServiceService {

  constructor(private http: HttpClient) { }

  staticData = "https://localhost:7118/api"

  getCategory(): Observable<any> {
    return this.http.get<any>(`${this.staticData}/Category/GetAllCategories`);

  }

  deleteCategory(id: any): Observable<any> {

    return this.http.delete<any>(`${this.staticData}/Category/${id}`)
  }

  getProduct(): Observable<any> {
    return this.http.get<any>(`${this.staticData}/Product/GetAllProducts`);

  }

  deleteProduct(id: any): Observable<any> {

    return this.http.delete<any>(`${this.staticData}/Product/DeleteProduct/${id}`)
  }


  getProject(): Observable<any> {
    return this.http.get<any>(`${this.staticData}/Project/GetAllProject`);

  }

  deleteProject(id: any): Observable<any> {

    return this.http.delete<any>(`${this.staticData}/Project/DeleteProject/${id}`)
  }


  getTeam(): Observable<any> {
    return this.http.get<any>(`${this.staticData}/Teams/GetAllTeams`);

  }

  deleteTeam(id: any): Observable<any> {

    return this.http.delete<any>(`${this.staticData}/Teams/DeleteTeam/${id}`)
  }


  getTiler(): Observable<any> {
    return this.http.get<any>(`${this.staticData}/Tiler/GetAllTilers`);

  }

  deleteTiler(id: any): Observable<any> {

    return this.http.delete<any>(`${this.staticData}/Tiler/DeleteTiler/${id}`)
  }

  getContact(): Observable<any> {
    return this.http.get<any>(`${this.staticData}/ContactUs/GetByDesc`);

  }

  deletContact(id: any): Observable<any> {

    return this.http.delete<any>(`${this.staticData}/ContactUs/DeleteContact/${id}`)
  }


}
