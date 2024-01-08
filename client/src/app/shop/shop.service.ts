import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Pagination } from '../shared/models/pagination';
import { Product } from '../shared/models/product';
import { Category } from '../shared/models/category';
import { Brand } from '../shared/models/brand';
import { Observable, map, of } from 'rxjs';
import { ShopParams } from '../shared/models/shopParams';

@Injectable({
  providedIn: 'root'
})
export class ShopService {

  constructor(private http: HttpClient) { }

  baseUrl = 'http://localhost:5214/api/';
  products: Product[] = [];
  brands: Brand[] = [];
  categories: Category[] = [];
  pagination?: Pagination<Product[]>;
  shopParams = new ShopParams();
  productCache = new Map<string, Pagination<Product[]>>;

  getProducts(useCache = true): Observable<Pagination<Product[]>> {

    if (!useCache) this.productCache = new Map();

    if (this.productCache.size > 0 && useCache) {
      if (this.productCache.has(Object.values(this.shopParams).join('-'))) {
        this.pagination = this.productCache.get(Object.values(this.shopParams).join('-'));
        if (this.pagination) return of(this.pagination);
      }
    }

    let params = new HttpParams();

    if (this.shopParams.brandId > 0) params = params.append('BrandId', this.shopParams.brandId);
    if (this.shopParams.categoryId) params = params.append('CategoryId', this.shopParams.categoryId);
    if (this.shopParams.pageIndex !== undefined) {
      params = params.append('PageIndex', this.shopParams.pageIndex);
    }
    if (this.shopParams.search) params = params.append('search', this.shopParams.search);
    params = params.append('PageSize', this.shopParams.pageSize);
    params = params.append('Sort', this.shopParams.sort);


    return this.http.get<Pagination<Product[]>>(this.baseUrl + 'Product/products', { params }).pipe(
      map(response => {
        this.productCache.set(Object.values(this.shopParams).join('-'), response);
        this.pagination = response;
        return response;
      })
    );
  }

  setshopParams(params: ShopParams) {
    this.shopParams = params;
  }

  getShopParams() {
    return this.shopParams;
  }

  getBrands() {
    if (this.brands.length > 0) return of(this.brands);

    return this.http.get<Brand[]>(this.baseUrl + 'Product/brands');
  }

  getCategories() {
    if (this.categories.length > 0) return of(this.categories);

    return this.http.get<Category[]>(this.baseUrl + 'Product/categories').pipe(
      map(categories => this.categories = categories)
    );
  }

  getProduct(id: number) {
    const product = [...this.productCache.values()]
      .reduce((acc, paginatedResult) => {
        return { ...acc, ...paginatedResult.data.find(x => x.id === id) }
      }, {} as Product)


    if (Object.keys(product).length !== 0) return of(product);

    return this.http.get<Product>(this.baseUrl + 'Product/' + id);

  }
}
