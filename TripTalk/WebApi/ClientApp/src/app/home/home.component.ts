import { Component } from '@angular/core'
import { ArticleService } from '../shared/article.service'
import { Article } from '../shared/interfaces/article.interface'

interface ArticleSection {
  name: string
  moreLink: string
  articleRows: Article[][]
}

@Component({
  selector: 'app-home-view',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss'],
})
export class HomeComponent {
  articleSections: ArticleSection[] | null = null

  constructor(private articleService: ArticleService) {
    articleService.getMainPageArticles().then(articles => {
      this.articleSections = [
        { name: 'Популярное', moreLink: '/popular', articleRows: this.createArticleRows(articles.popularArticles) },
        { name: 'Лучшие', moreLink: '/best', articleRows: this.createArticleRows(articles.bestArticles) },
        { name: 'Последние', moreLink: '/latest', articleRows: this.createArticleRows(articles.latestArticles) },
      ]
    })
  }

  createArticleRows(articles: Article[]) {
    const result: Article[][] = []

    for (let i = 0; i < articles.length; i += 2) {
      const row = [articles[i]]
      if (i + 1 < articles.length) row.push(articles[i + 1])
      result.push(row)
    }

    return result
  }

  get isAllSectionsEmpty() {
    if (this.articleSections === null) return false
    return !this.articleSections.some(section => section.articleRows.length > 0)
  }
}
