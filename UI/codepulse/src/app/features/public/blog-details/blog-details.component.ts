import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { BlogPostService } from '../../blog-post/services/blog-post.service';
import { Observable } from 'rxjs';
import { Blogpost } from '../../blog-post/models/blog-post.model';

@Component({
  selector: 'app-blog-details',
  templateUrl: './blog-details.component.html',
  styleUrls: ['./blog-details.component.css']
})
export class BlogDetailsComponent  implements OnInit{
  
  url:string | null =null;
  blogPost$?: Observable<Blogpost>;
  constructor(private router: ActivatedRoute,
      private blogPostService: BlogPostService
  ){

  }
  ngOnInit(): void {
    this.router.paramMap.subscribe({
      next:(param)=>{
        this.url = param.get('url');
      }
    });
    if(this.url)
    {
      this.blogPost$ = this.blogPostService.getBlogPostByUrl(this.url);
    }
  }



  

}
