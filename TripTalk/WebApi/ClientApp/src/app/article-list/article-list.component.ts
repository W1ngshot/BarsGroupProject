import { Component } from '@angular/core'
import { Article } from '../shared/interfaces/article.interface'
import { ArticleService } from '../shared/article.service'
import { ActivatedRoute } from '@angular/router'

interface ArticleResponse {
  articles: Article[]
  totalCount: number
}

@Component({
  selector: 'app-article-list',
  templateUrl: './article-list.component.html',
  styleUrls: ['./article-list.component.scss'],
})
export class ArticleListComponent {
  articles: Article[] | null = null
  totalArticleCount = 0

  currentPageNumber = 1
  currentCategory = 'popular'

  constructor(private articleService: ArticleService, route: ActivatedRoute) {
    this.currentCategory = route.snapshot.data['category'] as string

    route.data.subscribe(data => {
      this.currentCategory = data['category']
      this._updateArticles(this.currentCategory, this.currentPageNumber)
    })
    route.queryParams.subscribe(queryParams => {
      const parsedPageNumber = Number(queryParams['pageNumber'])
      if (!isNaN(parsedPageNumber)) {
        this.currentPageNumber = parsedPageNumber
        this._updateArticles(this.currentCategory, this.currentPageNumber)
      }
    })
  }

  private _updateArticles(category: string, pageNumber: number) {
    const request =
      category === 'my'
        ? this.articleService.getMyArticles(pageNumber)
        : this.articleService.getArticlesByCategory(category, pageNumber)

    request.then(({ articles, totalCount }) => {
      this.articles = articles
      this.totalArticleCount = totalCount
    })
  }

  get title() {
    switch (this.currentCategory) {
      case 'popular':
        return 'Популярное'
      case 'best':
        return 'Лучшее'
      case 'latest':
        return 'Последнее'
      case 'my':
      default:
        return 'Мои статьи'
    }
  }

  get totalPageCount() {
    return Math.ceil(this.totalArticleCount / 6)
  }

  get paginationLinkPrefix() {
    if (this.currentCategory === 'my') return `/profile/articles?pageNumber=`
    return `/${this.currentCategory}?pageNumber=`
  }
}
