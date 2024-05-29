export interface OrderDto{
    id?:number;
    paymentStatus?:string | undefined;
    sessionId?:string | undefined;
    paymentDate?:Date | undefined;
    userEmail?:string | undefined;
    items?:OrderItemDto[] | undefined;
    storeName: string;
}


export interface OrderItemDto{
    id?:number|undefined;
    productName: string;
    productPrice: number;
    productWeight: number;
    productCount:number;
}

