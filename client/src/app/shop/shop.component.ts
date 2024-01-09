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
  shopParams: ShopParams;
  sortOptions = [
    {name:'Alphabetical', value:'name'},
    {name:'Price: Low to High', value:'priceAsc'},
    {name:'Price: High to Low', value:'priceDesc'}
  ];
  totalCount = 0;

  constructor(private shopService : ShopService) {
    this.shopParams = shopService.getShopParams();
  }

  ngOnInit(): void {
    this.getProducts();
    this.getCategories();
    this.getBrands();
  }

  getProducts() {
    this.shopService.getProducts().subscribe({
      next: response => {
        this.products = response.data;
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
  const params = this.shopService.getShopParams();
  params.brandId = brandId;
  params.pageIndex = 1;
  this.shopService.setshopParams(params);
  this.shopParams = params;
  this.getProducts();
}

onCategorySelected(categoryId: number) {
  const params = this.shopService.getShopParams();
  params.categoryId = categoryId;
  params.pageIndex = 1;
  this.shopService.setshopParams(params);
  this.shopParams = params;
  this.getProducts();
}

onSortSelected(event: any) {
  const params = this.shopService.getShopParams();
  params.sort= event.target.value;
  this.shopService.setshopParams(params);
  this.shopParams = params;
  this.getProducts();
}

onPageChanged(event: any) {
  const params = this.shopService.getShopParams();
  if (params.pageIndex !== event) {
    params.pageIndex = event; 
    this.shopService.setshopParams(params);
    this.shopParams = params;
    this.getProducts();
  }
}

onSearch() {
  const params = this.shopService.getShopParams();
  params.search = this.searchTerm?.nativeElement.value;
  params.pageIndex = 1;
  this.shopService.setshopParams(params);
  this.shopParams = params;
  this.getProducts();
}

onReset() {
  if(this.searchTerm) this.searchTerm.nativeElement.value = '';
  this.shopParams = new ShopParams();
  this.shopService.setshopParams(this.shopParams);
  this.getProducts();
}

}
