export interface Article {
  id: number
  title: string
  shortDescription: string
  text: string
  uploadDate: string
  previewPictureLink: string
  tags: string[]

  rating: number
  views: number

  userId: number
  userNickname: string
  userAvatarLink: string
}
