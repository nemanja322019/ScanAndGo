import { Component, OnInit, TemplateRef, ViewChild } from '@angular/core';
import { UserService } from 'src/app/services/user.service';
import { Subscription } from 'rxjs';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { User } from 'src/app/models/user';
import { UserDialogComponent } from './user-dialog/user-dialog.component';
import { ToastrService } from 'ngx-toastr';
import { Page } from 'src/app/models/page';
import { UserOwnedStoresDialogComponent } from './user-owned-stores-dialog/user-owned-stores-dialog.component';

@Component({
  selector: 'app-user',
  templateUrl: './user.component.html',
  styleUrls: ['./user.component.scss']
})
export class UserComponent implements OnInit{
  id?:number
  name:string | undefined
  email: string | undefined 
  userType?: number | undefined
  subscription!: Subscription;
  userTypeMappings: { [key: number]: string } = {
    0: 'Admin',
    1: 'Buyer',
    2: 'Seller',
    3: 'Store owner'
  };
  
  dataSource:User[]=[]
  user:any;
  public page: number=1;
  public count: number=0;
  public tableSize:number=10;
  @ViewChild('deleteConfirmationModal') deleteConfirmationModal!: TemplateRef<any>;
  bsModalRef: BsModalRef | undefined;
  selectedUser: User| undefined;
  
  constructor(private userService : UserService,private modalService: BsModalService,private toastr:ToastrService){}
  
  ngOnInit(): void {
    this.loadData();
  }

  public loadData() {
  this.subscription = this.userService.getUsersPagination(this.page, this.tableSize).subscribe(
    (data: Page<User>) => {
      console.log(data.data)
      this.dataSource = data.data;
      this.count = data.totalCount
    },
    (error) => {
      console.error('Error fetching users:', error);
    }
  );
}
public openModal(){
  this.bsModalRef = this.modalService.show(UserDialogComponent);
  this.bsModalRef.content.dataLoad.subscribe(() => {
    this.loadData();
  });
}

openEditModal(item: User) {
  this.selectedUser=item;
  this.bsModalRef = this.modalService.show(UserDialogComponent, {
    initialState: {
      name: this.selectedUser.name,
      email: this.selectedUser.email,
      userType:this.selectedUser.userType,
      storeId: this.selectedUser.workingInStore?.id,
      selectedUser:item
    },
  });
  this.bsModalRef.content.dataLoad.subscribe(() => {
    this.loadData();
  });
}

confirmDelete(element: User) {
  this.selectedUser = element;
  this.bsModalRef = this.modalService.show(this.deleteConfirmationModal);
}
closeModal(){
  this.modalService.hide();
}
deleteUser() {
  if(this.selectedUser){
      this.userService.deleteUser(this.selectedUser.id).subscribe(
      response => {
        this.toastr.success('User is deleted!');
        this.page = 1
        this.loadData();
      },
    );
    }
    this.closeModal();
  }
  
  
  onTableDataChange(event: any){
    this.page=event;
    this.loadData();
  }

  openOwnedStoresModal(item: User) {
    this.selectedUser=item;
    this.bsModalRef = this.modalService.show(UserOwnedStoresDialogComponent, {
      initialState: {
        userId: item.id
      },
    });
  }
  
  
}



