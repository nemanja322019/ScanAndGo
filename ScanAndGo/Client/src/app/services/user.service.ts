import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'ssl/environments/environment';
import { Observable, ReplaySubject, map, of, retry, tap } from 'rxjs';
import { Route, Router } from '@angular/router';
import { ResponseDto } from 'src/app/models/responseDto';
import { Page } from 'src/app/models/page';
import { ChangePasswordDTO, User } from '../models/user';

@Injectable({
  providedIn: 'root'
})

export class UserService {
  baseUrl = environment.apiUrl;
  private currentUserSource=new ReplaySubject<ResponseDto | null>(1);
  currentUser$=this.currentUserSource.asObservable();
  constructor(private http:HttpClient,private router:Router) { }


  getUsers():Observable<User[]>{
    return this.http.get<User[]>(`${this.baseUrl}user/get-all`);
  }

  getUsersPagination(pageNumber: number, pageSize: number):Observable<Page<User>>{
    return this.http.get<Page<User>>(`${this.baseUrl}user/get-all/${pageNumber}/${pageSize}`);
  }

  getStoreOwners():Observable<User[]>{
    return this.http.get<User[]>(`${this.baseUrl}user/get-all-store-owners`);
  }

  updateUser(user:User){
    return this.http.put<User>(`${this.baseUrl}user`, user);
  }
  deleteUser(userId?:number):Observable<any>{
    return this.http.delete(`${this.baseUrl}user/${userId}`);
  }

  changePassword(dto : ChangePasswordDTO){
    return this.http.post<any>(`${this.baseUrl}user/change-password`, dto);
  }

  getUserById(id: number):Observable<User>{
    return this.http.get<User>(`${this.baseUrl}user/${id}`);
  }

  
}

 

