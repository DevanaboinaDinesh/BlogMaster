import { Component, OnInit } from '@angular/core';
import { Blogpost } from '../models/blog-post.model';
import { BlogPostService } from '../services/blog-post.service';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-blogpost-list',
  templateUrl: './blogpost-list.component.html',
  styleUrls: ['./blogpost-list.component.css']
})
export class BlogpostListComponent implements OnInit {
  
  blogPosts$?:Observable<Blogpost[]>;

  constructor(private blogPostService: BlogPostService)
  {

  }
  ngOnInit(): void {
    this.blogPosts$=this.blogPostService.getAllBlogPosts();
  }

}
