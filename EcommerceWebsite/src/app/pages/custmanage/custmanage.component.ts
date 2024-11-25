import { Component, OnInit } from '@angular/core';
import { ProductService } from '../../services/product.service';

@Component({
  selector: 'app-custmanage',
  templateUrl: './custmanage.component.html',
  styleUrl: './custmanage.component.css'
})
export class CustmanageComponent implements OnInit {
  customers: any[] = []; // Danh sách khách hàng
  selectedCustomer: any = null; // Dùng để lưu thông tin khách hàng được chọn
  isEditing: boolean = false; // Trạng thái chỉnh sửa

  // Các biến dữ liệu cho form
  customerId: number | null = null;
  userName: string = '';
  password: string = '';
  confirmPassword: string = '';
  email: string = '';

  constructor(private productService: ProductService) { }

  ngOnInit(): void {
    this.getCustomers();
  }

  // Lấy danh sách khách hàng
  getCustomers(): void {
    this.productService.getAllCustomer().subscribe({
      next: (data) => {
        this.customers = data;
      },
      error: (err) => {
        console.error('Lỗi khi tải danh sách khách hàng:', err);
      },
    });
  }

  // Bắt đầu chỉnh sửa khách hàng
  editCustomer(customer: any): void {
    this.isEditing = true;
    this.selectedCustomer = customer;

    // Điền dữ liệu của khách hàng được chọn vào form
    this.customerId = customer.customerId;
    this.userName = customer.userName;
    this.password = customer.password;
    this.confirmPassword = customer.confirmPassword;
    this.email = customer.email;
  }

  // Lưu thông tin khách hàng (cập nhật hoặc thêm mới)
  saveCustomer(): void {
    if (!this.userName || !this.password || !this.confirmPassword || !this.email) {
      alert('Vui lòng điền đầy đủ thông tin.');
      return;
    }

    if (this.password !== this.confirmPassword) {
      alert('Mật khẩu và xác nhận mật khẩu không khớp.');
      return;
    }

    const customerData = {
      customerId: this.customerId,
      userName: this.userName,
      password: this.password,
      confirmPassword: this.confirmPassword,
      email: this.email,
    };

    if (this.isEditing && this.customerId) {
      // Cập nhật thông tin khách hàng
      this.productService.updateCustomer(this.customerId, customerData).subscribe({
        next: () => {
          alert('Cập nhật thành công.');
          this.getCustomers();
          this.resetForm();
        },
        error: (err) => {
          console.error('Lỗi khi cập nhật khách hàng:', err);
        },
      });
    } else {
      alert('Chỉ hỗ trợ cập nhật khách hàng.');
    }
  }

  // Xóa khách hàng
  deleteCustomer(id: number): void {
    if (confirm('Bạn có chắc chắn muốn xóa khách hàng này?')) {
      this.productService.deleteCustomer(id).subscribe({
        next: () => {
          alert('Xóa thành công.');
          this.getCustomers();
        },
        error: (err) => {
          console.error('Lỗi khi xóa khách hàng:', err);
        },
      });
    }
  }

  // Reset form và trạng thái
  resetForm(): void {
    this.customerId = null;
    this.userName = '';
    this.password = '';
    this.confirmPassword = '';
    this.email = '';
    this.isEditing = false;
    this.selectedCustomer = null;
  }
}
