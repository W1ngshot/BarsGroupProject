import { Component } from '@angular/core'
import { ActivatedRoute } from '@angular/router'
import { ArticleService } from '../shared/article.service'
import { Article } from '../shared/interfaces/article.interface'

@Component({
  selector: 'app-search-view',
  templateUrl: './search.component.html',
  styleUrls: ['./search.component.scss'],
})
export class SearchComponent {
  searchInitialValue: string = ''
  searchInitialTags: string[] = []
  currentPageNumber = 1

  articles: Article[] | null = null
  totalArticleCount = 0

  constructor(private route: ActivatedRoute, private articleService: ArticleService) {
    this._handleQueryParams()
  }

  private _handleQueryParams() {
    this.route.queryParams.subscribe(queryParams => {
      const query = queryParams['q'] ?? ''
      const tags = queryParams['tags'] as string | null
      const pageNumber = queryParams['pageNumber'] as string

      this.searchInitialValue = query
      this.searchInitialTags = tags?.split(',').filter(tag => tag.length > 0) ?? []

      if (pageNumber) {
        const parsedPageNumber = Number(pageNumber)
        if (!isNaN(parsedPageNumber)) this.currentPageNumber = parsedPageNumber
      }

      this.articleService.searchForArticles(query, tags, this.currentPageNumber).then(articles => {
        this.articles = articles.articles
        this.totalArticleCount = articles.totalCount
      })
    })
  }

  get totalPageCount() {
    return Math.ceil(this.totalArticleCount / 6)
  }

  get paginationLinkPrefix() {
    return `/search?q=${this.searchInitialValue}&tags=${this.searchInitialTags.join(',')}&pageNumber=`
  }
}
