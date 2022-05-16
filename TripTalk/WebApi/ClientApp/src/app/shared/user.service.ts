import { HttpClient } from '@angular/common/http'
import { Injectable } from '@angular/core'
import { firstValueFrom } from 'rxjs'

import { baseApiUrl } from '../app.config'
import { ArticleService } from './article.service'
import { AuthService } from './auth.service'
import { Account } from './interfaces/account.interface'

@Injectable({ providedIn: 'root' })
export class UserService {
  constructor(private http: HttpClient, private authService: AuthService, private articleService: ArticleService) {}

  async getAccountById(id: number) {
    if (this.authService.myAccount.value && this.authService.myAccount.value.user.id === id)
      return this.authService.myAccount.value

    const response = await firstValueFrom(this.http.get<Account>(`${baseApiUrl}/Account/${id}`))

    for (const article of response.articles) {
      this.articleService.addToCache(article)
    }

    return response
  }
}
