import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { ShopService } from './shop.service';
import { Product } from '../shared/models/product';
import { Brand } from '../shared/models/brand';
import { Category } from '../shared/models/category';
import { ShopParams } from '../shared/models/shopParams';


@Component({
  selector: 'app-shop',
  templateUrl: './shop.component.html',
  styleUrl: './shop.component.scss'
})
export class ShopComponent implements OnInit{
  @ViewChild('search') searchTerm?: ElementRef; 
  products: Product[] = [];
  brands: Brand[] = [];
  categories: Category[] = [];
  shopParams = new ShopParams();
  sortOptions = [
    {name:'Alphabetical', value:'name'},
    {name:'Price: Low to High', value:'priceAsc'},
    {name:'Price: High to Low', value:'priceDesc'}
  ];
  totalCount = 0;

  constructor(private shopService : ShopService) {}

  ngOnInit(): void {
    this.getProducts();
    this.getCategories();
    this.getBrands();
  }

  getProducts() {
    this.shopService.getProducts(this.shopParams).subscribe({
      next: response => {
        this.products = response.data;
        this.shopParams.pageIndex = response.pageIndex;
        this.shopParams.pageSize = response.pageSize;
        this.totalCount = response.count;
      },
      error: error => console.log(error)
    })
  }

  getBrands() {
    this.shopService.getBrands().subscribe({
      next: response => this.brands = [{id: 0, name:'All'}, ...response],
      error: error => console.log(error)
  })

}

getCategories() {
  this.shopService.getCategories().subscribe({
    next: response => this.categories = [{id: 0, name:'All'}, ...response],
    error : error => console.log(error)
  })
}

onBrandSelected(brandId: number) {
  this.shopParams.brandId = brandId;
  this.shopParams.pageIndex = 1;
  this.getProducts();
}

onCategorySelected(categoryId: number) {
  this.shopParams.categoryId = categoryId;
  this.shopParams.pageIndex = 1;
  this.getProducts();
}

onSortSelected(event: any) {
this.shopParams.sort= event.target.value;
this.getProducts();
}

onPageChanged(page: number) {
  if (this.shopParams.pageIndex !== page) {
    this.shopParams.pageIndex = page; 
    this.getProducts();
  }
}

onSearch() {
  this.shopParams.search = this.searchTerm?.nativeElement.value;
  this.shopParams.pageIndex = 1;
  this.getProducts();
}

onReset() {
  if(this.searchTerm) this.searchTerm.nativeElement.value = '';
  this.shopParams = new ShopParams();
  this.getProducts();
}

}
