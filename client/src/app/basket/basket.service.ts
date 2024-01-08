import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { BehaviorSubject, map } from 'rxjs';
import { Basket, IBasket, IBasketItem, IBasketTotals } from '../shared/models/basket';
import { HttpClient } from '@angular/common/http';
import { Product } from '../shared/models/product';
import { DeliveryMethod } from '../shared/models/deliveryMethod';

@Injectable({
  providedIn: 'root'
})

export class BasketService {

  baseUrl = environment.apiUrl;

  private basketSource = new BehaviorSubject<IBasket | null>(null);

  basketSource$ = this.basketSource.asObservable();

  private basketTotalSource = new BehaviorSubject<IBasketTotals | null>(null);
  basketTotalSource$ = this.basketTotalSource.asObservable();

  constructor(private http: HttpClient) {}

  createPaymentIntent() {
    return this.http.post<Basket>(this.baseUrl + 'payments/' + this.getCurrentBasketValue()?.id, {})
    .pipe(map(basket => {
      this.basketSource.next(basket);
      console.log(basket)
    }))
  }

  setShippingPrice(deliveryMethod: DeliveryMethod) {
    const basket = this.getCurrentBasketValue();
    if(basket) {
      basket.shippingPrice = deliveryMethod.price;
      basket.deliveryMethodId = deliveryMethod.id;
      this.setBasket(basket);
    }
    this.calculateTotals();
  }

  getBasket(id: string) {
    return this.http.get<IBasket>(this.baseUrl + 'basket?id=' + id).subscribe({
      next: basket =>{ 
        this.basketSource.next(basket)
        this.calculateTotals();
      }
    })
  }

  setBasket(basket: IBasket) {
    return this.http.post<IBasket>(this.baseUrl + 'basket', basket).subscribe({
      next: basket => { 
        this.basketSource.next(basket)
        this.calculateTotals();
      }
    })
  }

  getCurrentBasketValue() {
    return this.basketSource.value;
  }

  addItemToBasket(item: Product | IBasketItem, quantity = 1) {
    if(this.isProduct(item)) item = this.mapProductItemToBasketItem(item);
    const basket = this.getCurrentBasketValue() ?? this.createBasket();
    basket.items = this.addOrUpdateItem(basket.items, item, quantity)
    this.setBasket(basket);
  }

  removeItemFromBasket(id: number, quantity = 1) {
    const basket = this.getCurrentBasketValue();
    if(!basket) return;
    const item = basket.items.find(x => x.id === id);
    if(item) {
      item.quantity -= quantity;
      if(item.quantity === 0) {
        basket.items = basket.items.filter(x => x.id !== id)
      }
      if(basket.items.length > 0) this.setBasket(basket);
      else this.deleteBasket(basket);
    
    }
  }

  deleteBasket(basket: IBasket) {
    return this.http.delete(this.baseUrl + 'basket?id=' + basket.id).subscribe({
      next: () => {
        this.basketSource.next(null);
        this.basketTotalSource.next(null);
        localStorage.removeItem('basket_id');
      }
    })
  }

  deleteLocalBasket() {
    this.basketSource.next(null);
    this.basketTotalSource.next(null);
    localStorage.removeItem('basket_id');
  }

  private addOrUpdateItem(items: IBasketItem[], itemToAdd: IBasketItem, quantity: number): IBasketItem[] {
    const item = items.find(x => x.id === itemToAdd.id);

    if(item) item.quantity += quantity;

    else {
      itemToAdd.quantity = quantity;
      items.push(itemToAdd);
    }

    return items;
  }

  private createBasket(): IBasket {
    const basket = new Basket();
    localStorage.setItem('basket_id',basket.id);
    return basket;
  }

  private mapProductItemToBasketItem(item: Product) : IBasketItem {
    return {
      id: item.id,
      productName: item.name,
      price: item.price,
      quantity: 0,
      imageUrl: item.imageUrl,
      brand: item.brand,
      category: item.category
    }

  }

  private calculateTotals() {
    const basket = this.getCurrentBasketValue();
    if(!basket) return;
    const subtotal = basket!.items.reduce((a, b) => (b.price * b.quantity) + a, 0);
    const total = subtotal + basket.shippingPrice;
    this.basketTotalSource.next({shipping: basket.shippingPrice, total, subtotal});
  }

  private isProduct(item: Product | IBasketItem): item is Product {
    return (item as Product).brand !== undefined;
  }

}
