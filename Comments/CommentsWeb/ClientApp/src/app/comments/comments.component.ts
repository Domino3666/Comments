import { Component } from '@angular/core';
import { IComment } from './comment';
import { HttpClient } from '@angular/common/http';
import { IResponse } from '../response';

@Component({
  selector: 'app-comments',
  templateUrl: './comments.component.html'
})
export class CommentsComponent {
  comments: IComment[];
  Text: '';

  constructor(private http: HttpClient) {
    this.http.get<IResponse>('/api/CommentsAPI/comments').subscribe(result => {
      this.comments = result.Model as IComment[];
    }, error => console.error(error));
  }

  addComment() {
    this.http.post<IResponse>('/api/CommentsAPI/addComments', {
      "Id": 0,
      "Text": this.Text,
      "Date": new Date()
    }).subscribe(result => {
      this.comments = result.Model as IComment[];
    }, error => console.error(error));
  }
}
