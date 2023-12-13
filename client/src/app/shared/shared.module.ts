import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NgbPagination } from '@ng-bootstrap/ng-bootstrap';
import { PagingHeaderComponent } from './paging-header/paging-header.component';
import { PagerComponent } from './pager/pager.component';
import { CarouselModule } from 'ngx-bootstrap/carousel';
import { OrderTotalsComponent } from './order-totals/order-totals.component';


@NgModule({
  declarations: [
    PagingHeaderComponent,
    PagerComponent,
    OrderTotalsComponent
  ],
  imports: [
    CommonModule,
    NgbPagination,
    CarouselModule.forRoot()
  ],
  exports: [
    NgbPagination,
    PagingHeaderComponent,
    PagerComponent,
    CarouselModule,
    OrderTotalsComponent
  ]
})

export class SharedModule { }

