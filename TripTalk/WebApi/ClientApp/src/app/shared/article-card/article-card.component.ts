import { Component, Input } from '@angular/core'

@Component({
  selector: 'app-article-card',
  templateUrl: './article-card.component.html',
  styleUrls: ['./article-card.component.scss'],
})
export class ArticleCardComponent {
  @Input() isViewsShown: boolean = false
  @Input() views: number = 0
  @Input() rating: number = 0
  @Input() userNickname: string = 'Author'
  @Input() userId: number = 0
  @Input() previewUrl: string = 'assets/missing-image.png'
  @Input() title: string = 'Article'
  @Input() shortDescription: string = 'Lorem ipsum dolor sit amet'
  @Input() tags: string[] = ['Sample', 'Location']
  @Input() isEditable: boolean = false
  @Input() id: number = 0

  get link() {
    return `/article/${this.id}/`
  }

  get userLink() {
    return `/profile/${this.userId}`
  }

  get editLink() {
    return `/article/edit/${this.id}`
  }
}
