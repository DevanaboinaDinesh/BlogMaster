import { Component, OnInit } from '@angular/core';
import { BlogPostService } from '../../blog-post/services/blog-post.service';
import { Blogpost } from '../../blog-post/models/blog-post.model';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  blogs$?:Observable<Blogpost[]>;
  constructor(private blogPostService: BlogPostService){

  }
  ngOnInit(): void {
    this.blogs$ = this.blogPostService.getAllBlogPosts();
  }

  

}
