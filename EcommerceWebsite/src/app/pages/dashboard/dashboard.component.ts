import { Component, OnInit } from '@angular/core';
import { ProductService } from '../../services/product.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent implements OnInit {

  products: any[] = [];
  selectedProduct: any = {
    gameId: null,
    title: '',
    year: 0,
    summary: '',
    categoryId: 0,
    price: 0,
    imageURL: ''
  };
  isEditing: boolean = false;

  constructor(private productService: ProductService, private router: Router) { }

  ngOnInit(): void {
    this.loadAllProducts();
  }

  loadAllProducts(): void {
    this.productService.getAllProducts().subscribe(
      (response) => {
        this.products = response;
      },
      (error) => {
        console.error('Error loading products', error);
      }
    );
  }

  addProduct(): void {
    // Check if the selected product is valid
    if (!this.selectedProduct) {
      alert('Invalid product data.');
      return;
    }

    this.productService.createProduct(this.selectedProduct).subscribe(
      (response) => {
        // Handle the case where the product already exists
        if (response.status === 409) {
          alert('Product with ID already exists.');
        } else {
          alert('Product added successfully');
          this.loadAllProducts();
          this.resetForm();
        }
      },
      (error) => {
        console.error('Error adding product', error);
        alert('Error adding product');
      }
    );
  }

  updateProduct(): void {
    // Ensure the selected product has a valid gameId before attempting to update
    if (!this.selectedProduct.gameId) {
      alert('Product ID is required for update.');
      return;
    }

    // Call the update product service method
    this.productService.updateProduct(this.selectedProduct.gameId, this.selectedProduct).subscribe(
      (response) => {
        alert('Product updated successfully');
        this.loadAllProducts();
        this.resetForm();
      },
      (error) => {
        console.error('Error updating product', error);
        alert('Error updating product');
      }
    );
  }

  deleteProduct(id: number): void {
    if (confirm('Are you sure you want to delete this product?')) {
      this.productService.deleteProduct(id).subscribe(
        (response) => {
          alert('Product deleted successfully');
          this.loadAllProducts();
        },
        (error) => {
          console.error('Error deleting product', error);
        }
      );
    }
  }

  selectProduct(product: any): void {
    this.selectedProduct = { ...product };
    this.isEditing = true;
  }

  resetForm(): void {
    this.selectedProduct = {
      gameId: null,
      title: '',
      year: 0,
      summary: '',
      categoryId: 0,
      price: 0,
      imageURL: ''
    };
    this.isEditing = false;
  }

}
