import { Component, OnInit } from '@angular/core';
import { OrderDto } from 'src/app/models/order';
import { __values } from 'tslib';
import { FormBuilder, FormGroup } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { OrdersService } from 'src/app/services/orders.service';
import { StoreService } from 'src/app/services/store.service';
import { Store } from 'src/app/models/store';
import { Subscription } from 'rxjs';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { StoreOwnerOrderItemsDialogComponent } from './order-items-dialog/store-owner-order-items-dialog.component';


@Component({
  selector: 'app-store-owner-orders',
  templateUrl: './store-owner-orders.component.html',
  styleUrls: ['./store-owner-orders.component.scss']
})
export class StoreOwnerOrdersComponent implements OnInit  {
  stores: Store[] = []
  selectedStore: number = -1
  filterForm: FormGroup =new FormGroup({});
  public orderList!:OrderDto[];
  public filter:string="";
  public page: number=1;
  public count: number=0;
  public tableSize:number=10;
  public tableSizes: any=[5,10,15,20];
  subscription!: Subscription;
  bsModalRef: BsModalRef | undefined;
  
  constructor(private ordersService: OrdersService, private storeService: StoreService, private fb: FormBuilder, private toastr: ToastrService, private modalService: BsModalService){}

  ngOnInit(): void {
    this.filterForm = this.fb.group({
      filter: [''],
      
    });
    this.loadStores() 
  }

  public loadStores() {
    this.subscription = this.storeService.getOwnedStores().subscribe(
      (data: Store[]) => {
        this.stores = data;
        if (this.stores.length != 0) {
          this.selectedStore = data[0].id ?? -1
          this.getOrders()
        }
      },
      (error) => {
        console.error('Error fetching users:', error);
      }
    );
  }

  getOrders(){
    this.ordersService.getOrdersByStoreId(this.selectedStore, this.page, this.tableSize, this.filter).subscribe(
      orders=>{
        this.orderList=orders.data;
        this.count = orders.totalCount;
      },
      (error) => {
        console.error('Error fetching orders:', error);
      }
    );
  }

  public onStoreChange(){
    this.getOrders()
  }
 
  onFilter(){
    this.filter=this.filterForm.value.filter;
    this.page = 1
    this.getOrders()
  }

  onTableDataChange(event: any){
    this.page=event;
    this.getOrders();
  }

  onTableSizeChange(event:any):void{
    this.tableSize=event.target.value;
    this.page=1;
    this.getOrders();
  }

  onDownload(){
    this.ordersService.downloadOrders()
  }

  onDelete(id: number){
      this.ordersService.deleteOrder(id).subscribe({
        next: next => {
          this.toastr.success('Order is deleted');
          this.page = 1
          this.getOrders()
        },
        error: err => {
          this.toastr.error(err.error.Message)
        }
      });
  }

  openItemsModal(id: number) {
    this.bsModalRef = this.modalService.show(StoreOwnerOrderItemsDialogComponent, {
      initialState: {
        orderId: id
      },
    });
  }

}
