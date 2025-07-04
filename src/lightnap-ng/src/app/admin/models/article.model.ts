export interface Article {
  id: number;
  articleNumber: string;
  name: string;
  articleCategory: string;
  bicycleCategories: string[];
  material: string;
  lengthMm?: number;
  widthMm?: number;
  heightMm?: number;
  netWeightG?: number;
  createdDate: string;
  lastModifiedDate: string;
}

export interface CreateArticleRequest {
  articleNumber: string;
  name: string;
  articleCategory: string;
  bicycleCategories: string[];
  material: string;
  lengthMm?: number;
  widthMm?: number;
  heightMm?: number;
  netWeightG?: number;
}

export interface UpdateArticleRequest extends CreateArticleRequest {}

export interface GetArticlesRequest {
  articleCategory?: string;
  bicycleCategory?: string | string[];
  material?: string;
  searchTerm?: string;
  sortBy?: string;
  sortDirection?: string;
  page?: number;
  pageSize?: number;
}

export interface GetArticlesResponse {
  articles: Article[];
  totalCount: number;
  page: number;
  pageSize: number;
  totalPages: number;
  hasNextPage: boolean;
  hasPreviousPage: boolean;
}
