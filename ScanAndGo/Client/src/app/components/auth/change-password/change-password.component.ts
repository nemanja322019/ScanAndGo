import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ConfirmPasswordValidator } from 'src/app/helpers/confirm-password.validator';
import { ChangePasswordDTO } from 'src/app/models/user';
import { ToastrService } from 'ngx-toastr';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-change-password',
  templateUrl: './change-password.component.html',
  styleUrls: ['./change-password.component.scss']
})
export class ChangePasswordComponent implements OnInit{
  changePasswordForm!:FormGroup;
  changePasswordObj=new ChangePasswordDTO();
  constructor(private fb:FormBuilder, private userService : UserService, private toastr:ToastrService) {}

  ngOnInit(): void {
    this.changePasswordForm=this.fb.group({
      password:[null,Validators.required],
      confirmPassword:[null,Validators.required]
      },{
          validator:ConfirmPasswordValidator("password","confirmPassword"),
    });
  }

  reset(){
    if(this.changePasswordForm.valid) {

      this.changePasswordObj.newPassword = this.changePasswordForm.value.password;
      this.changePasswordObj.confirmPassword = this.changePasswordForm.value.confirmPassword;

      
      this.userService.changePassword(this.changePasswordObj).subscribe({
        next:(res)=>{
            this.toastr.success("Your password is changed successufuly");
        },
        error:(err)=>{
          this.toastr.error(err.error.message);
        }
      });
      return;
    }
  }
}
