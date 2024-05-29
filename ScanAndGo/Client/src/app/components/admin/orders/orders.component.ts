import { Component, OnInit, ViewChild } from '@angular/core';
import { OrderDto } from 'src/app/models/order';
import { __values } from 'tslib';
import * as XLSX from 'xlsx'
import { FormBuilder, FormControl, FormGroup, NgForm } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { OrdersService } from 'src/app/services/orders.service';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { OrderItemsDialogComponent } from './order-items-dialog/order-items-dialog.component';




@Component({
  selector: 'app-orders',
  templateUrl: './orders.component.html',
  styleUrls: ['./orders.component.scss']
})
export class OrdersComponent implements OnInit  {
  filterForm: FormGroup =new FormGroup({});
  public orderList!:OrderDto[];
  public filter:string="";
  public page: number=1;
  public count: number=0;
  public tableSize:number=10;
  public tableSizes: any=[5,10,15,20];
  bsModalRef: BsModalRef | undefined;
  
constructor(private ordersService: OrdersService, private fb: FormBuilder, private toastr: ToastrService, private modalService: BsModalService){
  
}

  ngOnInit(): void {
    this.filterForm = this.fb.group({
      filter: [''],
      
    });
     this.getOrders();
  }

  getOrders(){
    this.ordersService.getOrders(this.page, this.tableSize, this.filter).subscribe(
      orders=>{
        console.log(orders.data)
        this.orderList=orders.data;
        this.count = orders.totalCount;
      },
      (error) => {
        console.error('Error fetching orders:', error);
      }
     );
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

  onPay(id : number){
      this.ordersService.payOrder(id).subscribe(
        data => {
          window.location.href = data.url; // Redirect to Stripe Checkout
        },
        error => {
          console.log("error", error);
        }
      ); 
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
    this.bsModalRef = this.modalService.show(OrderItemsDialogComponent, {
      initialState: {
        orderId: id
      },
    });
  }
  
}
