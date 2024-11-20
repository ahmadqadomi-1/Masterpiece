import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { catchError, Observable, throwError } from 'rxjs';
import { HttpHeaders } from '@angular/common/http';


@Injectable({
  providedIn: 'root'
})
export class ServiceService {

  constructor(private http: HttpClient) { }

  staticData = "https://localhost:7118/api"

  getCategory(): Observable<any> {
    return this.http.get<any>(`${this.staticData}/Category/GetAllCategories`);

  }

  getCategoryById(id: string): Observable<any> {
    return this.http.get<any>(`${this.staticData}/Category/GetOneCategoryByID/${id}`);
  }


  addCategory(data: any): Observable<any> {
    return this.http.post<any>(`${this.staticData}/Category`, data);
  }

  EditCategory(id: string | null, productData: any) {
    return this.http.put(`${this.staticData}/Category/${id}`, productData);
  }

  deleteCategory(id: any): Observable<any> {

    return this.http.delete<any>(`${this.staticData}/Category/${id}`)
  }

  getProduct(): Observable<any> {
    return this.http.get<any>(`${this.staticData}/Product/GetAllProducts`);

  }

  getproductbycategoryid(id: string): Observable<any> {
    return this.http.get<any>(`${this.staticData}/Product/GetAllProductsForOneCategory/${id}`);
  }

  getProductById(id: string): Observable<any> {
    return this.http.get<any>(`${this.staticData}/Product/GetProductByID/${id}`);
  }

  EditProduct(id: string | null, productData: any) {
    return this.http.put(`${this.staticData}/Product/UpdateTheProductByID/${id}`, productData);
  }


  addProduct(data: FormData): Observable<any> {
    return this.http.post<any>(`${this.staticData}/Product/AddNewProduct`, data);
  }

  addProductToCategory(categoryId: string, data: FormData): Observable<any> {
    return this.http.post<any>(`${this.staticData}/Category/AddProductToCategory/${categoryId}`, data);
  }



  deleteProduct(id: any): Observable<any> {

    return this.http.delete<any>(`${this.staticData}/Product/DeleteProduct/${id}`)
  }


  getProject(): Observable<any> {
    return this.http.get<any>(`${this.staticData}/Project/GetAllProject`);

  }

  getProjectById(id: string): Observable<any> {
    return this.http.get<any>(`${this.staticData}/Project/GetProjectByID/${id}`);
  }

  EditProject(id: any, data: any): Observable<any> {
    return this.http.put<any>(`${this.staticData}/Project/UpdateProjectByID/${id}`, data)
  }

  addProject(data: any): Observable<any> {
    return this.http.post<any>(`${this.staticData}/Project/AddProject`, data);
  }


  deleteProject(id: any): Observable<any> {
    return this.http.delete<any>(`${this.staticData}/Project/DeleteProject/${id}`)
  }


  getTeam(): Observable<any> {
    return this.http.get<any>(`${this.staticData}/Teams/GetAllTeams`);

  }

  getTeamById(id: string): Observable<any> {
    return this.http.get<any>(`${this.staticData}/Teams/GetTeamByID/${id}`);
  }

  EditTeam(id: any, data: any): Observable<any> {
    return this.http.put<any>(`${this.staticData}/Teams/UpdateTeamByID/${id}`, data);
  }


  addTeam(data: any): Observable<any> {
    return this.http.post<any>(`${this.staticData}/Teams/AddTeam`, data);
  }

  deleteTeam(id: any): Observable<any> {

    return this.http.delete<any>(`${this.staticData}/Teams/DeleteTeam/${id}`)
  }


  getTiler(): Observable<any> {
    return this.http.get<any>(`${this.staticData}/Tiler/GetAllTilers`);

  }

  getTilerById(id: string): Observable<any> {
    return this.http.get<any>(`${this.staticData}/Tiler/GetTilerByID/${id}`);
  }

  EditTiler(id: any, data: any): Observable<any> {
    return this.http.put<any>(`${this.staticData}/Tiler/UpdateTilerByID/${id}`, data);
  }

  addTiler(data: any): Observable<any> {
    return this.http.post<any>(`${this.staticData}/Tiler/AddTiler`, data);
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

  getUser(): Observable<any> {
    return this.http.get<any>(`${this.staticData}/User/GetAllUsers`);

  }

  deletUser(id: any): Observable<any> {
    return this.http.delete(`${this.staticData}/User/Delete/${id}`, { responseType: 'text' });
  }



  getOredebyUserId(id: string): Observable<any> {
    return this.http.get<any>(`${this.staticData}/Order/GetOrdersByUserId/${id}`);
  }

  getAllOrders(): Observable<any> {
    return this.http.get<any>(`${this.staticData}/Order/GetAllOrders`);

  }

  getStatusById(id: string): Observable<any> {
    return this.http.get<any>(`${this.staticData}/Order/Status/${id}`);
  }

  deletOrder(id: any): Observable<any> {

    return this.http.delete<any>(`${this.staticData}/Order/DeleteOrder/${id}`)
  }

  EditStatus(id: any, statusName: string): Observable<any> {
    return this.http.put(`${this.staticData}/Order/UpdateOrderStatus/${id}`, JSON.stringify(statusName), {
      headers: { 'Content-Type': 'application/json' }
    });
  }

  LoginAdmin(data: any): Observable<any> {
    return this.http.post<any>(`${this.staticData}/Admin/login`, data);
  }













}
