import { Injectable } from '@angular/core';
import { AddBlogPost } from '../models/add-blogpost.model';
import { Observable } from 'rxjs';
import { Blogpost } from '../models/blog-post.model';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment.development';
import { UpdateBlogPost } from '../models/update-blog-post.model';

@Injectable({
  providedIn: 'root'
})
export class BlogPostService {

  constructor(private http:HttpClient) { }

  createBlogPost(data:AddBlogPost):Observable<Blogpost>
  {
    return this.http.post<Blogpost>(`${environment.apiBaseUrl}/api/BlogPosts?addAuth=true`,data);
  }
  getAllBlogPosts():Observable<Blogpost[]>{
    return this.http.get<Blogpost[]>(`${environment.apiBaseUrl}/api/BlogPosts`);
  }
  getBlogPostById(id:string):Observable<Blogpost>{
    return this.http.get<Blogpost>(`${environment.apiBaseUrl}/api/BlogPosts/${id}`);
  }
  getBlogPostByUrl(url:string):Observable<Blogpost>{
    return this.http.get<Blogpost>(`${environment.apiBaseUrl}/api/BlogPosts/${url}`);
  }
  updateBlogPostById(id:string, blogPost:UpdateBlogPost):Observable<Blogpost>{
    return this.http.put<Blogpost>(`${environment.apiBaseUrl}/api/BlogPosts/${id}?addAuth=true`, blogPost);
  }
  deleteBlogPostById(id:string):Observable<Blogpost>{
    return this.http.delete<Blogpost>(`${environment.apiBaseUrl}/api/BlogPosts/${id}?addAuth=true`);
  }
  
}

