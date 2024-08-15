import { Component, inject, OnInit } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { TestService } from './shared/services/test/test.service';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss'
})
export class AppComponent implements OnInit {
  title = 'Password Secure';
  testStatus = 'pending';

  private testService = inject(TestService);
  
  ngOnInit(): void {
    this.testStatus = "running";
    this.testService.runTest().subscribe({
      next: ()=>{
        this.testStatus = "passed";
      },
      error: (e)=>{
        console.log(e);
        this.testStatus = "failed. see console logs.";
      }
    });
  }
}
