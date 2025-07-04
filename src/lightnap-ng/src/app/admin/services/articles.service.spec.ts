import { TestBed } from "@angular/core/testing";
import { HttpClientTestingModule, HttpTestingController } from "@angular/common/http/testing";
import { ArticlesService } from "./articles.service";
import { ApiResponse } from "@core/models/api";

describe("ArticlesService", () => {
  let service: ArticlesService;
  let httpMock: HttpTestingController;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      providers: [ArticlesService],
    });
    service = TestBed.inject(ArticlesService);
    httpMock = TestBed.inject(HttpTestingController);
  });

  afterEach(() => {
    httpMock.verify();
  });

  it("should fetch articles", () => {
    const mockResponse: ApiResponse<any> = { result: { articles: [], totalCount: 0 }, type: 0 };
    service.getArticles({}).subscribe(res => {
      expect(res.result!.articles).toEqual([]);
      expect(res.result!.totalCount).toBe(0);
    });
    const req = httpMock.expectOne(r => r.url.includes("/articles"));
    expect(req.request.method).toBe("GET");
    req.flush(mockResponse);
  });
});
