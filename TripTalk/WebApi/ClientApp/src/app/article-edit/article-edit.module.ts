import { NgModule } from '@angular/core'
import { CommonModule } from '@angular/common'

import { ArticleEditComponent } from './article-edit.component'
import { SharedModule } from '../shared/shared.module'
import { ReactiveFormsModule } from '@angular/forms'

@NgModule({
  declarations: [ArticleEditComponent],
  imports: [CommonModule, SharedModule, ReactiveFormsModule],
})
export class ArticleEditModule {}
