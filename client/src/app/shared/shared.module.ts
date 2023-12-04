import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NgbPagination } from '@ng-bootstrap/ng-bootstrap';


@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    NgbPagination,
  ],
  exports: [
    NgbPagination
  ]
})

export class SharedModule { }

