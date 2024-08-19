import { Component, inject, OnInit } from '@angular/core';
import { SharedModule } from '../../shared/shared.module';
import { Message, MessageService } from 'primeng/api';
import { NotificationService } from '../../shared/services/notification/notification.service';

@Component({
  selector: 'app-notification',
  standalone: true,
  imports: [SharedModule],
  templateUrl: './notification.component.html',
  styleUrl: './notification.component.scss'
})
export class NotificationComponent implements OnInit {
  
  private messageService = inject(MessageService);
  private notificationService = inject(NotificationService);
  ngOnInit(): void {
    this.notificationService.messageObserver.subscribe((msg:Message)=>{
      this.messageService.add(msg);
    });
  }


}
