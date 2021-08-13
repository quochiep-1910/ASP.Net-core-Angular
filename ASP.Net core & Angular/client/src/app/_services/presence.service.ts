import { User } from './../_models/user';
import { ToastrService } from 'ngx-toastr';
import { environment } from 'src/environments/environment';
import { Injectable } from '@angular/core';
import { HubConnection, HubConnectionBuilder } from '@microsoft/signalr';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class PresenceService {
  hubUrl = environment.hubUrl;
  private hubConnection : HubConnection;
  private onlineUsersSource = new BehaviorSubject<string[]>([]);
  onlineUsers$ = this.onlineUsersSource.asObservable();

  constructor( private toastr: ToastrService) { }
  createHubConnection(user:User){
    this.hubConnection = new HubConnectionBuilder()
    .withUrl(this.hubUrl+'presence',{
      accessTokenFactory:()=>user.token
    })
    .withAutomaticReconnect()
    .build()
    this.hubConnection.start().catch(error=>console.error());
    this.hubConnection.on('UserIsOnline',username=>{
      this.toastr.info(username+' đã online');
    })
    this.hubConnection.on('UserIsOffline',username=>{
      this.toastr.warning(username+' đã offline');
    })
    this.hubConnection.on('GetOnlineUsers',(username:string[])=>{
      this.onlineUsersSource.next(username);
    })
   
  }
  stopHubConnection(){
    this.hubConnection.stop().catch(error=>console.log(error));
  }
}
