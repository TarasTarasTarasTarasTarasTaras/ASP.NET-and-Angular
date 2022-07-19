import { HttpInterceptor, HttpRequest, HttpHandler, HttpEvent } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { Observable, throwError } from 'rxjs';
import { catchError, retry } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class ErrorInterceptorService implements HttpInterceptor {

  constructor(private router: Router) { }

  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    return next.handle(request).pipe(
      retry(1),
      catchError((err: any) => {
        if(err.status === 401){
          err.errorMessage = err['error']
          this.router.navigate(['login'])
        }
        else if(err.status === 404) {
          
        }
        else if(err.status===400) {
          err.errorMessage = []
          if(err['error']['errors']['Email'] != undefined) 
            err.errorMessage.push(err['error']['errors']['Email'])
          if(err['error']['errors']['UserName'] != undefined) 
            err.errorMessage.push(err['error']['errors']['UserName'])
          if(err['error']['errors']['Password'] != undefined) 
            err.errorMessage.push(err['error']['errors']['Password'])
        }
        else if(err.status===500) {
          
        }
        else {
          
        }
        return throwError(err)
      })
    )
  }
}
