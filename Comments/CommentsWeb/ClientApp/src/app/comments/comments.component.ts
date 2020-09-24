import { Component } from '@angular/core';
import { IComment } from './comment';
import { HttpClient } from '@angular/common/http';
import { IResponse } from '../response';
import Swal from 'sweetalert2'

@Component({
  selector: 'app-comments',
  templateUrl: './comments.component.html'
})
export class CommentsComponent {
  comments: IComment[];
  text: string = '';  

  constructor(private http: HttpClient) {
    this.loadComments();
  }

  loadComments() {
    this.http.get<IResponse>('/api/CommentsAPI/comments').subscribe(result => {
      if (result.Success) {
        this.comments = result.Model as IComment[];
      } else {
        Swal.fire({
          title: 'Error',
          text: result.ErrorMsg,
          icon: 'warning'
        });
      }
    }, error => Swal.fire({
      title: 'Error',
      text: error.message,
      icon: 'warning'
    })); 
  }

  addComment() {    
    this.http.post<IResponse>('/api/CommentsAPI/addComments', {
      "Id": 0,
      "Text": this.text,
      "Date": new Date()
    }).subscribe(result => {
      if (result.Success) {
        this.comments = result.Model as IComment[];        
      } else {
        Swal.fire({
          title: 'Error',
          text: result.ErrorMsg,
          icon: 'warning'
        });
      }
    }, error => Swal.fire({
      title: 'Error',
      text: error.message,
      icon: 'warning'
    }));
    this.text = '';
  }
}
