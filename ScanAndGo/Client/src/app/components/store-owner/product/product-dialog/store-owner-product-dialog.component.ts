import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { ToastrService } from 'ngx-toastr';
import { Observable, Subscription } from 'rxjs';
import { ProductService } from 'src/app/services/product.service';
import { StoreService } from 'src/app/services/store.service';
import { Product } from 'src/app/models/product';
import { Store } from 'src/app/models/store';

@Component({
  selector: 'app-store-owner-product-dialog',
  templateUrl: './store-owner-product-dialog.component.html',
  styleUrls: ['./store-owner-product-dialog.component.scss']
})
export class StoreOwnerProductDialogComponent {
  name!:string;
  price!:number;
  weight!: number;
  barcode!: string
  storeId!:number
  store?:string
  stores!:Store[];
  bsModalRef: BsModalRef | undefined;
  subscription!: Subscription;
  dataSource:Product[]=[];
  @Input() selectedProduct: Product | undefined;
  @Output() dataLoad: EventEmitter<void> = new EventEmitter<void>();
  
  constructor(private productService : ProductService, private storeService: StoreService, private modalService:BsModalService,private toastr:ToastrService){}


  closeModal(){
    this.modalService.hide();
  }
 
  addOrUpdateProduct() {
    if (this.selectedProduct) {
      const product = {
        id: this.selectedProduct.id,
        name: this.name,
        price: this.price,
        weight: this.weight,
        barcode: this.barcode,
        storeId:this.storeId
      };
      this.productService.updateProduct(product).subscribe(
        response => {
          this.toastr.success('Product is updated!');
          this.dataLoad.emit(); 
          this.closeModal();
          },
        error => {
          this.toastr.error(error.error.Message);
        }
      );
    } else {
      this.productService.addProduct({ name: this.name, price: this.price, weight: this.weight, barcode: this.barcode, storeId:+this.storeId}).subscribe(
        response => {
          this.toastr.success('Product is added!');
          this.dataLoad.emit(); 
          this.closeModal();
        },
        error => {
          this.toastr.error(error.error.Message); 
        }
      );
    }
  }
}
