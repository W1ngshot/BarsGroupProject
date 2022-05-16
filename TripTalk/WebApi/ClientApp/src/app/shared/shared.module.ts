import { NgModule } from '@angular/core'
import { CommonModule } from '@angular/common'

import { SearchComponent } from './search/search.component'
import { LocationTagComponent } from './location-tag/location-tag.component'
import { LocationTagAddComponent } from './location-tag-add/location-tag-add.component'
import { ArticleCardComponent } from './article-card/article-card.component'
import { HttpClientModule } from '@angular/common/http'
import { ReactiveFormsModule } from '@angular/forms'
import { LocationTagListComponent } from './location-tag-list/location-tag-list.component'
import { UserAvatarComponent } from './user-avatar/user-avatar.component'
import { RouterModule } from '@angular/router';
import { PaginationComponent } from './pagination/pagination.component'

@NgModule({
  declarations: [
    SearchComponent,
    LocationTagComponent,
    LocationTagAddComponent,
    ArticleCardComponent,
    LocationTagListComponent,
    UserAvatarComponent,
    PaginationComponent,
  ],
  imports: [CommonModule, HttpClientModule, ReactiveFormsModule, RouterModule],
  exports: [
    SearchComponent,
    LocationTagComponent,
    LocationTagAddComponent,
    LocationTagListComponent,
    ArticleCardComponent,
    UserAvatarComponent,
    PaginationComponent
  ],
})
export class SharedModule {}
