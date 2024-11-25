import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, Subject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ProductService {

  cartUpdated: Subject<boolean> = new Subject<boolean>();

  constructor(private http: HttpClient) { }

  getAllProductsByTitle(title: string): Observable<any[]> {
    return this.http.get<any[]>(`https://localhost:7279/api/Games?title=${title}`);
  }

  getAllProducts(): Observable<any[]> {
    return this.http.get<any[]>("https://localhost:7279/api/Games");
  }

  getAllProductsByCategory(id: number): Observable<any[]> {
    return this.http.get<any[]>("https://localhost:7279/api/GameCategories/category/" + id);
  }

  getAllCategory(): Observable<any[]> {
    return this.http.get<any[]>("https://localhost:7279/api/GameCategories");
  }

  register(obj: any): Observable<any> {
    return this.http.post<any>("https://localhost:7279/api/Register", obj);
  }

  login(obj: any): Observable<any> {
    return this.http.post<any>("https://localhost:7279/api/Register/logins", obj);
  }

  // Create a new product
  createProduct(obj: any): Observable<any> {
    return this.http.post<any>("https://localhost:7279/api/Games", obj);
  }

  // Update an existing product by ID
  updateProduct(id: number, obj: any): Observable<any> {
    return this.http.put<any>("https://localhost:7279/api/Games/" + id, obj);
  }

  // Delete a product by ID
  deleteProduct(id: number): Observable<any> {
    return this.http.delete<any>("https://localhost:7279/api/Games/" + id);
  }

  addtoCart(obj: any): Observable<any> {
    return this.http.post<any>("https://localhost:7279/api/AddToCart", obj);
  }

  getAddtocartdataByCust(id: number): Observable<any[]> {
    return this.http.get<any[]>("https://localhost:7279/api/AddToCart/GetCartGameByCustomerId/" + id);
  }


  removeProductFromCart(cartId: number): Observable<any[]> {
    return this.http.delete<any[]>("https://localhost:7279/api/AddToCart/" + cartId);
  }

  PlaceOrder(obj: any): Observable<any[]> {
    return this.http.post<any[]>("https://localhost:7279/api/Checkout", obj);
  }

}



