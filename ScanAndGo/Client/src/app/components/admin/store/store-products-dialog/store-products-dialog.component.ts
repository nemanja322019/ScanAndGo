import { Component } from '@angular/core';
import { Subscription } from 'rxjs';
import { Page } from 'src/app/models/page';
import { Product } from 'src/app/models/product';
import { ProductService } from 'src/app/services/product.service';

@Component({
  selector: 'app-store-products-dialog',
  templateUrl: './store-products-dialog.component.html',
  styleUrls: ['./store-products-dialog.component.scss']
})
export class StoreProductsDialogComponent {
  storeId!: number;
  dataSource:Product[]=[];
  public productsPage: number=1;
  public productsCount: number=0;
  public productsTableSize:number=10;
  subs: Subscription;

  constructor(private productService : ProductService){}

  ngOnInit(): void {
    this.loadData();  
  }
    public loadData() {
      this.subs = this.productService.getStoreProductsPagination(this.storeId, this.productsPage, this.productsTableSize).subscribe(
        (data: Page<Product>) => {
          this.dataSource = data.data;
          this.productsCount = data.totalCount
        },
        (error) => {
          console.error('Error fetching users:', error);
        }
      );
    }
    
  onTableDataChange(event: any){
    this.productsPage=event;
    this.loadData();
  }

}