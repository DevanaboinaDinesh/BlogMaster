import { Component, OnDestroy, OnInit } from '@angular/core';
import { AddBlogPost } from '../models/add-blogpost.model';
import { Blogpost } from '../models/blog-post.model';
import { BlogPostService } from '../services/blog-post.service';
import { Router } from '@angular/router';
import { CategoryService } from '../../category/services/category.service';
import { Category } from '../../category/Models/categoriy.model';
import { Observable, Subscription } from 'rxjs';
import { ImageService } from '../../shared/components/image-selector/image.service';

@Component({
  selector: 'app-add-blogpost',
  templateUrl: './add-blogpost.component.html',
  styleUrls: ['./add-blogpost.component.css']
})
export class AddBlogpostComponent implements OnInit, OnDestroy {

  model:AddBlogPost;
  category$?: Observable<Category[]>;
  isImageSelectorVisible: boolean = false;
  imageSelectorSubscription?: Subscription;
  constructor(private blogPostService: BlogPostService,
    private router:Router, 
    private categoryService:CategoryService,
    private imageService: ImageService){
    this.model={
      title:'',
      shortDescription:'',
      urlHandle:'',
      featuredImageUrl:'',
      content:'',
      publishedDate:new Date(),
      author:'',
      isVisible:true,
      categories:[],
    }
  }
  ngOnDestroy(): void {
    this.imageSelectorSubscription?.unsubscribe();
  }
  ngOnInit(): void { 
    this.category$=this.categoryService.getAllCategories();
    this.imageSelectorSubscription = this.imageService.onselectImage().subscribe({
      next: (response) =>{
        this.model.featuredImageUrl=response.url;
        this.CloseImageSelector();
      }
    });
  }
  onSubmit():void{
    console.log(this.model);
    console.log(this.model);
    this.blogPostService.createBlogPost(this.model).subscribe({
      next:(response)=>{
        this.router.navigateByUrl('/admin/blogposts');
      }
    });

  }
  openImageSelector():void{
    this.isImageSelectorVisible=true;
  }
  CloseImageSelector():void{
    this.isImageSelectorVisible=false;
  }
}


