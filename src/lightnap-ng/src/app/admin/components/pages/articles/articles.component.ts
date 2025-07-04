import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { 
  ButtonModule, 
  CardModule, 
  DataTableModule, 
  DialogModule, 
  DropdownModule, 
  InputTextModule, 
  MultiSelectModule, 
  PaginatorModule, 
  ToastModule 
} from 'primeng/primeng';
import { ConfirmationService, MessageService } from 'primeng/api';
import { ArticlesService } from '@admin/services/articles.service';
import { Article, GetArticlesRequest } from '@admin/models/article.model';
import { ApiResponse } from '@core/models/api';

@Component({
  selector: 'app-articles',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    ButtonModule,
    CardModule,
    DataTableModule,
    DialogModule,
    DropdownModule,
    InputTextModule,
    MultiSelectModule,
    PaginatorModule,
    ToastModule
  ],
  providers: [ConfirmationService, MessageService],
  template: `
    <div class="p-4">
      <p-toast></p-toast>
      
      <div class="card">
        <div class="flex justify-between items-center mb-4">
          <h1 class="text-2xl font-bold">Bicycle Component Articles</h1>
          <p-button 
            label="Add Article" 
            icon="pi pi-plus" 
            (onClick)="showAddDialog()"
            severity="success">
          </p-button>
        </div>

        <!-- Filters -->
        <div class="grid grid-cols-1 md:grid-cols-4 gap-4 mb-4">
          <div class="field">
            <label for="searchTerm" class="block text-sm font-medium mb-2">Search</label>
            <input 
              id="searchTerm"
              type="text" 
              pInputText 
              [(ngModel)]="filters.searchTerm"
              placeholder="Search by article number or name"
              (input)="onFilterChange()">
          </div>
          
          <div class="field">
            <label for="articleCategory" class="block text-sm font-medium mb-2">Article Category</label>
            <p-dropdown 
              id="articleCategory"
              [options]="articleCategories" 
              [(ngModel)]="filters.articleCategory"
              placeholder="Select category"
              [showClear]="true"
              (onChange)="onFilterChange()">
            </p-dropdown>
          </div>
          
          <div class="field">
            <label for="bicycleCategory" class="block text-sm font-medium mb-2">Bicycle Category</label>
            <p-multiSelect 
              id="bicycleCategory"
              [options]="bicycleCategories" 
              [(ngModel)]="selectedBicycleCategories"
              placeholder="Select categories"
              [showClear]="true"
              (onChange)="onBicycleCategoryChange()">
            </p-multiSelect>
          </div>
          
          <div class="field">
            <label for="material" class="block text-sm font-medium mb-2">Material</label>
            <p-dropdown 
              id="material"
              [options]="materials" 
              [(ngModel)]="filters.material"
              placeholder="Select material"
              [showClear]="true"
              (onChange)="onFilterChange()">
            </p-dropdown>
          </div>
        </div>

        <!-- Articles Table -->
        <p-dataTable 
          [value]="articles" 
          [paginator]="true" 
          [rows]="filters.pageSize || 20"
          [totalRecords]="totalRecords"
          [lazy]="true"
          [loading]="loading"
          [sortField]="filters.sortBy"
          [sortOrder]="filters.sortDirection === 'desc' ? -1 : 1"
          (onLazyLoad)="onLazyLoad($event)"
          [showCurrentPageReport]="true"
          currentPageReportTemplate="Showing {first} to {last} of {totalRecords} articles"
          [rowsPerPageOptions]="[10, 20, 50]">
          
          <ng-template pTemplate="header">
            <tr>
              <th pSortableColumn="articleNumber">
                Article Number
                <p-sortIcon field="articleNumber"></p-sortIcon>
              </th>
              <th pSortableColumn="name">
                Name
                <p-sortIcon field="name"></p-sortIcon>
              </th>
              <th pSortableColumn="articleCategory">
                Article Category
                <p-sortIcon field="articleCategory"></p-sortIcon>
              </th>
              <th pSortableColumn="bicycleCategory">
                Bicycle Category
                <p-sortIcon field="bicycleCategory"></p-sortIcon>
              </th>
              <th pSortableColumn="material">
                Material
                <p-sortIcon field="material"></p-sortIcon>
              </th>
              <th pSortableColumn="netWeightG">
                Net Weight (g)
                <p-sortIcon field="netWeightG"></p-sortIcon>
              </th>
              <th>Actions</th>
            </tr>
          </ng-template>
          
          <ng-template pTemplate="body" let-article>
            <tr>
              <td>{{ article.articleNumber }}</td>
              <td>{{ article.name }}</td>
              <td>{{ article.articleCategory }}</td>
              <td>{{ article.bicycleCategory }}</td>
              <td>{{ article.material }}</td>
              <td>{{ article.netWeightG || '-' }}</td>
              <td>
                <div class="flex gap-2">
                  <p-button 
                    icon="pi pi-pencil" 
                    severity="info" 
                    size="small"
                    (onClick)="editArticle(article)">
                  </p-button>
                  <p-button 
                    icon="pi pi-trash" 
                    severity="danger" 
                    size="small"
                    (onClick)="deleteArticle(article)">
                  </p-button>
                </div>
              </td>
            </tr>
          </ng-template>
          
          <ng-template pTemplate="emptymessage">
            <tr>
              <td colspan="7" class="text-center p-4">No articles found.</td>
            </tr>
          </ng-template>
        </p-dataTable>
      </div>

      <!-- Add/Edit Dialog -->
      <p-dialog 
        [(visible)]="dialogVisible" 
        [header]="editingArticle ? 'Edit Article' : 'Add Article'"
        [modal]="true" 
        [style]="{ width: '600px' }"
        [draggable]="false" 
        [resizable]="false">
        
        <div class="grid grid-cols-2 gap-4">
          <div class="field">
            <label for="articleNumber" class="block text-sm font-medium mb-2">Article Number *</label>
            <input 
              id="articleNumber"
              type="text" 
              pInputText 
              [(ngModel)]="articleForm.articleNumber"
              required>
          </div>
          
          <div class="field">
            <label for="name" class="block text-sm font-medium mb-2">Name *</label>
            <input 
              id="name"
              type="text" 
              pInputText 
              [(ngModel)]="articleForm.name"
              required>
          </div>
          
          <div class="field">
            <label for="articleCategory" class="block text-sm font-medium mb-2">Article Category *</label>
            <p-dropdown 
              id="articleCategory"
              [options]="articleCategories" 
              [(ngModel)]="articleForm.articleCategory"
              placeholder="Select category"
              required>
            </p-dropdown>
          </div>
          
          <div class="field">
            <label for="bicycleCategory" class="block text-sm font-medium mb-2">Bicycle Category *</label>
            <p-dropdown 
              id="bicycleCategory"
              [options]="bicycleCategories" 
              [(ngModel)]="articleForm.bicycleCategory"
              placeholder="Select category"
              required>
            </p-dropdown>
          </div>
          
          <div class="field">
            <label for="material" class="block text-sm font-medium mb-2">Material *</label>
            <p-dropdown 
              id="material"
              [options]="materials" 
              [(ngModel)]="articleForm.material"
              placeholder="Select material"
              required>
            </p-dropdown>
          </div>
          
          <div class="field">
            <label for="netWeightG" class="block text-sm font-medium mb-2">Net Weight (g)</label>
            <input 
              id="netWeightG"
              type="number" 
              pInputText 
              [(ngModel)]="articleForm.netWeightG"
              min="0">
          </div>
          
          <div class="field">
            <label for="lengthMm" class="block text-sm font-medium mb-2">Length (mm)</label>
            <input 
              id="lengthMm"
              type="number" 
              pInputText 
              [(ngModel)]="articleForm.lengthMm"
              min="0">
          </div>
          
          <div class="field">
            <label for="widthMm" class="block text-sm font-medium mb-2">Width (mm)</label>
            <input 
              id="widthMm"
              type="number" 
              pInputText 
              [(ngModel)]="articleForm.widthMm"
              min="0">
          </div>
          
          <div class="field">
            <label for="heightMm" class="block text-sm font-medium mb-2">Height (mm)</label>
            <input 
              id="heightMm"
              type="number" 
              pInputText 
              [(ngModel)]="articleForm.heightMm"
              min="0">
          </div>
        </div>
        
        <ng-template pTemplate="footer">
          <p-button 
            label="Cancel" 
            icon="pi pi-times" 
            (onClick)="closeDialog()"
            severity="secondary">
          </p-button>
          <p-button 
            label="Save" 
            icon="pi pi-check" 
            (onClick)="saveArticle()"
            severity="success">
          </p-button>
        </ng-template>
      </p-dialog>
    </div>
  `
})
export class ArticlesComponent implements OnInit {
  #articlesService = inject(ArticlesService);
  #router = inject(Router);
  #messageService = inject(MessageService);
  #confirmationService = inject(ConfirmationService);

