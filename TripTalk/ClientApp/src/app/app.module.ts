import { LOCALE_ID, NgModule } from '@angular/core'
import { BrowserModule } from '@angular/platform-browser'
import { registerLocaleData } from '@angular/common'
import localeRu from '@angular/common/locales/ru'

import { AppRoutingModule } from './app-routing.module'
import { AppComponent } from './app.component'
import { ArticleEditModule } from './article-edit/article-edit.module'
import { ArticleModule } from './article/article.module'
import { AuthModule } from './auth/auth.module'
import { HomeModule } from './home/home.module'
import { AuthLayoutModule } from './layouts/auth-layout/auth-layout.module'
import { MainLayoutModule } from './layouts/main-layout/main-layout.module'
import { ProfileModule } from './profile/profile.module'
import { SearchModule } from './search/search.module'
import { ArticleListModule } from './article-list/article-list.module'

registerLocaleData(localeRu)

@NgModule({
  declarations: [AppComponent],
  imports: [
    BrowserModule,
    AppRoutingModule,
    AuthLayoutModule,
    MainLayoutModule,
    AuthModule,
    HomeModule,
    ProfileModule,
    ArticleEditModule,
    SearchModule,
    ArticleModule,
    ArticleListModule,
  ],
  providers: [
    {
      provide: LOCALE_ID,
      useValue: 'ru',
    },
  ],
  bootstrap: [AppComponent],
})
export class AppModule {}
