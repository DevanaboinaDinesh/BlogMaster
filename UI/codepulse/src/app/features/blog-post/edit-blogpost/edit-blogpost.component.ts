import { Component, OnDestroy, OnInit } from '@angular/core';
import { BlogPostService } from '../services/blog-post.service';
import { ActivatedRoute, Router } from '@angular/router';
import { Observable, Subscription } from 'rxjs';
import { Blogpost } from '../models/blog-post.model';
import { CategoryService } from '../../category/services/category.service';
import { Category } from '../../category/Models/categoriy.model';
import { UpdateBlogPost } from '../models/update-blog-post.model';
import { ImageService } from '../../shared/components/image-selector/image.service';

@Component({
  selector: 'app-edit-blogpost',
  templateUrl: './edit-blogpost.component.html',
  styleUrls: ['./edit-blogpost.component.css']
})
export class EditBlogpostComponent implements OnInit, OnDestroy {
  id:string | null =null;
  routeSubscription?: Subscription;
  blogPostSubscription? : Subscription;
  getBlogPostSubscription? : Subscription;
  deleteSubscription? : Subscription;
  imageSelectSubscription?: Subscription;
  model?:Blogpost
  categories$?:Observable<Category[]>;
  selectedCategories?: string[];
  isImageSelectorVisible:boolean=false;

  constructor(private blogPostService: BlogPostService, private router:ActivatedRoute, private categoryService: CategoryService,
     private route: Router,
     private imageService: ImageService
     ){

  }
  onDelete():void{
    if(this.id){
      this.deleteSubscription = this.blogPostService.deleteBlogPostById(this.id).subscribe({
        next:(response)=>{
          this.route.navigateByUrl('/admin/blogposts');
        }
      });

    }
  }

  openImageSelector():void{
    this.isImageSelectorVisible=true;
  }
  CloseImageSelector():void{
    this.isImageSelectorVisible=false;
  }
  ngOnDestroy(): void {
    this.routeSubscription?.unsubscribe();
    this.blogPostSubscription?.unsubscribe();
    this.getBlogPostSubscription?.unsubscribe();
    this.deleteSubscription?.unsubscribe();
    this.imageSelectSubscription?.unsubscribe();
  }
  ngOnInit(): void {    
    this.categories$=this.categoryService.getAllCategories();

    this.routeSubscription=this.router.paramMap.subscribe({
      next:(params)=>{        
        this.id = params.get('id');
        if(this.id)
        {          
          this.getBlogPostSubscription = this.blogPostService.getBlogPostById(this.id).subscribe({
            next: (response)=>{
              this.model=response;
              this.selectedCategories=response.categories.map(x=>x.id);
            }
          });
        }
        this.imageSelectSubscription= this.imageService.onselectImage().subscribe({
          next:(response)=>{
              if(this.model)
              {
                this.model.featuredImageUrl=response.url;
                this.isImageSelectorVisible=false;
              }
          }
        });

      }
    })
  }
  onSubmit():void{
    if(this.model && this.id)
    {
      var updateBlogPost: UpdateBlogPost={
          author: this.model.author,
          content: this.model.content,
          shortDescription: this.model.shortDescription,
          featuredImageUrl: this.model.featuredImageUrl,
          isVisible: this.model.isVisible,
          publishedDate: this.model.publishedDate,
          title: this.model.title,
          urlHandle: this.model.urlHandle,
          categories: this.selectedCategories ?? []
      }
      this.blogPostSubscription = this.blogPostService.updateBlogPostById(this.id,updateBlogPost)
                  .subscribe({
                    next:(response)=>{
                      this.route.navigateByUrl('/admin/blogposts');
                    }
                  })
    }

    

  }

}
