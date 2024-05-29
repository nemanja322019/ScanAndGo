import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { Subscription } from 'rxjs';
import { User } from 'src/app/models/user';
import { UserComponent } from '../user.component';
import { ToastrService } from 'ngx-toastr';
import { UserService } from 'src/app/services/user.service';
import { Store } from 'src/app/models/store';
import { StoreService } from 'src/app/services/store.service';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-user-dialog',
  templateUrl: './user-dialog.component.html',
  styleUrls: ['./user-dialog.component.scss']
})
export class UserDialogComponent implements OnInit {
  bsModalRef: BsModalRef | undefined;
  id?:number | null 
  name!:string 
  email!: string  
  userTypes: { [key: number]: string } = {
    0: "Admin",
    2: "Seller",
    3: "StoreOwner"
  };
  stores: Store[] = []
  storeId: number | undefined
  userType!: number;
  subscription!: Subscription;
  dataSource: User[] =[];
  @Input() selectedUser: User | undefined;
  @Output() dataLoad: EventEmitter<void> = new EventEmitter<void>();

  constructor(public authService : AuthService, private userService: UserService, private storeService: StoreService, private modalService: BsModalService, private toastr:ToastrService){}

  ngOnInit(): void {
    console.log(this.storeId)
    this.getStores()
  }
 
  public getStores(){
    this.subscription = this.storeService.getStores().subscribe(
      (data: Store[]) => {
        this.stores = data;
      },
      (error) => {
        console.error('Error fetching users:', error);
      }
    );
  }

  closeModal(){
    this.modalService.hide();
  }
  addOrUpdateUser() {
    if (this.selectedUser) {
      const user = {
        id: this.selectedUser.id,
        name: this.name,
        email: this.email,
        userType:+this.userType,
        workingInStoreId: this.userType == 2 ? this.storeId : undefined
      };  
      this.userService.updateUser(user).subscribe({
        next: user => {
          this.toastr.success('User is updated!');
          this.dataLoad.emit(); 
          this.closeModal();
          },
        error: err => {
          this.toastr.error(err.error.Message);
        }
    });
    } else {
      this.authService.registerUser({ name: this.name, email: this.email, userType:+this.userType, workingInStoreId: this.storeId}).subscribe({
        next: user => {
          this.toastr.success('User is added!');
          this.dataLoad.emit(); 
          this.closeModal();
        }, error: err => {
          this.toastr.error(err.error.Message);
        }
    });
    }
  }

}


