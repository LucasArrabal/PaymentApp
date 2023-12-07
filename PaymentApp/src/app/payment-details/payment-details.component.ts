import { Component, OnInit } from '@angular/core';
import { PaymentDetailService } from '../shared/payment-detail.service';
import { PaymentDetail } from '../shared/payment-detail.model';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-payment-details',
  templateUrl: './payment-details.component.html',
  styles: ``
})
export class PaymentDetailsComponent  implements OnInit{

    constructor(public service: PaymentDetailService, private toastr: ToastrService){

    }
  ngOnInit(): void {
    this.service.refreshList();
  }
  populate(selectorRecord: PaymentDetail){
    this.service.formData = Object.assign({}, selectorRecord);
  }
  onDelete(id: number){
    if(confirm('Aree you  sure that tou wanna delete this recordd ?'))
    this.service.deletePaymentDetail(id)
    .subscribe({
      next: res => {
       this.service.list = res as PaymentDetail[]
       this.toastr.error('Deleted successfully', 'Payment Detail Register')
       },
       error: err => {console.error(err)}
    })
  }
}
