import { HttpErrorResponse, HttpEvent, HttpHandler, HttpInterceptor, HttpRequest, HttpResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { Observable, throwError } from 'rxjs';
import { map } from 'rxjs/internal/operators/map';
import { catchError } from 'rxjs/operators';
import { AuthService } from '../services/auth-service';
import { LoaderService } from '../services/load-spinner.service';

@Injectable({
  providedIn: 'root'
})

export class TokenInterceptor implements HttpInterceptor {
  constructor(
    private toastr: ToastrService,
    private loader: LoaderService
  ) { }
  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {

    const token: string = localStorage.getItem('token');
    if (token) {
      request = request.clone({ headers: request.headers.set('Authorization', 'Bearer ' + token) });
    }

    if (!request.headers.has('Content-Type')) {
      request = request.clone({ headers: request.headers.set('Content-Type', 'application/json') });
    }
    if (!request.headers.has('Accept')) {
      request = request.clone({ headers: request.headers.set('Accept', 'application/json') });
    }
    this.loader.setLoading(true, request.url);
    return next.handle(request).pipe(
      map((event: HttpEvent<any>) => {
        if (event instanceof HttpResponse) {
          this.loader.setLoading(false, request.url);
          return event;
        }
      }),
      catchError((error: HttpErrorResponse) => {
        let errorMessage = '';
        if (error.error instanceof ErrorEvent) {
          errorMessage = `Error: ${error.error.message}`;
        } else {
          if (error) {
            if (error.error.message) {
              errorMessage = `Error: ${error.error.message}`;
            }
            if (error.error) {
              errorMessage = `Error: ${error.error}`;
            }
          }
        }
        this.loader.setLoading(false, request.url);
        this.toastr.error(errorMessage);
        return throwError(errorMessage);
      })
    );
  }
}
