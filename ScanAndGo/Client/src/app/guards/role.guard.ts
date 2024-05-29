import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot, UrlTree } from '@angular/router';
import { AuthService } from '../services/auth.service';

@Injectable({
  providedIn: 'root'
})
export class RoleGuard implements CanActivate {
  constructor(private authService: AuthService, private router: Router){

  }
  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean {
          var routeRoles = []
          const role = this.authService.getRole()
          if (!!route.data['role']) {
            routeRoles = route.data['role'];
            if (routeRoles.includes(role)) {
            return true;
            }
        }

        if (role == "Admin") this.router.navigateByUrl('/admin/users'); 
        if (role == "Seller") this.router.navigateByUrl('/seller/my-store'); 
        if (role == "StoreOwner") this.router.navigateByUrl('/store-owner/stores'); 
        return false;
  }
  
}