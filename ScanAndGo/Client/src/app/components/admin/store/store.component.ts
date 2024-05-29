import { Component, OnInit, TemplateRef, ViewChild } from '@angular/core';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { ToastrService } from 'ngx-toastr';
import { Subscription } from 'rxjs';
import { Store } from 'src/app/models/store';
import { StoreDialogComponent } from './store-dialog/store-dialog.component';
import { Page } from 'src/app/models/page';
import { StoreService } from 'src/app/services/store.service';
import { StoreProductsDialogComponent } from './store-products-dialog/store-products-dialog.component';
import { StoreSellersDialogComponent } from './store-sellers/store-sellers-dialog.component';
@Component({
  selector: 'app-store',
  templateUrl: './store.component.html',
  styleUrls: ['./store.component.scss']
})
export class StoreComponent implements OnInit {
  dataSource:Store[]=[];
  subscription!: Subscription;
  selectedStore:Store | undefined;
  public page: number=1;
  public count: number=0;
  public tableSize:number=10;
  @ViewChild('deleteConfirmationModal') deleteConfirmationModal!: TemplateRef<any>;
  bsModalRef: BsModalRef | undefined;
  constructor(private storeService: StoreService,private modalService:BsModalService,private toastr:ToastrService){}

  ngOnInit(): void {
    this.loadData();
  }
  public loadData() {
    if (this.subscription) {
      this.subscription.unsubscribe();
    }

    this.subscription = this.storeService.getStoresPagination(this.page, this.tableSize).subscribe(
      (data: Page<Store>) => {
        this.dataSource = data.data;
        this.count = data.totalCount
      },
      (error) => {
        console.error('Error fetching users:', error);
      }
    );
  }
  public openModal(){
    this.bsModalRef = this.modalService.show(StoreDialogComponent);
    this.bsModalRef.content.dataLoad.subscribe(() => {
      this.loadData();
    });
  }
  openEditModal(item: Store) {
    this.selectedStore=item;
    this.bsModalRef = this.modalService.show(StoreDialogComponent, {
      initialState: {
        name: this.selectedStore.name,
        address: this.selectedStore.address,
        userId: this.selectedStore.user?.id,
        selectedStore:item
      },
    });
    this.bsModalRef.content.dataLoad.subscribe(() => {
      this.loadData();
    });
  }
  
  confirmDelete(element: Store) {
  this.selectedStore = element;
  this.bsModalRef = this.modalService.show(this.deleteConfirmationModal);
  }
  closeModal(){
    this.modalService.hide();
  }

deleteStore() {
  if(this.selectedStore){
      this.storeService.deleteStore(this.selectedStore.id).subscribe(
      response => {
        this.page = 1
        this.toastr.success('Store is deleted!');
        this.loadData();
      },        
      error => {
        this.toastr.error(error.error.Message);
      }
    );
    }
    this.closeModal();
  }
  
  onTableDataChange(event: any){
    this.page=event;
    this.loadData();
  }

  openStoreProductsModal(item: Store) {
    this.selectedStore=item;
    this.bsModalRef = this.modalService.show(StoreProductsDialogComponent, {
      initialState: {
       storeId: item.id,
      },
    });
  }

  openStoreSellersModal(item: Store) {
    this.selectedStore=item;
    this.bsModalRef = this.modalService.show(StoreSellersDialogComponent, {
      initialState: {
       storeId: item.id
      },
    });
  }
}

