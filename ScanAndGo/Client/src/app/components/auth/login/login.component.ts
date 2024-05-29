import { Component,OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators,FormBuilder } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
})
export class LoginComponent implements OnInit {
  loginForm: FormGroup =new FormGroup({});
  resetForm:FormGroup=new FormGroup({});
  showResetForm: boolean = false;
  public resetPasswordEmail!:string;
  public isValidEmail!:boolean;



  constructor(private fb: FormBuilder,private authService:AuthService, private router:Router,
    private toastr:ToastrService,private activatedRoute:ActivatedRoute) {
    }

  ngOnInit(): void {
    this.loginForm = this.fb.group({
      email: [''],  //Validators.required
      password: [''] //Validators.required
    });
    this.resetForm = this.fb.group({
      resetEmail: ['']   //[Validators.required, Validators.email]
    });
    
  }

  onSubmit(){
    const email = this.loginForm.get('email')!.value;
    const password = this.loginForm.get('password')!.value;
    this.authService.login(email, password).subscribe({
      next: response => {
        if(response.userDto.temporalPassword) {
          this.router.navigate(['/change-password']);
          return
        }
        if (response.userDto.userType == 0)
          this.router.navigate(['/admin/users']);
        if (response.userDto.userType == 2)
          this.router.navigate(['/seller/my-store']);
        if (response.userDto.userType == 3)
          this.router.navigate(['/store-owner/stores']);
     
        this.toastr.success('You are logged in!');
      },
      error: err => {
        this.toastr.error(err.error.Message);
      }
    });
   }
    
    toggleResetForm() {
      this.showResetForm = !this.showResetForm;
    }
    checkValidEmail(event:string){
      const value=event;
      const pattern= /^[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,3}$/;
      this.isValidEmail=pattern.test(value);
      return this.isValidEmail;
    }
    confirmToSend(){
      if(this.checkValidEmail(this.resetPasswordEmail)){
        this.authService.sendResetPasswordLink(this.resetPasswordEmail)
        .subscribe({
          next:(res)=>{
            console.log(res)
            this.toastr.success("Email sent!");
          },
          error:(err)=>{
            this.toastr.error(err.error.message);
          }
        })
      }
    }
}
