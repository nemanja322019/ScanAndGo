import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { HttpClient } from '@angular/common/http';
import { OrderDto } from "src/app/models/order";
import { environment } from "ssl/environments/environment";
import { Page } from "src/app/models/page";


@Injectable({
    providedIn: 'root'
  })
  
  export class OrdersService {

    baseUrl = environment.apiUrl;

  constructor(private http:HttpClient) { }
  
    getOrders(pageNumber: number, pageSize: number, filter: string):Observable<Page<OrderDto>>{
      return this.http.get<Page<OrderDto>>(`${this.baseUrl}order/get-all/${pageNumber}/${pageSize}/${filter}`);
    }

    getOrdersByStoreId(storeId: number, pageNumber: number, pageSize: number, filter: string):Observable<Page<OrderDto>>{
      return this.http.get<Page<OrderDto>>(`${this.baseUrl}order/get-all/store/${storeId}/${pageNumber}/${pageSize}/${filter}`);
    }

    getOrderById(id: number):Observable<OrderDto>{
      return this.http.get<OrderDto>(`${this.baseUrl}order/${id}`);
    }
    
    downloadOrders(){
      return this.http.get(this.baseUrl+`order/download`, {responseType: 'blob'}).subscribe((response: Blob) => {
        const file = new Blob([response], {type: 'application/octet-stream'})
        const fileName = "orders.xlsx"
        const a = document.createElement('a')
        const objectUrl = URL.createObjectURL(file)
        a.href = objectUrl
        a.download = fileName
        a.click()
        URL.revokeObjectURL(objectUrl)
      })
    }

    deleteOrder(id: number){
      return this.http.delete(this.baseUrl+`order/${id}`)
    }

    payOrder(id: number) {
      return this.http.get<any>(`${this.baseUrl}order/payOrder/${id}`);
    }

  }