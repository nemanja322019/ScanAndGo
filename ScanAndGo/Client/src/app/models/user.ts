import { Store } from "./store";

export interface User {
    id?:number;
    name:string
    email: string;
    userType:number;
    ownedStores?:Store[];
    workingInStoreId?: number;
    workingInStore?: Store;
    temporalPassword?: boolean
}
export class ResetPasswordDTO
{
    email!:string;
    emailToken!:string;
    newPassword!:string;
    confirmPassword!:string;
}

export class ChangePasswordDTO
{
    newPassword!:string;
    confirmPassword!:string;
}


export class UserDto 
{
    id?:number;
    token?: string;
}
