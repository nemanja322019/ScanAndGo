import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { AuthGuard } from 'src/app/guards/auth.guard';
import { AdminComponent } from './components/admin/admin.component';
import { OrdersComponent } from './components/admin/orders/orders.component';
import { ProductComponent } from './components/admin/product/product.component';
import { StoreComponent } from './components/admin/store/store.component';
import { UserComponent } from './components/admin/user/user.component';
import { LoginComponent } from './components/auth/login/login.component';
import { StoreOwnerStoreComponent } from './components/store-owner/store/store-owner-store.component';
import { StoreOwnerProductComponent } from './components/store-owner/product/store-owner-product.component';
import { SellerStoreComponent } from './components/seller/store/seller-store.component';
import { SellerProductComponent } from './components/seller/product/seller-product.component';
import { StoreOwnerOrdersComponent } from './components/store-owner/orders/store-owner-orders.component';
import { RoleGuard } from './guards/role.guard';
import { ChangePasswordComponent } from './components/auth/change-password/change-password.component';


const routes: Routes = [
  {path: '', component: LoginComponent},
  {path: 'login', component: LoginComponent},
  {path: 'admin', component: AdminComponent, canActivate:[AuthGuard, RoleGuard], data: {role: ['Admin']}},
  {path: 'admin/users',component: UserComponent, canActivate:[AuthGuard, RoleGuard], data: {role: ['Admin']}},
  {path: 'admin/stores',component: StoreComponent, canActivate:[AuthGuard, RoleGuard], data: {role: ['Admin']}},
  {path: 'admin/products',component: ProductComponent, canActivate:[AuthGuard, RoleGuard], data: {role: ['Admin']}},
  {path: 'admin/orders',component: OrdersComponent, canActivate:[AuthGuard, RoleGuard], data: {role: ['Admin']}},
  {path: 'store-owner/stores', component: StoreOwnerStoreComponent, canActivate:[AuthGuard, RoleGuard], data: {role: ['StoreOwner']}},
  {path: 'store-owner/products', component: StoreOwnerProductComponent, canActivate:[AuthGuard, RoleGuard], data: {role: ['StoreOwner']}},
  {path: 'store-owner/orders', component: StoreOwnerOrdersComponent, canActivate:[AuthGuard, RoleGuard], data: {role: ['StoreOwner']}},
  {path: 'seller/my-store', component: SellerStoreComponent, canActivate:[AuthGuard, RoleGuard], data: {role: ['Seller']}},
  {path: 'seller/products', component: SellerProductComponent, canActivate:[AuthGuard, RoleGuard], data: {role: ['Seller']}},
  {path: 'change-password', component: ChangePasswordComponent, canActivate: [AuthGuard]},
  {path: '**', redirectTo:'', pathMatch:'full'},
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
