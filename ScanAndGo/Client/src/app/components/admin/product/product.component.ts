import { Component, OnInit, TemplateRef, ViewChild } from '@angular/core';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { ToastrService } from 'ngx-toastr';
import { Subscription } from 'rxjs';
import { ProductDialogComponent } from './product-dialog/product-dialog.component';
import { Product } from 'src/app/models/product';
import { Page } from 'src/app/models/page';
import { ProductService } from 'src/app/services/product.service';

@Component({
  selector: 'app-product',
  templateUrl: './product.component.html',
  styleUrls: ['./product.component.scss']
})
export class ProductComponent implements OnInit {
  dataSource:Product[]=[];
  subscription!: Subscription;
  selectedProduct:Product | undefined;
  public page: number=1;
  public count: number=0;
  public tableSize:number=10;
  @ViewChild('deleteConfirmationModal') deleteConfirmationModal!: TemplateRef<any>;
  bsModalRef: BsModalRef | undefined;
  
  constructor(private productService : ProductService,private modalService:BsModalService,private toastr:ToastrService){}

  ngOnInit(): void {
  this.loadData();  
}
  public loadData() {
    this.subscription = this.productService.getProductsPagination(this.page, this.tableSize).subscribe(
      (data: Page<Product>) => {
        this.dataSource = data.data;
        this.count = data.totalCount
      },
      (error) => {
        console.error('Error fetching users:', error);
      }
    );
  }
  public openModal(){
    this.bsModalRef = this.modalService.show(ProductDialogComponent);
    this.bsModalRef.content.dataLoad.subscribe(() => {
      this.loadData();
    });
  }
  openEditModal(item: Product) {
    this.selectedProduct=item;
    this.bsModalRef = this.modalService.show(ProductDialogComponent, {
      initialState: {
        name: this.selectedProduct.name,
        price: this.selectedProduct.price,
        weight: this.selectedProduct.weight,
        barcode: this.selectedProduct.barcode,
        storeId: this.selectedProduct.store?.id,
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
