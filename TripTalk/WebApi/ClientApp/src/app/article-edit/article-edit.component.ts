import { HttpErrorResponse } from '@angular/common/http'
import { Component } from '@angular/core'
import { FormControl, FormGroup, Validators } from '@angular/forms'
import { ActivatedRoute, Router } from '@angular/router'

import { ArticleService } from '../shared/article.service'
import { AuthService } from '../shared/auth.service'
import { NewArticle } from '../shared/interfaces/new-article.interface'

@Component({
  selector: 'app-article-edit',
  templateUrl: './article-edit.component.html',
  styleUrls: ['./article-edit.component.scss'],
})
export class ArticleEditComponent {
  articleTitleControl = new FormControl(null, [Validators.required])
  articleDescriptionControl = new FormControl(null, [Validators.required])
  articleTextControl = new FormControl(null, [Validators.required])

  form: FormGroup

  articleTags: string[] = []
  errorMessages: string[] = []
  isBusy = false
  isLoading = true

  mode: 'edit' | 'add'
  editingArticleId: number | null = null

  constructor(
    private articleService: ArticleService,
    private authService: AuthService,
    private router: Router,
    private route: ActivatedRoute,
  ) {
    if (!authService.isLoggedIn) router.navigateByUrl('/')

    this.form = new FormGroup({
      name: this.articleTitleControl,
      description: this.articleDescriptionControl,
      text: this.articleTextControl,
    })

    this.mode = this.route.snapshot.data['mode']

    if (this.mode === 'add') {
      this.isLoading = false
      return
    }

    const articleId = Number(this.route.snapshot.params['id'])
    if (isNaN(articleId)) return

    this._fetchEditingArticle(articleId)
  }

  private _fetchEditingArticle(articleId: number) {
    this.articleService.getArticleById(articleId).then(article => {
      this.articleTitleControl.setValue(article.title)
      this.articleDescriptionControl.setValue(article.shortDescription)
      this.articleTextControl.setValue(article.text)
      this.articleTags = article.tags
      this.editingArticleId = article.id

      this.isLoading = false
    })
  }

  get heading() {
    return this.mode === 'add' ? 'Создание статьи' : 'Редактирование статьи'
  }

  get isPublishButtonDisabled() {
    return this.form.invalid || this.isBusy
  }

  onTagsChange(tags: string[]) {
    this.articleTags = tags
  }

  getArticleFromForm() {
    return {
      title: this.articleTitleControl.value,
      shortDescription: this.articleDescriptionControl.value,
      text: this.articleTextControl.value,
      tags: this.articleTags,
      pictureLink: '/assets/no-articles.jpg',
    }
  }

  onSubmit() {
    const article: NewArticle = this.getArticleFromForm()

    this.isBusy = true
    const request =
      this.mode === 'add'
        ? this.articleService.createNewArticle(article)
        : this.articleService.updateArticle(this.editingArticleId as number, article)

    request
      .then(article => {
        this.router.navigateByUrl(`/article/${article.id}`)
      })
      .catch((error: HttpErrorResponse) => {
        if (Array.isArray(error.error)) this.errorMessages = error.error
        this.isBusy = false
      })
  }
}
