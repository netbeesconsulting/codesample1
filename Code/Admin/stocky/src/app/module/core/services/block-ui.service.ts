import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class BlockUIService {

  private blockUI = new Subject<boolean>();
  start$ = this.blockUI.asObservable();

  constructor() { }

  BlockUI(message: boolean) {
    this.blockUI.next(message);
  }
}
