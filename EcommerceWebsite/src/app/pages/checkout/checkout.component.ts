import { Component, OnInit } from '@angular/core';
import { ProductService } from '../../services/product.service';

@Component({
  selector: 'app-checkout',
  templateUrl: './checkout.component.html',
  styleUrl: './checkout.component.css'
})
export class CheckoutComponent implements OnInit {


  loggedObj: any = {};
  cartItems: any[] = [];
  checkoutObj: any = {
    "saleId": 0,
    "customerId": 0,
    "saleDate": new Date(),
    "totalInvoiceAmount": 0,
    "discount": 0,
    "paymentNaration": "",
    "phoneNumber": "",
    "discountCode": "",
    "deliveryCity": "",
    "deliveryPinCode": "",
    "fullofName": ""
  }

  constructor(private productSrv: ProductService) {
    const localData = localStorage.getItem('GameEcommerceUser');
    if (localData != null) {
      const parseObj = JSON.parse(localData);
      this.loggedObj = parseObj;
      this.getCartData(this.loggedObj.customerId)
    }
  }

  ngOnInit(): void {

  }

  getCartData(id: number) {
    this.productSrv.getAddtocartdataByCust(id).subscribe((res: any) => {
      if (res && res.length > 0) {
        this.cartItems = res; // Assuming `res` directly contains the cart items
      } else {
        this.cartItems = []; // Handle empty or no cart items
      }
    }, error => {
      console.error('Error fetching cart data', error);
      // Optionally handle the error, e.g., show a notification
    });
  }


  getSubtotal(): number {
    return this.cartItems.reduce((total, item) => total + item.game.price, 0);
  }

  placeOrder() {
    // Create the checkoutDTO object directly from checkoutObj
    const checkoutDTO = {
      customerId: this.loggedObj.customerId,
      saleDate: new Date(),  // or use an appropriate date if needed
      totalInvoiceAmount: this.checkoutObj.totalInvoiceAmount,
      discount: this.checkoutObj.discount,
      paymentNaration: this.checkoutObj.paymentNaration,
      phoneNumber: this.checkoutObj.phoneNumber,
      discountCode: this.checkoutObj.discountCode,
      deliveryCity: this.checkoutObj.deliveryCity,
      deliveryPinCode: this.checkoutObj.deliveryPinCode,
      fullofName: this.checkoutObj.fullofName
    };

    this.productSrv.PlaceOrder(checkoutDTO).subscribe((res: any) => {
      // Assuming the server returns the CheckoutDTO with the SaleId
      if (res.saleId) {
        this.productSrv.cartUpdated.next(true);
        alert("Order Has Been Success");
      } else {
        alert("Order Creation Failed");
      }
    },
    );
  }
}
