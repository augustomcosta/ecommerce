import cuid from 'cuid';
import { BasketItem } from './basket';
export interface Basket {
  id: string;
  items: BasketItem[];
}

export interface BasketItem {
  id: number;
  productName: string;
  price: number;
  quantity: number;
  imageUrl: string;
  brand: string;
  category: string;
}

export class Basket implements Basket {
  id = cuid();
  items: BasketItem[] = [];
}
