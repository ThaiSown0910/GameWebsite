import { Component, OnInit } from '@angular/core';
import { ProductService } from '../../services/product.service';

@Component({
  selector: 'app-products',
  templateUrl: './products.component.html',
  styleUrls: ['./products.component.css']
})
export class ProductsComponent implements OnInit {
  searchTitle: string = '';
  productsArray: any[] = [];
  categories: any[] = [];
  selectedCategory: number = 0;
  loggedObj: any = {}
  newProduct: any = {}; // Khởi tạo với đối tượng rỗng
  editProductData: any = { gameId: null }; // Dữ liệu cho việc chỉnh sửa sản phẩm


  constructor(private productSrv: ProductService) {
    const localData = localStorage.getItem('GameEcommerceUser');
    if (localData != null) {
      const parseObj = JSON.parse(localData);
      this.loggedObj = parseObj;
    }
  }

  ngOnInit(): void {
    this.loadProducts();  // Tải danh sách sản phẩm khi component được khởi tạo
    this.loadCategory();  // Tải danh sách thể loại
  }

  loadProducts(): void {
    this.productSrv.getAllProducts().subscribe({
      next: (response: any) => {
        this.productsArray = response.data || response; // Điều chỉnh dựa trên phản hồi API
      },
      error: (error) => {
        console.error('Error loading products', error);
      }
    });
  }

  getAllProductsByCategory(categoryId: number): void {
    this.selectedCategory = categoryId;
    this.productSrv.getAllProductsByCategory(categoryId).subscribe({
      next: (response: any) => {
        this.productsArray = response.data || response; // Điều chỉnh theo phản hồi API
      },
      error: (error) => {
        console.error('Error loading products by category', error);
      }
    });
  }

  loadCategory(): void {
    this.productSrv.getAllCategory().subscribe({
      next: (response: any) => {
        this.categories = response.data || response; // Điều chỉnh theo phản hồi API
      },
      error: (error) => {
        console.error('Error loading categories', error);
      }
    });
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


  searchGames(): void {
    if (this.searchTitle) {
      this.productSrv.getAllProductsByTitle(this.searchTitle).subscribe({
        next: (games) => {
          this.productsArray = games;  // Lưu kết quả vào biến games
        },
        error: (err) => {
          console.error('Error fetching games:', err);
          this.productsArray = [];
        }
      });

    }
  }


}


