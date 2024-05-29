import { Component } from '@angular/core';
import { Subscription } from 'rxjs';
import { Store } from 'src/app/models/store';
import { User } from 'src/app/models/user';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-user-owned-stores-dialog',
  templateUrl: './user-owned-stores-dialog.component.html',
  styleUrls: ['./user-owned-stores-dialog.component.scss']
})
export class UserOwnedStoresDialogComponent {
  userId!: number 
  stores: Store[] | undefined = [];
  subscription: Subscription;
  
  constructor(private userService: UserService){}

  ngOnInit(): void {
    this.loadData();  
  }
    public loadData() {
      this.subscription = this.userService.getUserById(this.userId).subscribe(
        (data: User) => {
          this.stores = data.ownedStores
        },
        (error: any) => {
          console.error('Error fetching users:', error);
        }
      );
    }
}