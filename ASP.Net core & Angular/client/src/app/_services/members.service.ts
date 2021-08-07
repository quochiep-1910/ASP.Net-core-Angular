import { Photo } from './../_models/photo';
import { map } from 'rxjs/operators';
import { Member } from './../_models/member';
import { environment } from './../../environments/environment';
import { Injectable } from '@angular/core';
import { HttpHeaders, HttpClient } from '@angular/common/http';
import { Observable, of } from 'rxjs';


@Injectable({
  providedIn: 'root'
})
export class MembersService {
  baseUrl = environment.apiUrl;
  members: Member[] = [];
  constructor(private http: HttpClient) {

  }
  getMembers() {
    if (this.members.length > 0) return of(this.members); //load lại trang không cần GET API
    return this.http.get<Member[]>(this.baseUrl + 'user').pipe(
      map(members => {
        this.members = members;
        return members;
      })
    );
  }

  getMember(username: string) {
    const member = this.members.find(x => x.userName === username);
    if (member !== undefined) return of(member);
    return this.http.get<Member>(this.baseUrl + 'user/' + username);
  }
  updateMember(member: Member) {
    return this.http.put(this.baseUrl + 'user', member).pipe(
      map(() => {
        const index = this.members.indexOf(member);
        this.members[index] = member;
      })
    );
  }
  setMainPhoto(PhotoId: number){
    return this.http.put(this.baseUrl+'user/set-main-photo/'+PhotoId,{});
  }
  deletePhoto(photoId:number)
  {
    return this.http.delete(this.baseUrl + 'user/delete-photo/'+photoId);
  }
}