  articles: Article[] = [];
  loading = false;
  totalRecords = 0;
  
  // Filters
  filters: GetArticlesRequest = {
    page: 1,
    pageSize: 20,
    sortBy: 'articleNumber',
    sortDirection: 'asc'
  };
  
  selectedBicycleCategories: string[] = [];
  
  // Dropdown options
  articleCategories: string[] = [];
  bicycleCategories: string[] = [];
  materials: string[] = [];
  
  // Dialog
  dialogVisible = false;
  editingArticle: Article | null = null;
  articleForm = {
    articleNumber: '',
    name: '',
    articleCategory: '',
    bicycleCategory: '',
    material: '',
    lengthMm: undefined as number | undefined,
    widthMm: undefined as number | undefined,
    heightMm: undefined as number | undefined,
    netWeightG: undefined as number | undefined
  };

  ngOnInit() {
    this.loadArticles();
    this.loadDropdownOptions();
  }

  loadArticles() {
    this.loading = true;
    this.#articlesService.getArticles(this.filters).subscribe({
      next: (response: ApiResponse<any>) => {
        if (response.result) {
          this.articles = response.result.articles;
          this.totalRecords = response.result.totalCount;
        }
        this.loading = false;
      },
      error: (error) => {
        this.#messageService.add({
          severity: 'error',
          summary: 'Error',
          detail: 'Failed to load articles'
        });
        this.loading = false;
      }
    });
  }

  loadDropdownOptions() {
    // Load article categories
    this.#articlesService.getArticleCategories().subscribe({
      next: (response: ApiResponse<string[]>) => {
        if (response.result) {
          this.articleCategories = response.result;
        }
      }
    });

    // Load bicycle categories
    this.#articlesService.getBicycleCategories().subscribe({
      next: (response: ApiResponse<string[]>) => {
        if (response.result) {
          this.bicycleCategories = response.result;
        }
      }
    });

    // Load materials
    this.#articlesService.getMaterials().subscribe({
      next: (response: ApiResponse<string[]>) => {
        if (response.result) {
          this.materials = response.result;
        }
      }
    });
  }

  onFilterChange() {
    this.filters.page = 1;
    this.loadArticles();
  }

  onBicycleCategoryChange() {
    this.filters.bicycleCategory = this.selectedBicycleCategories.join(',');
    this.onFilterChange();
  }

  onLazyLoad(event: any) {
    this.filters.page = (event.first / event.rows) + 1;
    this.filters.pageSize = event.rows;
    this.filters.sortBy = event.sortField;
    this.filters.sortDirection = event.sortOrder === -1 ? 'desc' : 'asc';
    this.loadArticles();
  }

  showAddDialog() {
    this.editingArticle = null;
    this.resetForm();
    this.dialogVisible = true;
  }

  editArticle(article: Article) {
    this.editingArticle = article;
    this.articleForm = {
      articleNumber: article.articleNumber,
      name: article.name,
      articleCategory: article.articleCategory,
      bicycleCategory: article.bicycleCategory,
      material: article.material,
      lengthMm: article.lengthMm,
      widthMm: article.widthMm,
      heightMm: article.heightMm,
      netWeightG: article.netWeightG
    };
    this.dialogVisible = true;
  }

  saveArticle() {
    if (!this.isFormValid()) {
      this.#messageService.add({
        severity: 'error',
        summary: 'Validation Error',
        detail: 'Please fill in all required fields'
      });
      return;
    }

    const request = {
      articleNumber: this.articleForm.articleNumber,
      name: this.articleForm.name,
      articleCategory: this.articleForm.articleCategory,
      bicycleCategory: this.articleForm.bicycleCategory,
      material: this.articleForm.material,
      lengthMm: this.articleForm.lengthMm,
      widthMm: this.articleForm.widthMm,
      heightMm: this.articleForm.heightMm,
      netWeightG: this.articleForm.netWeightG
    };

    if (this.editingArticle) {
      this.#articlesService.updateArticle(this.editingArticle.id, request).subscribe({
        next: () => {
          this.#messageService.add({
            severity: 'success',
            summary: 'Success',
            detail: 'Article updated successfully'
          });
          this.closeDialog();
          this.loadArticles();
        },
        error: (error) => {
          this.#messageService.add({
            severity: 'error',
            summary: 'Error',
            detail: 'Failed to update article'
          });
        }
      });
    } else {
      this.#articlesService.createArticle(request).subscribe({
        next: () => {
          this.#messageService.add({
            severity: 'success',
            summary: 'Success',
            detail: 'Article created successfully'
          });
          this.closeDialog();
          this.loadArticles();
        },
        error: (error) => {
          this.#messageService.add({
            severity: 'error',
            summary: 'Error',
            detail: 'Failed to create article'
          });
        }
      });
    }
  }

  deleteArticle(article: Article) {
    this.#confirmationService.confirm({
      message: `Are you sure you want to delete article "${article.name}"?`,
      header: 'Confirm Delete',
      icon: 'pi pi-exclamation-triangle',
      accept: () => {
        this.#articlesService.deleteArticle(article.id).subscribe({
          next: () => {
            this.#messageService.add({
              severity: 'success',
              summary: 'Success',
              detail: 'Article deleted successfully'
            });
            this.loadArticles();
          },
          error: (error) => {
            this.#messageService.add({
              severity: 'error',
              summary: 'Error',
              detail: 'Failed to delete article'
            });
          }
        });
      }
    });
  }

  closeDialog() {
    this.dialogVisible = false;
    this.resetForm();
  }

  resetForm() {
    this.articleForm = {
      articleNumber: '',
      name: '',
      articleCategory: '',
      bicycleCategory: '',
      material: '',
      lengthMm: undefined,
      widthMm: undefined,
      heightMm: undefined,
      netWeightG: undefined
    };
  }

  isFormValid(): boolean {
    return !!(
      this.articleForm.articleNumber &&
      this.articleForm.name &&
      this.articleForm.articleCategory &&
      this.articleForm.bicycleCategory &&
      this.articleForm.material
    );
  }
} 