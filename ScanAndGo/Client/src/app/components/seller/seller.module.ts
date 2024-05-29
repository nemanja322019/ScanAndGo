import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import {HttpClientModule} from '@angular/common/http';
import { ReactiveFormsModule } from '@angular/forms';
import { NgxPaginationModule } from 'ngx-pagination';
import {MatIconModule} from '@angular/material/icon';
import { FormsModule } from '@angular/forms';
import { SellerComponent } from './seller.component';
import { SellerStoreComponent } from './store/seller-store.component';
import { SellerProductComponent } from './product/seller-product.component';
import { SellerProductDialogComponent } from './product/product-dialog/seller-product-dialog.component';



@NgModule({
  declarations: [  
    SellerComponent,
    SellerStoreComponent,
    SellerProductComponent,
    SellerProductDialogComponent
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
    SellerComponent
  ],
  providers: [
    
  ],
  bootstrap: [SellerComponent]
})
export class SellerModule { }
