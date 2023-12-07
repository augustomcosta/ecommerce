import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NgbPagination } from '@ng-bootstrap/ng-bootstrap';
import { PagingHeaderComponent } from './paging-header/paging-header.component';
import { PagerComponent } from './pager/pager.component';
import { CarouselModule } from 'ngx-bootstrap/carousel';


@NgModule({
  declarations: [
    PagingHeaderComponent,
    PagerComponent
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
    CarouselModule
  ]
})

export class SharedModule { }

