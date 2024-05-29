import { Component, OnInit } from '@angular/core';
import { OrderItemDto } from 'src/app/models/order';
import { OrdersService } from 'src/app/services/orders.service';

@Component({
  selector: 'app-store-owner-order-items-dialog',
  templateUrl: './store-owner-order-items-dialog.component.html',
  styleUrls: ['./store-owner-order-items-dialog.component.scss']
})
export class StoreOwnerOrderItemsDialogComponent implements OnInit{
  orderId!: number
  items: OrderItemDto[] = []

  constructor(private orderService: OrdersService) {}

  ngOnInit(): void {
    this.orderService.getOrderById(this.orderId).subscribe(
      order =>{
        this.items = order.items ?? [];
      },
      (error) => {
        console.error('Error fetching orders:', error);
      }
     );
  }
}