import { Store } from "./store"

export interface Product{
    id?:number
    name:string|undefined
    price:number | undefined
    weight: number;
    barcode: string;
    store?: Store;
}
export interface ProductDto{
    name:string
    price:number
    weight: number;
    barcode: string
    storeId: number
}