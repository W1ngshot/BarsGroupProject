import { NgModule } from '@angular/core'
import { RouterModule, Routes } from '@angular/router'
import { ArticleEditComponent } from './article-edit/article-edit.component'
import { ArticleListComponent } from './article-list/article-list.component'
import { ArticleComponent } from './article/article.component'
import { AuthComponent } from './auth/auth.component'
import { HomeComponent } from './home/home.component'
import { AuthLayoutComponent } from './layouts/auth-layout/auth-layout.component'
import { MainLayoutComponent } from './layouts/main-layout/main-layout.component'
import { ProfileComponent } from './profile/profile.component'
import { SearchComponent } from './search/search.component'

const routes: Routes = [
  {
    path: '',
    component: MainLayoutComponent,
    children: [
      {
        path: '',
        component: HomeComponent,
      },
    ],
  },
  {
    path: 'login',
    component: AuthLayoutComponent,
    children: [
      {
        path: '',
        component: AuthComponent,
      },
    ],
  },
  {
    path: 'register',
    component: AuthLayoutComponent,
    children: [
      {
        path: '',
        component: AuthComponent,
      },
    ],
  },
  {
    path: 'profile',
    component: MainLayoutComponent,
    children: [
      {
        path: '',
        component: ProfileComponent,
      },
      {
        path: 'articles',
        component: ArticleListComponent,
        data: {
          category: 'my',
        },
      },
      {
        path: ':id',
        component: ProfileComponent,
      },
    ],
  },
  {
    path: 'article',
    component: MainLayoutComponent,
    children: [
      {
        path: '',
        redirectTo: '/article/add',
        pathMatch: 'full',
      },
      {
        path: 'add',
        component: ArticleEditComponent,
        data: {
          mode: 'add',
        },
      },
      {
        path: 'edit/:id',
        component: ArticleEditComponent,
        data: {
          mode: 'edit',
        },
      },
      {
        path: ':id',
        component: ArticleComponent,
      },
    ],
  },
  {
    path: 'search',
    component: MainLayoutComponent,
    children: [
      {
        path: '',
        component: SearchComponent,
      },
    ],
  },
  {
    path: 'popular',
    component: MainLayoutComponent,
    children: [
      {
        path: '',
        component: ArticleListComponent,
        data: {
          category: 'popular',
        },
      },
    ],
  },
  {
    path: 'best',
    component: MainLayoutComponent,
    children: [
      {
        path: '',
        component: ArticleListComponent,
        data: {
          category: 'best',
        },
      },
    ],
  },
  {
    path: 'latest',
    component: MainLayoutComponent,
    children: [
      {
        path: '',
        component: ArticleListComponent,
        data: {
          category: 'latest',
        },
      },
    ],
  },
]

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
