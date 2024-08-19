import { inject, Injectable } from '@angular/core';
import { Message, MessageService } from 'primeng/api';
import { BehaviorSubject, Subject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class NotificationService {
  private message$ = new Subject<Message>();

  messageObserver = this.message$.asObservable();

  constructor() { }

  public showMessage(message:Message){
    this.message$.next(message);
  }


}
