import { Component } from '@angular/core';
import { BasketService } from '../../basket/basket.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-checkout-review',
  templateUrl: './checkout-review.component.html',
  styleUrl: './checkout-review.component.scss'
})
export class CheckoutReviewComponent {

  constructor(private basketService: BasketService, private toastr: ToastrService) {}


  createPaymentIntent() {
    this.basketService.createPaymentIntent().subscribe({
      next:() => this.toastr.success('Payment intent created'),
      error: error => this.toastr.error(error.message)
    })
  }
}
