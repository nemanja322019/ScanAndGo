import { Component, OnInit } from '@angular/core';
import { User } from 'src/app/models/user';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-side-bar',
  templateUrl: './side-bar.component.html',
  styleUrls: ['./side-bar.component.scss']
})
export class SideBarComponent implements OnInit {

    user : User
    constructor(public authService: AuthService){}

    ngOnInit(): void {
      this.authService.loadCurrentUser().subscribe(
        (data: User) => {
          this.user = data;
        },
        (error) => {
          console.error('Error fetching users:', error);
        }
      );
    }

    sidebarListAdmin:any=[
      {
        label:"Users",
        link:"/users",
        active:true
      },
      {
        label:"Stores",
        link:"/stores",
        active:false
      },
      {
        label:"Products",
        link:"/products",
        active:false
      },
      {
        label:"Orders",
        link:"/orders",
        active:false
      }
      
    ]

    sidebarListStoreOwner:any=[
      {
        label:"My Stores",
        link:"/stores",
        active:false
      },
      {
        label:"Products",
        link:"/products",
        active:false
      },
      {
        label:"Orders",
        link:"/orders",
        active:false
      }
    ]

    sidebarListSeller:any=[
      {
        label:"My Store",
        link:"/my-store",
        active:false
      },
      {
        label:"Products",
        link:"/products",
        active:false
      }
    ]
  
  
    changeRoute(index:number){
      this.sidebarListAdmin.forEach((item:any,i:number) => {
        this.sidebarListAdmin[i].active=false;
      });
      this.sidebarListAdmin[index].active=true;
    }

}
