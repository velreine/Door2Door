import { Injectable } from '@angular/core';   
import { ToastrService } from 'ngx-toastr';
   
@Injectable({
  providedIn: 'root'
})
export class NotificationService {
   
  constructor(private _toastr: ToastrService) { 
    this._toastr.toastrConfig.preventDuplicates = true;
    this._toastr.toastrConfig.positionClass = "toast-bottom-center";
  }
   
  showSuccess(message, title){
      this._toastr.success(message, title)
  }
   
  showError(message, title){
      this._toastr.error(message, title)
  }
   
  showInfo(message, title){
      this._toastr.info(message, title)
  }
   
  showWarning(message, title){
      this._toastr.warning(message, title)
  }   
}