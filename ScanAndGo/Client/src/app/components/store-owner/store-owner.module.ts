import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import {HttpClientModule} from '@angular/common/http';
import { ReactiveFormsModule } from '@angular/forms';
import { NgxPaginationModule } from 'ngx-pagination';
import {MatIconModule} from '@angular/material/icon';
import { FormsModule } from '@angular/forms';
import { StoreOwnerStoreComponent } from './store/store-owner-store.component';
import { StoreOwnerComponent } from './store-owner.component';
import { StoreOwnerOrdersComponent } from './orders/store-owner-orders.component';
import { StoreOwnerProductComponent } from './product/store-owner-product.component';
import { StoreOwnerProductDialogComponent } from './product/product-dialog/store-owner-product-dialog.component';
import { StoreOwnerStoreSellersDialogComponent } from './store/store-sellers-dialog/store-owner-store-sellers-dialog.component';
import { StoreOwnerOrderItemsDialogComponent } from './orders/order-items-dialog/store-owner-order-items-dialog.component';


@NgModule({
  declarations: [ 
    StoreOwnerComponent, 
    StoreOwnerStoreComponent,
    StoreOwnerStoreSellersDialogComponent,
    StoreOwnerOrdersComponent,
    StoreOwnerOrderItemsDialogComponent,
    StoreOwnerProductComponent,
    StoreOwnerProductDialogComponent
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
    StoreOwnerComponent
  ],
  providers: [
    
  ],
  bootstrap: [StoreOwnerComponent]
})
export class StoreOwnerModule { }
