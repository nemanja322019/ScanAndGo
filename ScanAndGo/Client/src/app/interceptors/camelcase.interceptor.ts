import { Injectable } from '@angular/core';
import { HttpInterceptor, HttpHandler, HttpRequest, HttpEvent, HttpResponse } from '@angular/common/http';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import camelcaseKeys from 'camelcase-keys';

@Injectable()
export class CamelcaseInterceptor implements HttpInterceptor {
  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    return next.handle(req).pipe(
      map(event => {
        if (event instanceof HttpResponse && event.headers.get('content-type')?.includes('application/json')) {
          const body = event.body;
          if (body) {
            const camelcaseBody = camelcaseKeys(body, { deep: true });
            return event.clone({ body: camelcaseBody });
          }
        }
        return event;
      })
    );
  }
}