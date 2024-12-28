import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { catchError } from 'rxjs/operators';
import { of } from 'rxjs';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  loginAdminObj: Login;

  constructor(private http: HttpClient, private router: Router) {
    // Khởi tạo đối tượng loginAdminObj
    this.loginAdminObj = new Login();
  }

  onAdminLogin() {
    // Gửi yêu cầu đăng nhập tới API
    this.http.post('https://localhost:7279/api/AdminAuth/Login', this.loginAdminObj)
      .pipe(
        // Xử lý lỗi nếu có
        catchError(err => {
          console.error('Login failed', err);
          return of(null); // Trả về null để không ảnh hưởng đến luồng xử lý
        })
      )
      .subscribe((res: any) => {
        console.log(res);
        if (res && res.Message === "Login successful") {
          // Hiển thị thông báo khi đăng nhập thành công
          alert("Login Success");

          // Điều hướng đến trang Dashboard trong LayoutComponent
          this.router.navigateByUrl('layout/dashboard');


        } else {
          // Hiển thị thông báo lỗi nếu đăng nhập thất bại
          alert(res?.message || 'Invalid username or password');
        }
      });
  }
}

// Định nghĩa class Login để tạo đối tượng loginAdminObj
export class Login {
  userName: string;
  password: string;

  constructor() {
    this.userName = '';
    this.password = '';
  }
}
