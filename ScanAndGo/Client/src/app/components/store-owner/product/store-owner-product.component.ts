import { Component, OnInit, TemplateRef, ViewChild } from '@angular/core';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { ToastrService } from 'ngx-toastr';
import { Subscription } from 'rxjs';
import { Product } from 'src/app/models/product';
import { Page } from 'src/app/models/page';
import { ProductService } from 'src/app/services/product.service';
import { StoreService } from 'src/app/services/store.service';
import { Store } from 'src/app/models/store';
import { StoreOwnerProductDialogComponent } from './product-dialog/store-owner-product-dialog.component';

@Component({
  selector: 'app-store-owner-product',
  templateUrl: './store-owner-product.component.html',
  styleUrls: ['./store-owner-product.component.scss']
})
export class StoreOwnerProductComponent implements OnInit {
  stores: Store[]=[]
  selectedStore: number = -1
  dataSource:Product[]=[];
  subscription!: Subscription;
  selectedProduct:Product | undefined;
  public page: number=1;
  public count: number=0;
  public tableSize:number=10;
  @ViewChild('deleteConfirmationModal') deleteConfirmationModal!: TemplateRef<any>;
  bsModalRef: BsModalRef | undefined;
  
  constructor(private productService : ProductService, private storeService : StoreService, private modalService:BsModalService,private toastr:ToastrService){}

  ngOnInit(): void {
    this.loadStores() 
  }

  public loadStores() {
    this.subscription = this.storeService.getOwnedStores().subscribe(
      (data: Store[]) => {
        this.stores = data;
        if (this.stores.length != 0) {
          this.selectedStore = data[0].id ?? -1
          this.loadData()
        }
      },
      (error) => {
        console.error('Error fetching users:', error);
      }
    );
  }

  public loadData() {
    this.subscription = this.productService.getStoreProductsPagination(this.selectedStore ?? -1, this.page, this.tableSize).subscribe(
      (data: Page<Product>) => {
        this.dataSource = data.data;
        this.count = data.totalCount
      },
      (error) => {
        console.error('Error fetching users:', error);
      }
    );
  }

 public onStoreChange(){
  this.loadData()
 }

  public openModal(){
    this.bsModalRef = this.modalService.show(StoreOwnerProductDialogComponent, {
      initialState: {
        storeId: this.selectedStore,
      },
    });
    this.bsModalRef.content.dataLoad.subscribe(() => {
      this.loadData();
    });
  }
  openEditModal(item: Product) {
    this.selectedProduct=item;
    this.bsModalRef = this.modalService.show(StoreOwnerProductDialogComponent, {
      initialState: {
        name: this.selectedProduct.name,
        price: this.selectedProduct.price,
        weight: this.selectedProduct.weight,
        barcode: this.selectedProduct.barcode,
        storeId: this.selectedStore,
        selectedProduct:item
      },
    });
    this.bsModalRef.content.dataLoad.subscribe(() => {
      this.loadData();
    });
  }


  confirmDelete(element: Product) {
  this.selectedProduct = element;
  this.bsModalRef = this.modalService.show(this.deleteConfirmationModal);
  }

  closeModal(){
    this.modalService.hide();
  }

  deleteProduct() {
    if(this.selectedProduct){
        this.productService.deleteProduct(this.selectedProduct.id).subscribe(
        response => {
          this.toastr.success('Product is deleted!');
          this.page = 1
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
}
