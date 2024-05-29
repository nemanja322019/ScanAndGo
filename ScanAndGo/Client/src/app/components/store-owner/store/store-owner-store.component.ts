import { Component, OnInit, TemplateRef, ViewChild } from '@angular/core';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { ToastrService } from 'ngx-toastr';
import { Subscription } from 'rxjs';
import { StoreService } from 'src/app/services/store.service';
import { Store } from 'src/app/models/store';
import { StoreSellersDialogComponent } from '../../admin/store/store-sellers/store-sellers-dialog.component';

@Component({
  selector: 'app-store-owner-store',
  templateUrl: './store-owner-store.component.html',
  styleUrls: ['./store-owner-store.component.scss']
})
export class StoreOwnerStoreComponent implements OnInit {
  dataSource:Store[]=[];
  subscription!: Subscription;
  selectedStore:Store | undefined;
  bsModalRef: BsModalRef | undefined;
  constructor(private storeService: StoreService, private modalService:BsModalService){}

  ngOnInit(): void {
    this.loadData();
  }
  public loadData() {
    this.subscription = this.storeService.getOwnedStores().subscribe(
      (data: Store[]) => {
        this.dataSource = data;
      },
      (error) => {
        console.error('Error fetching users:', error);
      }
    );
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

