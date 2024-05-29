import { Product } from "./product";
import { User } from "./user";

export interface Store {
    id?:number;
    name: string;
    address:string;
    user?: User
    sellers?: User[]
}
export interface StoreDto{
    name: string;
    address:string;
    userId?:number
}