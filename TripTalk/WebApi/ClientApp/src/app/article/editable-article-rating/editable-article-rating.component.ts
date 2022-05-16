import { Component, EventEmitter, Input, Output } from '@angular/core'

@Component({
  selector: 'app-editable-article-rating',
  templateUrl: './editable-article-rating.component.html',
  styleUrls: ['./editable-article-rating.component.scss'],
})
export class EditableArticleRatingComponent {
  @Input() rating: number = 0
  @Output() upvote = new EventEmitter()
  @Output() downvote = new EventEmitter()

  onUpvote() {
    this.upvote.emit()
  }

  onDownvote() {
    this.downvote.emit()
  }
}
