import { Component } from '@angular/core';
import { ProductService } from './services/product.service';
import { parseHostBindings } from '@angular/compiler';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  games: any[] = [];
  searchTitle: string = '';

  title = 'EcommerceWebsite';
  registerObj: any = {
    "customerId": 0,
    "userName": "",
    "password": "",
    "confirmPassword": "",
    "email": ""
  }

  loginObj: any = {
    "userName": "",
    "password": ""
  }

  loggedObj: any = {};
  cartItems: any[] = [];
  newProduct: any;

  constructor(private productSrv: ProductService) {
    const localData = localStorage.getItem('GameEcommerceUser');
    if (localData != null) {
      const parseObj = JSON.parse(localData);
      this.loggedObj = parseObj;
      this.getCartData(this.loggedObj.customerId)
    }
    this.productSrv.cartUpdated.subscribe((res: boolean) => {
      if (res) {
        this.getCartData(this.loggedObj.customerId)
      }
    })
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



  onRegister() {
    this.productSrv.register(this.registerObj).subscribe(
      (res: any) => {
        console.log(res); // In phản hồi ra console để kiểm tra cấu trúc

        // Kiểm tra nếu API trả về thông báo "Đã tạo tài khoản thành công"
        if (res.Message === "Đã tạo tài khoản thành công" || res.message === "Đã tạo tài khoản thành công") {
          this.loggedObj = res.data;
          alert("Đã tạo tài khoản thành công");
        }
      },
      (error) => {
        // Kiểm tra lỗi từ API, thông báo khi tài khoản đã tồn tại, email không hợp lệ hoặc các lỗi khác
        if (error.error === "Tài khoản đã tồn tại.") {
          alert("Tài khoản đã tồn tại.");
        } else if (error.error === "Email đã được sử dụng.") {
          alert("Email đã được sử dụng.");
        } else if (error.error === "Email không hợp lệ. Vui lòng nhập đúng định dạng (ví dụ: example@domain.com).") {
          alert("Email không hợp lệ. Vui lòng nhập đúng định dạng.");
        } else if (error.error === "Mật khẩu và xác nhận mật khẩu không khớp.") {
          alert("Mật khẩu và xác nhận mật khẩu không khớp.");
        } else {
          alert("Đăng ký thất bại, vui lòng thử lại.");
        }
      }
    );
  }


  onLogin() {
    this.productSrv.login(this.loginObj).subscribe(
      (res: any) => {
        // Kiểm tra thông báo trả về từ API để xác nhận đăng nhập thành công
        if (res.Message === "Đăng nhập thành công" || res.message === "Đăng nhập thành công") {
          alert("Đăng nhập thành công");
          this.loggedObj = res.data;
          // Lưu toàn bộ thông tin đăng ký của người dùng vào localStorage
          localStorage.setItem('GameEcommerceUser', JSON.stringify(res.data)); // Lưu `res.data` chứa toàn bộ thông tin
          this.getCartData(this.loggedObj.customerId)
        } else {
          alert("Sai tài khoản hoặc mật khẩu");
        }
      },
      (error) => {
        // Nếu có lỗi trả về từ API, hiển thị thông báo đăng nhập thất bại
        alert("Sai tài khoản hoặc mật khẩu");
      }
    );
  }

  removeItem(id: number) {
    this.productSrv.removeProductFromCart(id).subscribe(
      (res: any) => {
        // Kiểm tra thông báo trả về từ API để xác nhận xóa thành công
        if (res.message === "Cart deleted successfully.") {
          alert("Item removed successfully!");
          this.getCartData(this.loggedObj.customerId); // Cập nhật lại danh sách giỏ hàng
        }
      },
      (error) => {
        // Xử lý lỗi khi xóa thất bại
        console.error("Error removing item:", error);
        alert("Failed to remove item. Please try again.");
      }
    );
  }

  getSubtotal(): number {
    return this.cartItems.reduce((total, item) => total + item.game.price, 0);
  }

  searchGames(): void {
    if (this.searchTitle) {
      this.productSrv.getAllProductsByTitle(this.searchTitle).subscribe({
        next: (games) => {
          this.games = games;  // Lưu kết quả vào biến games
        },
        error: (err) => {
          console.error('Error fetching games:', err);
          this.games = [];
        }
      });

    }
  }


  addtocart(gameId: number): void {
    const cartItem = {
      gameId: gameId,
      customerId: this.loggedObj.customerId,
      quantity: 1, // Default quantity
      addedDate: new Date().toISOString() // ISO format ensures compatibility with most APIs
    };

    this.productSrv.addtoCart(cartItem).subscribe({
      next: (response: any) => {
        console.log('Item successfully added to cart:', response);
        this.productSrv.cartUpdated.next(true); // Notify observers that the cart has been updated
      },
      error: (error: any) => {
        console.error('Failed to add item to cart:', error);
        // Optional: Show a user-friendly message
        alert('An error occurred while adding the item to the cart. Please try again.');
      }
    });
  }

}
