import { Component, OnInit, TemplateRef, ViewChild } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { Subscription } from 'rxjs';
import { Store } from 'src/app/models/store';
import { User } from 'src/app/models/user';
import { AuthService } from 'src/app/services/auth.service';
@Component({
  selector: 'app-seller-store',
  templateUrl: './seller-store.component.html',
  styleUrls: ['./seller-store.component.scss']
})
export class SellerStoreComponent implements OnInit {
  store?:Store;
  subscription!: Subscription;

  constructor(private authService: AuthService, private toastr:ToastrService){}

  ngOnInit(): void {
    this.authService.loadCurrentUser().subscribe(
      (data: User) => {
        this.store = data.workingInStore;
      },
      (error) => {
        console.error('Error fetching users:', error);
      }
    );
  }
}

