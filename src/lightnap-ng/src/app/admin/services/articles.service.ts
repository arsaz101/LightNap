import { inject, Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { API_URL_ROOT, ApiResponse } from '@core';
import { 
  Article, 
  CreateArticleRequest, 
  UpdateArticleRequest, 
  GetArticlesRequest, 
  GetArticlesResponse 
} from '../models/article.model';

@Injectable({
  providedIn: 'root'
})
export class ArticlesService {
  #http = inject(HttpClient);
  #apiUrlRoot = `${inject(API_URL_ROOT)}articles/`;

  getArticles(request: GetArticlesRequest): Observable<ApiResponse<GetArticlesResponse>> {
    let params = new HttpParams();
    
    if (request.articleCategory) {
      params = params.set('articleCategory', request.articleCategory);
    }
    if (request.bicycleCategory) {
      params = params.set('bicycleCategory', request.bicycleCategory);
    }
    if (request.material) {
      params = params.set('material', request.material);
    }
    if (request.searchTerm) {
      params = params.set('searchTerm', request.searchTerm);
    }
    if (request.sortBy) {
      params = params.set('sortBy', request.sortBy);
    }
    if (request.sortDirection) {
      params = params.set('sortDirection', request.sortDirection);
    }
    if (request.page) {
      params = params.set('page', request.page.toString());
    }
    if (request.pageSize) {
      params = params.set('pageSize', request.pageSize.toString());
    }

    return this.#http.get<ApiResponse<GetArticlesResponse>>(this.#apiUrlRoot, { params });
  }

  getArticle(id: number): Observable<ApiResponse<Article>> {
    return this.#http.get<ApiResponse<Article>>(`${this.#apiUrlRoot}${id}`);
  }

  createArticle(request: CreateArticleRequest): Observable<ApiResponse<Article>> {
    return this.#http.post<ApiResponse<Article>>(this.#apiUrlRoot, request);
  }

  updateArticle(id: number, request: UpdateArticleRequest): Observable<ApiResponse<Article>> {
    return this.#http.put<ApiResponse<Article>>(`${this.#apiUrlRoot}${id}`, request);
  }

  deleteArticle(id: number): Observable<ApiResponse<boolean>> {
    return this.#http.delete<ApiResponse<boolean>>(`${this.#apiUrlRoot}${id}`);
  }

  getArticleCategories(): Observable<ApiResponse<string[]>> {
    return this.#http.get<ApiResponse<string[]>>(`${this.#apiUrlRoot}categories`);
  }

  getBicycleCategories(): Observable<ApiResponse<string[]>> {
    return this.#http.get<ApiResponse<string[]>>(`${this.#apiUrlRoot}bicycle-categories`);
  }

  getMaterials(): Observable<ApiResponse<string[]>> {
    return this.#http.get<ApiResponse<string[]>>(`${this.#apiUrlRoot}materials`);
  }
} 