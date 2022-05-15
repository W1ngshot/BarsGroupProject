import { Component, Input } from '@angular/core'
import { Router } from '@angular/router'

interface PaginationLink {
  value: number
  name: string | number
}

@Component({
  selector: 'app-pagination',
  templateUrl: './pagination.component.html',
  styleUrls: ['./pagination.component.scss'],
})
export class PaginationComponent {
  @Input() totalCount = 1
  @Input() current = 1
  @Input() linkPrefix = '/?pageNumber='

  constructor(private router: Router) {}

  get paginationLinks() {
    const result: PaginationLink[] = []

    if (this.totalCount <= 9)
      return Array(this.totalCount)
        .fill(null)
        .map((_, index) => ({ value: index + 1, name: index + 1 }))

    let centralElement = this.current
    if (this.current < 5) centralElement = 5
    if (this.current > this.totalCount - 4) centralElement = this.totalCount - 4

    //Adding first 4 links
    if (centralElement === 5) {
      for (let i = 1; i < centralElement; i++) {
        result.push({
          value: i,
          name: i.toString(),
        })
      }
    } else {
      result.push({ value: 1, name: 1 })
      result.push({ value: this.current - 3, name: '...' })
      result.push({ value: this.current - 2, name: this.current - 2 })
      result.push({ value: this.current - 1, name: this.current - 1 })
    }

    //Adding 7 links if pagination has ... at the end, else 9 links
    for (
      let i = centralElement;
      i <= this.totalCount && (result.length < 7 || centralElement + 4 === this.totalCount);
      i++
    ) {
      result.push({ value: i, name: i })
      if (i === this.totalCount) return result
    }

    result.push({ value: result[result.length - 1].value + 1, name: '...' })
    result.push({ value: this.totalCount, name: this.totalCount })
    return result
  }

  getLinkFor(value: string | number) {
    const link = this.linkPrefix + value
    const result = link.split('?')[0]
    return result
  }

  getQueryParamsFor(value: string | number) {
    const link = this.linkPrefix + value
    const parsedLink = this.router.parseUrl(link)
    return parsedLink.queryParams
  }
}
