import { HttpErrorResponse } from '@angular/common/http'
import { Component } from '@angular/core'
import { FormControl, Validators } from '@angular/forms'
import { ActivatedRoute, Router } from '@angular/router'

import { ArticleService } from '../shared/article.service'
import { AuthService } from '../shared/auth.service'
import { Article } from '../shared/interfaces/article.interface'
import { Comment } from '../shared/interfaces/comment.interface'

@Component({
  selector: 'app-article',
  templateUrl: './article.component.html',
  styleUrls: ['./article.component.scss'],
})
export class ArticleComponent {
  article: Article | null = null
  myRate = 0
  baseArticleRate = 0
  commentInputControl = new FormControl(null, [Validators.required])
  comments: Comment[] | null = null
  isCommentFormBusy = false

  constructor(
    private articleService: ArticleService,
    private authService: AuthService,
    private route: ActivatedRoute,
    private router: Router,
  ) {
    const id = route.snapshot.params['id']
    if (!id) router.navigateByUrl('/')
    const parsedId = Number(id)

    this._fetchArticleData(parsedId)
  }

  private _fetchArticleData(articleId: number) {
    Promise.all([
      this.articleService
        .getArticleById(articleId)
        .then(value => (this.article = value))
        .catch((error: HttpErrorResponse) => {
          if (error.status === 404) {
            this.router.navigateByUrl('/')
          }
        }),
      this.articleService.getMyRateForArticle(articleId).then(value => (this.myRate = value)),
    ]).then(() => {
      this.baseArticleRate = (this.article?.rating ?? 0) - this.myRate
    })

    this.articleService.getCommentsForArticle(articleId).then(comments => (this.comments = comments))
  }

  onUpvote() {
    if (!this.article) return
    this.myRate = 1
    this.article.rating = this.baseArticleRate + 1

    this.articleService.addRateForArticle(this.article.id, this.myRate)
  }

  onDownvote() {
    if (!this.article) return
    this.myRate = -1
    this.article.rating = this.baseArticleRate - 1

    this.articleService.addRateForArticle(this.article.id, this.myRate)
  }

  get isCommentSubmitButtonDisabled() {
    return !this.authService.isLoggedIn || this.commentInputControl.invalid || this.isCommentFormBusy
  }

  async onCommentAdd(event: Event) {
    event.preventDefault()

    if (this.isCommentFormBusy) return
    if (this.commentInputControl.invalid) return
    if (!this.article) return

    this.isCommentFormBusy = true
    const comment = await this.articleService.addCommentForArticle(this.article.id, this.commentInputControl.value)
    this.comments?.unshift(comment)
    this.commentInputControl.setValue(null)
    this.isCommentFormBusy = false
  }
}
