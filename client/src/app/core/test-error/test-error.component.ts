import { Component } from '@angular/core';
import { environment } from '../../../environments/environment';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-test-error',
  templateUrl: './test-error.component.html',
  styleUrl: './test-error.component.scss'
})
export class TestErrorComponent {
  url = environment.apiUrl;
  validationErrors: string[] = [];

  constructor(private http: HttpClient) {}

  get404Error() {
    this.http.get(this.url + 'products/42').subscribe({
      next: response => console.log(response),
      error: error => console.log(error)
    })
  }
    
  get500Error() {
    this.http.get(this.url + 'Buggy/servererror').subscribe({
      next: response => console.log(response),
      error: error => console.log(error)
    })
  }
    
  get400Error() {
    this.http.get(this.url + 'Buggy/badrequest').subscribe({
      next: response => console.log(response),
      error: error => console.log(error)
    })
  }

  get400ValidationError() {
    this.http.get(this.url + 'products/fortytwo').subscribe({
      next: response => console.log(response),
      error: error => console.log(error)
    })
  }

}
