import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import {HttpClientModule} from '@angular/common/http';
import { ReactiveFormsModule } from '@angular/forms';
import { NgxPaginationModule } from 'ngx-pagination';
import {MatIconModule} from '@angular/material/icon';
import { FormsModule } from '@angular/forms';
import { AdminComponent } from './admin.component';
import { UserDialogComponent } from './user/user-dialog/user-dialog.component';
import { UserComponent } from './user/user.component';
import { OrdersComponent } from './orders/orders.component';
import { ProductDialogComponent } from './product/product-dialog/product-dialog.component';
import { ProductComponent } from './product/product.component';
import { StoreDialogComponent } from './store/store-dialog/store-dialog.component';
import { StoreComponent } from './store/store.component';
import { StoreProductsDialogComponent } from './store/store-products-dialog/store-products-dialog.component';
import { StoreSellersDialogComponent } from './store/store-sellers/store-sellers-dialog.component';
import { UserOwnedStoresDialogComponent } from './user/user-owned-stores-dialog/user-owned-stores-dialog.component';
import { OrderItemsDialogComponent } from './orders/order-items-dialog/order-items-dialog.component';


@NgModule({
  declarations: [  
    AdminComponent,
    UserComponent,
    UserDialogComponent,
    UserOwnedStoresDialogComponent,
    StoreComponent,
    StoreDialogComponent,
    StoreProductsDialogComponent,
    StoreSellersDialogComponent,
    ProductComponent,
    ProductDialogComponent,
    OrdersComponent,
    OrderItemsDialogComponent
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    HttpClientModule,
    ReactiveFormsModule,
    FormsModule,
    NgxPaginationModule,
    MatIconModule
  ],
  exports:[
    AdminComponent
  ],
  providers: [
    
  ],
  bootstrap: [AdminComponent]
})
export class AdminModule { }
