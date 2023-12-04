import { Component, Input, NgModule } from '@angular/core';
import { Product } from '../../shared/models/product';
import { RouterModule } from '@angular/router';

@Component({
  selector: 'app-product-item',
  templateUrl: './product-item.component.html',
  styleUrl: './product-item.component.scss',
})

export class ProductItemComponent {
  @Input() product?: Product;
}