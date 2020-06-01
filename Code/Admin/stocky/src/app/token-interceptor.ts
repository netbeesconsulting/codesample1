import { HttpInterceptor, HttpRequest, HttpHandler, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable()
export class TokenInterceptor implements HttpInterceptor {

    constructor() { }

    intercept(req: HttpRequest<any>, next: HttpHandler) {
        // Get the auth token from the service.
        const authToken = localStorage.getItem("Token")

        // Clone the request and replace the original headers with
        // cloned headers, updated with the authorization.

        const headers = new HttpHeaders({
            'Authorization': 'bearer ' + authToken         
        });

        const cloneReq = req.clone({ headers });

        // send cloned request with header to the next handler.
        return next.handle(cloneReq);
    }
}