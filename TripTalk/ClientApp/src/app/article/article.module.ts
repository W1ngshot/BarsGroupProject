import { NgModule } from '@angular/core'
import { CommonModule } from '@angular/common'
import { RouterModule } from '@angular/router'
import { ReactiveFormsModule } from '@angular/forms'

import { SharedModule } from '../shared/shared.module'
import { ArticleComponent } from './article.component'
import { EditableArticleRatingComponent } from './editable-article-rating/editable-article-rating.component'

@NgModule({
  declarations: [ArticleComponent, EditableArticleRatingComponent],
  imports: [CommonModule, RouterModule, SharedModule, ReactiveFormsModule],
})
export class ArticleModule {}
