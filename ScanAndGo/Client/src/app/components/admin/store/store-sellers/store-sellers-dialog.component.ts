import { Component } from '@angular/core';
import { Subscription } from 'rxjs';
import { Store } from 'src/app/models/store';
import { User } from 'src/app/models/user';
import { StoreService } from 'src/app/services/store.service';

@Component({
  selector: 'app-store-sellers-dialog',
  templateUrl: './store-sellers-dialog.component.html',
  styleUrls: ['./store-sellers-dialog.component.scss']
})
export class StoreSellersDialogComponent {
  storeId!: number;
  sellers: User[] | undefined = [];
  subscription: Subscription;
  
  constructor(private storeService: StoreService){}

  ngOnInit(): void {
    this.loadData();  
  }
    public loadData() {
      this.subscription = this.storeService.getStoreById(this.storeId).subscribe(
        (data: Store) => {
          this.sellers = data.sellers
        },
        (error: any) => {
          console.error('Error fetching users:', error);
        }
      );
    }
}