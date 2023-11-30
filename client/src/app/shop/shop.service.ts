import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Pagination } from '../shared/models/pagination';
import { Product } from '../shared/models/product';
import { Category } from '../shared/models/category';
import { Brand } from '../shared/models/brand';
import { ShopParams } from '../shared/models/shopParams';

@Injectable({
  providedIn: 'root'
})
export class ShopService {

  constructor(private http: HttpClient) { }

  baseUrl = 'http://localhost:5214/api/';

  getProducts(shopParams : ShopParams) {
    let params = new HttpParams();

    if(shopParams.brandId) params = params.append('brandId',shopParams.brandId);
    if(shopParams.categoryId) params = params.append('categoryId',shopParams.categoryId);
    if(shopParams.sort) params = params.append('sort',shopParams.sort);
    
    return this.http.get<Pagination<Product[]>>(this.baseUrl + 'Product/products',{params});
  }

  getBrands() {
    return this.http.get<Brand[]>(this.baseUrl + 'Product/brands')
  }

  getCategories() {
    return this.http.get<Category[]>(this.baseUrl + 'Product/categories')
  }
}
