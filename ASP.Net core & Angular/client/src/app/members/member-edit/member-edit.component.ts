import { PreventUnsavedChangedGuard } from './../../_guards/prevent-unsaved-changed.guard';
import { ToastrService } from 'ngx-toastr';
import { take } from 'rxjs/operators';
import { MembersService } from './../../_services/members.service';
import { AccountService } from './../../_services/account.service';
import { User } from './../../_models/user';
import { Member } from './../../_models/member';
import { Component, HostListener, OnInit, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';

@Component({
  selector: 'app-member-edit',
  templateUrl: './member-edit.component.html',
  styleUrls: ['./member-edit.component.css']
})
export class MemberEditComponent implements OnInit {
  @ViewChild('editForm') editForm: NgForm;
  member: Member;
  user: User;
  @HostListener('window:beforeunload', ['$event']) unloadNotification($event: any) {
    if (this.editForm.dirty) {
      //lắng nghe sự kiện đóng cửa số 
      $event.returnValue = true;
    }
  }
  constructor(private accountService: AccountService, private memberService: MembersService
    , private toast: ToastrService) {
    this.accountService.currentUser$.pipe(take(1)).subscribe(user => this.user = user);
  }

  ngOnInit(): void {
    this.loadMember()
  }
  loadMember() {
    this.memberService.getMember(this.user.userName).subscribe(member => {
      this.member = member;
    })
  }
  updateMember() {
    this.memberService.updateMember(this.member).subscribe(() => {
      this.toast.success('Cập nhập thành công');
      this.editForm.reset(this.member);
    })

  }

}
