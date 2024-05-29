import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import {HTTP_INTERCEPTORS, HttpClientModule} from '@angular/common/http';
import { ReactiveFormsModule } from '@angular/forms';
import { NgxPaginationModule } from 'ngx-pagination';
import {MatIconModule} from '@angular/material/icon';
import { BsModalService } from 'ngx-bootstrap/modal';
import { FormsModule } from '@angular/forms';
import { ToastrModule } from 'ngx-toastr';
import { TokenInterceptor } from 'src/app/interceptors/token.interceptor';
import { LoginComponent } from './components/auth/login/login.component';
import { NavBarComponent } from './components/shared/nav-bar/nav-bar.component';
import { SideBarComponent } from './components/shared/side-bar/side-bar.component';
import { StoreOwnerModule } from './components/store-owner/store-owner.module';
import { AdminModule } from './components/admin/admin.module';
import { SellerModule } from './components/seller/seller.module';
import { RoleGuard } from './guards/role.guard';
import { ChangePasswordComponent } from './components/auth/change-password/change-password.component';
import { CamelcaseInterceptor } from './interceptors/camelcase.interceptor';

@NgModule({
  declarations: [
    AppComponent,    
    LoginComponent,
    NavBarComponent,
    SideBarComponent,
    ChangePasswordComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    HttpClientModule,
    ReactiveFormsModule,
    FormsModule,
    NgxPaginationModule,
    MatIconModule,
	  AdminModule,
    ToastrModule.forRoot({
      positionClass:'toast-bottom-right',
      preventDuplicates:true
    }),
    StoreOwnerModule,
    SellerModule
  ],
  exports:[
    AppComponent
  ],
  providers: [
    BsModalService,
      { provide: HTTP_INTERCEPTORS,useClass: TokenInterceptor, multi: true },
      RoleGuard,
      { provide: HTTP_INTERCEPTORS, useClass: CamelcaseInterceptor, multi: true }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
