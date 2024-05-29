import { HttpClient,  } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'ssl/environments/environment';
import {  Observable, ReplaySubject, map} from 'rxjs';
import { User } from 'src/app/models/user';
import { Router } from '@angular/router';
import { ResponseDto } from 'src/app/models/responseDto';
import { JwtHelperService } from '@auth0/angular-jwt';

@Injectable({
  providedIn: 'root'
})

export class AuthService {
  baseUrl = environment.apiUrl;
  private currentUserSource=new ReplaySubject<ResponseDto | null>(1);
  private userPayload: any
  constructor(private http:HttpClient,private router:Router) { 
  }

  registerUser(user:User){
    return this.http.post<User>(`${this.baseUrl}auth/registration`,user);
  }

  login(email: string, password: string): Observable<any> {
    const loginData = { email, password };
    return this.http.post<ResponseDto>(`${this.baseUrl}auth/login`, loginData).pipe(
      map(user=>{
        localStorage.setItem('token',user.token);
        this.userPayload = this.decodeToken()
        this.currentUserSource.next(user);
        return user;
      })
    )
  }
 
  sendResetPasswordLink(email:string){
    return this.http.post<any>(`${this.baseUrl}auth/send-reset-email/${email}`,{});
  }
 
  logout() {
    localStorage.removeItem('token');
    this.currentUserSource.next(null);
    this.router.navigateByUrl('/'); 
  }

  loadCurrentUser(){
    return this.http.get<User>(`${this.baseUrl}auth/current`);
  }

  getToken(){
    return localStorage.getItem('token')
  }

  decodeToken(){
    const jwtHelper = new JwtHelperService()
    const token = this.getToken()!
    return jwtHelper.decodeToken(token)
  }

  getRole(){
    this.userPayload = this.decodeToken()
    return this.userPayload['http://schemas.microsoft.com/ws/2008/06/identity/claims/role']
  }
}

 

