import { NgModule } from '@angular/core'
import { CommonModule } from '@angular/common'
import { ArticleListComponent } from './article-list.component'
import { SharedModule } from '../shared/shared.module'

@NgModule({
  declarations: [ArticleListComponent],
  imports: [CommonModule, SharedModule],
})
export class ArticleListModule {}
