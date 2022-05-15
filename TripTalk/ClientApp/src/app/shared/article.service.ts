import { Injectable } from '@angular/core'
import { HttpClient } from '@angular/common/http'
import { firstValueFrom } from 'rxjs'

import { baseApiUrl } from '../app.config'
import { Article } from './interfaces/article.interface'
import { NewArticle } from './interfaces/new-article.interface'
import { Comment } from './interfaces/comment.interface'
import { AuthService } from './auth.service'

interface ArticlesResponse {
  articles: Article[]
  totalCount: number
}

interface MainPageArticles {
  popularArticles: Article[]
  bestArticles: Article[]
  latestArticles: Article[]
}

@Injectable({
  providedIn: 'root',
})
export class ArticleService {
  private _articlesCache = new Map<number, Article>()

  constructor(private http: HttpClient, private authService: AuthService) {}

  async getPopularArticles() {
    const response = await firstValueFrom(this.http.get<ArticlesResponse>(`${baseApiUrl}/Article/Popular`))

    for (const article of response.articles) {
      this.addToCache(article)
    }

    return response.articles
  }

  async getMainPageArticles() {
    const response = await firstValueFrom(this.http.get<MainPageArticles>(`${baseApiUrl}/Main`))

    for (const article of response.bestArticles) this.addToCache(article)
    for (const article of response.latestArticles) this.addToCache(article)
    for (const article of response.latestArticles) this.addToCache(article)

    return response
  }

  async createNewArticle(article: NewArticle) {
    const response = await firstValueFrom(
      this.http.post<Article>(`${baseApiUrl}/Article/Create`, article, {
        headers: this.authService.headers,
      }),
    )

    this.addToCache(response)

    return response
  }

  async getArticleById(id: number) {
    const cachedArticle = this._articlesCache.get(id)
    if (cachedArticle) return cachedArticle

    const response = await firstValueFrom(this.http.get<Article>(`${baseApiUrl}/Article/${id}`))
    this.addToCache(response)

    return response
  }

  async updateArticle(id: number, article: NewArticle) {
    const response = await firstValueFrom(
      this.http.put<Article>(`${baseApiUrl}/Article/Edit?articleId=${id}`, article, {
        headers: this.authService.headers,
      }),
    )
    this.addToCache(response)

    return response
  }

  addToCache(article: Article) {
    this._articlesCache.set(article.id, article)
  }

  async searchForArticles(query: string, tags: string | null, pageNumber: number) {
    const response = await firstValueFrom(
      this.http.get<ArticlesResponse>(`${baseApiUrl}/Search?q=${query}&tags=${tags}&pageNumber=${pageNumber}`),
    )
    for (const article of response.articles) this.addToCache(article)
    return response
  }

  async addRateForArticle(articleId: number, rate: number) {
    firstValueFrom(
      this.http.post<any>(`${baseApiUrl}/Article/AddRate?articleId=${articleId}&rate=${rate}`, '', {
        headers: this.authService.headers,
      }),
    )
  }

  async getMyRateForArticle(articleId: number) {
    const value = await firstValueFrom(
      this.http.get(`${baseApiUrl}/Article/GetCurrentUserRate?articleId=${articleId}`, {
        responseType: 'text',
        headers: this.authService.headers,
      }),
    )

    const parsedValue = Number(value)
    return parsedValue
  }

  async getArticlesByCategory(category: string, pageNumber: number) {
    const response = await firstValueFrom(
      this.http.get<ArticlesResponse>(`${baseApiUrl}/Article/${category}?pageNumber=${pageNumber}`),
    )
    for (const article of response.articles) this.addToCache(article)
    return response
  }

  async getMyArticles(pageNumber: number) {
    const response = await firstValueFrom(
      this.http.get<ArticlesResponse>(`${baseApiUrl}/Account/MyArticles?pageNumber=${pageNumber}`, {
        headers: this.authService.headers,
      }),
    )
    for (const article of response.articles) this.addToCache(article)
    return response
  }

  async getCommentsForArticle(articleId: number) {
    return await firstValueFrom(
      this.http.get<Comment[]>(`${baseApiUrl}/Comment/ArticleComments/${articleId}`, {
        headers: this.authService.headers,
      }),
    )
  }

  async addCommentForArticle(articleId: number, message: string) {
    return await firstValueFrom(
      this.http.post<Comment>(
        `${baseApiUrl}/Comment/Add`,
        { articleId, message },
        { headers: this.authService.headers },
      ),
    )
  }
}
