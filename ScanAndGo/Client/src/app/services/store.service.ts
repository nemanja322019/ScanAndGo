import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'ssl/environments/environment';
import { Observable, map, } from 'rxjs';
import { Store, StoreDto } from 'src/app/models/store';
import { Page } from 'src/app/models/page';


@Injectable({
  providedIn: 'root'
})

export class StoreService {
  baseUrl = environment.apiUrl;
  constructor(private http:HttpClient) { }

  getStoresPagination(pageNumber : number, pageSize : number, pageuseCache = true): Observable<Page<Store>> {
    return this.http.get<Page<Store>>(this.baseUrl + `store/get-all/${pageNumber}/${pageSize}`).pipe(
      map(response => {
        return response;
      })
    )
  }

  getStores(pageuseCache = true): Observable<Store[]> {
    return this.http.get<Store[]>(this.baseUrl + `store`).pipe(
      map(response => {
        return response;
      })
    )
  }
  addStore(store:StoreDto){
    return this.http.post<StoreDto>(this.baseUrl + 'store',store);
  }
  updateStore(store:StoreDto){
    return this.http.put<StoreDto>(this.baseUrl + 'store', store);
  }
  deleteStore(storeId?:number):Observable<any>{
    return this.http.delete(this.baseUrl + 'store/'+storeId);
  }

  getOwnedStores(pageuseCache = true): Observable<Store[]> {
    return this.http.get<Store[]>(this.baseUrl + `store/store-owner`);
  }

  getStoreById(id: number): Observable<Store> {
    return this.http.get<Store>(this.baseUrl + `store/${id}`).pipe(
      map(response => {
        return response;
      })
    )
  }
}

 

