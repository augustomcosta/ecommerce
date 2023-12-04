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

    if(shopParams.brandId > 0) params = params.append('BrandId',shopParams.brandId);
    if(shopParams.categoryId) params = params.append('CategoryId',shopParams.categoryId);
    if(shopParams.pageIndex !== undefined) {
      params = params.append('PageIndex', shopParams.pageIndex);
    }
    if(shopParams.search) params = params.append('search', shopParams.search);
    params = params.append('PageSize',shopParams.pageSize);
    params = params.append('Sort',shopParams.sort);
    
    
    return this.http.get<Pagination<Product[]>>(this.baseUrl + 'Product/products',{params});
  }

  getBrands() {
    return this.http.get<Brand[]>(this.baseUrl + 'Product/brands')
  }

  getCategories() {
    return this.http.get<Category[]>(this.baseUrl + 'Product/categories')
  }

}
