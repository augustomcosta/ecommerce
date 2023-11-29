import { HttpClient} from '@angular/common/http';
import { ConstantPool } from '@angular/compiler';
import { Component, OnInit } from '@angular/core';
import { Pagination } from './models/pagination';
import { Product } from './models/product';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss'
})
export class AppComponent implements OnInit{
  title = 'Skinet';
  products: any[] = [];

  constructor (private http: HttpClient) {}
  ngOnInit(): void {
    this.http.get<Pagination<Product[]>>('http://localhost:5214/api/Product/products?Search=Angular').subscribe({
      next: response => this.products = response.data,
      error: error => console.log(error),
      complete : () => {
        console.log('request completed');
      }
    })
  }
}
