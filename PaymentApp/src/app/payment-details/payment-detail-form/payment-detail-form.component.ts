import { Component } from '@angular/core';
import { NgForm } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { PaymentDetail } from '../../shared/payment-detail.model';
import { PaymentDetailService } from '../../shared/payment-detail.service';

@Component({
  selector: 'app-payment-detail-form',
  templateUrl: './payment-detail-form.component.html',
  styles: [

  ]
})
export class PaymentDetailFormComponent {

    constructor(public service: PaymentDetailService , private toastr: ToastrService){

    }

    onSubmit(form: NgForm){
      this.service.formSubmitted = true
      if(form.valid){
        if(this.service.formData.paymentDetailId == 0){
          this.insertRecord(form)
        }else{
          this.updateRecord(form)
        }
        }
      }

    

    insertRecord(form: NgForm){
      this.service.postPaymentDetail()
      .subscribe({
        next: res => {
         this.service.list = res as PaymentDetail[]
         this.service.resetForm(form)
         this.toastr.success('Inserted successfully', 'Payment Detail Register')
         },
         error: err => {console.error(err)}
      })
    }
    updateRecord(form: NgForm){
      this.service.putPaymentDetail()
      .subscribe({
        next: res => {
         this.service.list = res as PaymentDetail[]
         this.service.resetForm(form)
         this.toastr.info('Update successfully', 'Payment Detail Register')
         },
         error: err => {console.error(err)}
      })
    }
  }
