import { DeliveryMethod } from './../../shared/models/deliveryMethod';
import { Component, Input } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { BasketService } from '../../basket/basket.service';
import { ToastrService } from 'ngx-toastr';
import { CheckoutService } from '../checkout.service';
import { Basket } from '../../shared/models/basket';
import { Address } from '../../shared/models/user';

@Component({
  selector: 'app-checkout-payment',
  templateUrl: './checkout-payment.component.html',
  styleUrl: './checkout-payment.component.scss'
})
export class CheckoutPaymentComponent {
  @Input() checkoutForm?: FormGroup;

  constructor(private basketService: BasketService, private checkoutService: CheckoutService, private toastr: ToastrService) {}

  submitOrder() {
    const basket = this.basketService.getCurrentBasketValue();
    if(!basket) return;
    const orderToCreate = this.getOrderToCreate(basket);
    if(!orderToCreate) return;
    this.checkoutService.createOrder(orderToCreate).subscribe({
      next: order => {
        this.toastr.success('Order created successfully');
        console.log(order)
      }
    })
  }

  private getOrderToCreate(basket: Basket) {
    const deliveryMethodId = this.checkoutForm?.get('deliveryForm')?.get('deliveryMethod')?.value;
    const shipToAddress = this.checkoutForm?.get('addressForm')?.value as Address;
    if(!deliveryMethodId || !shipToAddress) return;
    return {
      basketId: basket.id,
      deliveryMethodId: deliveryMethodId,
      shipToAddress: shipToAddress
    }
  }
}
