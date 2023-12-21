import { HttpInterceptor,
HttpRequest,
HttpHandler,
HttpEvent,
} from '@angular/common/http';
import { Observable, take } from 'rxjs';
import { AccountService } from '../../account/account.service';
import { Injectable } from '@angular/core';

@Injectable()
export class JwtInterceptor implements HttpInterceptor {
  token?: string;

  constructor(private accountService: AccountService) {}

  intercept(req: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    this.accountService.currentUser$.pipe(take(1)).subscribe({
      next: user => this.token = user?.token
    })

    if(this.token) {
      req = req.clone({
        setHeaders: {
          Authorization: `Bearer ${this.token}`
        }
      })
    }
    return next.handle(req);
  }
}
