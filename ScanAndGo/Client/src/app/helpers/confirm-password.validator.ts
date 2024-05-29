import { FormGroup } from "@angular/forms";

export function ConfirmPasswordValidator(controlName:string,matchControlName:string){
    return(formGroup:FormGroup)=>{
        const passwordControl=formGroup.controls[controlName];
        const confirmPasswordControl=formGroup.controls[matchControlName];
        if(confirmPasswordControl.errors && confirmPasswordControl.errors['confirmPasswordValidator']){
            return;
        }
        if(passwordControl.value!== confirmPasswordControl.value){
            confirmPasswordControl.setErrors({ConfirmPasswordValidator:true})
        }else{
            confirmPasswordControl.setErrors(null)
        }
    }
}

/*export function IsRequired(password: string) {
    return (formGroup: FormGroup) => {
      const passwordControl = formGroup.controls[password];
      if (!passwordControl.value || passwordControl.value.trim() === '') {
        passwordControl.setErrors({ IsRequired: true });
      } else {
        if (passwordControl.errors && passwordControl.errors['IsRequired']) {
          delete passwordControl.errors['IsRequired'];
          if (Object.keys(passwordControl.errors).length === 0) {
            passwordControl.setErrors(null);
          }
        }
      }
    };
  }*/