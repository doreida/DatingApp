import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { getPaginationResult, getPainationHeaders } from './paginationHelper';
import { Message } from '../_models/message';

@Injectable({
  providedIn: 'root'
})
export class MessageService {
  baseUrl = environment.apiUrl;


  constructor(private http: HttpClient) { }

  getMessages(pageNumber: number, pageSize: number, container: string)
  {
    let params = getPainationHeaders(pageNumber,pageSize);
    params =  params.append('Container', container);
    return getPaginationResult<Message[]>(this.baseUrl + 'messages', params, this.http);
  }
}


