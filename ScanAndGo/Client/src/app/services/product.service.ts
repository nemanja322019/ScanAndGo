import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'ssl/environments/environment';
import { Observable, map, } from 'rxjs';
import { Page } from 'src/app/models/page';
import { Product, ProductDto } from 'src/app/models/product';


@Injectable({
  providedIn: 'root'
})

export class ProductService {
  baseUrl = environment.apiUrl;
  constructor(private http:HttpClient) { }

  getProducts(useCache = true): Observable<Product[]> {
    return this.http.get<Product[]>(this.baseUrl + 'product').pipe(
      map(response => {
        return response;
      })
    )
  }

  getProductsPagination(pageNumber : number, pageSize : number, useCache = true): Observable<Page<Product>> {
    return this.http.get<Page<Product>>(this.baseUrl + `product/get-all/${pageNumber}/${pageSize}`).pipe(
      map(response => {
        return response;
      })
    )
  }

  getStoreProductsPagination(storeId: number, pageNumber : number, pageSize : number, useCache = true): Observable<Page<Product>> {
    return this.http.get<Page<Product>>(this.baseUrl + `product/get-all-by-store-id/${storeId}/${pageNumber}/${pageSize}`).pipe(
      map(response => {
        return response;
      })
    )
  }

  addProduct(product:ProductDto){
    return this.http.post<ProductDto>(this.baseUrl + 'product',product);
  }
  updateProduct(product:ProductDto){
    return this.http.put<ProductDto>(this.baseUrl + 'product', product);
  }
  deleteProduct(productId?:number):Observable<any>{
    return this.http.delete(this.baseUrl + 'product/'+productId);
  }
}

 

