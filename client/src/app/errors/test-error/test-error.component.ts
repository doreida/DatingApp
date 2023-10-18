import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-test-error',
  templateUrl: './test-error.component.html',
  styleUrls: ['./test-error.component.css']
})
export class TestErrorComponent implements OnInit{

  baseUrl = "https://localhost:5001/api/";
  validationErrors: string[] = [];

  constructor(private http: HttpClient) {
  
  }

  ngOnInit(): void {
    
  }

  get404Error(){
    this.http.get(this.baseUrl+ 'errors/not-found').subscribe({
      next: response=> console.log(response),
      error: error => console.log(error)
    })
  }

  get400Error(){
    this.http.get(this.baseUrl+ 'errors/bad-request').subscribe({
      next: response=> console.log(response),
      error: error => console.log(error)
    })
  }

  get500Error(){
    this.http.get(this.baseUrl+ 'errors/server-error').subscribe({
      next: response=> console.log(response),
      error: error => console.log(error)
    })
  }

  get401Error(){
    this.http.get(this.baseUrl+ 'errors/auth').subscribe({
      next: response=> console.log(response),
      error: error => console.log(error)
    })
  }

  get400ValidationError(){
    this.http.post(this.baseUrl+ 'account/register',{}).subscribe({
      next: response=> console.log(response),
      error: error => {
        console.log(error);
        this.validationErrors = error;

      }
    })
  }

}
