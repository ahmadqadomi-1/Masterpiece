import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { DashboardComponent } from './Admn/dashboard/dashboard.component';
import { RouterModule } from '@angular/router';
import { CategryComponent } from './Admn/categry/categry.component';
import { ProductComponent } from './Admn/product/product.component';
import { ProjectComponent } from './Admn/project/project.component';
import { TeamComponent } from './Admn/team/team.component';
import { TilerComponent } from './Admn/tiler/tiler.component';
import { ContactUsComponent } from './Admn/contact-us/contact-us.component';
import { AddCategoryComponent } from './Admn/add-category/add-category.component';
import { AddProductComponent } from './Admn/add-product/add-product.component';
import { AddProjectComponent } from './Admn/add-project/add-project.component';
import { AddMemberComponent } from './Admn/add-member/add-member.component';
import { AddTilerComponent } from './Admn/add-tiler/add-tiler.component';
import { UpdateCategoryComponent } from './Admn/update-category/update-category.component';
import { UpdateProductComponent } from './Admn/update-product/update-product.component';
import { UpdateProjectComponent } from './Admn/update-project/update-project.component';
import { UpdateMemberComponent } from './Admn/update-member/update-member.component';
import { UpdateTilerComponent } from './Admn/update-tiler/update-tiler.component';
import { UserComponent } from './Admn/user/user.component';
import { UserDetailsComponent } from './Admn/user-details/user-details.component';
import { OrderComponent } from './Admn/order/order.component';
import { UpdateOrderComponent } from './Admn/update-order/update-order.component';
import { LogInComponent } from './Admn/log-in/log-in.component';
import { ProductToOneCategoryComponent } from './Admn/product-to-one-category/product-to-one-category.component';
import { NewsComponent } from './Admn/news/news.component';
import { AddNewsComponent } from './Admn/add-news/add-news.component';
import { EditNewsComponent } from './Admn/edit-news/edit-news.component';

@NgModule({
  declarations: [
    AppComponent,
    DashboardComponent,
    CategryComponent,
    ProductComponent,
    ProjectComponent,
    TeamComponent,
    TilerComponent,
    ContactUsComponent,
    AddCategoryComponent,
    AddProductComponent,
    AddProjectComponent,
    AddMemberComponent,
    AddTilerComponent,
    UpdateCategoryComponent,
    UpdateProductComponent,
    UpdateProjectComponent,
    UpdateMemberComponent,
    UpdateTilerComponent,
    UserComponent,
    UserDetailsComponent,
    OrderComponent,
    UpdateOrderComponent,
    LogInComponent,
    ProductToOneCategoryComponent,
    NewsComponent,
    AddNewsComponent,
    EditNewsComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    FormsModule, 
    ReactiveFormsModule, 
    AppRoutingModule,
    RouterModule.forRoot([
      { path: "LogIn", component: LogInComponent },
      { path: '', redirectTo: 'dashboard/catgory', pathMatch: 'full' },
      {
        path: "dashboard", component: DashboardComponent, children: [
          { path: "catgory", component: CategryComponent },
          { path: "ProductToOneCategory/:id", component: ProductToOneCategoryComponent },
          { path: "User", component: UserComponent },
          { path: "UserDetails/:id", component: UserDetailsComponent },
          { path: "Product", component: ProductComponent },
          { path: "Order", component: OrderComponent },
          { path: "UpdateOrder/:id", component: UpdateOrderComponent },
          { path: "Project", component: ProjectComponent },
          { path: "Team", component: TeamComponent },
          { path: "Tiler", component: TilerComponent },
          { path: "News", component: NewsComponent },
          { path: "ContactUs", component: ContactUsComponent },
          { path: "AddCategory", component: AddCategoryComponent },
          { path: 'AddProduct/:categoryId', component: AddProductComponent },
          { path: "AddProject", component: AddProjectComponent },
          { path: "AddNews", component: AddNewsComponent },
          { path: "AddMember", component: AddMemberComponent },
          { path: "AddTiler", component: AddTilerComponent },
          { path: "UpdateCategory/:id", component: UpdateCategoryComponent },
          { path: "EditNews/:id", component: EditNewsComponent },
          { path: "UpdateProduct/:id", component: UpdateProductComponent },
          { path: "UpdateProject/:id", component: UpdateProjectComponent },
          { path: "UpdateMember/:id", component: UpdateMemberComponent },
          { path: "UpdateTiler/:id", component: UpdateTilerComponent },
        ]
      }
    ])
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
