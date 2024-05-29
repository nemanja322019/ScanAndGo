import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { ToastrService } from 'ngx-toastr';
import { Subscription } from 'rxjs';
import { StoreService } from 'src/app/services/store.service';
import { Store } from 'src/app/models/store';
import { User } from 'src/app/models/user';
import { UserService } from 'src/app/services/user.service';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-store-dialog',
  templateUrl: './store-dialog.component.html',
  styleUrls: ['./store-dialog.component.scss']
})
export class StoreDialogComponent implements OnInit {
  name!:string;
  address!:string;
  userId?:number
  users!:User[];
  bsModalRef: BsModalRef | undefined;
  subscription!: Subscription;
  dataSource:Store[]=[];
  @Input() selectedStore: Store | undefined;
  @Output() dataLoad: EventEmitter<void> = new EventEmitter<void>();

  constructor(private storeService: StoreService,private modalService:BsModalService,
    private toastr:ToastrService, private userService : UserService, public authService : AuthService){}

  ngOnInit(): void {
    this.subscription = this.userService.getStoreOwners().subscribe(
      (data: User[]) => {
        this.users = data;
      },
      (error) => {
        console.error('Error fetching users:', error);
      }
    );
  }
  closeModal(){
    this.modalService.hide();
  }
  addOrUpdateStore() {
    if (this.selectedStore) {
      const store = {
        id: this.selectedStore.id,
        name: this.name,
        address: this.address,
        userId:this.userId
      };
      this.storeService.updateStore(store).subscribe({
        next: store => {
          this.toastr.success('Store is updated!');
          this.dataLoad.emit(); 
          },
        error: err => {
          this.toastr.error(err.error.Message);
        }
    });
    } else {
      this.storeService.addStore({ name: this.name, address: this.address,userId:this.userId}).subscribe({
        next : store => {
          this.toastr.success('Store is added!');
          this.dataLoad.emit(); 
        },
        error : err => {
          this.toastr.error(err.error.Message);
        }
    });
    }
    this.closeModal();
  }
}