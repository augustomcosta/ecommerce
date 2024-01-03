import { DeliveryMethod } from "./deliveryMethod";
import { Address } from "./user";

export interface OrderToCreate {
  basketId: string;
  deliveryMethodId: number;
  shipToAddress: Address;
}

export interface Order {
  buyerEmail: string;
  orderDate: string;
  shipToAddress?: any;
  deliveryMethod: DeliveryMethod;
  orderItems: OrderItem[];
  subtotal: number;
  status: string;
  id: number;
}

export interface OrderItem {
  itemOrdered?: any;
  price: number;
  quantity: number;
  id: number;
}
