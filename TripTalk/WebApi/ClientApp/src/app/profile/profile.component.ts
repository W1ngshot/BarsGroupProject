import { HttpErrorResponse } from '@angular/common/http'
import { Component } from '@angular/core'
import { ActivatedRoute, Router } from '@angular/router'
import { AuthService } from '../shared/auth.service'
import { Article } from '../shared/interfaces/article.interface'
import { User } from '../shared/interfaces/user.interface'
import { UserService } from '../shared/user.service'

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.scss'],
})
export class ProfileComponent {
  currentUser: User | null = null
  currentUserArticles: Article[] = []
  isSelfProfile = false
  totalArticleCount = 0

  constructor(
    private userService: UserService,
    private authService: AuthService,
    private route: ActivatedRoute,
    private router: Router,
  ) {
    if (route.snapshot.params['id']) {
      const parsedId = Number(route.snapshot.params['id'])
      if (isNaN(parsedId)) router.navigateByUrl('/')

      this._redirectOnMyProfile(parsedId)
      this._fetchAccount(parsedId)
    } else {
      this._setMyAccount()
    }
  }

  private _redirectOnMyProfile(userId: number) {
    this.authService.myAccount.subscribe(account => {
      if (!account) return
      if (account.user.id === userId)
        this.router.navigateByUrl('/profile', {
          replaceUrl: true,
        })
    })
  }

  private _fetchAccount(userId: number) {
    this.userService
      .getAccountById(userId)
      .then(account => {
        this.currentUser = account.user
        this.currentUserArticles = account.articles
        this.totalArticleCount = account.totalCount
      })
      .catch((error: HttpErrorResponse) => {
        if (error.status !== 0) this.router.navigateByUrl('/')
      })
  }

  private _setMyAccount() {
    this.authService.myAccount.subscribe(account => {
      if (!account) return

      this.currentUser = account.user
      this.currentUserArticles = account.articles
      this.totalArticleCount = account.totalCount
    })

    this.isSelfProfile = true
  }

  get currentUserArticleRows() {
    const result: Article[][] = []

    for (let i = 0; i < this.currentUserArticles.length; i += 2) {
      const row = [this.currentUserArticles[i]]
      if (i + 1 < this.currentUserArticles.length) row.push(this.currentUserArticles[i + 1])
      result.push(row)
    }

    return result
  }
}
