import { Article } from './article.interface'
import { User } from './user.interface'

export interface Account {
  user: User
  articles: Article[]
  totalCount: number
}
