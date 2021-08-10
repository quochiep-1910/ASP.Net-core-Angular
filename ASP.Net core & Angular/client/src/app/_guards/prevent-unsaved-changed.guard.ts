import { MemberEditComponent } from './../members/member-edit/member-edit.component';
import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanDeactivate, RouterStateSnapshot, UrlTree } from '@angular/router';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class PreventUnsavedChangedGuard implements CanDeactivate<unknown> {
  canDeactivate(
    component: MemberEditComponent): boolean | UrlTree {
    if (component.editForm.dirty) {
      return confirm('Bạn muốn rời khỏi trang? Nội dung sẽ không được lưu lại! ')
    }
    return true;
  }

}
