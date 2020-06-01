import { async } from '@angular/core/testing';
import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { map, catchError } from 'rxjs/operators';
import { Observable, throwError } from 'rxjs';
import { HttpOperation } from '../enums/httpOperation';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export abstract class BaseService {

  constructor(protected http: HttpClient) { }

  protected httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json',
    })
  };

  protected httpOptionsFormType = {
    headers: new HttpHeaders({
      'Content-Type': 'application/x-www-form-urlencoded',
    })
  };

  protected httpOptionsFormDataType = {
    headers: new HttpHeaders({
      'enctype': 'multipart/form-data'
    })
  };

  async generateRequest(actionUrl: string, type: HttpOperation, body?: any, files?: FileList) {
    switch (type) {
      case HttpOperation.Get: {
        return this.http.get(actionUrl, this.httpOptions).pipe(map((response) => (response)), catchError(this.errorHandler));
        break;
      }
      case HttpOperation.Post: {
        return this.http.post(actionUrl, body, this.httpOptions).pipe(map((response) => (response)), catchError(this.errorHandler));
        break;
      }
      case HttpOperation.Delete: {
        return this.http.delete(actionUrl, this.httpOptions).pipe(map((response) => (response)), catchError(this.errorHandler));
        break;
      }
      case HttpOperation.Put: {
        return this.http.put(actionUrl, body).pipe(map((response) => (response)), catchError(this.errorHandler));
        break;
      }
      case HttpOperation.FilesPost: {
        let formData: FormData = new FormData();
        if (files != null && files != undefined && files.length > 0) {
          for (let i = 0; i < files.length; i++) {
            formData.append('imageUpload' + i, files[i]);
          }
        }
        formData.append('entityData', JSON.stringify(body));
        return this.http.post(actionUrl, formData).pipe(map((response) => (response)), catchError(this.errorHandler));
        break;
      }
      case HttpOperation.FilesPut: {
        let formData: FormData = new FormData();
        if (files != null && files != undefined && files.length > 0) {
          for (let i = 0; i < files.length; i++) {
            formData.append('imageUpload' + i, files[i]);
          }
        }
        formData.append('entityData', JSON.stringify(body));
        return this.http.put(actionUrl, formData).pipe(map((response) => (response)), catchError(this.errorHandler));
        break;
      }
    }
  }

  protected errorHandler(error: Response | any) {
    let errMsg: string;
    if (error instanceof Response) {
      console.log(error)
      const body = error.json() || '';
      const err = body || JSON.stringify(body);
      errMsg = `${error.status} - ${error.statusText || ''} ${err}`;
    } else {
      errMsg = error.error ? error.error : error.toString();
    }
    return throwError(errMsg);
  }
}
