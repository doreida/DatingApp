import { CanDeactivateFn } from '@angular/router';
import { MemberEditComponent } from '../members/member-edit/member-edit.component';

export const preventUnsavedChangesGuard: CanDeactivateFn<MemberEditComponent> = (component) => {
  
    if (component.editForm?.dirty) {
      const confirmation = window.confirm('Are you sure you want to continue? Any unsaved changes will be lost.');
      return confirmation;
    }
  
  return true;
};
