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
  shippingPrice: number;
  orderItems: OrderItem[];
  subtotal: number;
  total: number;
  status: string;
  id: number;
}

export interface OrderItem {
  productId: number;
  productName: string;
  imageUrl: string;
  price: number;
  quantity: number;
}
